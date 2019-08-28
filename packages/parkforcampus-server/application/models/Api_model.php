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

        $q  = $this->db->select('password, nomor_induk, id_type')->from('tbl_pengguna')
            ->where('nomor_induk',$username)
            ->where('status_id', '1')
            ->get()->row();
        $q2 = null;
        if($q != "") {
            if($q->id_type == 1) {
                $q2 = $this->db->select('nama')->from('tbl_mahasiswa')->where('nim',$username)->where('status_id', 1)
                    ->get()->row();
            } else {
                $q2 = $this->db->select('nama')->from('tbl_pegawai')->where('nip',$username)->where('status_id', 1)
                    ->get()->row();
            }
        }


        if($q == "" || $q2 == null){
          return array('status' => 403,'message' => 'Username inactive/ not found.');
        } else {
            $hashed_password = $q->password;
            $id              = $q->nomor_induk;

            if (hash_equals($hashed_password, md5($password))) {
               $last_login = date('Y-m-d H:i:s');
//               $token = crypt(substr(md5(rand()), 0, 7),'');
               $expired_at = date("Y-m-d H:i:s", strtotime('+6 hours'));
               $this->db->trans_start();
               $this->db->where('nomor_induk',$id)->where('status_id',1)->update('tbl_pengguna',array('last_login' => $last_login));

               $tokenData = new StdClass();
               $tokenData->nomor_induk = $username;
               $tokenData->id_type = $q->id_type;
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
        return array('status' => 400,'valid'=> false,'message' => 'Token not found.');
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
            $this->db->where('nomor_induk', $data->nomor_induk)->where('status_id', 1)->update('tbl_pengguna', array('last_logout'=> $last_logout));
        }

        $this->db->where('token',$token)->where('status_id',1)->update('tbl_users_authentication', array('status_id'=> 2));
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
                $this->db->where('token',$token)->where('status_id',1)->update('tbl_users_authentication',array('status_id'=>'2','updated_at' => $updated_at));
                return json_output(401,array('status' => 401,'message' => 'Your session has been expired.'));
            } else {
                $updated_at = date('Y-m-d H:i:s');
                $expired_at = date("Y-m-d H:i:s", strtotime('+12 hours'));
                $this->db->where('token',$token)->where('status_id', 1)->update('tbl_users_authentication',array('updated_at' => $updated_at));
                $q2 = null;
                if($data->id_type == 1) {
                    $q2 = $this->db->select('*')->from('tbl_mahasiswa')->where('nim', $data->nomor_induk)->where('status_id', 1)
                    ->get()->row();
                } else {
                    $q2 = $this->db->select('*')->from('tbl_pegawai')->where('nip', $data->nomor_induk)->where('status_id', 1)
                    ->get()->row();
                }

                if($q2 == null ){
                    return json_output(401,array('status' => 401,'message' => 'Unauthorized.'));
                } else {
                    return array('status' => 200,'user_info'=>$data,'message' => 'Authorized.');
                }


            }
        }
    }

    public function authAdmin() {
        $token     = $this->input->get_request_header('Authorization', TRUE);
        $data = AUTHORIZATION::decode($token);
        if(isset($data) && $data != "" ) {
            if($data->id_type != 3 && $data->id_type != 4) {
                return json_output(401, array('status' => 401, 'message' => 'user_type is unauthorized.' ));
            } else {
                return array('status' => 200, 'message' => 'user_type is authorized.' );
            }
        } else {
            return json_output(400, array('status' => 400, 'message' => 'invalid token!'));
        }

    }

    public function list_pengguna($type = null,$start=null, $limit=null, $order='asc', $search = null) {
        $query = "
            SELECT `p`.`id`,`p`.`nomor_induk`, `m`.`nama`, `m`.`alamat`, `p`.`no_kartu`, `p`.`id_type`
            FROM `tbl_pengguna` as `p`, `tbl_mahasiswa` as `m`
            WHERE `m`.`nim` = `p`.`nomor_induk` AND `p`.`status_id` = 1";
        $querylimit = "
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
                $querylimit.= " AND p.id_type = ".$typeres->id;
            }
        }

        if(isset($search)) {
            $query.= " AND (`m`.`nim` LIKE '%".$search."%' "." OR `m`.`nama` LIKE '%".$search."%' ) ";
            $querylimit.= " AND (`m`.`nim` LIKE '%".$search."%' "." OR `m`.`nama` LIKE '%".$search."%' ) ";
        }

        $query.="
            UNION
            SELECT `p`.`id`,`p`.`nomor_induk`, `m`.`nama`, `m`.`alamat`, `p`.`no_kartu`, `p`.`id_type`
            FROM `tbl_pengguna` as `p`, `tbl_pegawai` as `m`
            WHERE `m`.`nip` = `p`.`nomor_induk`
            AND `p`.`status_id` = 1";
        $querylimit.="
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
                $querylimit.= " AND p.id_type = ".$typeres->id;
            }

        }

        if(isset($search)) {
            $query.= " AND (`m`.`nip` LIKE '%".$search."%' "." OR `m`.`nama` LIKE '%".$search."%' ) ";
            $querylimit.= " AND (`m`.`nip` LIKE '%".$search."%' "." OR `m`.`nama` LIKE '%".$search."%' ) ";
        }

        if(isset($order)) {
            $query.= " ORDER BY id ".$order;
        }
        if(isset($start) && isset($limit)) {
            $query.= " LIMIT ".$limit." OFFSET ".$start;
        }


        try {
            $data = $this->db->query($query)->result();
            $total =  $this->db->query($querylimit)->num_rows();
            return array('status' => 200, 'total'=> $total, 'data' => $data);
        } catch(Exception $err) {
            return array('status' => 400, 'message' => $err, 'ci_db_message' => $this->db->_error_message());
        }
    }

    public function list_pegawai($start=null, $limit=null, $order='asc', $search = null) {
        $totalquery = "select count(*) as count from tbl_pegawai where status_id = 1";
        if(isset($search)) {
            $totalquery.= " AND (nip LIKE '%".$search."%' OR nama LIKE '%".$search."%')";
        }

        $query = "
            SELECT *
            FROM `tbl_pegawai`
            WHERE `status_id` = 1";

        if(isset($search)) {
            $query.= " AND (nip LIKE '%".$search."%' OR nama LIKE '%".$search."%')";
        }

        if(isset($order)) {
            $query.= " ORDER BY id ".$order;
        }
        if(isset($start) && isset($limit)) {
            $query.= " LIMIT ".$limit." OFFSET ".$start;
        }

        try {
            $data = $this->db->query($query)->result();
            $total = $this->db->query($totalquery)->result();

            return array('status' => 200, 'total' =>$total[0]->count, 'data' => $data);
        } catch(Exception $err) {
            return array('status' => 400, 'message' => $err, 'ci_db_message' => $this->db->_error_message());
        }
    }

    public function list_mahasiswa($start=null, $limit=null, $order='asc', $search = null) {
        $totalquery = "select count(*) as count from tbl_mahasiswa where status_id = 1";
        if(isset($search)) {
            $totalquery.= " AND (nim LIKE '%".$search."%' OR nama LIKE '%".$search."%')";
        }

        $query = "
            SELECT *
            FROM `tbl_mahasiswa`
            WHERE `status_id` = 1";

        if(isset($search)) {
            $query.= " AND (nim LIKE '%".$search."%' OR nama LIKE '%".$search."%')";
        }

        if(isset($order)) {
            $query.= " ORDER BY id ".$order;
        }
        if(isset($start) && isset($limit)) {
            $query.= " LIMIT ".$limit." OFFSET ".$start;
        }

        try {
            $data = $this->db->query($query)->result();
            $total = $this->db->query($totalquery)->result();

            return array('status' => 200, 'total' =>$total[0]->count, 'data' => $data);
        } catch(Exception $err) {
            return array('status' => 400, 'message' => $err, 'ci_db_message' => $this->db->_error_message());
        }
    }

    public function list_type_pengguna() {
        try {
            $data = $this->db->select('*')->from('tbl_type_pengguna')->where('status_id', '1')->get()->result();
            return array('status' => 200, 'data' => $data);
        } catch(Exception $err) {
            return array('status' => 400, 'message' => $err, 'ci_db_message' => $this->db->_error_message());
        }
    }

    public function insert_mahasiswa($data) {
        $q  = $this->db->select('*')->from('tbl_mahasiswa')
            ->where('nim',$data['nim'])
            ->where('status_id', '1')
            ->get()->row();
        if($q == ""){
            $data = $this->crud_info($data, "create");

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
            $data = $this->crud_info($data, "update");
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
            $data = $this->crud_info($data, "delete");
            $this->db->where('nim', $data['nim'])->where('status_id', '1')->update('tbl_mahasiswa', $data);
            if($this->db->affected_rows() > 0) {
                return array('status' =>200 , 'message'=> 'Data mahasiswa telah terhapus');
            } else {
                return array('status' => 400, 'message' => 'No data has deleted.');
            }
        }
    }

    public function insert_pegawai($data) {
        $q  = $this->db->select('*')->from('tbl_pegawai')
            ->where('nip',$data['nip'])
            ->where('status_id', '1')
            ->get()->row();
        if($q == ""){
            $data = $this->crud_info($data, "create");

            $this->db->insert('tbl_pegawai', $data);
            if($this->db->affected_rows() > 0) {
                return array('status' =>200 , 'message'=> 'Data pegawai telah dibuat');
            }

        } else {
            return array('status' => 400,'message' => 'Data dengan nip sudah ada!');
        }


    }

    public function update_pegawai($data) {
        $q  = $this->db->select('*')->from('tbl_pegawai')
            ->where('nip',$data['nip'])
            ->where('status_id', '1')
            ->get()->row();
        if($q == ""){
            return array('status' => 400,'message' => 'Data tidak ditemukan');
        } else {
            $data = $this->crud_info($data, "update");
            $this->db->where('nip', $data['nip'])->where('status_id', '1')->update('tbl_pegawai', $data);
            if($this->db->affected_rows() > 0) {
                return array('status' =>200 , 'message'=> 'Data pegawai telah diubah');
            } else {
                return array('status' => 400, 'message' => 'No data has updated.');
            }
        }
    }

    public function delete_pegawai($data) {
        $q  = $this->db->select('*')->from('tbl_pegawai')
            ->where('nip',$data['nip'])
            ->where('status_id', '1')
            ->get()->row();
        if($q == ""){
            return array('status' => 400,'message' => 'Data tidak ditemukan');
        } else {
            $data = $this->crud_info($data, "delete");
            $this->db->where('nip', $data['nip'])->where('status_id', '1')->update('tbl_pegawai', $data);
            if($this->db->affected_rows() > 0) {
                return array('status' =>200 , 'message'=> 'Data pegawai telah terhapus');
            } else {
                return array('status' => 400, 'message' => 'No data has deleted.');
            }
        }
    }

    public function insert_pengguna($data) {
        $datauser = null;
        $q  = $this->db->select('*')->from('tbl_pengguna')->where('nomor_induk', $data['nomor_induk'])
            ->where('status_id', 1)
            ->get()->row();

        if($q != "") {
            return array('status' => 400, 'message' => 'user already exist!');
        } else {
            $gettipe  = $this->db->select('*')->from('tbl_type_pengguna')->where('keterangan', $data['tipe'])
                ->where('status_id', 1)
                ->get()->row();

            $tipe = $gettipe->id;
            if($tipe == 1) {
                $datauser = $this->db->select('*')->from('tbl_mahasiswa')->where('nim', $data['nomor_induk'])->where('status_id', 1)
                    ->get()->row();
            } else if($tipe == 2 || $tipe == 3) {
                $datauser= $this->db->select('*')->from('tbl_pegawai')->where('nip', $data['nomor_induk'])->where('status_id', 1)
                    ->get()->row();
            } else {
                return array('status' => 400, 'message' => 'invalid user_type!');
            }

            if($datauser == "" || $datauser == null) {
                return array('status'=> 400, 'message' => 'data with field \'nomor_induk\' = '.$data['nomor_induk'].' not found.');
            } else {
                $newdata = new StdClass();
                $newdata->nomor_induk = $data['nomor_induk'];
                $newdata->password = md5($data['password']);
                $newdata->id_type = $tipe;
                $newdata->no_kartu = $data['nomor_induk'];
                $newdata->status_id = 1;
                $newdata = $this->crud_info_std($newdata, "create");
                $this->db->insert('tbl_pengguna', $newdata);
                return array('status'=> 200, 'data'=> $datauser);
            }
        }
    }

    public function update_pengguna($data) {
        $datauser = null;
        $q  = $this->db->select('*')->from('tbl_pengguna')->where('nomor_induk', $data['nomor_induk'])
            ->where('status_id', 1)
            ->get()->row();

        if($q == "") {
            return array('status' => 400, 'message' => 'user not exist!');
        } else {
            $gettipe  = $this->db->select('*')->from('tbl_type_pengguna')->where('keterangan', $data['tipe'])
                ->where('status_id', 1)
                ->get()->row();

            $tipe = $gettipe->id;
            if($tipe == 1) {
                $datauser = $this->db->select('*')->from('tbl_mahasiswa')->where('nim', $data['nomor_induk'])->where('status_id', 1)
                    ->get()->row();
            } else if($tipe == 2 || $tipe == 3 || $tipe == 4) {
                $datauser= $this->db->select('*')->from('tbl_pegawai')->where('nip', $data['nomor_induk'])->where('status_id', 1)
                    ->get()->row();
            } else {
                return array('status' => 400, 'message' => 'invalid user_type!');
            }

            if($datauser == "" || $datauser == null) {
                return array('status'=> 400, 'message' => 'data with field \'nomor_induk\' = '.$data['nomor_induk'].' not found.');
            } else {
                $newdata = new StdClass();
                $newdata->nomor_induk = $data['nomor_induk'];
                $newdata->password = md5($data['password']);
                $newdata->id_type = $tipe;
                $newdata->no_kartu = $data['nomor_induk'];
                $newdata->status_id = 1;
                $newdata = $this->crud_info_std($newdata, "update");
                $this->db->where('nomor_induk',$newdata->nomor_induk)->where('status_id', 1)->update('tbl_pengguna', $newdata);
                return array('status'=> 200, 'data'=> $datauser);
            }
        }
    }

    public function delete_pengguna($data) {
        $datauser = null;
        $q  = $this->db->select('*')->from('tbl_pengguna')->where('nomor_induk', $data['nomor_induk'])
            ->where('status_id', 1)
            ->get()->row();

        if($q == "") {
            return array('status' => 400, 'message' => 'user not exist!');
        } else {
            $gettipe  = $this->db->select('*')->from('tbl_type_pengguna')->where('keterangan', $data['tipe'])
                ->where('status_id', 1)
                ->get()->row();

            $tipe = $gettipe->id;
            if($tipe == 1) {
                $datauser = $this->db->select('*')->from('tbl_mahasiswa')->where('nim', $data['nomor_induk'])->where('status_id', 1)
                    ->get()->row();
            } else if($tipe == 2 || $tipe == 3 || $tipe == 4) {
                $datauser= $this->db->select('*')->from('tbl_pegawai')->where('nip', $data['nomor_induk'])->where('status_id', 1)
                    ->get()->row();
            } else {
                return array('status' => 400, 'message' => 'invalid user_type!');
            }

            if($datauser == "" || $datauser == null) {
                return array('status'=> 400, 'message' => 'data with field \'nomor_induk\' = '.$data['nomor_induk'].' not found.');
            } else {
                $newdata = new StdClass();
                $newdata->nomor_induk = $data['nomor_induk'];
                $newdata->status_id = 0;
                $newdata = $this->crud_info_std($newdata, "delete");
                $this->db->where('nomor_induk',$newdata->nomor_induk)->where('status_id', 1)->update('tbl_pengguna', $newdata);
                return array('status'=> 200, 'data'=> 'succesfully delete');
            }
        }
    }

    public function crud_info($data, $type = null) {
        $token     = $this->input->get_request_header('Authorization', TRUE);
        $decode = AUTHORIZATION::decode($token);
        if($type == "create") {
            $data['created_at'] = date('Y-m-d h:m:s');
            $data['created_by'] = $decode->nomor_induk;
        }
        if($type == "update") {
            $data['updated_at'] = date('Y-m-d h:m:s');
            $data['updated_by'] = $decode->nomor_induk;
        }

        if($type == "delete") {
            $data['deleted_at'] = date('Y-m-d h:m:s');
            $data['deleted_by'] = $decode->nomor_induk;
        }

        return $data;
    }

    public function crud_info_std($data, $type = null) {
        $token     = $this->input->get_request_header('Authorization', TRUE);
        $decode = AUTHORIZATION::decode($token);
        if($type == "create") {
            $data->created_at = date('Y-m-d h:m:s');
            $data->created_by = $decode->nomor_induk;
        }
        if($type == "update") {
            $data->updated_at = date('Y-m-d h:m:s');
            $data->updated_by = $decode->nomor_induk;
        }

        if($type == "delete") {
            $data->deleted_at = date('Y-m-d h:m:s');
            $data->deleted_by = $decode->nomor_induk;
        }

        return $data;
    }

    public function check_nomor_induk($nomor_induk) {
        $q = $this->db->select('*')->from('tbl_pengguna')->where('nomor_induk', $nomor_induk)->where('status_id', 1)
            ->get()->row();
        if($q == "") {
            return array('status' => 401,'message' => 'kartu tidak terdaftar');
        } else {
            if($q->id_type == 1) {
                $q = $this->db->select('*')->from('tbl_mahasiswa')->where('nim', $nomor_induk)->where('status_id', 1)
                    ->get()->row();
            } else {
                $q = $this->db->select('*')->from('tbl_pegawai')->where('nip', $nomor_induk)->where('status_id', 1)
                    ->get()->row();
            }

            if($q == "") {
                return array('status' => 401,'message' => 'data pengguna tidak terdaftar!');
            } else {
                return array('status'=> 200);
            }
        }
    }

    public function proses_parkir($data) {
        if($data['jenis_parkir'] == "masuk") {
            $q = $this->db->select('*')->from('tbl_parkir')
                ->where('nomor_induk', $data['nomor_induk'])
                ->where('jenis_parkir', 'masuk')
                ->where('status_id', 1)->get()->row();
            if($q == "") {
                try {
                    $data['waktu'] = date('Y-m-d H:i:s');
                    $data['status_id'] = 1;
                    $data = $this->crud_info($data, "create");
                    $this->db->insert('tbl_parkir', $data);
                    return array('status'=>200, 'command'=>'open_portal_masuk', 'message'=>'success proses parkir');
                } catch(Exception $err) {
                    return array('status'=> 400, 'message'=> 'error proses parkir!', 'error_ci' => $this->db->_error_message());
                }
            } else {
                return array('status' => 400, 'command'=>'open_portal_masuk','message' => 'user sudah masuk / belum keluar.');
            }
        }
        else if($data['jenis_parkir'] == "keluar") {
            $q = $this->db->select('*')->from('tbl_parkir')
                ->where('nomor_induk', $data['nomor_induk'])
                ->where('jenis_parkir', 'masuk')
                ->where('status_id', 1)->get()->row();

            if($q == "") {
                return array('status'=> 400, 'message' => 'user belum masuk!');
            } else {
                $data['waktu'] = date('Y-m-d H:i:s');
                $data['status_id'] = 1;
                $data = $this->crud_info($data, "create");
                $this->db->insert('tbl_parkir', $data);

                $newData = array(
                    'status_id'=> 2,
                    'updated_at'=> date('Y-m-d H:i:s')
                );
                $newData = $this->crud_info($newData, "update");
                $this->db
                    ->where('nomor_induk', $data['nomor_induk'])
                    ->where('status_id', 1)
                    ->where('jenis_parkir','masuk')
                    ->update('tbl_parkir', $newData);
                return array('status' => 200, 'message'=> 'success keluar.', 'command'=> 'open_portal_keluar');
            }
        }
        else {
            return array('status' => 400, 'message' => 'jenis tidak terdaftar!');
        }

    }
}
