using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace FeedVinc.Common.Services
{
    public class EmailService
    {
        const string displayName = "FeedVinc";
        const string email = "info@feedvinc.com";
        const string userName = "info@feedvinc.com";
        const string password = "Feedvinc.23";
        const string smtpHost = "smtp.yandex.com.tr";
        const int smtpPort = 587;
        const bool ssl = true;

        public static bool SendMail(List<MailAddress> toList, List<MailAddress> ccList, List<MailAddress> bccList, string subject, string body, string logoPath)
        {
            try
            {
                MailMessage msg = new MailMessage();

                if (toList != null)
                {
                    foreach (var item in toList)
                    {
                        if (item == null) continue;
                        msg.To.Add(item);
                    }
                }

                if (ccList != null)
                {
                    foreach (var item in ccList)
                    {
                        if (item == null) continue;
                        msg.CC.Add(item);
                    }
                }
                if (bccList != null)
                {
                    foreach (var item in bccList)
                    {
                        if (item == null) continue;
                        msg.Bcc.Add(item);
                    }
                }

                msg.From = new MailAddress(email, displayName, System.Text.Encoding.UTF8);
                msg.Subject = subject;
                msg.SubjectEncoding = System.Text.Encoding.UTF8;
                msg.BodyEncoding = System.Text.Encoding.UTF8;
                msg.IsBodyHtml = true;
                msg.Priority = MailPriority.Normal;
                //msg.Headers["FeedVinc"] = "FeedVinc";

                if (logoPath != null)
                {
                    var linkedLogo = new LinkedResource(logoPath);
                    linkedLogo.ContentId = Guid.NewGuid().ToString();
                    body = body.Replace("{LOGO_CID}", linkedLogo.ContentId);

                    var av = AlternateView.CreateAlternateViewFromString(body, null, "text/html");
                    av.LinkedResources.Add(linkedLogo);
                    msg.AlternateViews.Add(av);
                }
                else
                {
                    msg.Body = body;
                }

                SmtpClient smtp = new SmtpClient(smtpHost, smtpPort);
                smtp.UseDefaultCredentials = false;
                smtp.Credentials = new System.Net.NetworkCredential(userName, password);
                smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                smtp.EnableSsl = ssl;

                smtp.Send(msg);
                msg.Dispose();

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

    }
}

