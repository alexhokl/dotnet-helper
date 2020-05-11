using System;
using System.Net;
using System.Net.Mail;


namespace Alexhokl.Helpers
{
    public static class MailHelper
    {
        /// <summary>
        /// Sends a mail.
        /// </summary>
        /// <param name="senderName">Name of the sender.</param>
        /// <param name="subject">The subject.</param>
        /// <param name="body">The body.</param>
        /// <param name="mailFrom">The mail from.</param>
        /// <param name="mailTo">The to copies.</param>
        /// <param name="mailCc">The carbon copies.</param>
        /// <param name="smtp">The SMTP.</param>
        /// <param name="smtpUser">The SMTP user.</param>
        /// <param name="smtpPassword">The SMTP password.</param>
        /// <param name="smtpPort">The SMTP port.</param>
        public static void Send(
            string senderName, string subject, string body,
            string mailFrom, string mailTo, string mailCc,
            string smtp, string smtpUser, string smtpPassword, int smtpPort)
        {
            try
            {
                MailAddress from = new MailAddress(mailFrom, senderName);
                MailAddress to = new MailAddress(mailTo);
                using (var message = new MailMessage(from, to))
                {
                    if (!string.IsNullOrWhiteSpace(mailCc))
                    {
                        message.CC.Add(mailCc);
                    }

                    message.Subject = subject;
                    message.IsBodyHtml = true;
                    message.Body = body;

                    using (SmtpClient smtpClient = new SmtpClient(smtp))
                    {
                        smtpClient.UseDefaultCredentials = false;
                        smtpClient.Credentials = new NetworkCredential(smtpUser, smtpPassword);
                        smtpClient.Port = smtpPort;
                        smtpClient.Send(message);
                    }
                }
            }
            catch (SmtpFailedRecipientException failedRecipientEx)
            {
                throw new ApplicationException(
                    string.Format(
                        "Failed to send mail [{0}] to [{1}].",
                        subject, failedRecipientEx.FailedRecipient),
                    failedRecipientEx);
            }
        }
    }
}
