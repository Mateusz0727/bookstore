using Bookshop.Helpers;
using Bookshop.Mailing;
using MimeKit;

namespace Bookshop.App.Jobs
{
    public class ExampleSendMailJob : IScheduledJob
    {
        private readonly Mailer _mailer;

        public ExampleSendMailJob(Mailer mailer) {
            _mailer = mailer;
        }
        public void Execute(CancellationToken token)
        {
            var message = _mailer.Prepare(
             new List<MailboxAddress> { new MailboxAddress("KMMK", "kmmk420024@gmail.com") },
             new List<MailboxAddress> { new MailboxAddress("KMMK", "kmmk420024@gmail.com") },
             "example subject",
             "example body text");
            _mailer.Send(message);
        }
    }
}
