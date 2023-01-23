using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MailKit.Net.Imap.ImapMailboxFilter;

namespace Bookshop.Mailing
{
    public class Mailer
    {
        private SmtpClient _client;
        public IConfiguration Configuration { get; }
        public Mailer(IConfiguration configuration,SmtpClient client)
        {
            Configuration = configuration;
            _client = client;
        }

  

        public MimeMessage Prepare(List<MailboxAddress> From,List<MailboxAddress> To,string subject,string textbody)
        {
            var message = new MimeMessage();
            var builder = new BodyBuilder();
            builder.TextBody = textbody;
            message.From.AddRange(From);
            message.To.AddRange(To);
            message.Subject = subject;
            message.Body = builder.ToMessageBody();
            return message;
        }
        public async Task Send(MimeMessage message) 
        {

           
            if (!_client.IsConnected)
            {
                _client.Connect(Configuration["Mail:Host"],
                    Convert.ToInt32(Configuration["Mail:Port"]),
                    Convert.ToBoolean(Configuration["Mail:Ssl"])
                );

                _client.AuthenticationMechanisms.Remove("XOAUTH2");

                if (!String.IsNullOrEmpty(Configuration["Mail:Username"]))
                {
                    _client.Authenticate(
                        Configuration["Mail:Username"],
                        Configuration["Mail:Password"]
                       
                    ) ;
                }
            }

            if (message.From.Count == 0)
            {
                message.From.Add(new MailboxAddress("", ""
                ));
            }
            _client.Send(message);

        }
     
    }
}
