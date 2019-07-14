<?php
defined('BASEPATH') OR exit('No direct script access allowed');

class Penyegelan_model extends MY_Model {

  var $table = 'penyegelan';
  var $column_order = array('id_segel', 'm.no_seri','pl.nama_pel','tanggal'); //set column field database for datatable orderable
  var $column_search = array('id_segel', 'm.no_seri','pl.nama_pel','tanggal'); //set column field database for datatable searchable
  var $order = array('id_segel' => 'asc'); // default order
  var $key = 'id_segel'; // default order

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
        'id_segel' => $this->input->post('id_segel'),
        'no_segel' => $this->input->post('no_segel'),
        'id_pemakaian' => $this->input->post('id_pemakaian'),
        'tanggal' => $this->input->post('tanggal'),
        'alasan' => $this->input->post('alasan'),
        'petugas' => $this->session->userdata('fullname')
    );
    return $data;
  }

  function _get_datatable_from()
  {
    $this->db->select('p.*,m.*, pl.nama_pel,pl.no_pelanggan,pl.alamat');
    $this->db->from($this->table.' as p,pemakaian as pm,material as m, pelanggan as pl');
    $this->db->where('p.id_pemakaian = pm.id_pemakaian');
    $this->db->where('pm.id_material = m.id_material');
    $this->db->where('pm.id_pelanggan = pl.id_pel');
  }

  /**
   * gunakan fungsi ini apabila ingin menambahkan data yang ingin diikutkan kedalam view form
   * @return [type] [description]
   */
  function _data_tambahan_form($data)
  {
    $this->load->model('Pemakaian_model','pemakaian');
    $this->pemakaian->data_pemakaian_aktif();
    $data{'pemakaian'} = $this->db->get();

    $data['css'] = array(base_url('asset/plugins/select2/select2.min.css'),base_url('asset/plugins/datepicker/datepicker3.css'));
    $data['script'] = array(base_url('asset/plugins/select2/select2.full.min.js'),base_url('asset/plugins/datepicker/bootstrap-datepicker.js'));
    $data['js'] = 'pemeriksaanformJS';
    return $data;
  }

  /**
   * fungsi yang dijalankan ketika sudah selesai menahmbahkan data ke database
   * @param  [type] $id data last id insert ke database
   * @return [type]     [description]
   */
  public function afterInsert($id)
  {
    $dataedit = array('statusid' => '0');

    $this->db->where('id_pemakaian', $this->input->post('id_pemakaian'));
    $this->db->update('pemakaian', $dataedit);

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

  // public function before_delete($id)
  // {
  //   $data = $this->cari($id);

  // }
  public function print_penyegelan($id){
      $this->db->select('*, DATE_FORMAT(a.tanggal, "%d-%m-%Y") as tgl_baru, DAYOFWEEK(a.tanggal) as hari');
      $this->db->from('penyegelan as a');
      $this->db->join('pemakaian as b','a.id_pemakaian=b.id_pemakaian','left');
      $this->db->join('pelanggan as c','b.id_pelanggan=c.id_pel','left');
      $this->db->join('material as d','b.id_material=d.id_material','left');
      $this->db->join('pemeriksaan as e','e.id_pemakaian=a.id_pemakaian','left');
      $this->db->where('a.id_segel='.$id);

      return $this->db->get();


  }

}
/* End of file agen_model.php */
/* Location: ./application/models/absen_model.php */
