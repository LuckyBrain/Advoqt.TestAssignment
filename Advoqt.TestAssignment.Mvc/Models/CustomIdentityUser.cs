namespace Advoqt.TestAssignment.Mvc.Models
{
    using System;
    using Microsoft.AspNet.Identity;

    public class CustomIdentityUser : IUser<string>
    {
        public CustomIdentityUser()
        {
            Id = Guid.NewGuid().ToString();
        }

        public CustomIdentityUser(string userName)
        {
            UserName = userName;
        }

        /// <summary>
        /// Gets the ID (Primary Key).
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the salted/hashed form of the user password.
        /// </summary>
        public string PasswordHash { get; set; }

        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        public string Email { get; set; }
    }
}