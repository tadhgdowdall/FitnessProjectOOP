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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FitnessProjectOOP
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        private List<WorkoutTemplate> _workoutTemplates = new List<WorkoutTemplate>();


        public MainWindow()
        {

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

        private void Window_Loaded_1(object sender, RoutedEventArgs e)
        {

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
                    Description = createTemplate.Description,
                    Exercises = createTemplate.Exercises.ToList()
                };

                // Refresh your UI ListBox/DataGrid
                lbxWorkoutTemplate.ItemsSource = null;
                lbxWorkoutTemplate.ItemsSource = _workoutTemplates;
                MessageBox.Show("Template created successfully!");
            }
        }
    }
}
