using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace FitnessProjectOOP
{
    /// <summary>
    /// Interaction logic for WorkoutSession.xaml
    /// </summary>
    public partial class WorkoutSessionWindow : Window
    {
        public List<CompletedExercise> CompletedExercises { get; set; } = new List<CompletedExercise>();


        private string templateName;

        public WorkoutSessionWindow(WorkoutTemplate template)
        {
            InitializeComponent();

            templateName = template.Name;
            txtTemplateName.Text = templateName;

            // Convert WorkoutExercise to CompletedExercise
            CompletedExercises = template.Exercises
                .Select(e => new CompletedExercise
                {
                    ExerciseName = e.ExerciseName,
                    Sets = e.Sets,
                    Reps = e.Reps,
                    IsCompleted = false
                }).ToList();

            exerciseList.ItemsSource = CompletedExercises;
        }

        private void FinishAndSave_Click(object sender, RoutedEventArgs e)
        {
            if (!int.TryParse(txtDuration.Text, out int duration) || duration < 0)
            {
                MessageBox.Show("Please enter a valid workout duration.");
                return;
            }

            int completedCount = CompletedExercises.Count(ex => ex.IsCompleted);
            string notes = txtNotes.Text;

            MessageBox.Show(
                $"Workout complete!\n\n" +
                $"Exercises completed: {completedCount}/{CompletedExercises.Count}\n" +
                $"Duration: {duration} minutes\n" +
                $"Notes: {notes}",
                "Session Summary",
                MessageBoxButton.OK,
                MessageBoxImage.Information
            );

            this.Close();
        }


    }
}
