<?php
defined('BASEPATH') OR exit('No direct script access allowed');

class Apimobile extends CI_Controller {

	public function __construct($config='rest')
    {
		header('Access-Control-Allow-Origin: *');
    	header("Access-Control-Allow-Methods: GET, POST, OPTIONS, PUT, DELETE");
    	header("Access-Control-Allow-Headers: Content-Type, Authorization, Content-Length, X-Requested-With");

        parent::__construct();
        $this->load->model('Api_model', 'Api_model', TRUE);
		$this->load->helper(array('form', 'url'));
        /*
        $check_auth_client = $this->MyModel->check_auth_client();
		if($check_auth_client != true){
			die($this->output->get_output());
		}
		*/
    }

	public function index()
	{
		// $method = $_SERVER['REQUEST_METHOD'];
		// if($method != 'GET'){
		// 	json_output(400,array('status' => 400,'message' => 'Bad request.'));
		// } else {
		// 	$check_auth_client = $this->Api_model->check_auth_client();
		// 	if($check_auth_client == true){
		//         $response = $this->Api_model->auth();
		//         if($response['status'] == 200){
		//         	$resp = $this->Api_model->pelanggan_all_data();
	  //   			json_output($response['status'],$resp);
		//         }
		// 	}
		// }
	}

  public function get_all_pelanggan()
  {
    // code...
    $method = $_SERVER['REQUEST_METHOD'];
    if($method != 'GET'){
    	json_output(400,array('status' => 400,'message' => 'Bad request.'));
    } else {
    	$check_auth_client = $this->Api_model->check_auth_client();
    	if($check_auth_client == true){
            $response = $this->Api_model->auth();
            if($response['status'] == 200){
            	// $resp = $this->Api_model->pelanggan_all_data();
				$resp = array('data' => $this->Api_model->pelanggan_all_data());
      			json_output($response['status'],$resp);
            }
    	}
    }
  }

  public function get_all_material()
  {
    // code...
    $method = $_SERVER['REQUEST_METHOD'];
    if($method != 'GET'){
      json_output(400,array('status' => 400,'message' => 'Bad request.'));
    } else {
      $check_auth_client = $this->Api_model->check_auth_client();
      if($check_auth_client == true){
            $response = $this->Api_model->auth();
            if($response['status'] == 200){
              	// $resp = $this->Api_model->material_all_data();
				$resp = array('data' => $this->Api_model->material_all_data());
            	json_output($response['status'],$resp);
            }
      }
    }
  }

  public function get_all_pemakaian()
  {
    // code...
    $method = $_SERVER['REQUEST_METHOD'];
    if($method != 'GET'){
      json_output(400,array('status' => 400,'message' => 'Bad request.'));
    } else {
      $check_auth_client = $this->Api_model->check_auth_client();
      if($check_auth_client == true){
            $response = $this->Api_model->auth();
            if($response['status'] == 200){
              	// $resp = $this->Api_model->pemakaian_all_data();
				$resp = array('data' => $this->Api_model->pemakaian_all_data());
            	json_output($response['status'],$resp);
            }
      }
    }
  }

  public function get_all_pemeriksaan()
  {
    // code...
    $method = $_SERVER['REQUEST_METHOD'];
    if($method != 'GET'){
      json_output(400,array('status' => 400,'message' => 'Bad request.'));
    } else {
      $check_auth_client = $this->Api_model->check_auth_client();
      if($check_auth_client == true){
            $response = $this->Api_model->auth();
            if($response['status'] == 200){
              	//$resp = $this->Api_model->pemeriksaan_all_data();
			  	$resp = array('data' => $this->Api_model->pemeriksaan_all_data());
            	json_output($response['status'],$resp);
            }
      }
    }
  }

  public function get_all_penyegelan()
  {
    // code...
    $method = $_SERVER['REQUEST_METHOD'];
    if($method != 'GET'){
      json_output(400,array('status' => 400,'message' => 'Bad request.'));
    } else {
      $check_auth_client = $this->Api_model->check_auth_client();
      if($check_auth_client == true){
            $response = $this->Api_model->auth();
            if($response['status'] == 200){
              	// $resp = $this->Api_model->penyegelan_all_data();
				$resp = array('data' => $this->Api_model->penyegelan_all_data());
            	json_output($response['status'],$resp);
            }
      }
    }
  }

