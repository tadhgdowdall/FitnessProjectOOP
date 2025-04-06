using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessProjectOOP
{
    public class Exercise
    {
        public string bodyPart { get; set; }
        public string equipment { get; set; }
        public string name { get; set; }
        public string target { get; set; }

        // Override ToString() for better display
        public override string ToString()
        {
            return $"{name} ({equipment})";
        }
    }
}
