﻿using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;

namespace FitnessProjectOOP
{
    public partial class CreateTemplate : Window
    {
        public class TemplateExercise
        {
            public string Name { get; set; }
            public int Sets { get; set; }
            public int Reps { get; set; }
        }


        public ObservableCollection<TemplateExercise> Exercises { get; } = new ObservableCollection<TemplateExercise>(); // Creates a collection of exercises for the current template


        public WorkoutTemplate CreatedTemplate { get; private set; } 

        public string TemplateName => txtTemplateName.Text; // Returns Name of template
        public string SelectedMuscleGroup => (cmbMuscleGroups.SelectedItem as ComboBoxItem).Content.ToString(); // gets the selected musclegroup from combo box

        public CreateTemplate()
        {
            InitializeComponent();
            dgExercises.ItemsSource = Exercises;
        }



        private void cmbMuscleGroups_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbMuscleGroups.SelectedItem == null) return;
            else
            {

            }

            // Enable exercise selection controls
            cmbExercises.IsEnabled = true;
            btnAddExercise.IsEnabled = true;
            cmbExercises.Items.Clear();

            // Load exercises for selected muscle group
            switch (SelectedMuscleGroup)
            {
                case "Back":
                    cmbExercises.Items.Add(new { Name = "Pull-ups" });
                    cmbExercises.Items.Add(new { Name = "Lat Pulldown" });
                    cmbExercises.Items.Add(new { Name = "Bent-over Rows" });
                    cmbExercises.Items.Add(new { Name = "Deadlifts" });
                    cmbExercises.Items.Add(new { Name = "T-Bar Rows" });
                    cmbExercises.Items.Add(new { Name = "Seated Cable Rows" });
                    break;

                case "Chest":
                    cmbExercises.Items.Add(new { Name = "Bench Press" });
                    cmbExercises.Items.Add(new { Name = "Push-ups" });
                    cmbExercises.Items.Add(new { Name = "Chest Fly" });
                    cmbExercises.Items.Add(new { Name = "Incline Press" });
                    cmbExercises.Items.Add(new { Name = "Decline Press" });
                    cmbExercises.Items.Add(new { Name = "Cable Crossovers" });
                    break;

                case "Shoulders":
                    cmbExercises.Items.Add(new { Name = "Overhead Press" });
                    cmbExercises.Items.Add(new { Name = "Lateral Raises" });
                    cmbExercises.Items.Add(new { Name = "Front Raises" });
                    cmbExercises.Items.Add(new { Name = "Rear Delt Flyes" });
                    cmbExercises.Items.Add(new { Name = "Shrugs" });
                    cmbExercises.Items.Add(new { Name = "Arnold Press" });
                    break;

                case "Arms":
                    cmbExercises.Items.Add(new { Name = "Bicep Curls" });
                    cmbExercises.Items.Add(new { Name = "Tricep Dips" });
                    cmbExercises.Items.Add(new { Name = "Hammer Curls" });
                    cmbExercises.Items.Add(new { Name = "Skull Crushers" });
                    cmbExercises.Items.Add(new { Name = "Preacher Curls" });
                    cmbExercises.Items.Add(new { Name = "Tricep Pushdowns" });
                    break;

                case "Legs":
                    cmbExercises.Items.Add(new { Name = "Squats" });
                    cmbExercises.Items.Add(new { Name = "Lunges" });
                    cmbExercises.Items.Add(new { Name = "Leg Press" });
                    cmbExercises.Items.Add(new { Name = "Deadlifts" });
                    cmbExercises.Items.Add(new { Name = "Leg Curls" });
                    cmbExercises.Items.Add(new { Name = "Calf Raises" });
                    break;

                case "Core":
                    cmbExercises.Items.Add(new { Name = "Plank" });
                    cmbExercises.Items.Add(new { Name = "Russian Twists" });
                    cmbExercises.Items.Add(new { Name = "Leg Raises" });
                    cmbExercises.Items.Add(new { Name = "Crunches" });
                    cmbExercises.Items.Add(new { Name = "Hanging Knee Raises" });
                    cmbExercises.Items.Add(new { Name = "Cable Woodchoppers" });
                    break;
            }

            cmbExercises.DisplayMemberPath = "Name";
            cmbExercises.SelectedIndex = 0; // Auto-select first exercise
        }

        //Adds exercise to your template
        private void btnAddExercise_Click(object sender, RoutedEventArgs e)
        {

          

            if (cmbExercises.SelectedItem == null) return;

            if (!int.TryParse(txtSets.Text, out int sets) || sets <= 0)
            {
                MessageBox.Show("Please enter valid number of sets");
                return;
            }

            if (!int.TryParse(txtReps.Text, out int reps) || reps <= 0)
            {
                MessageBox.Show("Please enter valid number of reps");
                return;
            }

            dynamic selectedExercise = cmbExercises.SelectedItem;
            Exercises.Add(new TemplateExercise
            {
                Name = selectedExercise.Name,
                Sets = sets,
                Reps = reps
            });
        }

        private void RemoveExercise_Click(object sender, RoutedEventArgs e)
        {
            if (dgExercises.SelectedItem is TemplateExercise exercise)
            {
                Exercises.Remove(exercise);


                // Try catch to try and find and delete data from the database

                try
                {
                    using (var db = new FitnessDbContext())
                    {
                        var existing = db.WorkoutExercises.FirstOrDefault(ex =>
                        ex.ExerciseName == exercise.Name &&
                        ex.Sets == exercise.Sets &&
                            ex.Reps == exercise.Reps);

                        if (existing != null)
                        {
                            db.WorkoutExercises.Remove(existing);
                            db.SaveChanges();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Error deleting exercise: {ex.Message}");
                }
            }


       

        }


        // Button To Save Template
        private void btnSave_Click(object sender, RoutedEventArgs e)
        {



            if (string.IsNullOrWhiteSpace(TemplateName))
            {
                MessageBox.Show("Please enter a template name");
                return;
            }

            if (string.IsNullOrEmpty(SelectedMuscleGroup))
            {
                MessageBox.Show("Please select a muscle group");
                return;
            }

            if (Exercises.Count == 0)
            {
                MessageBox.Show("Please add at least one exercise");
                return;
            }

            // Create and add the new workout template
            CreatedTemplate = new WorkoutTemplate
            {
                Name = TemplateName,
                MuscleGroup = SelectedMuscleGroup,
                Exercises = Exercises.Select(ex => new WorkoutExercise
                {
                    ExerciseName = ex.Name,
                    Sets = ex.Sets,
                    Reps = ex.Reps

                }).ToList()
            };

            // Save to database
            try
            {
                using (var db = new FitnessDbContext())
                {
                    db.WorkoutTemplates.Add(CreatedTemplate);
                    db.SaveChanges();
                }

                MessageBox.Show("Workout template saved to database!");
                Console.WriteLine("Workout template is saved to database");
                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error saving template: {ex.Message}");
            }

        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}