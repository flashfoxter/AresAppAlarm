using System;
using System.Drawing;

using MonoTouch.Foundation;
using MonoTouch.UIKit;
using System.Net.Mail;
using System.Text;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using MonoTouch.CoreLocation;

namespace AresAppAlarm
{
	public partial class AresAppAlarmViewController : UIViewController
	{

		private CLLocationManager locationManager;


		public AresAppAlarmViewController () : base ("AresAppAlarmViewController", null)
		{
		

		
		}




		void Initialize ()
		{
			locationManager = new CLLocationManager();
			locationManager.DistanceFilter = 10;
			locationManager.HeadingFilter = 3;

			locationManager.UpdatedLocation += (object sender
			                                                , CLLocationUpdatedEventArgs e) => {
				Console.WriteLine(e.NewLocation.Altitude.ToString () + "Meters");
			};



//			this._mainScreen.LblLongitude.Text = e.NewLocation.Coordinate.Longitude
//				.ToString () + "o";
//			this._mainScreen.LblLatitude.Text = e.NewLocation.Coordinate.Latitude
//				.ToString () + "o";
//			this._mainScreen.LblCourse.Text = e.NewLocation.Course.ToString () + "o"; this._mainScreen.LblSpeed.Text = e.NewLocation.Speed.ToString () + "meters/s"; this._mainScreen.LblDistanceToParis.Text = (e.NewLocation.DistanceFrom(
//				new CLLocation(48.857, 2.351)) / 1000).ToString() + "km";
//
			//throw new NotImplementedException ();

			btnRoundSos.TouchUpInside += btnRoundSosTouch;
		}


		void AsyncSendEmail() {
		

				System.Net.Mail.MailMessage mailMessage = new System.Net.Mail.MailMessage();
				
				mailMessage.To.Add(new MailAddress("toSomeone@gmail.com"));
				mailMessage.From = new MailAddress("fromMe@hotmail.com");
				
				mailMessage.Subject = "my test subject";
				mailMessage.Body = "my test body";
				mailMessage.IsBodyHtml = true;
				
				SmtpClient smtpClient = new SmtpClient();
				smtpClient.EnableSsl = true;
				
				object userState = mailMessage;
				
				smtpClient.SendCompleted += new SendCompletedEventHandler(smtpClient_SendCompleted);
				smtpClient.SendAsync(mailMessage, userState);

		}



		void smtpClient_SendCompleted(object sender, System.ComponentModel.AsyncCompletedEventArgs e)
		{
			MailMessage mailMessage = default(MailMessage);
			
			mailMessage = (MailMessage)e.UserState;
			
			if ((e.Cancelled))
			{
				//lblMessage.Text = "Sending of email message was cancelled. Address=" + mailMessage.To[0].Address;
			}
			if ((e.Error != null))
			{
				//lblMessage.Text = "Error occured, info :" + e.Error.Message;
			}
			else
			{
				//lblMessage.Text = "Mail sent successfully";
			}
		}



		void SyncSendEmail ()
		{
			//throw new NotImplementedException ();

		// ОБРАЗЕЦ ОТПРАВКИ ГИПЕР ССЫЛКИ 
		// https://maps.google.com/maps?z=50&t=k&q=59.880905  30.438244

			//ПОЛУЧАЕМ СВОИ КООРДИНАТЫ




			try {
				MailMessage mm = new MailMessage (); 
				mm.To.Add (new MailAddress ("areshelpapp@gmail.com")); 
				mm.From = new MailAddress ("areshelpapp@gmail.com"); 
				
				mm.Subject = "Ares Sos"; 
				
				//int summ = StaticHolder.likeVotes + StaticHolder.unlikeVotes;
				
				//				if (textFieldReport.Text != ""){
				//					
				//					
				//					mm.Body = textFieldReport.Text + " | Like: " +  StaticHolder.likeVotes.ToString() + " Unlike: " + StaticHolder.unlikeVotes.ToString() + " ALL: " + summ.ToString();
				//				}
				//				else mm.Body = "NO COMMENTS" +  StaticHolder.likeVotes.ToString() + " Unlike: " + StaticHolder.unlikeVotes.ToString() + " ALL: " + summ.ToString();;
				
				
				mm.Body = "ALARM";
				
				mm.BodyEncoding = UTF8Encoding.UTF8; 
				mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure; 
				NetworkCredential _Credential = new NetworkCredential ("areshelpapp", "123qweASD)_+"); 

				SmtpClient ss = new SmtpClient ("smtp.gmail.com"); 
				ss.UseDefaultCredentials = false; 
				ss.EnableSsl = true; 
				ss.Credentials = _Credential; 
				ss.Port = 587; 
				
				ss.SendCompleted += HandleSsSendCompleted; 
				
				ServicePointManager.ServerCertificateValidationCallback = 
				delegate(object s, X509Certificate certificate, X509Chain chant, SslPolicyErrors error) { 
					return true; 
				}; 
				

				object userState = mm;
				//ss.Send (mm);
				ss.SendAsync(mm, userState);
					//this.NavigationController.PopToRootViewController(true);
			}		
				catch (Exception exm) {
					
					Console.WriteLine("Exception: " +  exm.Message);
				}
		}
			





		/// <summary>
		/// Отправить СОС
		/// </summary>
		/// <param name="sender">Sender.</param>
		/// <param name="e">E.</param>
		void btnRoundSosTouch (object sender, EventArgs e)
		{
			SyncSendEmail();
			//AsyncSendEmail();

		}


		void HandleSsSendCompleted (object sender, System.ComponentModel.AsyncCompletedEventArgs e)
		{
			
			
			//throw new NotImplementedException ();
			Console.WriteLine("Message Sended: " + e.Error + " " + DateTime.Now);
			
		}


		public override void DidReceiveMemoryWarning ()
		{
			// Releases the view if it doesn't have a superview.
			base.DidReceiveMemoryWarning ();
			
			// Release any cached data, images, etc that aren't in use.
		}
		
		public override void ViewDidLoad ()
		{
			base.ViewDidLoad ();

			Initialize();
			// Perform any additional setup after loading the view, typically from a nib.
		}
		
		public override void ViewDidUnload ()
		{
			base.ViewDidUnload ();
			
			// Clear any references to subviews of the main view in order to
			// allow the Garbage Collector to collect them sooner.
			//
			// e.g. myOutlet.Dispose (); myOutlet = null;
			
			ReleaseDesignerOutlets ();
		}
		
		public override bool ShouldAutorotateToInterfaceOrientation (UIInterfaceOrientation toInterfaceOrientation)
		{
			// Return true for supported orientations
			return (toInterfaceOrientation != UIInterfaceOrientation.PortraitUpsideDown);
		}
	}
}

