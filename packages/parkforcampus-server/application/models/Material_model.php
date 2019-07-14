<?php
defined('BASEPATH') OR exit('No direct script access allowed');

class Material_model extends MY_Model {

  var $table = 'material';
  var $column_order = array('no_seri', 'material','type'); //set column field database for datatable orderable
  var $column_search = array('no_seri', 'material','type'); //set column field database for datatable searchable
  var $order = array('no_seri' => 'asc'); // default order
  var $key = 'id_material'; // default order

  /**
   * fungsi untuk validasi form
   */
	public function setRules()
  {
    $form = array(
            array(
                'field' => 'no_seri',
                'label' => 'Nomor Seri',
                'rules' => 'required|max_length[15]'
            ),
            array(
                'field' => 'material',
                'label' => 'Nama Material',
                'rules' => 'required'
            ),
            array(
                'field' => 'type',
                'label' => 'Type',
                'rules' => 'required|max_length[15]'
            ),
            array(
                'field' => 'merk',
                'label' => 'Merk',
                'rules' => 'required|max_length[20]'
            ),
            array(
                'field' => 'tahun',
                'label' => 'Tahun',
                'rules' => 'required|max_length[5]'
            ),
            array(
                'field' => 'arus',
                'label' => 'Arus',
                'rules' => 'required|max_length[4]'
            ),
            // array(
            //     'field' => 'stand',
            //     'label' => 'Stand',
            // ),
            array(
                'field' => 'tarif',
                'label' => 'Tarif',
                'rules' => 'required'
            )
    );
    return $form;
  }

  public function GetDataPostForm()
  {
    $data = array(
        'id_material' => $this->input->post('id_material'),
        'no_seri' => $this->input->post('no_seri'),
        'material' => $this->input->post('material'),
        'type' => $this->input->post('type'),
        'merk' => $this->input->post('merk'),
        'tahun' => $this->input->post('tahun'),
        'arus' => $this->input->post('arus'),
        // 'stand' => $this->input->post('stand'),
        'tarif' => $this->input->post('tarif'),
        // 'daya' => $this->input->post('daya'),
    );
    return $data;
  }
}
/* End of file agen_model.php */
/* Location: ./application/models/absen_model.php */
