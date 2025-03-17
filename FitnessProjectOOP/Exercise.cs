using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessProjectOOP
{
    public class Exercise
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string MuscleGroup { get; set; } // e.g., Biceps, Chest, Legs
        public string Equipment { get; set; }  // e.g., Dumbbells, Barbell, Bodyweight
    }
}
