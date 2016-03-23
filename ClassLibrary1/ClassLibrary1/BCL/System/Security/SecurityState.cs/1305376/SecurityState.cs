// ==++== 
//
//   Copyright (c) Microsoft Corporation.  All rights reserved.
//
// <OWNER>[....]</OWNER> 

using System; 
using System.Security; 
using System.Security.Permissions;
 
namespace System.Security
{
    [System.Security.SecurityCritical]  // auto-generated_required
    [PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)] 
    public abstract class SecurityState
    { 
        protected SecurityState(){} 

        [System.Security.SecurityCritical]  // auto-generated 
        public bool IsStateAvailable()
        {
            AppDomainManager domainManager = AppDomainManager.CurrentAppDomainManager;
            return domainManager != null ? domainManager.CheckSecuritySettings(this) : false; 
        }
        // override this function and throw the appropriate 
        public abstract void EnsureState(); 
    }
 
}

// File provided for Reference Use Only by Microsoft Corporation (c) 2007.
// ==++== 
//
//   Copyright (c) Microsoft Corporation.  All rights reserved.
//
// <OWNER>[....]</OWNER> 

using System; 
using System.Security; 
using System.Security.Permissions;
 
namespace System.Security
{
    [System.Security.SecurityCritical]  // auto-generated_required
    [PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)] 
    public abstract class SecurityState
    { 
        protected SecurityState(){} 

        [System.Security.SecurityCritical]  // auto-generated 
        public bool IsStateAvailable()
        {
            AppDomainManager domainManager = AppDomainManager.CurrentAppDomainManager;
            return domainManager != null ? domainManager.CheckSecuritySettings(this) : false; 
        }
        // override this function and throw the appropriate 
        public abstract void EnsureState(); 
    }
 
}

// File provided for Reference Use Only by Microsoft Corporation (c) 2007.
