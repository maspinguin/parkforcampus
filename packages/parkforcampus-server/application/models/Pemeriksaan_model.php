<?php
defined('BASEPATH') OR exit('No direct script access allowed');

class Pemeriksaan_model extends MY_Model {

  var $table = 'pemeriksaan';
  var $column_order = array('id_periksa', 'm.no_seri','pl.nama_pel','tanggal'); //set column field database for datatable orderable
  var $column_search = array('id_periksa', 'm.no_seri','pl.nama_pel','tanggal'); //set column field database for datatable searchable
  var $order = array('id_periksa' => 'asc'); // default order
  var $key = 'id_periksa'; // default order

  // ubah variabel ini mendaji true untuk melakukan proses upload data
  var $is_upload = true;

  /**
   * fungsi untuk validasi form
   */
  public function setRules()
  {
    $form = array(
            array(
                'field' => 'id_pemakaian',
                'label' => 'Pemakaian',
                'rules' => 'required|callback_check_default'
            ),
            array(
                'field' => 'tanggal',
                'label' => 'Tanggal',
                'rules' => 'required'
            ),
    );
    return $form;
  }

  public function GetDataPostForm()
  {
    $data = array(
        'id_periksa' => $this->input->post('id_periksa'),
        'id_pemakaian' => $this->input->post('id_pemakaian'),
        'tanggal' => $this->input->post('tanggal'),
        'alasan' => $this->input->post('alasan'),
        'lattitude' => $this->input->post('lattitude'),
        'longitude' => $this->input->post('longitude'),
        'stand' => $this->input->post('stand'),
        'petugas' => $this->session->userdata('fullname')
    );
    // print_r($data);die;
    return $data;
  }

  function _get_datatable_from()
  {
    $this->db->select('p.*,m.*, pl.nama_pel,pl.no_pelanggan,pl.alamat');
    $this->db->from($this->table.' as p,pemakaian as pm,material as m, pelanggan as pl');
    $this->db->where('p.id_pemakaian = pm.id_pemakaian');
    $this->db->where('pm.id_material = m.id_material');
    $this->db->where('pm.id_pelanggan = pl.id_pel');
    // 0 segel, 1 belum diperiksa, 2 sudah diperiksa tidak segel
    $this->db->where('pm.statusid = 1');
  }

  // public function print_pemeriksaan($id){
  //     $this->db->select('*, DATE_FORMAT(a.tanggal, "%d-%m-%Y") as tgl_baru, DAYOFWEEK(a.tanggal) as hari');
  //     $this->db->from('pemeriksaan as a');
  //     $this->db->join('pemakaian as b','a.id_pemakaian=b.id_pemakaian','left');
  //     $this->db->join('pelanggan as c','b.id_pelanggan=c.id_pel','left');
  //     $this->db->join('material as d','b.id_material=d.id_material','left');
  //     $this->db->where('a.id_periksa='.$id);
  //
  //     return $this->db->get();
  // }

  public function set_upload_options($id)
  {
    $config = array();
    $config['upload_path'] = './asset/img/pemeriksaan/';
    $config['allowed_types'] = 'gif|jpg|png';
    $config['max_size']      = '0';
    $config['overwrite']     = TRUE;
    $config['file_name']     = $id;

    return $config;
  }

  /**
   * pencarian data agen dengan id, untuk edit dan detail
   * @param  number $id id agen
   * @return object data agen
   */
  // public function cari($id)
  // {
  //   $this->db->select('p.*,pm.lattitude,pm.longitude');
  //   $this->db->from($this->table.' p,pemakaian pm');
  //   $this->db->where('p.id_pemakaian = pm.id_pemakaian');
  //   $this->db->limit(1);
  //   $query = $this->db->get();

  //   return $query->row();
  // }

  /**
   * gunakan fungsi ini apabila ingin menambahkan data yang ingin diikutkan kedalam view form
   * @return [type] [description]
   */
  function _data_tambahan_form($data)
  {
    $this->load->model('Pemakaian_model','pemakaian');
    // $this->pemakaian->data_pemakaian_aktif();
    $pieces = explode("/", $data['form_action']);
    if(count($pieces) == 3){
      $this->pemakaian->_get_datatable_from();
    }else{
      $this->pemakaian->data_pemakaian_aktif();
    }
    $data['pemakaian'] = $this->db->get();

    // untuk mendapatkan koordinat melalui modal pop-up
    $config['zoom'] = 'auto';
    $config['onclick'] = 'getLatLong(event.latLng.lat(),event.latLng.lng());';
    $this->googlemaps->initialize($config);
    $data['map'] = $this->googlemaps->create_map();

    $data['css'] = array(base_url('asset/plugins/select2/select2.min.css'),base_url('asset/plugins/datepicker/datepicker3.css'));
    $data['script'] = array(base_url('asset/plugins/select2/select2.full.min.js'),base_url('asset/plugins/datepicker/bootstrap-datepicker.js'));
    $data['js'] = 'pemeriksaanformJS';
    // print_r($data['pemakaian']->result());
    return $data;
  }

  public function afterInsert($id)
  {
    $data = array(
        'lattitude' => $this->input->post('lat'),
        'longitude' => $this->input->post('long'),
    );

    $this->db->where('id_pemakaian', $this->input->post('id_pemakaian'));
    $this->db->update('pemakaian', $data);

    $this->db->trans_complete();

    if($this->db->affected_rows() > 0)
    {
        return TRUE;
    }
    else
    {
      if ($this->db->trans_status() === FALSE) {
        return FALSE;
      }else{
        return TRUE;
      }
    }
  }

  public function print_pemeriksaan($id){
      $this->db->select('*, DATE_FORMAT(a.tanggal, "%d-%m-%Y") as tgl_baru, DAYOFWEEK(a.tanggal) as hari');
      $this->db->from('pemeriksaan as a');
      $this->db->join('pemakaian as b','a.id_pemakaian=b.id_pemakaian','left');
      $this->db->join('pelanggan as c','b.id_pelanggan=c.id_pel','left');
      $this->db->join('material as d','b.id_material=d.id_material','left');
      $this->db->where('a.id_periksa='.$id);

      return $this->db->get();
  }

}
/* End of file agen_model.php */
/* Location: ./application/models/absen_model.php */
