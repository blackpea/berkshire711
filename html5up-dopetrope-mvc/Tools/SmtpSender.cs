namespace html5up_dopetrope_mvc.Tools
{
    using System;
    using System.Net.Mail;
    using System.Text;

    public class SmtpSender
    {
        private SmtpClient _Client = new SmtpClient();

        public SmtpSender(string ServerAddress, int? Port) : this(ServerAddress, Port, "", "", false) { }

        public SmtpSender(string ServerAddress, int? Port, string UserAccount, string UserPassword, bool? EnableSsl)
        {
            //初始化StmpClient
            this._Client = new SmtpClient(ServerAddress, Port.HasValue ? Port.Value  : 25);
            this._Client.Host = ServerAddress;
            if (Port.HasValue)
            {
                this._Client.Port = Port.Value;
            }
            this._Client.DeliveryMethod = SmtpDeliveryMethod.Network;

            //this._Client.UseDefaultCredentials = !EnableSsl.HasValue;

            //是否啟用Ssl
            if (EnableSsl.HasValue) 
            {
                this._Client.EnableSsl = EnableSsl.Value;
            }

            if (!string.IsNullOrEmpty(ServerAddress) && !string.IsNullOrEmpty(UserPassword))
            {
                this._Client.Credentials = new System.Net.NetworkCredential(UserAccount, UserPassword);
            }
        }

        /// <summary>
        /// 送出Email
        /// </summary>
        /// <param name="Mail"></param>
        /// <returns></returns>
        public bool Send(MailMessage Mail)
        {
            try
            {
                this._Client.Send(Mail);
                return true;
            }
            catch(Exception e)
            {
                return false;
            }
        }

        public bool Send(string Sender, string Receiver, string Subject, string Content) 
        {
            MailMessage message = new MailMessage(new MailAddress(Sender), new MailAddress(Receiver));
            message.Subject = Subject;
            message.SubjectEncoding = Encoding.UTF8;
            message.Body = Content;
            message.BodyEncoding = Encoding.UTF8;
            message.IsBodyHtml = true;
            message.Priority = MailPriority.High;

            return this.Send(message);
        }

        public bool TrySend(MailMessage Mail, out string ErrorMessage)
        {
            try
            {
                this._Client.Send(Mail);
                ErrorMessage = "";
                return true;
            }
            catch (Exception e)
            {
                ErrorMessage = e.Message;
                return false;
            }
        }

        public bool TrySend(string Sender, string Receiver, string Subject, string Content, out string ErrorMessage)
        {
            MailMessage message = new MailMessage(new MailAddress(Sender), new MailAddress(Receiver));
            message.Subject = Subject;
            message.SubjectEncoding = Encoding.UTF8;
            message.Body = Content;
            message.BodyEncoding = Encoding.UTF8;
            message.IsBodyHtml = true;
            message.Priority = MailPriority.High;

            return this.TrySend(message, out ErrorMessage);
        }
    }
    
}