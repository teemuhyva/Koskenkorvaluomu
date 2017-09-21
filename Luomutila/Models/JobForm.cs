using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace LuomuTila.Models {
    public class JobForm {

        public JobForm() {
        }
        
        [Required(ErrorMessage = "Etunimi pakollinen")]
        [StringLength(50)]
        public string FirstName {
            get; set;
        }

        [Required(ErrorMessage = "Sukunimi pakollinen")]
        [StringLength(50)]
        public string LastName {
            get; set;
        }

        [Required(ErrorMessage = "Email pakollinen")]
        [EmailAddress]
        public string Email {
            get; set;
        }

        [Required(ErrorMessage = "Puhelinnumero pakollinen")]
        public string Phone {
            get; set;
        }

        [Required(ErrorMessage = "Ikä pakollinen tieto")]
        public int Age {
            get; set;
        }

        [Required(ErrorMessage = "Valitse aloitusajankohta")]
        public DateTime StartingDate {
            get; set;
        }

        [Required(ErrorMessage = "Valitse loppumisajankohta")] 
        public DateTime EndDate {
            get; set;
        }

        public void SendJobForm(JobForm jobform) {

            try {

                SmtpClient smtp = new SmtpClient("smtp.gmail.com");

                MailAddress from = new MailAddress("teemuhyva@gmail.com", "From");
                MailAddress to = new MailAddress("teemuhyva@gmail.com");
                MailMessage mail = new MailMessage(from, to);

                mail.Subject = "Kesätyöpaikka hakemus: " + jobform.FirstName + " " + jobform.LastName;
                mail.Body =
                "Nimi: " + jobform.FirstName + " " + jobform.LastName + "\n" +  
                "Sähköposti: " + jobform.Email + "\n" +
                "Puhelin: " + jobform.Phone + "\n" +
                "Aloitusajankohta: " + jobform.StartingDate + "\n" +
                "Lopetusajankohta: " + jobform.EndDate;

                //fix certification issue as this will only take security off 
                //not production use
                ServicePointManager.ServerCertificateValidationCallback =
                delegate (object s, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) {
                    return true;
                };


                smtp.Port = 587;
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new NetworkCredential("teemuhyva", "*********");
                smtp.EnableSsl = true;
                smtp.Send(mail);

            } catch(Exception e) {

                Console.WriteLine("Exception happened " + e);
            }

        }
    }
}