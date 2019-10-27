using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utility
{
    public static class MyUtilities
    {
        public static string Trim(string text, string trimValue)
        {
            string returnValue = "";

            returnValue = text.Substring(0, text.IndexOf(trimValue));

            return returnValue;
        }
    }
}
