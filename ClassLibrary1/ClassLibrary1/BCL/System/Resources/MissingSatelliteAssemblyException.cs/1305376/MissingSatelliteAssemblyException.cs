// ==++== 
//
//   Copyright (c) Microsoft Corporation.  All rights reserved.
//
// ==--== 
/*============================================================
** 
** Class:  MissingSatelliteAssemblyException 
**
** <OWNER>[....]</OWNER> 
**
**
** Purpose: Exception for a missing satellite assembly needed
**          for ultimate resource fallback.  This usually 
**          indicates a setup and/or deployment problem.
** 
** 
===========================================================*/
 
using System;
using System.Runtime.Serialization;

namespace System.Resources { 
    [Serializable]
[System.Runtime.InteropServices.ComVisible(true)] 
    public class MissingSatelliteAssemblyException : SystemException 
    {
        private String _cultureName; 

        public MissingSatelliteAssemblyException()
            : base(Environment.GetResourceString("MissingSatelliteAssembly_Default")) {
            SetErrorCode(__HResults.COR_E_MISSINGSATELLITEASSEMBLY); 
        }
 
        public MissingSatelliteAssemblyException(String message) 
            : base(message) {
            SetErrorCode(__HResults.COR_E_MISSINGSATELLITEASSEMBLY); 
        }

        public MissingSatelliteAssemblyException(String message, String cultureName)
            : base(message) { 
            SetErrorCode(__HResults.COR_E_MISSINGSATELLITEASSEMBLY);
            _cultureName = cultureName; 
        } 

        public MissingSatelliteAssemblyException(String message, Exception inner) 
            : base(message, inner) {
            SetErrorCode(__HResults.COR_E_MISSINGSATELLITEASSEMBLY);
        }
 
#if FEATURE_SERIALIZATION
        [System.Security.SecuritySafeCritical]  // auto-generated 
        protected MissingSatelliteAssemblyException(SerializationInfo info, StreamingContext context) : base (info, context) { 
        }
#endif // FEATURE_SERIALIZATION 

        public String CultureName {
            get { return _cultureName; }
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
** Class:  MissingSatelliteAssemblyException 
**
** <OWNER>[....]</OWNER> 
**
**
** Purpose: Exception for a missing satellite assembly needed
**          for ultimate resource fallback.  This usually 
**          indicates a setup and/or deployment problem.
** 
** 
===========================================================*/
 
using System;
using System.Runtime.Serialization;

namespace System.Resources { 
    [Serializable]
[System.Runtime.InteropServices.ComVisible(true)] 
    public class MissingSatelliteAssemblyException : SystemException 
    {
        private String _cultureName; 

        public MissingSatelliteAssemblyException()
            : base(Environment.GetResourceString("MissingSatelliteAssembly_Default")) {
            SetErrorCode(__HResults.COR_E_MISSINGSATELLITEASSEMBLY); 
        }
 
        public MissingSatelliteAssemblyException(String message) 
            : base(message) {
            SetErrorCode(__HResults.COR_E_MISSINGSATELLITEASSEMBLY); 
        }

        public MissingSatelliteAssemblyException(String message, String cultureName)
            : base(message) { 
            SetErrorCode(__HResults.COR_E_MISSINGSATELLITEASSEMBLY);
            _cultureName = cultureName; 
        } 

        public MissingSatelliteAssemblyException(String message, Exception inner) 
            : base(message, inner) {
            SetErrorCode(__HResults.COR_E_MISSINGSATELLITEASSEMBLY);
        }
 
#if FEATURE_SERIALIZATION
        [System.Security.SecuritySafeCritical]  // auto-generated 
        protected MissingSatelliteAssemblyException(SerializationInfo info, StreamingContext context) : base (info, context) { 
        }
#endif // FEATURE_SERIALIZATION 

        public String CultureName {
            get { return _cultureName; }
        } 
    }
} 

// File provided for Reference Use Only by Microsoft Corporation (c) 2007.