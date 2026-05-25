using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD.Domain
{
    public class clsPeople
    {
        public enum enMode { AddNew, Update };
        public enMode _Mode = enMode.AddNew;
        public int _PersonID { get; set; }
        public string _NationalNo { get; set; }
        public int _NationalityCountryID { get; set; }
        public string _FirstName { get; set; }
        public string _SecondName { get; set; }
        public string _ThirdName { get; set; }
        public string _LastName { get; set; }
        public string _Address { get; set; }
        public string _Phone { get; set; }
        public string _Email { get; set; }
        public string _ImagePath { get; set; }
        public string _Gender { get; set; }

        public DateTime _DateOfBirth = DateTime.Now;

        public string FullName
        {
            get { return _FirstName + " " + _SecondName + " " + _ThirdName + " " + _LastName; }

        }


    }
}
