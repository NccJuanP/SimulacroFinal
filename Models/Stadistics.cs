using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pdf.Models
{
    public class Stadistics
    {
        public int Id { get; set; }
        public int EventId { get; set; }
        public int ParticipantsCount { get; set; }
        public int AttendanceCount { get; set; }
    }
}