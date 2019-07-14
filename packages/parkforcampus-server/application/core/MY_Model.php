<?php if (!defined('BASEPATH')) exit('No direct script access allowed');

class  MY_Model extends CI_Model
{
  var $schema = null;
  var $table = null;
  var $sequence = null;
  var $key = null;
  var $value = null;
  var $order = null;
  var $label = null;
  var $is_upload = false;
  var $admin_pasar = null;

  // untuk paging
  var $nav = 3;
  var $limit = 10;
  var $findlimit = 20;

  public function __construct()
  {
    parent::__construct();
    $this->admin_pasar = $this->session->userdata('id_pasar');
  }

  /**
   * Mendapatkan nama tabel, dengan schema-nya jika ada
   * @param string $table
   * @return string
   */
  function getTable($table = null) {
      if (empty($table))
          $table = $this->table;

      return $table;
  }

  /**
   * Mendapatkan kueri list dasar
   * @return string
   */
  function getBasePagerQuery($limit = '') {
    return $this->db->get($this->table);
  }

  /**
   * fungsi untuk validasi form sebelum penyimpanan
   * @return boolean sukses untuk lolos
   */
  function validasi()
  {
    $form = $this->setRules();
    $this->form_validation->set_rules($form);

    if ($this->form_validation->run()) {
      return TRUE;
    }else{
      return FALSE;
    }
  }

  function _get_datatable_from()
  {
    $this->db->from($this->table);
  }

  public function _get_datatables_query()
  {
    
    $this->_get_datatable_from();
    $i = 0;
  
    foreach ($this->column_search as $item) // loop column 
    {
      if(isset($_POST['search']['value'])) // if datatable send POST for search
      {
        
        if($i===0) // first loop
        {
          $this->db->group_start(); // open bracket. query Where with OR clause better with bracket. because maybe can combine with other WHERE with AND.
          $this->db->like($item, $_POST['search']['value']);
        }
        else
        {
          $this->db->or_like($item, $_POST['search']['value']);
        }

        if(count($this->column_search) - 1 == $i) //last loop
          $this->db->group_end(); //close bracket
      }
      $i++;
    }
    
    if(isset($_POST['order'])) // here order processing
    {
      $this->db->order_by($this->column_order[$_POST['order']['0']['column']], $_POST['order']['0']['dir']);
    } 
    else if(isset($this->order))
    {
      $order = $this->order;
      $this->db->order_by(key($order), $order[key($order)]);
    }
  }

  public function get_datatables()
  {
    $this->_get_datatables_query();
    if(isset($_POST['length']) && $_POST['length'] != -1)
    $this->db->limit($_POST['length'], $_POST['start']);
    $query = $this->db->get();
    return $query->result();
  }

  public function count_filtered()
  {
    $this->_get_datatables_query();
    $query = $this->db->get();
    return $query->num_rows();
  }

  public function count_all()
  {
    $this->_get_datatable_from();
    $this->db->get();
    return $this->db->affected_rows();
  }


  /**
   * pencarian data agen dengan id, untuk edit dan detail
   * @param  number $id id agen
   * @return object data agen
   */
  public function cari($id)
  {
      return $this->db->select('*')
          ->where($this->key, $id)
          ->limit(1)
          ->get($this->table)
          ->row();
  }

    /**
   * fungsi tambah data umum, insert data ke database
   * @return boleean true apabila sukses
   */
  public function tambah()
  {
    $data = $this->GetDataPostForm();

    $this->db->insert($this->table, $data);

    if($this->db->affected_rows() > 0)
    {
      return TRUE;
    }
    else
    {
      return FALSE;
    }
  }

  /**
   * fungsi perubahan data umum
   * @param  number $id id data
   * @return boolean
   */
  public function edit($id,$foto = '')
  {
  
    $data = $this->GetDataPostForm();

    $dataedit = array_splice($data, 1);

    $this->db->where($this->key, $id);
    $this->db->update($this->table, $dataedit);
    
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


  /**
   * penghapusan data umum
   * @param  number $id_agen id agen
   * @return boolean
   */
  public function hapus($id)
  {
    $this->before_delete($id);
    $this->db->where($this->key, $id)->delete($this->table);

    if($this->db->affected_rows() > 0)
    {
      return TRUE;
    }
    else
    {
      return FALSE;
    }
  }

  /**
   * gunakan fungsi ini apabila ingin menambahkan data yang ingin diikutkan kedalam view form
   * @return [type] [description]
   */
  function _data_tambahan_form($data)
  {
    return $data;
  }

  public function upload_foto($id='')
  {
    if (!empty($_FILES['foto']['name'])) {

      $this->load->library('upload');
      $this->upload->initialize($this->set_upload_options($id));

      if ($this->upload->do_upload('foto')) 
      {
        $this->_edit_data_upload_foto($id,$this->upload->data('file_name'));
        return TRUE;
      }
      else
      {
        return FALSE;
      }
    }else{
      return TRUE;
    }
  }

  public function _edit_data_upload_foto($id='', $foto='')
  {
    $data = array('foto' => $foto);

    $this->db->where($this->key, $id);
    $this->db->update($this->table, $data);
    
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

  public function set_upload_options($id)
  {
    $config = array();
    return $config;
  }

  /**
   * fungsi yang dijalankan ketika sudah selesai menahmbahkan data ke database
   * @param  [type] $id data last id insert ke database
   * @return [type]     [description]
   */
  public function afterInsert($id)
  {
    // proses dijalankan ketika sudah selesai menahmbahkan data ke database
  }

  /**
   * proses yang dijalankan ketika sistem akan menghapus data
   * @param  [type] $id [description]
   * @return [type]     [description]
   */
  public function before_delete($id)
  {
    # proses dijalankan sebelum penghapusan data
  }

}
/* End of file MY_Model.php */
/* Location: ./application/core/MY_Controller.php */