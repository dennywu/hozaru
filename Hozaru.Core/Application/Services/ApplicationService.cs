using Hozaru.Core.Application.Features;
using Hozaru.Core.Runtime.Session;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hozaru.Core.Application.Services
{
    /// <summary>
    /// This class can be used as a base class for application services. 
    /// </summary>
    public abstract class ApplicationService : HozaruServiceBase, IApplicationService
    {
        /// <summary>
        /// Gets current session information.
        /// </summary>
        public IHozaruSession HozaruSession { get; set; }

        /// <summary>
        /// Reference to the permission manager.
        /// </summary>
        //public IPermissionManager PermissionManager { protected get; set; }

        /// <summary>
        /// Reference to the permission checker.
        /// </summary>
        //public IPermissionChecker PermissionChecker { protected get; set; }

        /// <summary>
        /// Reference to the feature manager.
        /// </summary>
        public IFeatureManager FeatureManager { protected get; set; }

        /// <summary>
        /// Reference to the permission checker.
        /// </summary>
        public IFeatureChecker FeatureChecker { protected get; set; }

        /// <summary>
        /// Gets current session information.
        /// </summary>
        [Obsolete("Use HozaruSession property instead. CurrentSetting will be removed in future releases.")]
        protected IHozaruSession CurrentSession { get { return HozaruSession; } }

        /// <summary>
        /// Constructor.
        /// </summary>
        protected ApplicationService()
        {
            HozaruSession = NullHozaruSession.Instance;
            //PermissionChecker = NullPermissionChecker.Instance;
        }

        /// <summary>
        /// Checks if current user is granted for a permission.
        /// </summary>
        /// <param name="permissionName">Name of the permission</param>
        //protected virtual Task<bool> IsGrantedAsync(string permissionName)
        //{
        //    return PermissionChecker.IsGrantedAsync(permissionName);
        //}

        ///// <summary>
        ///// Checks if current user is granted for a permission.
        ///// </summary>
        ///// <param name="permissionName">Name of the permission</param>
        //protected virtual bool IsGranted(string permissionName)
        //{
        //    return PermissionChecker.IsGranted(permissionName);
        //}

        ///// <summary>
        ///// Checks if given feature is enabled for current tenant.
        ///// </summary>
        ///// <param name="featureName">Name of the feature</param>
        ///// <returns></returns>
        //protected virtual Task<bool> IsEnabledAsync(string featureName)
        //{
        //    return FeatureChecker.IsEnabledAsync(featureName);
        //}

        ///// <summary>
        ///// Checks if given feature is enabled for current tenant.
        ///// </summary>
        ///// <param name="featureName">Name of the feature</param>
        ///// <returns></returns>
        //protected virtual bool IsEnabled(string featureName)
        //{
        //    return FeatureChecker.IsEnabled(featureName);
        //}
    }
}
