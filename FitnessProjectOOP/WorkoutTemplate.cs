using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessProjectOOP
{



    public class WorkoutTemplate
    {
        // Workout Template Properties 
        public int Id { get; set; }
        public string Name { get; set; }
        public string ExerciseName { get; set; }
        public string MuscleGroup { get; set; }
        public virtual ICollection<WorkoutExercise> Exercises { get; set; } = new List<WorkoutExercise>();


        public int Sets { get; set; } // Number of sets
        public int Reps { get; set; } // Number of repetitions per set
      


    }


    public class WorkoutExercise
    {
        public int Id { get; set; } // Required for EF primary key
        public string ExerciseName { get; set; }
        public int Sets { get; set; }
        public int Reps { get; set; }

        public int WorkoutTemplateId { get; set; }
        public virtual WorkoutTemplate WorkoutTemplate { get; set; }
    }


    //For the code first database management

    public class FitnessDbContext : DbContext
    {
        public DbSet<WorkoutTemplate> WorkoutTemplates { get; set; }
        public DbSet<WorkoutExercise> WorkoutExercises { get; set; }
    }

}
