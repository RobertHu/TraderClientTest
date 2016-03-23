// ==++== 
//
//   Copyright (c) Microsoft Corporation.  All rights reserved.
//
// ==--== 
/*==============================================================================
** 
** Class: CLSCompliantAttribute 
**
** 
** Purpose: Container for assemblies.
**
**
=============================================================================*/ 

namespace System { 
[Serializable] 
    [AttributeUsage (AttributeTargets.All, Inherited=true, AllowMultiple=false)]
[System.Runtime.InteropServices.ComVisible(true)] 
    public sealed class CLSCompliantAttribute : Attribute
    {
        private bool m_compliant;
 
        public CLSCompliantAttribute (bool isCompliant)
        { 
            m_compliant = isCompliant; 
        }
        public bool IsCompliant 
        {
            get
            {
                return m_compliant; 
            }
        } 
    } 
}

// File provided for Reference Use Only by Microsoft Corporation (c) 2007.
// ==++== 
//
//   Copyright (c) Microsoft Corporation.  All rights reserved.
//
// ==--== 
/*==============================================================================
** 
** Class: CLSCompliantAttribute 
**
** 
** Purpose: Container for assemblies.
**
**
=============================================================================*/ 

namespace System { 
[Serializable] 
    [AttributeUsage (AttributeTargets.All, Inherited=true, AllowMultiple=false)]
[System.Runtime.InteropServices.ComVisible(true)] 
    public sealed class CLSCompliantAttribute : Attribute
    {
        private bool m_compliant;
 
        public CLSCompliantAttribute (bool isCompliant)
        { 
            m_compliant = isCompliant; 
        }
        public bool IsCompliant 
        {
            get
            {
                return m_compliant; 
            }
        } 
    } 
}

// File provided for Reference Use Only by Microsoft Corporation (c) 2007.
