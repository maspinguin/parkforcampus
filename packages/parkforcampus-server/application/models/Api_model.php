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

    public function list_pengguna($start=null, $limit=null, $order='asc') {
        $query = "
            SELECT `p`.`id`,`p`.`nomor_induk`, `m`.`nama`, `m`.`alamat`, `p`.`no_kartu`
            FROM `tbl_pengguna` as `p`, `tbl_mahasiswa` as `m`
            WHERE `m`.`nim` = `p`.`nomor_induk` AND `p`.`status_id` = 1
            UNION
            SELECT `p`.`id`,`p`.`nomor_induk`, `m`.`nama`, `m`.`alamat`, `p`.`no_kartu`
            FROM `tbl_pengguna` as `p`, `tbl_pegawai` as `m`
            WHERE `m`.`nip` = `p`.`nomor_induk`
            AND `p`.`status_id` = 1";
        if(isset($order)) {
            $query.= " ORDER BY id ".$order;
        }
        if(isset($start) && isset($limit)) {
            $query.= " LIMIT ".$limit." OFFSET ".$start;
        }
        return $this->db->query($query)->result();
    }


//    public function pemeriksaan_create_data($data)
//    {
//        $this->db->insert('pemeriksaan',$data);
//        if($this->db->affected_rows() > 0){
//            $config['upload_path']          = 'asset/upload_pemeriksaan/';
//			$config['allowed_types']        = 'jpg';
//			$config['max_size']             = 8000;
//            $config['file_name']            = $this->db->insert_id().'.jpg';
//
//			$this->load->library('upload', $config);
//			if(!$this->upload->do_upload('foto')){
//				return array('status' =>403 , 'message'=> 'Data pemeriksaan telah dibuat. Gagal mengupload foto. '.$this->upload->display_errors('',''));
//			}
//
//            $data2['foto'] = $this->db->insert_id().".jpg";
//            $this->db->where('id_periksa', $this->db->insert_id());
//            $this->db->update('pemeriksaan', $data2);
//            if($this->db->affected_rows() > 0) {
//                return array('status' =>203 , 'message'=> 'Data Pemeriksaan Telah Dibuat');
//            }
//            return array('status' =>403 , 'message'=> 'Data pemeriksaan Telah Dibuat, nama foto belum terupdate');
//
//        }
//        else{
//            return array('status' =>403 , 'message'=> 'Gagal membuat data pemeriksaan');
//        }
//        // return array('status' => 201,'message' => 'Data pemeriksaan has been created.');
//    }
//
//    public function penyegelan_create_data($data)
//    {
//        $this->db->insert('penyegelan',$data);
//        if($this->db->affected_rows() > 0)
//        {
//            $dataedit = array('statusid' => '0');
//
//            $this->db->where('id_pemakaian', $data['id_pemakaian']);
//            $this->db->update('pemakaian', $dataedit);
//
//            $this->db->trans_complete();
//
//            if($this->db->affected_rows() > 0)
//            {
//                // return TRUE;
//                return array('status' => 203,'message' => 'Data penyegelan has been created.');
//            }
//            else
//            {
//              if ($this->db->trans_status() === FALSE) {
//                  return array('status' => 403,'message' => 'Data penyegelan failed to add.');
//
//                // return FALSE;
//              }else{
//                  return array('status' => 203,'message' => 'Data penyegelan has been created.');
//                // return TRUE;
//              }
//          }
//      }else {
//          return array('status' => 403,'message' => 'Data penyegelan failed to add.');
//      }
//
//    }

    // public function pelanggan_update_data($id,$data)
    // {
    //     $this->db->where('id_pel',$id)->update('pelanggan',$data);
    //     return array('status' => 200,'message' => 'Data pelanggan has been updated.');
    // }
    //
    // public function pelanggan_delete_data($id)
    // {
    //     $this->db->where('id_pel',$id)->delete('pelanggan');
    //     return array('status' => 200,'message' => 'Data pelanggan has been deleted.');
    // }






}
