<?php
defined('BASEPATH') OR exit('No direct script access allowed');

/*
| -------------------------------------------------------------------------
| URI ROUTING
| -------------------------------------------------------------------------
| This file lets you re-map URI requests to specific controller functions.
|
| Typically there is a one-to-one relationship between a URL string
| and its corresponding controller class/method. The segments in a
| URL normally follow this pattern:
|
|	example.com/class/method/id/
|
| In some instances, however, you may want to remap this relationship
| so that a different class/function is called than the one
| corresponding to the URL.
|
| Please see the user guide for complete details:
|
|	https://codeigniter.com/user_guide/general/routing.html
|
| -------------------------------------------------------------------------
| RESERVED ROUTES
| -------------------------------------------------------------------------
|
| There are three reserved routes:
|
|	$route['default_controller'] = 'welcome';
|
| This route indicates which controller class should be loaded if the
| URI contains no data. In the above example, the "welcome" class
| would be loaded.
|
|	$route['404_override'] = 'errors/page_missing';
|
| This route will tell the Router which controller/method to use if those
| provided in the URL cannot be matched to a valid route.
|
|	$route['translate_uri_dashes'] = FALSE;
|
| This is not exactly a route, but allows you to automatically route
| controller and method names that contain dashes. '-' isn't a valid
| class or method name character, so it requires translation.
| When you set this option to TRUE, it will replace ALL dashes in the
| controller and method URI segments.
|
| Examples:	my-controller/index	-> my_controller/index
|		my-controller/my-method	-> my_controller/my_method
*/

//true endpoint use standart v1
$route['api/v1/auth/login']['post']           = 'auth/login';
$route['api/v1/auth/logout']['post']          = 'auth/logout';
$route['api/v1/auth/checkToken']['post']      = 'auth/checkToken';
$route['api/v1/map/lists']['get']             = '';

$route['api/v1/Apimobile/pelanggan']['get']          = 'Apimobile/get_all_pelanggan';
$route['api/v1/Apimobile/pelanggan/(:num)']['get']   = 'Apimobile/get_detail_pelanggan/$1';

$route['api/v1/Apimobile/material']['get']          = 'Apimobile/get_all_material';
$route['api/v1/Apimobile/material/(:num)']['get']   = 'Apimobile/get_detail_material/$1';

$route['api/v1/Apimobile/pemakaian']['get']          = 'Apimobile/get_all_pemakaian';
$route['api/v1/Apimobile/pemakaian/(:num)']['get']   = 'Apimobile/get_detail_pemakaian/$1';

$route['api/v1/Apimobile/pemeriksaan']['get']          = 'Apimobile/get_all_pemeriksaan';
$route['api/v1/Apimobile/pemeriksaan/(:num)']['get']   = 'Apimobile/get_detail_pemeriksaan/$1';

$route['api/v1/Apimobile/penyegelan']['get']          = 'Apimobile/get_all_penyegelan';
$route['api/v1/Apimobile/penyegelan/(:num)']['get']   = 'Apimobile/get_detail_penyegelan/$1';

$route['api/v1/Apimobile/update_pemeriksaan/(:num)']['post']    = 'Apimobile/update_pemeriksaan/$1';

$route['api/v1/Apimobile/insert_pemeriksaan']['post']  = 'Apimobile/insert_pemeriksaan'; 
//redirect wrong endpoint
$route['auth/(:any)'] = 'Wrong';


//default configuration
$route['default_controller'] = 'Home';
$route['404_override'] = 'Wrong';
$route['translate_uri_dashes'] = FALSE;
