using Hozaru.Core.Identity.Authorization.Users;
using Hozaru.Identity.MultiTenancy;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Identity.Users
{
    public class User : HozaruUser<Tenant, User>
    {
        public override string ToString()
        {
            return string.Format("[User {0}] {1}", Id, UserName);
        }

        public static User CreateTenantAdminUser(Tenant tenant, string emailAddress, string password, string firstName, string lastName)
        {
            return new User
            {
                TenantId = tenant.Id,
                UserName = AdminUserName,
                Name = string.Format("{0} {1}", firstName, lastName),
                Surname = firstName,
                FirstName = firstName,
                LastName = lastName,
                EmailAddress = emailAddress,
                Password = new PasswordHasher().HashPassword(password)
            };
        }
    }
}
