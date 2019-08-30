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
                    json_output($resp['data']['status'], array('message'=>'hello'));
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
                    $search = null;
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

					if(isset($params['search'])) {
					    $search = $params['search'];
                    }
                    $resp = array('data' => $this->Api_model->list_pengguna($type, $start, $limit, $orderBy, $search));
                    json_output($resp['data']['status'], $resp);
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
                    $search = null;
                    if(isset($params['start']) && isset($params['limit'])) {
                        $start = $params['start'];
                        $limit = $params['limit'];
                    }

                    if(isset($params['orderBy'])) {
                        $orderBy = $params['orderBy'];
                    }
                    if(isset($params['search'])) {
                        $search = $params['search'];
                    }
                    $resp = array('data' => $this->Api_model->list_pegawai($start, $limit, $orderBy, $search));
                    json_output($resp['data']['status'], $resp);
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
                    $search = null;
                    if(isset($params['start']) && isset($params['limit'])) {
                        $start = $params['start'];
                        $limit = $params['limit'];
                    }

                    if(isset($params['orderBy'])) {
                        $orderBy = $params['orderBy'];
                    }

                    if(isset($params['search'])) {
                        $search = $params['search'];
                    }
                    $resp = array('data' => $this->Api_model->list_mahasiswa($start, $limit, $orderBy, $search));
                    json_output($resp['data']['status'], $resp);
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
                    json_output($resp['data']['status'], $resp);
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
                $response2 = $this->Api_model->authAdmin();
                if($response['status'] == 200 && $response2['status'] == 200){
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
                    json_output($resp['data']['status'], $resp);
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
                $response2 = $this->Api_model->authAdmin();
                if($response['status'] == 200 && $response2['status'] == 200){
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
                    json_output($resp['data']['status'], $resp);
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
                $response2 = $this->Api_model->authAdmin();
                if ($response['status'] == 200 && $response2['status'] == 200) {
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
                    json_output($resp['data']['status'], $resp);
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
                $response2 = $this->Api_model->authAdmin();
                if($response['status'] == 200 && $response2['status'] == 200){
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
                    json_output($resp['data']['status'], $resp);
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
                $response2 = $this->Api_model->authAdmin();
                if($response['status'] == 200 && $response2['status'] == 200){
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
                    json_output($resp['data']['status'], $resp);
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
                $response2 = $this->Api_model->authAdmin();
                if ($response['status'] == 200 && $response2['status'] == 200) {
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
                    json_output($resp['data']['status'], $resp);
                }
            }
        }
    }

    public function insert_pengguna() {
        $method = $_SERVER['REQUEST_METHOD'];
        if($method != 'POST'){
            json_output(400,array('status' => 400,'message' => 'Bad request.'));
        } else {
            $check_auth_client = $this->Api_model->check_auth_client();
            if($check_auth_client == true){
                $response = $this->Api_model->auth();
                $response2 = $this->Api_model->authAdmin();
                if($response['status'] == 200 && $response2['status'] == 200){
                    $params = json_decode(file_get_contents('php://input'), true);

                    $nomor_induk = null ;
                    $tipe = null ;
                    $password = null;

                    if(!isset($params['nomor_induk']) || $params['nomor_induk'] == "") {
                        return json_output(203,array('status' =>203 , 'message'=> 'nomor_induk tidak ada' ));
                    } else {
                        $nomor_induk = $params['nomor_induk'];
                    }

                    if(!isset($params['tipe']) || $params['tipe'] == "") {
                        return json_output(203,array('status' =>203 , 'message'=> 'tipe tidak ada' ));
                    } else {
                        $tipe = $params['tipe'];
                    }

                    if(!isset($params['password']) || $params['password'] == "") {
                        return json_output(203,array('status' =>203 , 'message'=> 'password tidak ada' ));
                    } else {
                        $password = $params['password'];
                    }


                    $newData = array(
                        'nomor_induk' => $nomor_induk,
                        'password' => $password,
                        'tipe' => $tipe
                    );

                    $resp = array('data' => $this->Api_model->insert_pengguna($newData));
                    json_output($resp['data']['status'], $resp);
                }
            }
        }
    }

    public function update_pengguna() {
        $method = $_SERVER['REQUEST_METHOD'];
        if($method != 'PUT'){
            json_output(400,array('status' => 400,'message' => 'Bad request.'));
        } else {
            $check_auth_client = $this->Api_model->check_auth_client();
            if($check_auth_client == true){
                $response = $this->Api_model->auth();
                $response2 = $this->Api_model->authAdmin();
                if($response['status'] == 200 && $response2['status'] == 200){
                    $params = json_decode(file_get_contents('php://input'), true);

                    $nomor_induk = null ;
                    $tipe = null ;
                    $password = null;

                    if(!isset($params['nomor_induk']) || $params['nomor_induk'] == "") {
                        return json_output(203,array('status' =>203 , 'message'=> 'nomor_induk tidak ada' ));
                    } else {
                        $nomor_induk = $params['nomor_induk'];
                    }

                    if(!isset($params['tipe']) || $params['tipe'] == "") {
                        return json_output(203,array('status' =>203 , 'message'=> 'tipe tidak ada' ));
                    } else {
                        $tipe = $params['tipe'];
                    }

                    if(!isset($params['password']) || $params['password'] == "") {
                        return json_output(203,array('status' =>203 , 'message'=> 'password tidak ada' ));
                    } else {
                        $password = $params['password'];
                    }


                    $newData = array(
                        'nomor_induk' => $nomor_induk,
                        'password' => $password,
                        'tipe' => $tipe
                    );

                    $resp = array('data' => $this->Api_model->update_pengguna($newData));
                    json_output($resp['data']['status'], $resp);
                }
            }
        }
    }

    public function delete_pengguna() {
        $method = $_SERVER['REQUEST_METHOD'];
        if($method != 'DELETE'){
            json_output(400,array('status' => 400,'message' => 'Bad request.'));
        } else {
            $check_auth_client = $this->Api_model->check_auth_client();
            if($check_auth_client == true){
                $response = $this->Api_model->auth();
                $response2 = $this->Api_model->authAdmin();
                if($response['status'] == 200 && $response2['status'] == 200){
                    $params = json_decode(file_get_contents('php://input'), true);

                    $nomor_induk = null ;

                    if(!isset($params['nomor_induk']) || $params['nomor_induk'] == "") {
                        return json_output(203,array('status' =>203 , 'message'=> 'nomor_induk tidak ada' ));
                    } else {
                        $nomor_induk = $params['nomor_induk'];
                    }

                    if(!isset($params['tipe']) || $params['tipe'] == "") {
                        return json_output(203,array('status' =>203 , 'message'=> 'tipe tidak ada' ));
                    } else {
                        $tipe = $params['tipe'];
                    }

                    $newData = array(
                        'nomor_induk' => $nomor_induk,
                        'tipe' => $tipe
                    );

                    $resp = array('data' => $this->Api_model->delete_pengguna($newData));
                    json_output($resp['data']['status'], $resp);
                }
            }
        }
    }

    public function check_nomor_induk($nomor_induk = null) {
        $method = $_SERVER['REQUEST_METHOD'];
        if($method != 'GET'){
            json_output(400,array('status' => 400,'message' => 'Bad request.'));
        } else {
            $check_auth_client = $this->Api_model->check_auth_client();
            if($check_auth_client == true){
                $response = $this->Api_model->auth();
                if($response['status'] == 200){

                    if($nomor_induk == null) {
                        return json_output(203,array('status' =>203 , 'message'=> 'nomor_induk tidak ada' ));
                    }

                    $resp = array('data' => $this->Api_model->check_nomor_induk($nomor_induk));
                    json_output($resp['data']['status'], $resp);
                }
            }
        }
    }

    public function proses_parkir() {
        $method = $_SERVER['REQUEST_METHOD'];
        if($method != 'POST'){
            json_output(400,array('status' => 400,'message' => 'Bad request.'));
        } else {
            $check_auth_client = $this->Api_model->check_auth_client();
            if($check_auth_client == true){
                $response = $this->Api_model->auth();
                $response2 = $this->Api_model->authAdmin();
                if($response['status'] == 200 && $response2['status']== 200){

                    $params = json_decode(file_get_contents('php://input'), true);
                    $nomor_induk = null;
                    $jenis = null;

                    if(!isset($params['nomor_induk']) || $params['nomor_induk'] == "") {
                        return json_output(203,array('status' =>203 , 'message'=> 'nomor_induk tidak ada' ));
                    } else {
                        $nomor_induk = $params['nomor_induk'];
                    }

                    if(!isset($params['jenis']) || $params['jenis'] == "") {
                        return json_output(203,array('status' =>203 , 'message'=> 'jenis tidak ada' ));
                    } else {
                        $jenis = $params['jenis'];
                    }

                    $resp1 = $this->Api_model->check_nomor_induk($nomor_induk);
                    if($resp1['status'] != 200) {
                        return json_output(400, $resp1);
                    }
                    else {
                        $newData = array(
                            'jenis_parkir' => $jenis,
                            'nomor_induk' => $nomor_induk
                        );

                        $resp = array('data' => $this->Api_model->proses_parkir($newData));
                        json_output($resp['data']['status'], $resp);
                    }


                }
            }
        }
    }

		public function list_parkir() {
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
									$search = null;
									$jenis = null;
									$date_start = null;
									$date_end = null;
									if(isset($params['start']) && isset($params['limit'])) {
											$start = $params['start'];
											$limit = $params['limit'];
									}

									if(isset($params['orderBy'])) {
											$orderBy = $params['orderBy'];
									}

									if(isset($params['search'])) {
											$search = $params['search'];
									}

									if(isset($params['jenis'])&& $params['jenis']!="")  {
											$jenis = $params['jenis'];
									}

									if(isset($params['dateStart']) && isset($params['dateEnd'])) {
										$date_start = $params['dateStart'];
										$date_end = $params['dateEnd'];
									}

									$resp = array('data' => $this->Api_model->list_parkir($jenis, $start, $limit, $orderBy, $search, $date_start, $date_end));
									json_output($resp['data']['status'], $resp);
							}
					}
			}
		}

}
?>
