using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
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

        public readonly ObservableCollection<WorkoutTemplate> _workoutTemplates = new ObservableCollection<WorkoutTemplate>();

        private readonly ExerciseApi _exerciseApi = new ExerciseApi();

        private readonly string profileFilePath = "userprofile.json";
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

        private void BtnCreateTemplate_Click(object sender, RoutedEventArgs e)
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


            
                _workoutTemplates.Add(createTemplate.CreatedTemplate);
                MessageBox.Show("Template created successfully!");

            }

            }

        private void LoadSampleTemplates()
        {
            // Sample data which will  be used on load
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


            _workoutTemplates.Add(chestWorkout);
            _workoutTemplates.Add(backWorkout);

        }

        //private async void btnRecommendExercises_Click(object sender, RoutedEventArgs e)
        //{

        //    // Gets selected combobox item and puts the value into string name
        //    ComboBoxItem ComboItem = (ComboBoxItem)cbxRecommendedExcercises.SelectedItem;
        //    string name = ComboItem.Name;

        //    Console.WriteLine(name);

        //    ExerciseApi exerciseApi = new ExerciseApi();


        //    exerciseApi.BodyPart = name.ToLower();

        //    await exerciseApi.FetchExercisesAsync();







        //}


        // Uses API service to recommend exercise 
        private async void BtnRecommendExercises_Click_1(object sender, RoutedEventArgs e)
        {
            if (cbxRecommendedExcercises.SelectedItem == null)
            {
                MessageBox.Show("Please select a muscle group");
                return;
            }

            string selectedMuscleGroup = ((ComboBoxItem)cbxRecommendedExcercises.SelectedItem).Content.ToString();

            try
            {
                //  loading 
                lbxRecommendedExercises.ItemsSource = new List<string> { "Loading exercises..." };
                btnRecommendExercises.IsEnabled = false;
                lbxRecommendedExercises.Items.Refresh(); // Refreshes the UI 

                var exercises = await _exerciseApi.GetExercisesByBodyPartAsync(selectedMuscleGroup);

  
                if (exercises?.Count > 0)
                {
                    Debug.WriteLine($"First exercise: {exercises[0].name} | {exercises[0].equipment}");
                }

       

                    // Puts recommended exercises into listbox
                    lbxRecommendedExercises.ItemsSource = exercises;

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error fetching exercises: {ex.Message}");

            }
            finally
            {
                btnRecommendExercises.IsEnabled = true;
            }
        }


        // Deletes Template on Button Click
        private void BtnDeleteTemplate_Click(object sender, RoutedEventArgs e)
        {
            var selectedTemplates = lbxWorkoutTemplate.SelectedItems.Cast<WorkoutTemplate>().ToList();

            if (selectedTemplates.Count == 0)
            {
                MessageBox.Show("Please select a template to delete.");
                return;
            }

            foreach (var template in selectedTemplates)
            {
                _workoutTemplates.Remove(template);
            }

            MessageBox.Show("Template(s) deleted successfully.");
        }

        private void BtnStartWorkout_Click(object sender, RoutedEventArgs e)
        {
            if (lbxWorkoutTemplate.SelectedItem is WorkoutTemplate selectedTemplate)
            {
                // create instance of new window and pass in the selected item
                var sessionWindow = new WorkoutSessionWindow(selectedTemplate);  
                sessionWindow.Owner = this;
                sessionWindow.ShowDialog();
            }
            else
            {
                MessageBox.Show("Please select a workout template to start.");
            }
        }


        // Navigates Tabs From home Page
        private void BtnStartWorkoutHome_Click(object sender, RoutedEventArgs e)
        {

            TabControl.SelectedItem = TemplateManagementTab;

        }

        private void BtnManageProfile_Click(object sender, RoutedEventArgs e)
        {
            TabControl.SelectedItem = ProfileTab;
        }

        private void BtnExploreExercise_Click(object sender, RoutedEventArgs e)
        {
            TabControl.SelectedItem = ExerciseFinderTab;
        }

        private void BtnSaveProfile_Click(object sender, RoutedEventArgs e)
        {


            MessageBox.Show("Profile saved successfully.");
        }
    }
}
