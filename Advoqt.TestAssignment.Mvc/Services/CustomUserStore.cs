namespace Advoqt.TestAssignment.Mvc.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Abstract;
    using Microsoft.AspNet.Identity;
    using Models;

    public class CustomUserStore<TUser> :
        IUserStore<TUser>,
        IUserPasswordStore<TUser>,
        IUserEmailStore<TUser>,
        IQueryableUserStore<TUser>,
        IUserLockoutStore<TUser, string>,
        IUserTwoFactorStore<TUser, string>
        where TUser : ApplicationUser
    {
        private readonly IDataAccess _dataAccess;

        public CustomUserStore(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess ?? throw new ArgumentNullException(nameof(dataAccess));
        }

        public void Dispose()
        {
            // nothing to do by now but required by the interface
        }

        public Task CreateAsync(TUser user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            return Task.Run(() =>
            {
                const string Sql = @"
INSERT dbo.AspNetUsers
	(Id, Email, EmailConfirmed, PasswordHash, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnabled, AccessFailedCount, UserName, IsAdmin)
SELECT
	Id = @id,
	Email = @email,
	EmailConfirmed = 0,
	PasswordHash = @passwordHash,
    PhoneNumberConfirmed = 0,
	TwoFactorEnabled = 0,
	LockoutEnabled = 0,
	AccessFailedCount = 0,
	UserName = @userName,
	IsAdmin = @isAdmin
WHERE	NOT EXISTS
		(
			SELECT *
			FROM dbo.AspNetUsers
			WHERE	Email = @email
				OR	UserName = @userName
		)";
                _dataAccess.Execute(Sql, new { user.Id, user.Email, user.PasswordHash, user.UserName, user.IsAdmin });
            });
        }

        public Task UpdateAsync(TUser user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            return Task.Run(() =>
            {
                const string Sql = @"
UPDATE dbo.AspNetUsers SET
	PasswordHash = @passwordHash
WHERE	Id = @id";
                _dataAccess.Execute(Sql, new { user.Id, user.PasswordHash });
            });
        }

        public Task DeleteAsync(TUser user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            return Task.Run(() =>
            {
                const string Sql = @"
DELETE dbo.AspNetUsers
WHERE   Id = @id";
                _dataAccess.Execute(Sql, new { user.Id });
            });
        }

        public Task<TUser> FindByIdAsync(string userId)
        {
            return Task.Run(() =>
            {
                const string Sql = @"
SELECT *
FROM dbo.AspNetUsers X
WHERE   X.Id = @userId";
                var sequence = _dataAccess.GetSequence<TUser>(Sql, new { userId });
                return sequence.FirstOrDefault();
            });
        }

        public Task<TUser> FindByNameAsync(string userName)
        {
            return Task.Run(() =>
            {
                const string Sql = @"
SELECT *
FROM dbo.AspNetUsers X
WHERE   X.UserName = @userName";
                var sequence = _dataAccess.GetSequence<TUser>(Sql, new { userName });
                return sequence.FirstOrDefault();
            });
        }

        public Task SetPasswordHashAsync(TUser user, string passwordHash)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            user.PasswordHash = passwordHash;
            return Task.CompletedTask;
        }

        public Task<string> GetPasswordHashAsync(TUser user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            return Task.FromResult(user.PasswordHash);
        }

        public Task<bool> HasPasswordAsync(TUser user)
        {
            return Task.FromResult(user.PasswordHash != null);
        }

        public Task SetEmailAsync(TUser user, string email)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            user.Email = email;
            return Task.CompletedTask;
        }

        public Task<string> GetEmailAsync(TUser user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            return Task.FromResult(user.Email);
        }

        public Task<bool> GetEmailConfirmedAsync(TUser user)
        {
            throw new NotSupportedException();
        }

        public Task SetEmailConfirmedAsync(TUser user, bool confirmed)
        {
            throw new NotSupportedException();
        }

        public Task<TUser> FindByEmailAsync(string email)
        {
            return Task.Run(() =>
            {
                const string Sql = @"
SELECT *
FROM dbo.AspNetUsers X
WHERE   X.Email = @email";
                var sequence = _dataAccess.GetSequence<TUser>(Sql, new { email });
                return sequence.FirstOrDefault();
            });
        }

        public IQueryable<TUser> Users => GetAllUsers().AsQueryable();

        private IEnumerable<TUser> GetAllUsers()
        {
            const string Sql = @"
SELECT *
FROM dbo.AspNetUsers X";
            var sequence = _dataAccess.GetSequence<TUser>(Sql);
            return sequence;
        }

        public Task<DateTimeOffset> GetLockoutEndDateAsync(TUser user)
        {
            throw new NotSupportedException();
        }

        public Task SetLockoutEndDateAsync(TUser user, DateTimeOffset lockoutEnd)
        {
            throw new NotSupportedException();
        }

        public Task<int> IncrementAccessFailedCountAsync(TUser user)
        {
            throw new NotSupportedException();
        }

        public Task ResetAccessFailedCountAsync(TUser user)
        {
            throw new NotSupportedException();
        }

        public Task<int> GetAccessFailedCountAsync(TUser user)
        {
            return Task.FromResult(0);
        }

        public Task SetLockoutEnabledAsync(TUser user, bool enabled)
        {
            throw new NotSupportedException();
        }

        public Task<bool> GetLockoutEnabledAsync(TUser user)
        {
            return Task.FromResult(false);
        }

        public Task SetTwoFactorEnabledAsync(TUser user, bool enabled)
        {
            throw new NotSupportedException();
        }

        public Task<bool> GetTwoFactorEnabledAsync(TUser user)
        {
            return Task.FromResult(false);
        }
    }
}