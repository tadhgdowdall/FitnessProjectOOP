using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace FitnessProjectOOP
{
    public partial class CreateTemplate : Window
    {
        // Class to hold exercise details in the template
        public class TemplateExercise
        {
            public string Name { get; set; }
            public int Sets { get; set; }
            public int Reps { get; set; }
            public string ExerciseId { get; set; } // If you need to reference original exercise
        }

        // Public properties to access the created template
        public string TemplateName => txtTemplateName.Text;
        public string Description => txtDescription.Text;
        public ObservableCollection<TemplateExercise> Exercises { get; } = new ObservableCollection<TemplateExercise>();

        public CreateTemplate()
        {
            InitializeComponent();
            dgExercises.ItemsSource = Exercises;
            LoadExercises(); // Load available exercises
        }

        private void LoadExercises()
        {
            // Replace this with your actual exercise loading logic
            cmbExercises.Items.Add(new { Name = "Push-ups", Id = "1" });
            cmbExercises.Items.Add(new { Name = "Pull-ups", Id = "2" });
            cmbExercises.Items.Add(new { Name = "Squats", Id = "3" });
            cmbExercises.DisplayMemberPath = "Name";
        }

        private void btnAddExercise_Click(object sender, RoutedEventArgs e)
        {
            if (cmbExercises.SelectedItem == null)
            {
                MessageBox.Show("Please select an exercise");
                return;
            }

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
                ExerciseId = selectedExercise.Id,
                Sets = sets,
                Reps = reps
            });

            // Reset inputs
            txtSets.Text = "3";
            txtReps.Text = "10";
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