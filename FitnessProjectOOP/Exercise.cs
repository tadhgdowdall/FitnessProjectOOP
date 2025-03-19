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
        public string gifUrl { get; set; }
        public string id { get; set; }
        public string name { get; set; }
        public string target { get; set; }
        public List<string> secondaryMuscles { get; set; }
        public List<string> instructions { get; set; }
    }
}
