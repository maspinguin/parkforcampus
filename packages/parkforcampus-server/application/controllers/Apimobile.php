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
	}

	public function get_test() {
		$method = $_SERVER['REQUEST_METHOD'];
		if($method != 'GET'){
			json_output(400,array('status' => 400,'message' => 'Bad request.'));
		} else {
			$check_auth_client = $this->Api_model->check_auth_client();
			if($check_auth_client == true){
                $response = $this->Api_model->auth();
                if($response['status'] == 200){
                    // $resp = $this->Api_model->pelanggan_all_data();
                    //$resp = array('data' => $this->Api_model->pelanggan_all_data());
                    json_output($response['status'], array('message'=>'hello'));
                }
			}
		}
	}

	public function list_pengguna() {
        $method = $_SERVER['REQUEST_METHOD'];
        if($method != 'POST'){
            json_output(400,array('status' => 400,'message' => 'Bad request.'));
        } else {
            $check_auth_client = $this->Api_model->check_auth_client();
            if($check_auth_client == true){
                $response = $this->Api_model->auth();
                if($response['status'] == 200){
                	$params = json_decode(file_get_contents('php://input'), true);

					//get from input post
	//				$params = $_REQUEST;
					$start = null ;
					$limit = null ;
					$orderBy = null;
					$type = null;
					if(isset($params['tipe'])) {
						$type = $params['tipe'];
					}
                	if(isset($params['start']) && isset($params['limit'])) {
                		$start = $params['start'];
                		$limit = $params['limit'];
                	}

                	if(isset($params['orderBy'])) {
                		$orderBy = $params['orderBy'];
					}
                    $resp = array('data' => $this->Api_model->list_pengguna($type, $start, $limit, $orderBy));
                    json_output($response['status'], $resp);
                }
            }
        }
	}

    public function list_pegawai() {
        $method = $_SERVER['REQUEST_METHOD'];
        if($method != 'POST'){
            json_output(400,array('status' => 400,'message' => 'Bad request.'));
        } else {
            $check_auth_client = $this->Api_model->check_auth_client();
            if($check_auth_client == true){
                $response = $this->Api_model->auth();
                if($response['status'] == 200){
                    $params = json_decode(file_get_contents('php://input'), true);

                    //get from input post
                    //				$params = $_REQUEST;
                    $start = null ;
                    $limit = null ;
                    $orderBy = null;
                    if(isset($params['start']) && isset($params['limit'])) {
                        $start = $params['start'];
                        $limit = $params['limit'];
                    }

                    if(isset($params['orderBy'])) {
                        $orderBy = $params['orderBy'];
                    }
                    $resp = array('data' => $this->Api_model->list_pegawai($start, $limit, $orderBy));
                    json_output($response['status'], $resp);
                }
            }
        }
    }

    public function list_mahasiswa() {
        $method = $_SERVER['REQUEST_METHOD'];
        if($method != 'POST'){
            json_output(400,array('status' => 400,'message' => 'Bad request.'));
        } else {
            $check_auth_client = $this->Api_model->check_auth_client();
            if($check_auth_client == true){
                $response = $this->Api_model->auth();
                if($response['status'] == 200){
                    $params = json_decode(file_get_contents('php://input'), true);

                    //get from input post
                    //				$params = $_REQUEST;
                    $start = null ;
                    $limit = null ;
                    $orderBy = null;
                    if(isset($params['start']) && isset($params['limit'])) {
                        $start = $params['start'];
                        $limit = $params['limit'];
                    }

                    if(isset($params['orderBy'])) {
                        $orderBy = $params['orderBy'];
                    }
                    $resp = array('data' => $this->Api_model->list_mahasiswa($start, $limit, $orderBy));
                    json_output($response['status'], $resp);
                }
            }
        }
    }

	public function list_type_pengguna() {
        $method = $_SERVER['REQUEST_METHOD'];
        if($method != 'GET'){
            json_output(400,array('status' => 400,'message' => 'Bad request.'));
        } else {
            $check_auth_client = $this->Api_model->check_auth_client();
            if($check_auth_client == true){
                $response = $this->Api_model->auth();
                if($response['status'] == 200){
                    $resp = array('data' => $this->Api_model->list_type_pengguna());
                    json_output($response['status'], $resp);
                }
            }
        }
	}

	public function insert_mahasiswa() {
        $method = $_SERVER['REQUEST_METHOD'];
        if($method != 'POST'){
            json_output(400,array('status' => 400,'message' => 'Bad request.'));
        } else {
            $check_auth_client = $this->Api_model->check_auth_client();
            if($check_auth_client == true){
                $response = $this->Api_model->auth();
                if($response['status'] == 200){
                    $params = json_decode(file_get_contents('php://input'), true);

                    $nim = null ;
                    $nama = null ;
                    $alamat = null;
                    $email = null;
                    if(!isset($params['nim']) || $params['nim'] == "") {
                        return json_output(203,array('status' =>203 , 'message'=> 'nim tidak ada' ));
                    } else {
                        $nim = $params['nim'];
                    }

                    if(!isset($params['nama']) || $params['nama'] == "") {
                        return json_output(203,array('status' =>203 , 'message'=> 'nama tidak ada' ));
                    } else {
                        $nama = $params['nama'];
                    }

                    if(!isset($params['email']) || $params['email'] == "") {
                        return json_output(203,array('status' =>203 , 'message'=> 'email tidak ada' ));
                    } else {
                        $email = $params['email'];
                    }

                    if(!isset($params['alamat']) || $params['alamat'] == "") {
                        return json_output(203,array('status' =>203 , 'message'=> 'alamat tidak ada' ));
                    } else {
                        $alamat = $params['alamat'];
                    }

                    $newData = array(
                        'nim' => $nim,
                        'nama' => $nama,
                        'alamat' => $alamat,
                        'email' => $email,
                        'status_id' => 1
                    );

                    $resp = array('data' => $this->Api_model->insert_mahasiswa($newData));
                    json_output($response['status'], $resp);
                }
            }
        }
	}

    public function update_mahasiswa() {
        $method = $_SERVER['REQUEST_METHOD'];
        if($method != 'PUT'){
            json_output(400,array('status' => 400,'message' => 'Bad request.'));
        } else {
            $check_auth_client = $this->Api_model->check_auth_client();
            if($check_auth_client == true){
                $response = $this->Api_model->auth();
                if($response['status'] == 200){
                    $params = json_decode(file_get_contents('php://input'), true);

                    $nim = null ;
                    $nama = null ;
                    $alamat = null;
                    $email = null;
                    if(!isset($params['nim']) || $params['nim'] == "") {
                        return json_output(203,array('status' =>203 , 'message'=> 'nim tidak ada' ));
                    } else {
                        $nim = $params['nim'];
                    }

                    if(!isset($params['nama']) || $params['nama'] == "") {
                        return json_output(203,array('status' =>203 , 'message'=> 'nama tidak ada' ));
                    } else {
                        $nama = $params['nama'];
                    }

                    if(!isset($params['email']) || $params['email'] == "") {
                        return json_output(203,array('status' =>203 , 'message'=> 'email tidak ada' ));
                    } else {
                        $email = $params['email'];
                    }

                    if(!isset($params['alamat']) || $params['alamat'] == "") {
                        return json_output(203,array('status' =>203 , 'message'=> 'alamat tidak ada' ));
                    } else {
                        $alamat = $params['alamat'];
                    }

                    $newData = array(
                        'nim' => $nim,
                        'nama' => $nama,
                        'alamat' => $alamat,
                        'email' => $email
                    );

                    $resp = array('data' => $this->Api_model->update_mahasiswa($newData));
                    json_output($response['status'], $resp);
                }
            }
        }
    }

    public function delete_mahasiswa()
    {
        $method = $_SERVER['REQUEST_METHOD'];
        if ($method != 'DELETE') {
            json_output(400, array('status' => 400, 'message' => 'Bad request.'));
        } else {
            $check_auth_client = $this->Api_model->check_auth_client();
            if($check_auth_client == true) {
                $response = $this->Api_model->auth();
                if ($response['status'] == 200) {
                    $params = json_decode(file_get_contents('php://input'), true);

                    $nim = null ;
                    if(!isset($params['nim']) || $params['nim'] == "") {
                        return json_output(203,array('status' =>203 , 'message'=> 'nim tidak ada' ));
                    } else {
                        $nim = $params['nim'];
                    }

                    $newData = array(
                        'nim' => $nim,
                        'status_id' => 0
                    );

                    $resp = array('data' => $this->Api_model->delete_mahasiswa($newData));
                    json_output($response['status'], $resp);
                }
            }
        }
    }


    public function insert_pegawai() {
        $method = $_SERVER['REQUEST_METHOD'];
        if($method != 'POST'){
            json_output(400,array('status' => 400,'message' => 'Bad request.'));
        } else {
            $check_auth_client = $this->Api_model->check_auth_client();
            if($check_auth_client == true){
                $response = $this->Api_model->auth();
                if($response['status'] == 200){
                    $params = json_decode(file_get_contents('php://input'), true);

                    $nip = null ;
                    $nama = null ;
                    $alamat = null;
                    $email = null;
                    if(!isset($params['nip']) || $params['nip'] == "") {
                        return json_output(203,array('status' =>203 , 'message'=> 'nip tidak ada' ));
                    } else {
                        $nip = $params['nip'];
                    }

                    if(!isset($params['nama']) || $params['nama'] == "") {
                        return json_output(203,array('status' =>203 , 'message'=> 'nama tidak ada' ));
                    } else {
                        $nama = $params['nama'];
                    }

                    if(!isset($params['email']) || $params['email'] == "") {
                        return json_output(203,array('status' =>203 , 'message'=> 'email tidak ada' ));
                    } else {
                        $email = $params['email'];
                    }

                    if(!isset($params['alamat']) || $params['alamat'] == "") {
                        return json_output(203,array('status' =>203 , 'message'=> 'alamat tidak ada' ));
                    } else {
                        $alamat = $params['alamat'];
                    }

                    $newData = array(
                        'nip' => $nip,
                        'nama' => $nama,
                        'alamat' => $alamat,
                        'email' => $email,
                        'status_id' => 1
                    );

                    $resp = array('data' => $this->Api_model->insert_pegawai($newData));
                    json_output($response['status'], $resp);
                }
            }
        }
    }

    public function update_pegawai() {
        $method = $_SERVER['REQUEST_METHOD'];
        if($method != 'PUT'){
            json_output(400,array('status' => 400,'message' => 'Bad request.'));
        } else {
            $check_auth_client = $this->Api_model->check_auth_client();
            if($check_auth_client == true){
                $response = $this->Api_model->auth();
                if($response['status'] == 200){
                    $params = json_decode(file_get_contents('php://input'), true);

                    $nip = null ;
                    $nama = null ;
                    $alamat = null;
                    $email = null;
                    if(!isset($params['nip']) || $params['nip'] == "") {
                        return json_output(203,array('status' =>203 , 'message'=> 'nip tidak ada' ));
                    } else {
                        $nip = $params['nip'];
                    }

                    if(!isset($params['nama']) || $params['nama'] == "") {
                        return json_output(203,array('status' =>203 , 'message'=> 'nama tidak ada' ));
                    } else {
                        $nama = $params['nama'];
                    }

                    if(!isset($params['email']) || $params['email'] == "") {
                        return json_output(203,array('status' =>203 , 'message'=> 'email tidak ada' ));
                    } else {
                        $email = $params['email'];
                    }

                    if(!isset($params['alamat']) || $params['alamat'] == "") {
                        return json_output(203,array('status' =>203 , 'message'=> 'alamat tidak ada' ));
                    } else {
                        $alamat = $params['alamat'];
                    }

                    $newData = array(
                        'nip' => $nip,
                        'nama' => $nama,
                        'alamat' => $alamat,
                        'email' => $email
                    );

                    $resp = array('data' => $this->Api_model->update_pegawai($newData));
                    json_output($response['status'], $resp);
                }
            }
        }
    }

    public function delete_pegawai()
    {
        $method = $_SERVER['REQUEST_METHOD'];
        if ($method != 'DELETE') {
            json_output(400, array('status' => 400, 'message' => 'Bad request.'));
        } else {
            $check_auth_client = $this->Api_model->check_auth_client();
            if($check_auth_client == true) {
                $response = $this->Api_model->auth();
                if ($response['status'] == 200) {
                    $params = json_decode(file_get_contents('php://input'), true);

                    $nim = null ;
                    if(!isset($params['nip']) || $params['nip'] == "") {
                        return json_output(203,array('status' =>203 , 'message'=> 'nim tidak ada' ));
                    } else {
                        $nip = $params['nip'];
                    }

                    $newData = array(
                        'nip' => $nip,
                        'status_id' => 0
                    );

                    $resp = array('data' => $this->Api_model->delete_pegawai($newData));
                    json_output($response['status'], $resp);
                }
            }
        }
    }


