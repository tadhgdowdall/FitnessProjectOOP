using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

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

        public string TemplateName => txtTemplateName.Text;
        public string SelectedMuscleGroup => cmbMuscleGroups.SelectedItem?.ToString();
        public ObservableCollection<TemplateExercise> Exercises { get; } = new ObservableCollection<TemplateExercise>();

        public CreateTemplate()
        {
            InitializeComponent();
            dgExercises.ItemsSource = Exercises;
        }

        private void cmbMuscleGroups_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbMuscleGroups.SelectedItem == null) return;

            // Enable exercise selection
            cmbExercises.IsEnabled = true;
            btnAddExercise.IsEnabled = true;

            // Clear previous exercises
            cmbExercises.Items.Clear();

            // Load exercises for selected muscle group
            // Replace this with your actual exercise loading logic
            switch (SelectedMuscleGroup)
            {
                case "Back":
                    cmbExercises.Items.Add(new { Name = "Pull-ups" });
                    cmbExercises.Items.Add(new { Name = "Lat Pulldown" });
                    cmbExercises.Items.Add(new { Name = "Bent-over Rows" });
                    break;
                case "Chest":
                    cmbExercises.Items.Add(new { Name = "Bench Press" });
                    cmbExercises.Items.Add(new { Name = "Push-ups" });
                    cmbExercises.Items.Add(new { Name = "Chest Fly" });
                    break;
                    // Add other muscle groups...
            }

            cmbExercises.DisplayMemberPath = "Name";
            cmbExercises.SelectedIndex = 0;
        }

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
            }
        }

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

            DialogResult = true;
            Close();
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}