using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DVLD.Domain
{
    public class clsDrivers
    {
        public int Person_ID { get; set; }
        public string National_No { get; set; }
        public int CreatedByUser_ID { get; set; }
        public string Full_Name { get; set; }
        public int Driver_ID { get; set; }
        public int NumberOf_ActiveLicenses { get; set; }

        public DateTime Created_Date = DateTime.Now;



    }
}