//  public function insert_pemeriksaan()
//  {
//    $method = $_SERVER['REQUEST_METHOD'];
//    if($method != 'POST' ){
//  	json_output(400,array('status' => 400,'message' => 'Bad request.'));
//    }
//    else {
//  	$check_auth_client = $this->Api_model->check_auth_client();
//  	if($check_auth_client == true){
//  	  $response = $this->Api_model->auth();
//  	  $respStatus = $response['status'];
//  	  if($response['status'] == 200){
//  		//$params = json_decode(file_get_contents('php://input'), TRUE);
//		$params = $_REQUEST;
//  		if(!isset($params['id_pemakaian'])||$params['id_pemakaian'] == ''){
//			return json_output(203,array('status' =>203 , 'message'=> 'id_pemakaian tidak ada' ));
//		}
//
//		if(!isset($params['tanggal'])||$params['tanggal'] == ''){
//			return json_output(203,array('status' =>203 , 'message'=> 'tanggal tidak ada' ));
//		}
//
//		// if($params['id_material'] == ''){
//		// 	return json_output(203,array('status' =>203 , 'message'=> 'id_material tidak ada' ));
//		// }
//
//		if(!isset($params['lattitude'])||$params['lattitude'] == ''){
//			return json_output(203,array('status' =>203 , 'message'=> 'latittude tidak ada' ));
//		}
//
//		if(!isset($params['longitude'])||$params['longitude'] == ''){
//			return json_output(203,array('status' =>203 , 'message'=> 'longitude tidak ada' ));
//		}
//
//		if(!isset($params['alasan'])||$params['alasan'] == ''){
//			return json_output(203,array('status' =>203 , 'message'=> 'alasan tidak ada' ));
//		}
//
//		if(!isset($params['stand'])||$params['stand'] == ''){
//			return json_output(203,array('status' =>203 , 'message'=> 'Stand tidak ada' ));
//		}
//
//		if(!isset($params['petugas'])||$params['petugas'] == ''){
//			return json_output(203,array('status' =>203 , 'message'=> 'petugas tidak ada' ));
//		}
//		if(!isset($_FILES['foto'])) {
//			return json_output(203,array('status' =>203 , 'message'=> 'foto tidak ada' ));
//		}
//
//		$params['statusid'] = '0';
//  		// if ($params['nama_pel'] == "" || $params['nama_penghuni'] == ""|| $params['alamat'] == "" || $params['tarif'] == "") {
//  		//   $respStatus = 400;
//  		//   $resp = array('status' => 400,'message' =>  'Error, some field cant be empty');
//  		// } else {
//  		//   $resp = $this->MyModel->pelanggan_update_data($id,$params);
//  		// }
//  		// json_output($respStatus,$resp);
//		$resp = $this->Api_model->pemeriksaan_create_data($params);
//  		json_output($respStatus,$resp);
//  	  }
//  	}
//    }
//  }

//  }

}
?>
