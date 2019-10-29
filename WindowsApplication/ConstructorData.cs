using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BITCollege_BC;
using Utility;

namespace WindowsApplication
{
    /// <summary>
    /// given:TO BE MODIFIED
    /// this class is used to capture data to be passed
    /// among the windows forms
    /// </summary>
    public class ConstructorData
    {
        public BITCollege_BC.Models.Student Student { get; set; }

        public BITCollege_BC.Models.Registration Registration { get; set; }
    }
}
