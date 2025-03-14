using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessProjectOOP
{



    public class WorkoutTemplate
    {
        // Workout Template Properties 

        public string Name { get; set; }
        public string Description { get; set; }

        public int Sets { get; set; } // Number of sets
        public int Reps { get; set; } // Number of repetitions per set
        //public List<Exercise> Exercises { get; set; } 
        // Don't have an excercise class yet 


    }
}
