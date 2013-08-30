using System;
using System.Configuration;
using System.IO;
using System.Net.Mail;
using System.Text;
using System.Web;
using BI.Repository.Context;
using BI.Repository.Entities;
using BI.Umbraco.helpers;

namespace BI.Umbraco.services
{
    public class EmailService
    {


        public EmailStatus SendContactFormularEmail(ContactformularValues data)
        {
            EmailStatus emailStatus = new EmailStatus();

            try
            {
                data.HtmlEmail = CreateContactformularEmail(data);
                data.RecipientEmail = (string.IsNullOrEmpty(data.RecipientEmail) ? ConfigurationManager.AppSettings["contactFormular"] : data.RecipientEmail);
                data.CreatedDate = DateTime.Now;


                ContactformularValuesRepository repo = new ContactformularValuesRepository();
                repo.Insert(data);



                SmtpClient mailClient = new SmtpClient();
                MailMessage mailMessage = new MailMessage("no-reply@blikkenslagerenint.dk", data.RecipientEmail);
                mailMessage.Subject = "Kontakthenvendelse";
                mailMessage.Body = data.HtmlEmail ;
                mailMessage.IsBodyHtml = true;
                mailMessage.Priority = MailPriority.High;
                mailClient.Send(mailMessage);
                emailStatus.EmailSent = true;
                emailStatus.ConfirmEmailSent = true;
                try
                {
                    SendEmailToCustomer(data.Email, data.Name);

                }
                catch (Exception ex)
                {
                    SendError(ex.ToString());
                    emailStatus.ConfirmEmailSent = false;
                    emailStatus.Exception = ex.InnerException + " " + ex.Message;

                }


                emailStatus.StatusMessage = "Din henvendelse er nu sent";
            }
            catch (Exception ex)
            {
                emailStatus.EmailSent = false;
                emailStatus.StatusMessage = SendError(ex.ToString());
            }

            return emailStatus;
        }
        public void SendEmailToCustomer(string customerEmail, string name)
        {
            try
            {
                string _htmlMail = CreateEmailToCustomer(name);
                SmtpClient mailClient = new SmtpClient();
                MailMessage mailMessage = new MailMessage("no-reply@blikkenslagerenint.dk", customerEmail);
                mailMessage.Subject = "Bekræftelse af kontakthenvendelse";
                mailMessage.Body = _htmlMail;
                mailMessage.IsBodyHtml = true;
                mailMessage.Priority = MailPriority.High;
                mailClient.Send(mailMessage);
            }
            catch (Exception ex)
            {
                SendError(ex.ToString());
            }
        }




        private string CreateEmailToCustomer(string name)
        {
            StringBuilder _sb = new StringBuilder();
            _sb.Append("<style type=\"text/css\">");
            _sb.Append("<!--");

            _sb.Append(".table {");
            _sb.Append("width:800px;");
            _sb.Append("height: 100%;");
            _sb.Append("font-family: Calibri,Verdana, Arial, Helvetica, sans-serif;");
            _sb.Append("font-size: 12px;");
            _sb.Append("}");

            _sb.Append(".tr {");
            _sb.Append("background-color:#c9c9c9;");
            _sb.Append("width:300px;");
            _sb.Append("}");

            _sb.Append("-->");
            _sb.Append("</style>");


            _sb.Append("<table class=\"table\" border=\"1\" cellpadding=\"0\" cellspacing=\"0\"");
            //************************ TR ***************************
            _sb.Append("<tr>");

            _sb.Append("<td>");
            _sb.Append("Kære " + name);
            _sb.Append("<br></br>");
            _sb.Append("<br></br>");
            _sb.Append("Vi har modtaget din  henvendelse, vi vender tilbage inden for 48 timer. (i weekender/helligdage, dog først den nærmeste arbejdsdag).");
            _sb.Append("<br></br>");
            _sb.Append("<br></br>");
            _sb.Append("Med venlig hilsen");
            _sb.Append("<br></br>");
            _sb.Append("Blikkenslageren int");
            _sb.Append("</td>");



            _sb.Append("</tr>");





            //************************SLUT * ***************************
            _sb.Append("</table>");


            return _sb.ToString();
        }



        private string CreateContactformularEmail(ContactformularValues data)
        {
            StringBuilder _sb = new StringBuilder();
            _sb.Append("<style type=\"text/css\">");
            _sb.Append("<!--");

            _sb.Append(".table {");
            _sb.Append("width:600px;");
            _sb.Append("height: 100%;");
            _sb.Append("font-family: Calibri,Verdana, Arial, Helvetica, sans-serif;");
            _sb.Append("font-size: 11px;");

            _sb.Append("}");

            _sb.Append(".tr {");
            _sb.Append("width:200px;");
            _sb.Append("}");

            _sb.Append("-->");
            _sb.Append("</style>");


            _sb.Append("<table class=\"table\" border=\"1\" cellpadding=\"0\" cellspacing=\"0\"");
            //*********Firmanavn************

            _sb.Append("<tr class=\"tr\">");

            _sb.Append("<td colspan='2'>");
            _sb.Append("<b>Dette er en kontakthenvendelse</b>");
            _sb.Append("</td>");

            _sb.Append("</tr>");
            //*********Firmanavn************

            _sb.Append("<tr class=\"tr\">");

            _sb.Append("<td>");
            _sb.Append("<b>Navn:</b>");
            _sb.Append("</td>");

            _sb.Append("<td>");
            _sb.Append(string.IsNullOrEmpty(data.Name) ? "-" : data.Name);
            _sb.Append("</td>");

            _sb.Append("</tr>");

            //*********Adresse************
            _sb.Append("<tr>");
            _sb.Append("<td>");
            _sb.Append("<b>Tlf.:</b>");
            _sb.Append("</td>");
            _sb.Append("<td>");
            _sb.Append(string.IsNullOrEmpty(data.Phone) ? "-" : data.Phone);
            _sb.Append("</td>");
            _sb.Append("</tr>");

            //*********Postnr************
            _sb.Append("<tr>");
            _sb.Append("<td>");
            _sb.Append("<b>Email.:</b>");
            _sb.Append("</td>");
            _sb.Append("<td>");
            _sb.Append(string.IsNullOrEmpty(data.Email) ? "-" : data.Email);
            _sb.Append("</td>");
            _sb.Append("</tr>");

            //*********By************
            _sb.Append("<tr>");
            _sb.Append("<td>");
            _sb.Append("<b>Besked:</b>");
            _sb.Append("</td>");
            _sb.Append("<td>");
            _sb.Append(string.IsNullOrEmpty(data.Message) ? "-" : data.Message);
            _sb.Append("</td>");
            _sb.Append("</tr>");

          

            //************************SLUT * ***************************
            _sb.Append("</table>");


            return _sb.ToString();


        }

       

        public string SendError(string ex)
        {
            try
            {
                SmtpClient mailClient = new SmtpClient();
                MailMessage mailMessage = new MailMessage("no-reply@blikkenslagerenint.dk", "kongsune@hotmail.com");
                mailMessage.Subject = "Fejl";
                mailMessage.Body = ex;
                mailMessage.IsBodyHtml = true;
                mailMessage.Priority = MailPriority.High;
                mailClient.Send(mailMessage);
                return "";
            }
            catch (Exception)
            {
                return "Vi har problemer med at sende"; 
            }

        }

       

    }
}