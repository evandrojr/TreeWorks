#region Using directives

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

#endregion

namespace Spartacus {
    public class Validation {
        public Validation() {

        }


        public static bool IsInteger(string val) {
        
            Regex pat = new Regex("^[0-9]+$");
            if (pat.IsMatch(val.Trim()))
                return true;
            else
                return false;
        }
    }
}
