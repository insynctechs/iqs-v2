<?php
session_start();
if(isset($_POST["captcha"])&&$_POST["captcha"]!=""&&$_SESSION["code"]==$_POST["captcha"])
{

}
else
{
die("Wrong Code Entered");
} 
  
if(isset($_POST['E-mail'])) {
	
	    // EDIT THE 2 LINES BELOW AS REQUIRED
    $email_to = "mikem@iqsdirectory.com, jacki@iqsdirectory.com, graphics@iqsdirectory.com";
    $email_subject = "IQS Marketing: Website Health Report Submission.";    
    $email_from = $_POST['E-mail']; // required

    
if(isset($_POST) && (!empty($_POST))){
   foreach ( $_POST as $key => $value ) {
     if(empty($value)){
       $_POST=""; 
     } else{
		$key = str_replace('_', ' ', $key);
    $body .= $key. ": ".htmlspecialchars($value). "\r\n\r\n";
     }

   }
 }
   
// create email headers
$headers = 'From: '.$email_from."\r\n".
'Reply-To: '.$email_from."\r\n" .
'X-Mailer: PHP/' . phpversion();

$ok = mail($email_to, $email_subject, $body, $headers); 
if ($ok) {
header('Location: http://www.iqsdirectory.com/web-marketing/thank-you.html');
} else {
 echo "<p>Mail could not be sent. Sorry!</p>";
	}
}	
 
?>
