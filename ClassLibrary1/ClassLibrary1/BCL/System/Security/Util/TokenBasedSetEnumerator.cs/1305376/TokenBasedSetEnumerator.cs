// ==++== 
//
//   Copyright (c) Microsoft Corporation.  All rights reserved.
//
// ==--== 
// TokenBasedSetEnumerator.cs
// 
// <OWNER>[....]</OWNER> 
//
 
namespace System.Security.Util
{
    using System;
    using System.Collections; 

    internal struct TokenBasedSetEnumerator 
    { 
        public Object Current;
        public int Index; 

        private TokenBasedSet _tb;

        public bool MoveNext() 
        {
            return _tb != null ? _tb.MoveNext(ref this) : false; 
        } 

        public void Reset() 
        {
            Index = -1;
            Current = null;
        } 

        public TokenBasedSetEnumerator(TokenBasedSet tb) 
        { 
            Index = -1;
            Current = null; 
            _tb = tb;
        }
    }
} 


// File provided for Reference Use Only by Microsoft Corporation (c) 2007.
// ==++== 
//
//   Copyright (c) Microsoft Corporation.  All rights reserved.
//
// ==--== 
// TokenBasedSetEnumerator.cs
// 
// <OWNER>[....]</OWNER> 
//
 
namespace System.Security.Util
{
    using System;
    using System.Collections; 

    internal struct TokenBasedSetEnumerator 
    { 
        public Object Current;
        public int Index; 

        private TokenBasedSet _tb;

        public bool MoveNext() 
        {
            return _tb != null ? _tb.MoveNext(ref this) : false; 
        } 

        public void Reset() 
        {
            Index = -1;
            Current = null;
        } 

        public TokenBasedSetEnumerator(TokenBasedSet tb) 
        { 
            Index = -1;
            Current = null; 
            _tb = tb;
        }
    }
} 


// File provided for Reference Use Only by Microsoft Corporation (c) 2007.