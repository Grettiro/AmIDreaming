using UnityEngine;
using System.Collections;
using System;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

public class gmailSender {
	
	public void sendMail(string message)
	{
		MailMessage mail = new MailMessage();
		
		mail.From = new MailAddress("amidreaminggame@gmail.com");
		mail.To.Add("amidreaminggame@gmail.com");
		mail.Subject = "name of player here";
		mail.Body = message;
		
		SmtpClient smtpServer = new SmtpClient("smtp.gmail.com");
		smtpServer.Port = 587;
		smtpServer.Credentials = new System.Net.NetworkCredential("amidreaminggame@gmail.com", "nightmareofdreams") as ICredentialsByHost;
		smtpServer.EnableSsl = true;
		ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors SslPolicyErrors)
		{ return true; };
		smtpServer.Send(mail);
		
	}
}