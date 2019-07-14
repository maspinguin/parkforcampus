<?php if (!defined('BASEPATH')) exit('No direct script access allowed');

class Login_model extends CI_Model {

    public $db_tabel = 'user';

    public function load_form_rules()
    {
      $form_rules = array(
                          array(
                              'field' => 'username',
                              'label' => 'Username',
                              'rules' => 'required'
                          ),
                          array(
                              'field' => 'password',
                              'label' => 'Password',
                              'rules' => 'required'
                          ),
      );
      return $form_rules;
    }

    public function validasi()
    {
      $form = $this->load_form_rules();
      $this->form_validation->set_rules($form);

      if ($this->form_validation->run())
      {
          return TRUE;
      }
      else
      {
          return FALSE;
      }
    }

    // cek status user, login atau tidak?
    public function cek_user()
    {
      $username = $this->input->post('username');
      $password = md5($this->input->post('password'));

      $query = $this->db->select('*')
                        ->where('username', $username)
                        ->where('password', $password)
                        ->limit(1)
                        ->get($this->db_tabel);
      
      if ($query->num_rows() == 1)
      {
        $hasil = $query->row();

        $data = array('id_user' => $hasil->id_user, 'username' => $username, 'login' => TRUE, 'fullname' => $hasil->full_name);
        // buat data session jika login benar
        $this->session->set_userdata($data);
        return TRUE;
      }
      else
      {
          return FALSE;
      }
    }

    public function logout()
    {
      $this->session->unset_userdata(array('id_user' => '', 'username' => '', 'login' => FALSE, 'fullname' => ''));
      $this->session->sess_destroy();
    }
}
/* End of file login_model.php */
/* Location: ./application/models/login_model.php */