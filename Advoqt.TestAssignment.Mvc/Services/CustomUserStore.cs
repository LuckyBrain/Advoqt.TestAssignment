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

        private TUser FindUser(string userId = null, string userName = null, string email = null)
        {
            const string Sql = @"
EXEC dbo.usp_GetUsers
    @userId = @userId,
    @userName = @userName,
    @email = @email";
            var sequence = _dataAccess.GetSequence<TUser>(Sql, new { userId, userName, email });
            return sequence.FirstOrDefault();
        }

        private IEnumerable<TUser> GetAllUsers()
        {
            const string Sql = @"
EXEC dbo.usp_GetUsers";
            var sequence = _dataAccess.GetSequence<TUser>(Sql);
            return sequence;
        }

        public Task CreateAsync(TUser user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            return Task.Run(() =>
            {
                const string Sql = @"
EXEC dbo.usp_AddUser
    @id = @id,
    @userName = @userName,
    @email = @email,
	@passwordHash = @passwordHash,
	@isAdmin = @isAdmin";
                _dataAccess.Execute(Sql, new { user.Id, user.Email, user.PasswordHash, user.UserName, user.IsAdmin });
            });
        }

        public Task UpdateAsync(TUser user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            return Task.Run(() =>
            {
                const string Sql = @"
EXEC dbo.usp_ModifyUser
    @id = @id,
	@passwordHash = @passwordHash";
                _dataAccess.Execute(Sql, new { user.Id, user.PasswordHash });
            });
        }

        public Task DeleteAsync(TUser user)
        {
            if (user == null) throw new ArgumentNullException(nameof(user));

            return Task.Run(() =>
            {
                const string Sql = @"
EXEC dbo.usp_RemoveUser
    @id = @id";
                _dataAccess.Execute(Sql, new { user.Id });
            });
        }

        public Task<TUser> FindByIdAsync(string userId)
        {
            return Task.Run(() => FindUser(userId: userId));
        }

        public Task<TUser> FindByNameAsync(string userName)
        {
            return Task.Run(() => FindUser(userName: userName));
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
            return Task.Run(() => FindUser(email: email));
        }

        public IQueryable<TUser> Users => GetAllUsers().AsQueryable();

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