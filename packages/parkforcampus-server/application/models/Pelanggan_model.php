<?php
defined('BASEPATH') OR exit('No direct script access allowed');

class Pelanggan_model extends MY_Model {

  var $table = 'pelanggan';
  var $column_order = array('no_pelanggan', 'nama_pel','nama_penghuni','alamat'); //set column field database for datatable orderable
  var $column_search = array('no_pelanggan', 'nama_pel','nama_penghuni','alamat'); //set column field database for datatable searchable
  var $order = array('id_pel' => 'asc'); // default order
  var $key = 'id_pel'; // default order

  /**
   * fungsi untuk validasi form
   */
	public function setRules()
  {
    $form = array(
            array(
                'field' => 'no_pelanggan',
                'label' => 'Nomor Pelanggan',
                'rules' => 'required|max_length[15]|is_unique[pelanggan.no_pelanggan]'
            ),
            array(
                'field' => 'nama_pel',
                'label' => 'Nama Pelanggan',
                'rules' => 'required',
            )
    );

     $this->form_validation->set_message('is_unique', '%s telah digunakan');
     $this->form_validation->set_message('required', '%s belum diisi');


     return $form;
  }

  public function GetDataPostForm()
  {
    $data = array(
        'id_pel' => $this->input->post('id_pel'),
        'no_pelanggan' => $this->input->post('no_pelanggan'),
        'nama_pel' => $this->input->post('nama_pel'),
        // 'nama_penghuni' => $this->input->post('nama_penghuni'),
        'alamat' => $this->input->post('alamat'),
    );
    return $data;
  }
  // public function cari($id)
  // {
  //     return $this->db->select('*')
  //         ->where('no_pelanggan', $id)
  //         ->limit(1)
  //         ->get($this->table)
  //         ->num_rows();
  // }
  // public function tambah()
  // {
  //   $data = $this->GetDataPostForm();
  //   // print_r($this->cari($this->input->post('no_pelanggan'))); die;
  //   if($this->cari($this->input->post('no_pelanggan'))>0){
  //       return FALSE;
  //   }else {
  //       $this->db->insert($this->table, $data);
  //       if($this->db->affected_rows() > 0)
  //       {
  //         return TRUE;
  //       }
  //       else
  //       {
  //         return FALSE;
  //       }
  //   }
  // }
}
/* End of file agen_model.php */
/* Location: ./application/models/absen_model.php */
