using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TimerTracker.DataAcess.Models
{
    public class WorkHouers
    {
        public int Id { get; set; }

        public DateTime Date { get; set; }

        public int Housers { get; set; }

        public int Minutes { get; set; }

        public int ProjectID { get; set; }

        public Project Project { get; set; }

    }
}
