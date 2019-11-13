using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Core.Runtime.Session
{
    /// <summary>
    /// Extension methods for <see cref="IHozaruSession"/>.
    /// </summary>
    public static class HozaruSessionExtensions
    {
        /// <summary>
        /// Gets current User's Id.
        /// Throws <see cref="HozaruException"/> if <see cref="IHozaruSession.UserId"/> is null.
        /// </summary>
        /// <param name="session">Session object.</param>
        /// <returns>Current User's Id.</returns>
        public static long GetUserId(this IHozaruSession session)
        {
            if (!session.UserId.HasValue)
            {
                throw new HozaruException("Session.UserId is null! Probably, user is not logged in.");
            }

            return session.UserId.Value;
        }

        /// <summary>
        /// Gets current Tenant's Id.
        /// Throws <see cref="HozaruException"/> if <see cref="IHozaruSession.TenantId"/> is null.
        /// </summary>
        /// <param name="session">Session object.</param>
        /// <returns>Current Tenant's Id.</returns>
        /// <exception cref="HozaruException"></exception>
        public static int GetTenantId(this IHozaruSession session)
        {
            if (!session.TenantId.HasValue)
            {
                throw new HozaruException("Session.TenantId is null! Possible problems: No user logged in or current logged in user in a host user (TenantId is always null for host users).");
            }

            return session.TenantId.Value;
        }
    }
}
