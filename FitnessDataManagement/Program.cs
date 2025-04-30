using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitnessProjectOOP;

namespace FitnessDataManagement
{
     class Program
    {
        static void Main(string[] args)
        {

            FitnessDbContext db = new FitnessDbContext();

            using (db)
            {

                // Create test data
                var chestWorkout = new WorkoutTemplate
                {
                    Name = "Upper Body Workout",
                    MuscleGroup = "Chest",
                    Exercises = new List<WorkoutExercise>
                    {
                        new WorkoutExercise { ExerciseName = "Bench Press", Sets = 4, Reps = 8 },
                        new WorkoutExercise { ExerciseName = "Incline Press", Sets = 3, Reps = 10 }
                    }
                };

                var backWorkout = new WorkoutTemplate
                {
                    Name = "Pull Day",
                    MuscleGroup = "Back",
                    Exercises = new List<WorkoutExercise>
                    {
                        new WorkoutExercise { ExerciseName = "Pull-ups", Sets = 4, Reps = 6 },
                        new WorkoutExercise { ExerciseName = "Bent-over Rows", Sets = 3, Reps = 10 }
                    }
                };



                db.WorkoutTemplates.Add(chestWorkout);
                db.WorkoutTemplates.Add(backWorkout);

                db.SaveChanges();

                Console.WriteLine("Workout templates seeded successfully!");

            }

        }
    }
}
