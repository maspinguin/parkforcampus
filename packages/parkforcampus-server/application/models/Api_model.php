<?php
defined('BASEPATH') OR exit('No direct script access allowed');

class Api_model extends CI_Model {

    var $client_service = "frontend-client";
    var $auth_key       = "parkir_sttar";

    public function check_auth_client(){
        $client_service = $this->input->get_request_header('Client-Service', TRUE);
        $auth_key  = $this->input->get_request_header('Auth-Key', TRUE);
        if($client_service == $this->client_service && $auth_key == $this->auth_key){
            return true;
        } else {
            return json_output(401,array('status' => 401,'message' => 'Unauthorized.'));
        }
    }

    public function login($username='',$password='')
    {
        if($username == ''){
          return array('status' => 203 ,'message' => 'Field username is empty' );
        }

        if($password == ''){
          return array('status' => 203 ,'message' => 'Field password is empty' );
        }

        $q  = $this->db->select('password, nomor_induk')->from('tbl_pengguna')
            ->where('nomor_induk',$username)
            ->where('status_id', '1')
            ->get()->row();
        if($q == ""){
          return array('status' => 403,'message' => 'Username inactive/ not found.');
        } else {
            $hashed_password = $q->password;
            $id              = $q->nomor_induk;

            if (hash_equals($hashed_password, md5($password))) {
               $last_login = date('Y-m-d H:i:s');
//               $token = crypt(substr(md5(rand()), 0, 7),'');
               $expired_at = date("Y-m-d H:i:s", strtotime('+6 hours'));
               $this->db->trans_start();
               $this->db->where('nomor_induk',$id)->update('tbl_pengguna',array('last_login' => $last_login));

               $tokenData = new StdClass();
               $tokenData->nomor_induk = $username;
               $tokenData->last_login = $last_login;
               $tokenData->expired_at = $expired_at;
               $newtoken = AUTHORIZATION::generateToken($tokenData);


                $this->db->insert('tbl_users_authentication',array('nomor_induk' => $id,'token' => $newtoken,'expired_at' => $expired_at, 'created_at' => date('Y-m-d h:m:s'), 'status_id' => 1));
               if ($this->db->trans_status() === FALSE){
                  $this->db->trans_rollback();
                  return array('status' => 500,'message' => 'Internal server error.');
               } else {
                  $this->db->trans_commit();
                  return array('status' => 200,'message' => 'Successfully login.','nomor_induk' => $id, 'token' => $newtoken);
               }
            } else {
               return array('status' => 403,'message' => 'Wrong password.');
            }
        }
    }

    public function checkToken($token='')
    {
      // code...
      if($token == ''){
        return array('status' => 203 ,'message' => 'Field token is empty' );
      }

      $q  = $this->db->select('nomor_induk,expired_at')->from('tbl_users_authentication')->where('token',$token)->where('status_id', '1')->get()->row();
      if($q == ""){
        return array('status' => 204,'message' => 'Token not found.');
      } else {
        if($q->expired_at < date('Y-m-d H:i:s')){
            return array('status' => 200, 'valid'=> false,'message' => 'Your session has been expired.');
        }else{
          return array('status' => 200, 'valid'=> true,'message' => 'Token valid.');
        }
      }
    }

    public function logout()
    {
        $token     = $this->input->get_request_header('Authorization', TRUE);
        if(empty($token)){
          return array('status' => 401 ,'message' => 'Unauthorized' );
        }

        $last_logout = date('Y-m-d H:i:s');
        $data = AUTHORIZATION::decode($token);
        if(isset($data->nomor_induk)) {
            $this->db->where('nomor_induk', $data->nomor_induk)->update('tbl_pengguna', array('last_logout'=> $last_logout));
        }

        $this->db->where('token',$token)->update('tbl_users_authentication', array('status_id'=> 2));
        return array('status' => 200,'message' => 'Successfully logout.');
    }

    public function auth()
    {
        $token     = $this->input->get_request_header('Authorization', TRUE);
        $q  = $this->db->select('expired_at')->from('tbl_users_authentication')->where('token',$token)->where('status_id','1')->get()->row();
        if($q == ""){
            return json_output(401,array('status' => 401,'message' => 'Unauthorized.'));
        } else {
            $data = AUTHORIZATION::decode($token);
            if($data->expired_at < date('Y-m-d H:i:s')){
                $updated_at = date('Y-m-d H:i:s');
                $this->db->where('token',$token)->update('tbl_users_authentication',array('status_id'=>'2','updated_at' => $updated_at));
                return json_output(401,array('status' => 401,'message' => 'Your session has been expired.'));
            } else {
                $updated_at = date('Y-m-d H:i:s');
                $expired_at = date("Y-m-d H:i:s", strtotime('+12 hours'));
                $this->db->where('token',$token)->update('tbl_users_authentication',array('updated_at' => $updated_at));
                return array('status' => 200,'message' => 'Authorized.');
            }
        }
    }

