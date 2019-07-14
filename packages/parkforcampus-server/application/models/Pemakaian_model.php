<?php
defined('BASEPATH') OR exit('No direct script access allowed');

class Pemakaian_model extends MY_Model {
  
  var $table = 'pemakaian';
  var $column_order = array('nama_pel','material','lattitude'); //set column field database for datatable orderable
  var $column_search = array('nama_pel','material','lattitude'); //set column field database for datatable searchable 
  var $order = array('nama_pel' => 'asc'); // default order 
  var $key = 'id_pemakaian'; // default order 

  /**
   * fungsi untuk validasi form
   */
	public function setRules()
  {
    $form = array(
            array(
                'field' => 'id_pel',
                'label' => 'Pelanggan',
                'rules' => 'required|callback_check_default'
            ),
            array(
                'field' => 'id_material',
                'label' => 'Material',
                'rules' => 'required|callback_check_default'
            )
    );
    return $form;
  }

  function _get_datatable_from()
  {
    $this->db->select('p.*,pl.nama_pel,pl.no_pelanggan,pl.nama_penghuni,m.*');
    $this->db->from('pemakaian p, pelanggan pl, material m');
    $this->db->where('p.id_pelanggan = pl.id_pel');
    $this->db->where('p.id_material = m.id_material');
  }

  function data_pemakaian_aktif()
  {
    $this->_get_datatable_from();
    $this->db->where('statusid = 1');
  }

  public function GetDataPostForm()
  {
    $data = array(
        'id_pemakaian' => $this->input->post('id_pemakaian'),
        'id_pelanggan' => $this->input->post('id_pel'),
        'id_material' => $this->input->post('id_material'),
        'statusid' => '1',
    );
    return $data;
  }

  /**
   * gunakan fungsi ini apabila ingin menambahkan data yang ingin diikutkan kedalam view form
   * @return [type] [description]
   */
  function _data_tambahan_form($data)
  {
    $this->load->model('Pelanggan_model','pelanggan');
    $this->pelanggan->_get_datatable_from();
    $data['pelanggan'] = $this->db->get();

    $this->load->model('Material_model','material');
    $this->material->_get_datatable_from();
    $data['material'] = $this->db->get();
    
    $data['css'] = array(base_url('asset/plugins/select2/select2.min.css'));
    $data['script'] = array(base_url('asset/plugins/select2/select2.full.min.js'));
    $data['js'] = 'materialformJS';
    return $data;
  }
}
/* End of file agen_model.php */
/* Location: ./application/models/absen_model.php */