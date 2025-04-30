using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessProjectOOP
{

    /// <summary>
    /// This is for the api call
    /// </summary>

    public class Exercise
    {
        public string bodyPart { get; set; }
        public string equipment { get; set; }
        public string name { get; set; }
        public string target { get; set; }

        // For the database
        public int WorkoutTemplateId { get; set; }
        public virtual WorkoutTemplate WorkoutTemplate { get; set; }

        // Override ToString() for better display
        public override string ToString()
        {
            return $"{name} ({equipment})";
        }
    }
}
