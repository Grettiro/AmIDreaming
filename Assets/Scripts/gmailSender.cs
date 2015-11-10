using UnityEngine;
using System.Collections;
using System;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.ComponentModel;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Threading;

public class gmailSender {

	private MailMessage mail = new MailMessage();
	private SmtpClient client = new SmtpClient("smtp.sendgrid.net");	

	private static void SendCompletedCallback(object sender, AsyncCompletedEventArgs e)
	{
		// Get the unique identifier for this asynchronous operation.
		String token = (string) e.UserState;

		if (e.Error != null)
		{
			Debug.Log(token + "\n" + e.Error.ToString());
		}else
		{
			Debug.Log("Message sent.");
		}
	}
	
	public gmailSender()
	{
		mail.From = new MailAddress("amidreaminggame@gmail.com");
		mail.To.Add("amidreaminggame@gmail.com");
		
		client.Port = 587;
		client.Credentials = new System.Net.NetworkCredential("AmIDreaming", "Dreamingofnightm4r3s") as ICredentialsByHost;
		client.EnableSsl = true;
		ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors SslPolicyErrors)
		{ return true; };
		client.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);
	}

	public void sendMail(string nameOfPlayer, string message, bool asynch)
	{
		mail.Subject = nameOfPlayer;
		mail.Body = message;
		mail.Headers.Add("X-SMTPAPI", "{\"category\":\"game_email\"}");

		if(asynch)
		{
			string userState = "Mail token";

			client.SendAsync(mail, userState);
		}
		else
			client.Send(mail);

		mail.Dispose();
	}
}