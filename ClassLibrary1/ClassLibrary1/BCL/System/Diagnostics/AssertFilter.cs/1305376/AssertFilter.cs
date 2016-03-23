// ==++== 
//
//   Copyright (c) Microsoft Corporation.  All rights reserved.
//
// ==--== 
namespace System.Diagnostics {
 
 
    using System;
    using System.Runtime.Versioning; 
   // A Filter is used to decide whether an assert failure
   // should terminate the program (or invoke the debugger).
   // Typically this is done by popping up a dialog & asking the user.
   // 
   // The default filter brings up a simple Win32 dialog with 3 buttons.
 
    [Serializable] 
    abstract internal class AssertFilter
    { 

        // Called when an assert fails.  This should be overridden with logic which
        // determines whether the program should terminate or not.  Typically this
        // is done by asking the user. 
        //
 
        abstract public AssertFilters  AssertFailure(String condition, String message, 
                                  StackTrace location);
 
    }
    // No data, does not need to be marked with the serializable attribute
    internal class DefaultFilter : AssertFilter
    { 
        internal DefaultFilter()
        { 
        } 

        [System.Security.SecuritySafeCritical]  // auto-generated 
        [ResourceExposure(ResourceScope.Process)]
        [ResourceConsumption(ResourceScope.Process)]
        public override AssertFilters  AssertFailure(String condition, String message,
                                  StackTrace location) 

        { 
            return (AssertFilters) Assert.ShowDefaultAssertDialog (condition, message, location.ToString()); 
        }
    } 

}

// File provided for Reference Use Only by Microsoft Corporation (c) 2007.
// ==++== 
//
//   Copyright (c) Microsoft Corporation.  All rights reserved.
//
// ==--== 
namespace System.Diagnostics {
 
 
    using System;
    using System.Runtime.Versioning; 
   // A Filter is used to decide whether an assert failure
   // should terminate the program (or invoke the debugger).
   // Typically this is done by popping up a dialog & asking the user.
   // 
   // The default filter brings up a simple Win32 dialog with 3 buttons.
 
    [Serializable] 
    abstract internal class AssertFilter
    { 

        // Called when an assert fails.  This should be overridden with logic which
        // determines whether the program should terminate or not.  Typically this
        // is done by asking the user. 
        //
 
        abstract public AssertFilters  AssertFailure(String condition, String message, 
                                  StackTrace location);
 
    }
    // No data, does not need to be marked with the serializable attribute
    internal class DefaultFilter : AssertFilter
    { 
        internal DefaultFilter()
        { 
        } 

        [System.Security.SecuritySafeCritical]  // auto-generated 
        [ResourceExposure(ResourceScope.Process)]
        [ResourceConsumption(ResourceScope.Process)]
        public override AssertFilters  AssertFailure(String condition, String message,
                                  StackTrace location) 

        { 
            return (AssertFilters) Assert.ShowDefaultAssertDialog (condition, message, location.ToString()); 
        }
    } 

}

// File provided for Reference Use Only by Microsoft Corporation (c) 2007.