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
        public int Id { get; set; } 
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


    // Used for tracking the workout sessions 
    public class WorkoutSession
    {
        public int Id { get; set; }
        public string TemplateName { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public string Notes { get; set; }
        public int DurationMinutes { get; set; }

        public virtual ICollection<CompletedExercise> CompletedExercises { get; set; } = new List<CompletedExercise>();
    }

    public class CompletedExercise
    {
        public int Id { get; set; }
        public string ExerciseName { get; set; }
        public int Sets { get; set; }
        public int Reps { get; set; }
        public bool IsCompleted { get; set; }

        public int WorkoutSessionId { get; set; }
        public virtual WorkoutSession WorkoutSession { get; set; }
    }


}
