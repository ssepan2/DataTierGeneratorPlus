//Note: as this module is based on the DotNetNuke file Null.vb, I have included this copyright notice.--SJS
//
// DotNetNuke� - http://www.dotnetnuke.com
// Copyright (c) 2002-2006
// by Perpetual Motion Interactive Systems Inc. ( http://www.perpetualmotion.ca )
//
// Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated 
// documentation files (the "Software"), to deal in the Software without restriction, including without limitation 
// the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and 
// to permit persons to whom the Software is furnished to do so, subject to the following conditions:
//
// The above copyright notice and this permission notice shall be included in all copies or substantial portions 
// of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED 
// TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL 
// THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF 
// CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER 
// DEALINGS IN THE SOFTWARE.
//
using System;
using System.Reflection;

namespace Cs_BuiltIn_WcfServiceApp {

    public sealed class NullHandler
    {
		private  NullHandler() {}

        // convert db null  to app null 
        public static Object HandleDbNull(Object objValue)
        {
            Object returnValue; 

            if (objValue == System.DBNull.Value )
            {
                //complex object
                returnValue = null;
            }
             else
            {
                // return value
                returnValue = objValue;
            }
            return returnValue;

        }

        // convert app null  to db null 
       public static Object HandleAppNull(Object objField, Object objDBNull)
        {
            Object returnValue = objField;

          if (objField == null)
            {
                returnValue = objDBNull;
            }
            else
            {
                returnValue = objField;
            }

            return returnValue;

        }
    }

}