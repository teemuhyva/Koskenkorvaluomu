using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace LuomuTila.Models {
    public class AppointmentForm {
        
        public string Name {
            get; set;
        }

        public string Phone {
            get; set;
        }
        
        public DateTime ShowingDate {
            get; set;
        }

        public string Email { get; set; }

        public string Description { get; set; }

        public void SendEmail(AppointmentForm sendEmail) {

            try {

                SmtpClient smtp = new SmtpClient("smtp.gmail.com");

                MailAddress from = new MailAddress("teemuhyva@gmail.com", "From");
                MailAddress to = new MailAddress("teemuhyva@gmail.com");
                MailMessage mail = new MailMessage(from, to);

                mail.Subject = "Marjoja Keräämään: " + sendEmail.Name;
                mail.Body =
                "Nimi: " + sendEmail.Name + "\n" +
                "Sähköposti: " + sendEmail.Email + "\n" +
                "Puhelin: " + sendEmail.Phone + "\n" +
                "Paikalle tulo: " + sendEmail.ShowingDate + "\n" +
                "Lisätiedot: " + sendEmail.Description;


                //fix certification issue as this will only take security off
                //not production use
                ServicePointManager.ServerCertificateValidationCallback =
                delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) {
                    return true;
                };


                smtp.Port = 587;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("teemuhyva", "Spollaritj0");
                smtp.EnableSsl = true;
                smtp.Send(mail);

            } catch(Exception e) {

                Console.WriteLine("Exception happened " + e);
            }

        }

    }
}