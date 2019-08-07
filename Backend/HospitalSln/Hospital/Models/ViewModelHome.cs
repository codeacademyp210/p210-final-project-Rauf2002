using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Hospital.Models
{
    public class ViewModelHome
    {
        public Settings Setting { get; set; }
        public Admin Admin { get; set; }
        public List<Gallery> Galleries { get; set; }
        public List<Messages> Messages { get; set; }
        public List<Departments> Departments { get; set; }
        public List<Appointments> Appointments { get; set; }
        public List<Doctors> Doctors { get; set; }
        public List<Nurses> Nurses { get; set; }
        public List<Allergies> Allergies { get; set; }
        public List<Vaccines> Vaccines{ get; set; }
        public List<Patients> Patients { get; set; }

    }
}