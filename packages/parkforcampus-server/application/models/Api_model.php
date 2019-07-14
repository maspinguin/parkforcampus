<?php
defined('BASEPATH') OR exit('No direct script access allowed');

class Api_model extends CI_Model {

    var $client_service = "frontend-client";
    var $auth_key       = "ap3tl";

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

        $q  = $this->db->select('password, id_user')->from('user')->where('username',$username)->get()->row();
        if($q == ""){
          return array('status' => 204,'message' => 'Username  not found.');
        } else {
            $hashed_password = $q->password;
            $id              = $q->id_user;
            if (hash_equals($hashed_password, md5($password))) {
               $last_login = date('Y-m-d H:i:s');
               $token = crypt(substr(md5(rand()), 0, 7),'');
               $expired_at = date("Y-m-d H:i:s", strtotime('+6 hours'));
               $this->db->trans_start();
               $this->db->where('id_user',$id)->update('user',array('last_login' => $last_login));
               $this->db->insert('users_authentication',array('id_user' => $id,'token' => $token,'expired_at' => $expired_at, 'created_at' => date('Y-m-d h:m:s'), 'status' => 1));
               if ($this->db->trans_status() === FALSE){
                  $this->db->trans_rollback();
                  return array('status' => 500,'message' => 'Internal server error.');
               } else {
                  $this->db->trans_commit();
                  return array('status' => 200,'message' => 'Successfully login.','id_user' => $id, 'token' => $token);
               }
            } else {
               return array('status' => 204,'message' => 'Wrong password.');
            }
        }
    }

    public function checkToken($token='')
    {
      // code...
      if($token == ''){
        return array('status' => 203 ,'message' => 'Field token is empty' );
      }

      $q  = $this->db->select('id_user,expired_at')->from('users_authentication')->where('token',$token)->where('status', '1')->get()->row();
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

        $users_id  = $this->input->get_request_header('User-ID', TRUE);
        $token     = $this->input->get_request_header('Authorization', TRUE);
        if(empty($users_id) || empty($token)){
          return array('status' => 401 ,'message' => 'Unauthorized' );
        }


        $this->db->where('id_user',$users_id)->where('token',$token)->update('users_authentication', array('status'=> 2));
        return array('status' => 200,'message' => 'Successfully logout.');
    }

    public function auth()
    {
        $users_id  = $this->input->get_request_header('User-ID', TRUE);
        $token     = $this->input->get_request_header('Authorization', TRUE);
        $q  = $this->db->select('expired_at')->from('users_authentication')->where('id_user',$users_id)->where('token',$token)->where('status','1')->get()->row();
        if($q == ""){
            return json_output(401,array('status' => 401,'message' => 'Unauthorized.'));
        } else {
            if($q->expired_at < date('Y-m-d H:i:s')){
                return json_output(401,array('status' => 401,'message' => 'Your session has been expired.'));
            } else {
                $updated_at = date('Y-m-d H:i:s');
                $expired_at = date("Y-m-d H:i:s", strtotime('+12 hours'));
                $this->db->where('id_user',$users_id)->where('token',$token)->update('users_authentication',array('expired_at' => $expired_at,'updated_at' => $updated_at));
                return array('status' => 200,'message' => 'Authorized.');
            }
        }
    }

    public function pelanggan_all_data()
    {
        return $this->db->select('*')->from('pelanggan')->order_by('id_pel','asc')->get()->result();
    }

    public function material_all_data()
    {
        return $this->db->select('*')->from('material')->order_by('id_material','asc')->get()->result();
    }

    public function pemakaian_all_data()
    {
        return $this->db->select('*')->from('pemakaian')
            ->join('pelanggan', 'pelanggan.id_pel = pemakaian.id_pelanggan')
            ->order_by('id_pemakaian','asc')->get()->result();
    }

    public function pemeriksaan_all_data()
    {
      return $this->db->select('p.*,m.*, pl.nama_pel,pl.no_pelanggan,pl.alamat')
      ->from('pemeriksaan as p,pemakaian as pm,material as m, pelanggan as pl')
        ->where('p.id_pemakaian = pm.id_pemakaian')
        ->where('pm.id_material = m.id_material')
        ->where('pm.id_pelanggan = pl.id_pel')
      // 0 segel, 1 belum diperiksa, 2 sudah diperiksa tidak segel
        ->where('pm.statusid  = 1')
        ->order_by('p.tanggal','desc')->get()->result();


        // return $this->db->select('*')->from('pemeriksaan')->order_by('id_periksa','asc')->get()->result();
    }

    public function penyegelan_all_data()
    {
        return $this->db->select('*')->from('penyegelan')->order_by('id_segel','asc')->get()->result();
    }

    public function pelanggan_detail_data($id='')
    {
        return $this->db->select('*')->from('pelanggan')->where('id_pel',$id)->order_by('id_pel','desc')->get()->row();
    }

    public function material_detail_data($id='')
    {
        return $this->db->select('*')->from('material')->where('id_material',$id)->order_by('id_material','asc')->get()->row();
    }

    public function pemakaian_detail_data($id='')
    {
        return $this->db->select('*')->from('pemakaian')->where('id_pemakaian',$id)->order_by('id_pemakaian','asc')->get()->row();
    }

    public function pemeriksaan_detail_data($id='')
    {
      return $this->db->select('p.*,m.*, pl.nama_pel,pl.no_pelanggan,pl.alamat')
      ->from('pemeriksaan as p,pemakaian as pm,material as m, pelanggan as pl')
        ->where('p.id_pemakaian = pm.id_pemakaian')
        ->where('pm.id_material = m.id_material')
        ->where('pm.id_pelanggan = pl.id_pel')
      // 0 segel, 1 belum diperiksa, 2 sudah diperiksa tidak segel

        ->where('p.id_periksa',$id)
        ->get()->result();

        // return $this->db->select('*')->from('pemeriksaan')->where('id_periksa',$id)->order_by('id_periksa','asc')->get()->row();
    }

    public function penyegelan_detail_data($id='')
    {
        return $this->db->select('*')->from('penyegelan')->where('id_segel',$id)->order_by('id_segel','asc')->get()->row();
    }



    public function pemakaian_create_data($data)
    {
        $this->db->insert('pemakaian',$data);
        return array('status' => 201,'message' => 'Data pemakaian has been created.');
    }

    public function pemeriksaan_create_data($data)
    {
        $this->db->insert('pemeriksaan',$data);
        if($this->db->affected_rows() > 0){
            $config['upload_path']          = 'asset/upload_pemeriksaan/';
			$config['allowed_types']        = 'jpg';
			$config['max_size']             = 8000;
            $config['file_name']            = $this->db->insert_id().'.jpg';

			$this->load->library('upload', $config);
			if(!$this->upload->do_upload('foto')){
				return array('status' =>403 , 'message'=> 'Data pemeriksaan telah dibuat. Gagal mengupload foto. '.$this->upload->display_errors('',''));
			}

            $data2['foto'] = $this->db->insert_id().".jpg";
            $this->db->where('id_periksa', $this->db->insert_id());
            $this->db->update('pemeriksaan', $data2);
            if($this->db->affected_rows() > 0) {
                return array('status' =>203 , 'message'=> 'Data Pemeriksaan Telah Dibuat');
            }
            return array('status' =>403 , 'message'=> 'Data pemeriksaan Telah Dibuat, nama foto belum terupdate');

        }
        else{
            return array('status' =>403 , 'message'=> 'Gagal membuat data pemeriksaan');
        }
        // return array('status' => 201,'message' => 'Data pemeriksaan has been created.');
    }

    public function penyegelan_create_data($data)
    {
        $this->db->insert('penyegelan',$data);
        if($this->db->affected_rows() > 0)
        {
            $dataedit = array('statusid' => '0');

            $this->db->where('id_pemakaian', $data['id_pemakaian']);
            $this->db->update('pemakaian', $dataedit);

            $this->db->trans_complete();

            if($this->db->affected_rows() > 0)
            {
                // return TRUE;
                return array('status' => 203,'message' => 'Data penyegelan has been created.');
            }
            else
            {
              if ($this->db->trans_status() === FALSE) {
                  return array('status' => 403,'message' => 'Data penyegelan failed to add.');

                // return FALSE;
              }else{
                  return array('status' => 203,'message' => 'Data penyegelan has been created.');
                // return TRUE;
              }
          }
      }else {
          return array('status' => 403,'message' => 'Data penyegelan failed to add.');
      }

    }

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
