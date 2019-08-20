<?php
class MY_Exceptions extends CI_Exceptions {
    function show_error($heading, $message, $template = 'error_general', $status_code = 500)
    {
        header('Cache-Control: no-cache, must-revalidate');
        header('Content-type: application/json');
        header("HTTP/1.1 500 Internal Server Error");

        echo json_encode(
            array(
                'status' => $status_code,
                'error' => 'Internal Server Error',
                'message' => $message
            )
        );

        exit;
    }
}
?>