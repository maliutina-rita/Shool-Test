using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp
{
    class AccountDatabaseStub : IAccountDatabase
    {
        private readonly Dictionary<string, Account> _accounts = new Dictionary<string, Account>();

        public AccountDatabaseStub()
        {
            _accounts["alice"] = new Account
            {
                ExternalId = "alice",
                InternalId = 1,
                UserName = "alice@mailinator.com",
                Role = "Admin"
            };
            _accounts["bob"] = new Account
            {
                ExternalId = "bob",
                InternalId = 2,
                UserName = "bob@mailinator.com",
                Role = "user"
            };
        }

        public Task<Account> GetOrCreateAccountAsync(string id)
        {
            lock (this)
            {
                if (!_accounts.TryGetValue(id, out var account))
                {
                    account = new Account()
                    {
                        ExternalId = id
                    };
                    _accounts[id] = account;
                }   
                return Task.FromResult(account.Clone());
            }
        }
        
        public Task<Account> GetOrCreateAccountAsync(long id)
        {
            lock (this)
            {
                var account = _accounts.FirstOrDefault(x => x.Value.InternalId == id).Value; 
                if (account == null)
                {
                    account = new Account()
                    {
                        InternalId = id,
                        ExternalId = Guid.NewGuid().ToString()
                    };
                    _accounts[account.ExternalId] = account;
                }   
                return Task.FromResult(account.Clone());
            }
        }

        public Task<Account> FindByUserNameAsync(string userName)
        {
            lock (this)
            {
                var account = _accounts.Values.FirstOrDefault(x => x.UserName == userName);
                return Task.FromResult(account?.Clone());
            }
        }
    }}