	public function get_detail_pelanggan($id='')
	{
		$method = $_SERVER['REQUEST_METHOD'];
		if($method != 'GET' || $this->uri->segment(5) == '' || is_numeric($this->uri->segment(5)) == FALSE){
			json_output(400,array('status' => 400,'message' => 'Bad request.'));
		} else {
			$check_auth_client = $this->Api_model->check_auth_client();
			if($check_auth_client == true){
		        $response = $this->Api_model->auth();
		        if($response['status'] == 200){
		        	// $resp = $this->Api_model->pelanggan_detail_data($id);
					$resp = array('data' => $this->Api_model->pelanggan_detail_data($id));
					json_output($response['status'],$resp);
		        }
			}
		}
	}

  public function get_detail_material($id='')
	{
		$method = $_SERVER['REQUEST_METHOD'];
		if($method != 'GET' || $this->uri->segment(5) == '' || is_numeric($this->uri->segment(5)) == FALSE){
			json_output(400,array('status' => 400,'message' => 'Bad request.'));
		} else {
			$check_auth_client = $this->Api_model->check_auth_client();
			if($check_auth_client == true){
		        $response = $this->Api_model->auth();
		        if($response['status'] == 200){
		        	// $resp = $this->Api_model->material_detail_data($id);
					$resp = array('data' => $this->Api_model->material_detail_data($id));
					json_output($response['status'],$resp);
		        }
			}
		}
	}

  public function get_detail_pemakaian($id='')
	{
		$method = $_SERVER['REQUEST_METHOD'];
		if($method != 'GET' || $this->uri->segment(5) == '' || is_numeric($this->uri->segment(5)) == FALSE){
			json_output(400,array('status' => 400,'message' => 'Bad request.'));
		} else {
			$check_auth_client = $this->Api_model->check_auth_client();
			if($check_auth_client == true){
		        $response = $this->Api_model->auth();
		        if($response['status'] == 200){
		        	// $resp = $this->Api_model->pemakaian_detail_data($id);
					$resp = array('data' => $this->Api_model->pemeriksaan_detail_data($id));
					json_output($response['status'],$resp);
		        }
			}
		}
	}

  public function get_detail_pemeriksaan($id='')
	{
		$method = $_SERVER['REQUEST_METHOD'];
		if($method != 'GET' || $this->uri->segment(5) == '' || is_numeric($this->uri->segment(5)) == FALSE){
			json_output(400,array('status' => 400,'message' => 'Bad request.'));
		} else {
			$check_auth_client = $this->Api_model->check_auth_client();
			if($check_auth_client == true){
		        $response = $this->Api_model->auth();
		        if($response['status'] == 200){
		        	//$resp = $this->Api_model->pemeriksaan_detail_data($id);
					$resp = array('data' => $this->Api_model->pemeriksaan_detail_data($id));
					json_output($response['status'],$resp);
		        }
			}
		}
	}

  public function get_detail_penyegelan($id='')
	{
		$method = $_SERVER['REQUEST_METHOD'];
		if($method != 'GET' || $this->uri->segment(5) == '' || is_numeric($this->uri->segment(5)) == FALSE){
			json_output(400,array('status' => 400,'message' => 'Bad request.'));
		} else {
			$check_auth_client = $this->Api_model->check_auth_client();
			if($check_auth_client == true){
		        $response = $this->Api_model->auth();
		        if($response['status'] == 200){
		        	// $resp = $this->Api_model->penyegelan_detail_data($id);
					$resp = array('data' => $this->Api_model->penyegelan_detail_data($id));
					json_output($response['status'],$resp);
		        }
			}
		}
	}


  public function update_pemeriksaan($id='')
  {
    $method = $_SERVER['REQUEST_METHOD'];
    if($method != 'POST' || $this->uri->segment(5) == '' || is_numeric($this->uri->segment(5)) == FALSE){
      json_output(400,array('status' => 400,'message' => 'Bad request.'));
    }
    else {
      $check_auth_client = $this->Api_model->check_auth_client();
      if($check_auth_client == true){
        $response = $this->Api_model->auth();
        $respStatus = $response['status'];
        if($response['status'] == 200){
          $params = json_decode(file_get_contents('php://input'), TRUE);
          $params['updated_at'] = date('Y-m-d H:i:s');
          // if ($params['nama_pel'] == "" || $params['nama_penghuni'] == ""|| $params['alamat'] == "" || $params['tarif'] == "") {
          //   $respStatus = 400;
          //   $resp = array('status' => 400,'message' =>  'Error, some field cant be empty');
          // } else {
          //   $resp = $this->MyModel->pelanggan_update_data($id,$params);
          // }
          // json_output($respStatus,$resp);
          json_output($respStatus,$params);
        }
      }
    }
  }