    public function list_pengguna($type = null,$start=null, $limit=null, $order='asc') {
        $query = "
            SELECT `p`.`id`,`p`.`nomor_induk`, `m`.`nama`, `m`.`alamat`, `p`.`no_kartu`, `p`.`id_type`
            FROM `tbl_pengguna` as `p`, `tbl_mahasiswa` as `m`
            WHERE `m`.`nim` = `p`.`nomor_induk` AND `p`.`status_id` = 1";
        if(isset($type)) {
            $typeres = $this->db->select('keterangan, id')->from('tbl_type_pengguna')
                ->where('keterangan',$type)
                ->where('status_id', '1')
                ->get()->row();

            if($typeres != "") {
                $query.= " AND p.id_type = ".$typeres->id;
            }

        }

        $query.="
            UNION
            SELECT `p`.`id`,`p`.`nomor_induk`, `m`.`nama`, `m`.`alamat`, `p`.`no_kartu`, `p`.`id_type`
            FROM `tbl_pengguna` as `p`, `tbl_pegawai` as `m`
            WHERE `m`.`nip` = `p`.`nomor_induk`
            AND `p`.`status_id` = 1";
        if(isset($type)) {
            $typeres = $this->db->select('keterangan, id')->from('tbl_type_pengguna')
                ->where('keterangan',$type)
                ->where('status_id', '1')
                ->get()->row();

            if($typeres != "") {
                $query.= " AND p.id_type = ".$typeres->id;
            }

        }
        if(isset($order)) {
            $query.= " ORDER BY id ".$order;
        }
        if(isset($start) && isset($limit)) {
            $query.= " LIMIT ".$limit." OFFSET ".$start;
        }
        return $this->db->query($query)->result();
    }

    public function list_pegawai($start=null, $limit=null, $order='asc') {
        $query = "
            SELECT *
            FROM `tbl_pegawai`
            WHERE `status_id` = 1";

        if(isset($order)) {
            $query.= " ORDER BY id ".$order;
        }
        if(isset($start) && isset($limit)) {
            $query.= " LIMIT ".$limit." OFFSET ".$start;
        }
        return $this->db->query($query)->result();
    }

    public function list_mahasiswa($start=null, $limit=null, $order='asc') {
        $query = "
            SELECT *
            FROM `tbl_mahasiswa`
            WHERE `status_id` = 1";

        if(isset($order)) {
            $query.= " ORDER BY id ".$order;
        }
        if(isset($start) && isset($limit)) {
            $query.= " LIMIT ".$limit." OFFSET ".$start;
        }
        return $this->db->query($query)->result();
    }

    public function list_type_pengguna() {

        return $this->db->select('*')->from('tbl_type_pengguna')->where('status_id', '1')->get()->result();
    }

    public function insert_mahasiswa($data) {
        $q  = $this->db->select('*')->from('tbl_mahasiswa')
            ->where('nim',$data['nim'])
            ->where('status_id', '1')
            ->get()->row();
        if($q == ""){
            $this->db->insert('tbl_mahasiswa', $data);
            if($this->db->affected_rows() > 0) {
                return array('status' =>200 , 'message'=> 'Data mahasiswa telah dibuat');
            }

        } else {
            return array('status' => 400,'message' => 'Data dengan nim sudah ada!');
        }


    }

    public function update_mahasiswa($data) {
        $q  = $this->db->select('*')->from('tbl_mahasiswa')
            ->where('nim',$data['nim'])
            ->where('status_id', '1')
            ->get()->row();
        if($q == ""){
            return array('status' => 400,'message' => 'Data tidak ditemukan');
        } else {
            $this->db->where('nim', $data['nim'])->where('status_id', '1')->update('tbl_mahasiswa', $data);
            if($this->db->affected_rows() > 0) {
                return array('status' =>200 , 'message'=> 'Data mahasiswa telah diubah');
            } else {
                return array('status' => 400, 'message' => 'No data has updated.');
            }
        }
    }

    public function delete_mahasiswa($data) {
        $q  = $this->db->select('*')->from('tbl_mahasiswa')
            ->where('nim',$data['nim'])
            ->where('status_id', '1')
            ->get()->row();
        if($q == ""){
            return array('status' => 400,'message' => 'Data tidak ditemukan');
        } else {
            $this->db->where('nim', $data['nim'])->where('status_id', '1')->update('tbl_mahasiswa', $data);
            if($this->db->affected_rows() > 0) {
                return array('status' =>200 , 'message'=> 'Data mahasiswa telah terhapus');
            } else {
                return array('status' => 400, 'message' => 'No data has deleted.');
            }
        }
    }
}
