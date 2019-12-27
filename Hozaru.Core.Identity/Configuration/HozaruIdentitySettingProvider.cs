using Hozaru.Core.Configurations;
using Hozaru.Core.Localization;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hozaru.Core.Identity.Configuration
{
    public class HozaruIdentitySettingProvider : SettingProvider
    {
        public override IEnumerable<SettingDefinition> GetSettingDefinitions(SettingDefinitionProviderContext context)
        {
            return new List<SettingDefinition>
                   {
                       new SettingDefinition(
                           HozaruIdentitySettingNames.UserManagement.IsEmailConfirmationRequiredForLogin,
                           "false",
                           new FixedLocalizableString("Is email confirmation required for login."),
                           scopes: SettingScopes.Application | SettingScopes.Tenant,
                           isVisibleToClients: true
                           ),
                       new SettingDefinition(
                           HozaruIdentitySettingNames.OrganizationUnits.MaxUserMembershipCount,
                           int.MaxValue.ToString(),
                           new FixedLocalizableString("Maximum allowed organization unit membership count for a user."),
                           scopes: SettingScopes.Application | SettingScopes.Tenant,
                           isVisibleToClients: true
                           )
                   };
        }
    }
}
