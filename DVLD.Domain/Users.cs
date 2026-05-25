using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD.Domain
{
    public class clsUsers
    {
        public enum enMode { AddNew, Update };
        public enMode _Mode = enMode.AddNew;
        public int _UserID { get; set; }
        public string _UserName { get; set; }
        public string _Password { get; set; }
        public int _PersonID { get; set; }
        public string _FullName { get; set; }
        public bool _IsActive { get; set; }


    }
}
