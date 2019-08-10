<?php
defined('BASEPATH') OR exit('No direct script access allowed');

class Auth extends CI_Controller {
	public function __construct()
    	{
        	parent::__construct();
			$this->load->model('Api_model', 'Api_model', TRUE);
            $this->load->helper(['jwt', 'authorization']);
        }


	public function login()
	{
		$method = $_SERVER['REQUEST_METHOD'];
		if($method != 'POST'){
			json_output(400,array('status' => 400,'message' => 'Bad request.'));
		} else {
			$check_auth_client = $this->Api_model->check_auth_client();
			if($check_auth_client == true){
        	// var_dump( (file_get_contents('php://input')))	;
  				// var_dump($this->input->post('username'));

  			//get from raw data
			$params = json_decode(file_get_contents('php://input'), true);

  			//get from input post
			//$params = $_REQUEST;
			$username = '';
			$password = '';
			if(isset($params['username'])) {
                $username = $params['username'];
            }

            if(isset($params['password'])) {
                $password = $params['password'];
			}

			$response = $this->Api_model->login($username,$password);
				json_output($response['status'],$response);
			}
		}
	}

	public function checkToken() {
		$method = $_SERVER['REQUEST_METHOD'];
		if($method != 'POST'){
			json_output(400,array('status' => 400,'message' => 'Bad request.'));
		} else {
			$check_auth_client = $this->Api_model->check_auth_client();
			if($check_auth_client == true){
				// var_dump( (file_get_contents('php://input')))	;
					// var_dump($this->input->post('username'));

				//get from raw data
				$params = json_decode(file_get_contents('php://input'), true);

				//get from input post
//				$params = $_REQUEST;
				$token = '';
                if(isset($params['token'])) {
                    $token = $params['token'];
                }

				$response = $this->Api_model->checkToken($token);
				json_output($response['status'],$response);
			}
		}
	}


	public function logout()
	{
		$method = $_SERVER['REQUEST_METHOD'];
		if($method != 'POST'){
			json_output(400,array('status' => 400,'message' => 'Bad request.'));
		} else {
			$check_auth_client = $this->Api_model->check_auth_client();
			if($check_auth_client == true){
		        $response = $this->Api_model->logout();
				json_output($response['status'],$response);
			}
		}
	}

}
?>
