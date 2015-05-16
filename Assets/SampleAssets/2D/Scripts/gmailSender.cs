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
	private SmtpClient client = new SmtpClient("smtp.gmail.com");	

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
		client.Credentials = new System.Net.NetworkCredential("amidreaminggame@gmail.com", "nightmareofdreams") as ICredentialsByHost;
		client.EnableSsl = true;
		ServicePointManager.ServerCertificateValidationCallback = delegate(object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors SslPolicyErrors)
		{ return true; };
	}

	public void sendMail(string nameOfPlayer, string message)
	{
		mail.Subject = nameOfPlayer;
		mail.Body = message;

		client.SendCompleted += new SendCompletedEventHandler(SendCompletedCallback);
		string userState = "Mail token";

		client.SendAsync(mail, userState);

		mail.Dispose();
	}
}