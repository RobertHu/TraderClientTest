// ==++== 
//
//   Copyright (c) Microsoft Corporation.  All rights reserved.
//
// ==--== 
/*============================================================
** 
** File:    AssemblyNameProxy 
**
** <OWNER>[....]</OWNER> 
** <OWNER>[....]</OWNER>
**
**
** Purpose: Remotable version the AssemblyName 
**
** 
===========================================================*/ 
namespace System.Reflection {
    using System; 
    using System.Runtime.Versioning;

    [System.Runtime.InteropServices.ComVisible(true)]
    public class AssemblyNameProxy : MarshalByRefObject 
    {
        [System.Security.SecuritySafeCritical]  // auto-generated 
        [ResourceExposure(ResourceScope.Machine)] 
        [ResourceConsumption(ResourceScope.Machine)]
        public AssemblyName GetAssemblyName(String assemblyFile) 
        {
            return AssemblyName.GetAssemblyName(assemblyFile);
        }
    } 
}

// File provided for Reference Use Only by Microsoft Corporation (c) 2007.
// ==++== 
//
//   Copyright (c) Microsoft Corporation.  All rights reserved.
//
// ==--== 
/*============================================================
** 
** File:    AssemblyNameProxy 
**
** <OWNER>[....]</OWNER> 
** <OWNER>[....]</OWNER>
**
**
** Purpose: Remotable version the AssemblyName 
**
** 
===========================================================*/ 
namespace System.Reflection {
    using System; 
    using System.Runtime.Versioning;

    [System.Runtime.InteropServices.ComVisible(true)]
    public class AssemblyNameProxy : MarshalByRefObject 
    {
        [System.Security.SecuritySafeCritical]  // auto-generated 
        [ResourceExposure(ResourceScope.Machine)] 
        [ResourceConsumption(ResourceScope.Machine)]
        public AssemblyName GetAssemblyName(String assemblyFile) 
        {
            return AssemblyName.GetAssemblyName(assemblyFile);
        }
    } 
}

// File provided for Reference Use Only by Microsoft Corporation (c) 2007.
