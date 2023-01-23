using MimeKit;
using MailKit.Net.Smtp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace Bookshop.Mailing
{
    internal class SmtpSender
    {
        private SmtpClient client;
        protected IConfiguration Configuration { get; private set; }
        public SmtpSender(IConfiguration configuration)
        {
            Configuration = configuration;

            client = new SmtpClient();
        }
        #region Send()
        public void Send(MimeMessage message)
        {
            if (!client.IsConnected)
            {
                client.Connect(
                    Configuration["Mail:Host"],
                    Convert.ToInt32(Configuration["Mail:Port"]),
                    Convert.ToBoolean(Configuration["Mail:Ssl"])
                );

                client.AuthenticationMechanisms.Remove("XOAUTH2");

                if (!String.IsNullOrEmpty(Configuration["Mail:Username"]))
                {
                    client.Authenticate(
                        Configuration["Mail:Username"],
                        Configuration["Mail:Password"]
                    );
                }
            }

            if (message.From.Count == 0)
            {
                message.From.Add(new MailboxAddress(
                    Configuration["Mail:From:Name"],
                    Configuration["Mail:From:Address"]
                ));
            }

            client.Send(message);
        }
        #endregion
    }
}
