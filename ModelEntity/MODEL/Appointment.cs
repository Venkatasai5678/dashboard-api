using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelEntity.MODEL
{
    public class Appointment
    {
        public int AppointmentId { get; set; }

        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        public int PatientId { get; set; }
        public Patient Patient { get; set; }

        public DateTime AppointmentDate { get; set; }
        public string Status { get; set; } // Booked, Completed

        public string DoctorNotes { get; set; } // Diagnosis / Prescription
    }

}