  public function insert_pemeriksaan()
  {
    $method = $_SERVER['REQUEST_METHOD'];
    if($method != 'POST' ){
  	json_output(400,array('status' => 400,'message' => 'Bad request.'));
    }
    else {
  	$check_auth_client = $this->Api_model->check_auth_client();
  	if($check_auth_client == true){
  	  $response = $this->Api_model->auth();
  	  $respStatus = $response['status'];
  	  if($response['status'] == 200){
  		//$params = json_decode(file_get_contents('php://input'), TRUE);
		$params = $_REQUEST;
  		if(!isset($params['id_pemakaian'])||$params['id_pemakaian'] == ''){
			return json_output(203,array('status' =>203 , 'message'=> 'id_pemakaian tidak ada' ));
		}

		if(!isset($params['tanggal'])||$params['tanggal'] == ''){
			return json_output(203,array('status' =>203 , 'message'=> 'tanggal tidak ada' ));
		}

		// if($params['id_material'] == ''){
		// 	return json_output(203,array('status' =>203 , 'message'=> 'id_material tidak ada' ));
		// }

		if(!isset($params['lattitude'])||$params['lattitude'] == ''){
			return json_output(203,array('status' =>203 , 'message'=> 'latittude tidak ada' ));
		}

		if(!isset($params['longitude'])||$params['longitude'] == ''){
			return json_output(203,array('status' =>203 , 'message'=> 'longitude tidak ada' ));
		}

		if(!isset($params['alasan'])||$params['alasan'] == ''){
			return json_output(203,array('status' =>203 , 'message'=> 'alasan tidak ada' ));
		}

		if(!isset($params['stand'])||$params['stand'] == ''){
			return json_output(203,array('status' =>203 , 'message'=> 'Stand tidak ada' ));
		}

		if(!isset($params['petugas'])||$params['petugas'] == ''){
			return json_output(203,array('status' =>203 , 'message'=> 'petugas tidak ada' ));
		}
		if(!isset($_FILES['foto'])) {
			return json_output(203,array('status' =>203 , 'message'=> 'foto tidak ada' ));
		}

		$params['statusid'] = '0';
  		// if ($params['nama_pel'] == "" || $params['nama_penghuni'] == ""|| $params['alamat'] == "" || $params['tarif'] == "") {
  		//   $respStatus = 400;
  		//   $resp = array('status' => 400,'message' =>  'Error, some field cant be empty');
  		// } else {
  		//   $resp = $this->MyModel->pelanggan_update_data($id,$params);
  		// }
  		// json_output($respStatus,$resp);
		$resp = $this->Api_model->pemeriksaan_create_data($params);
  		json_output($respStatus,$resp);
  	  }
  	}
    }
  }

  public function insert_penyegelan()
  {
    $method = $_SERVER['REQUEST_METHOD'];
    if($method != 'POST' ){
  	json_output(400,array('status' => 400,'message' => 'Bad request.'));
    }
    else {
  	$check_auth_client = $this->Api_model->check_auth_client();
  	if($check_auth_client == true){
  	  $response = $this->Api_model->auth();
  	  $respStatus = $response['status'];
  	  if($response['status'] == 200){
  		//$params = json_decode(file_get_contents('php://input'), TRUE);
		$params = $_REQUEST;
  		if($params['no_segel'] == ''){
			return json_output(203,array('status' =>203 , 'message'=> 'No segel tidak ada' ));
		}
		// if($params['id_material'] == ''){
		// 	return json_output(203,array('status' =>203 , 'message'=> 'id_material tidak ada' ));
		// }

		if($params['id_pemakaian'] == ''){
			// return json_output(203,array('status' =>203 , 'message'=> 'latittude tidak ada' ));
			return json_output(203,array('status' =>203 , 'message'=> 'ID Pelanggan tidak ada' ));
		}


		if($params['alasan'] == ''){
			return json_output(203,array('status' =>203 , 'message'=> 'alasan tidak ada' ));
		}

		if($params['tanggal'] == ''){
			return json_output(203,array('status' =>203 , 'message'=> 'tanggal tidak ada' ));
		}

		if($params['petugas'] == ''){
			return json_output(203,array('status' =>203 , 'message'=> 'petugas tidak ada' ));
		}

		// $params['statusid'] = '0';
  		// if ($params['nama_pel'] == "" || $params['nama_penghuni'] == ""|| $params['alamat'] == "" || $params['tarif'] == "") {
  		//   $respStatus = 400;
  		//   $resp = array('status' => 400,'message' =>  'Error, some field cant be empty');
  		// } else {
  		//   $resp = $this->MyModel->pelanggan_update_data($id,$params);
  		// }
  		// json_output($respStatus,$resp);
		$resp = $this->Api_model->penyegelan_create_data($params);
  		json_output($respStatus,$resp);
  	  }
  	}
    }
  }

}
?>
