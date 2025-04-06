using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FitnessProjectOOP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private ObservableCollection<WorkoutTemplate> _workoutTemplates = new ObservableCollection<WorkoutTemplate>();


        public MainWindow()
        {


            InitializeComponent();
            // Loading sample templates
            LoadSampleTemplates();

            lbxWorkoutTemplate.ItemsSource = _workoutTemplates;
        }

              // Event handler for Border_MouseDown
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove(); // Allows the window to be dragged
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

          
        }

        private void btnCreateTemplate_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Creating a new workout template");



        }

        private void LoadSampleTemplates()
        {
            // Sample data - replace with your actual loading logic
            var chestWorkout = new WorkoutTemplate
            {
                Name = "Chest Day",
                MuscleGroup = "Chest",
                Exercises = new List<WorkoutTemplate>
            {
                new WorkoutTemplate { ExerciseName = "Bench Press", Sets = 4, Reps = 8 },
                new WorkoutTemplate { ExerciseName = "Incline Press", Sets = 3, Reps = 10 }
            }
            };

            var backWorkout = new WorkoutTemplate
            {
                Name = "Back Blaster",
                MuscleGroup = "Back",
                Exercises = new List<WorkoutTemplate>
            {
                new WorkoutTemplate { ExerciseName = "Pull-ups", Sets = 4, Reps = 6 },
                new WorkoutTemplate { ExerciseName = "Bent-over Rows", Sets = 3, Reps = 10 }
            }
            };

            _workoutTemplates.Add(chestWorkout);
            _workoutTemplates.Add(backWorkout);
        }

        private async void btnRecommendExercises_Click(object sender, RoutedEventArgs e)
        {

            // Gets selected combobox item and puts the value into string name
            ComboBoxItem ComboItem = (ComboBoxItem)cbxRecommendedExcercises.SelectedItem;
            string name = ComboItem.Name;

            Console.WriteLine(name);

            ExerciseApi exerciseApi = new ExerciseApi();


            exerciseApi.BodyPart = name.ToLower();

            await exerciseApi.FetchExercisesAsync();







        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var createTemplate = new CreateTemplate();
            
            if (createTemplate.ShowDialog() == true)
            {
                // Create the template object
                var newTemplate = new
                {
                    Name = createTemplate.TemplateName,
                    MuscleGroup = createTemplate.SelectedMuscleGroup,
                    Exercises = createTemplate.Exercises.Select(ex => new WorkoutTemplate
                    {
                        Sets = ex.Sets,
                        Reps = ex.Reps
                    }).ToList()
                };

               
                lbxWorkoutTemplate.ItemsSource = null;
                lbxWorkoutTemplate.ItemsSource = _workoutTemplates;
                MessageBox.Show("Template created successfully!");
            }
        }
    }
}
