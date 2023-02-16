namespace Bookshop.App.Models.User
{
    public class UserFormModel
    {
        public long Id { get; set; }

        public string UserName { get; set; }

        public string Email { get; set; }

        public bool EmailConfirmed { get; set; }

        public string NewPassword { get; set; }

        public string RepeatPassword { get; set; }

        public bool ForcePasswordChange { get; set; }

        public DateTime? LockoutEndDateUtc { get; set; }

        public int FailedAttempts { get; set; }

        public string GivenName { get; set; }

        public string Surname { get; set; }
    }
}
