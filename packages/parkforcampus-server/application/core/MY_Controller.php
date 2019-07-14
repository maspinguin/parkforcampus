<?php if (!defined('BASEPATH')) exit('No direct script access allowed');

class  MY_Controller extends CI_Controller
{

  var $a_page_form = '';
  var $a_kontroler = '';
  var $a_nama = '';


  public function __construct()
  {
    parent::__construct();

    if(!isset($_SESSION))
    {
      session_start();
    }

    // cek status login user
    if ($this->session->userdata('login') == FALSE)
    {
      $this->session->set_flashdata('pesan', 'Anda harus login terlebih dahulu.');
      redirect('Login');
    }
  }

  /**
   * fungsi untuk combobox yang required
   * @param  [type] $array [description]
   * @return [type]        [description]
   */
  function check_default($post_string)
  {
    return $post_string == '0' ? FALSE : TRUE;
  }

  /**
   * untuk ajax data table
   * @return [type] [description]
   */
  public function listdatatable($query = '')
  {
    $list = $this->model->get_datatables();

    $data = array();
    $data = $this->makeDataTable($list);
    $output = array(
            "recordsTotal" => $this->model->count_all(),
            "recordsFiltered" => $this->model->count_filtered(),
            "data" => $data,
        );

    $output['draw'] = (isset($_POST['draw'])) ? $_POST['draw'] : '' ;

    //output to json format
    echo json_encode($output);
  }

  /**
   * controller form tambah data
   * @return [type] [description]
   */
  public function tambah()
  {
    $this->load->helper('form');
    $this->data['main_view'] = $this->a_page_form;
    $this->data['form_action'] = $this->a_kontroler.'/tambah';

    $a_data = $this->model->_data_tambahan_form($this->data);

    // apabila ada submit masuk
    if ($this->input->post('submit')) {
      // melakukan validasi data sebelum disimpan
      if ($this->model->validasi()) {
        // jika validasi sukses, maka simpan data
        if($this->model->tambah()){
          $this->model->afterInsert($this->db->insert_id());

          if ($this->model->is_upload == TRUE) {
            if($this->model->upload_foto($this->db->insert_id())){
              $this->session->set_flashdata('pesan', array('info','Proses tambah data '.$this->a_nama.' berhasil'));
            }else{
              $this->session->set_flashdata('pesan', array('warning','Proses tambah data '.$this->a_nama.' berhasil. Proses Upload gagal'));
            }
          }else{
            // tambah data sukses
            $this->session->set_flashdata('pesan', array('info','Proses tambah data '.$this->a_nama.' berhasil'));
          }
          redirect($this->a_kontroler);
        }else{

          // tambah data gagal
          $this->data['pesan'] = 'Proses tambah data gagal. pastikan anda tidak memasuki program melalui menu. apabila pesan ini masih berlanjut, segera hubungi administrator';
          $this->load->view('template', $a_data);
        }
      }else{
        $this->data['pesan'] = 'Proses validasi data gagal. pastikan anda tidak memasuki program melalui menu. apabila pesan ini masih berlanjut, segera hubungi administrator';
        // validasi gagal
        $this->load->view('template',$a_data);
      }
    }else{

      // belum ada submit masuk, tampilkan form kosong
      $this->load->view('template',$a_data);
    }
  }

  public function edit($id = NULL)
  {
    $this->load->helper('form');
    $this->data['main_view'] = $this->a_page_form;
    $this->data['form_action'] = $this->a_kontroler.'/edit/'. $id;

    $a_data = $this->model->_data_tambahan_form($this->data);
    // Mencegah error (edit tanpa ada parameter)
    // Ada parameter
    if( ! empty($id))
    {
        // submit
        if($this->input->post('submit'))
        {
          if ($this->model->validasi()) {
            if($this->model->edit($id)){
              if ($this->model->is_upload == TRUE) {
                if($this->model->upload_foto($id)){
                  $this->session->set_flashdata('pesan', array('info','Proses perubahan data '.$this->a_nama.' berhasil'));
                }else{
                  $this->session->set_flashdata('pesan', array('warning','Proses perubahan data '.$this->a_nama.' berhasil. Proses Upload gagal'));
                }
              }else{
                // tambah data sukses
                $this->session->set_flashdata('pesan', array('info','Proses perubahan data '.$this->a_nama.' berhasil'));
              }
              redirect($this->a_kontroler);
            }else{
              $this->data['pesan'] = 'Proses perubahan data gagal. pastikan anda tidak memasuki program melalui menu. apabila pesan ini masih berlanjut, segera hubungi administrator';
              $this->load->view('template', $a_data);
            }
          }else{
            $this->load->view('template',$a_data);
          }

        }
        // tidak disubmit, form pertama kali dimuat
        else
        {
            // ambil data dari database, $form_value sebagai nilai default form
            $data = $this->model->cari($id);
            foreach($data as $key => $value)
            {
                $a_data['form_value'][$key] = $value;
            }

            $this->load->view('template', $a_data);
        }
    }
    // tidak ada parameter $id di URL, kembalikan ke halaman siswa
    else
    {
        redirect($this->a_kontroler);
    }
  }




  public function hapus($id)
  {
    if( ! empty($id))
    {
      if($this->model->hapus($id)){
        $this->session->set_flashdata('pesan', array('success','Proses penghapusan data '.$this->a_nama.' berhasil.'));
        redirect($this->a_kontroler);
      }else{
        $this->session->set_flashdata('pesan', array('danger','Proses penghapusan data '.$this->a_nama.' gagal.'));
        redirect($this->a_kontroler);
      }
    }
    // tidak ada parameter $nis di URL, kembalikan ke halaman agen
    else
    {
      $this->session->set_flashdata('pesan', array('warning','Pastikan anda memasuki sistem melalui menu, jika pesan ini berlanjut segera hubungi administrator.'));
      redirect($this->a_kontroler);
    }
  }
}
/* End of file MY_Controller.php */
/* Location: ./application/core/MY_Controller.php */
