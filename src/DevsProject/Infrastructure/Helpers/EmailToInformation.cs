using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Infrastructure.Services.Models;

namespace Infrastructure.Helpers
{
    //Gönderilecek bilgilendirme mailinin şablonu oluşturulur
    public class EmailToInformation
    {
        //appsettingE taşındı.Oradan EmailVerifyModel ile taşındı.
        //private string smtpServer = "smtp.office365.com"; //gönderim yapacak hizmetin smtp adresi
        //private int smtpPort = 587;
        //private string username = "yasnesra@outlook.com";
        //private string password = "Esra1030515786";
        //private string senderEmail = "yasnesra@outlook.com";

        //appsettingsdeki değerlere IConfiguration yapılandırmasıyla erişilir

        IConfiguration _configuration;

        public EmailToInformation(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        private EmailVerifyModel EmailSetting()
        {
            //GetSection(nameof(EmailVerifyModel)) ; appsettinsdeki modele erişir.
            //Get<EmailVerifyModel>() ; EmailVerifyModel sınıfını, appsettingsdeki EmailVerifyModel bölümündeki verileri eşleştirir
            var emailSetting = _configuration.GetSection(nameof(EmailVerifyModel)).Get<EmailVerifyModel>();

            var result = new EmailVerifyModel()
            {
                SmtpServer = emailSetting.SmtpServer,
                SmtpPort = emailSetting.SmtpPort,
                Username = emailSetting.Username,                      
                Password = emailSetting.Password,
                SenderEmail = emailSetting.SenderEmail
            };

            return result;
        }

        // e-posta gönderimi için gerekli işlemleri gerçekleştirir
        public void SendEmail(List<string> to, string message)
        {
            var emailSetting = EmailSetting();

            // E-posta gönderimi için SMTP istemci oluşturma
            //SMTP/Gönderici bilgilerinin yer aldığı erişim/doğrulama bilgileri
            SmtpClient smtpClient = new SmtpClient(emailSetting.SmtpServer, Convert.ToInt32(emailSetting.SmtpPort));
            smtpClient.EnableSsl = true;
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new NetworkCredential(emailSetting.Username, emailSetting.Password);   

            foreach(var target in to)
            {
                string template =
               @$"<html>
                <body>
                
                    Merhaba Gİriş için Doğrulama kodunu giriniz. {target}  <br
                    <h2>Doğrulama Kodu : {message}</h2>
                        <hr/>              
                </body>
                </html>";

                // E-posta oluşturma (mesajı oluşturma)
                MailMessage mailMessage = new MailMessage(emailSetting.SenderEmail, target);
                mailMessage.Subject = $"Doğrulama Maili";
                mailMessage.Body = template;
                mailMessage.IsBodyHtml = true;

                try
                {
                    // E-postayı gönderme
                    smtpClient.Send(mailMessage);
                    Console.WriteLine("E-posta gönderildi.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("E-posta gönderilirken bir hata oluştu: " + ex.Message);
                }
                finally
                {
                    // Kaynakları serbest bırakma
                    mailMessage.Dispose();
                    smtpClient.Dispose();
                }
            }

           
            
        }
    }
}

