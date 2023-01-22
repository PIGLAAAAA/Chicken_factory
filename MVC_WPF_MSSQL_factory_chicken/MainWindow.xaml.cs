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
using System.Xml.Linq;

namespace MVC_WPF_MSSQL_factory_chicken
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
           
        }

       
        private void SmallEggClick(object sender, RoutedEventArgs e)
        {
            var context = new MyDbContext();
            Controller controller = new Controller(context);
            controller.SmallEggClick(ref ChickenGrid);
        }

        private void BiggerEggClick(object sender, RoutedEventArgs e)
        {
            ChickenGrid.Columns.Clear();
            ChickenGrid.Items.Clear();

            var context = new MyDbContext();
            Controller controller = new Controller(context);
            controller.BiggerEggClick(ref BigEgg);
        }

        private void NumberOfCollectedEggs(object sender, RoutedEventArgs e)
        {
            ChickenGrid.Columns.Clear();
            ChickenGrid.Items.Clear();

            var context = new MyDbContext();
            Controller controller = new Controller(context);
            controller.NumberOfCollectedEggs(ref ChickenGrid); 

        }

        private void WeightAndAgeClick(object sender, RoutedEventArgs e)
        {
            if(AgeTextBox.Text.Length == 0 && Weight.Text.Length == 0 ) 
            {
                MessageBoxResult result = MessageBox.Show("Заполните поля веса и возраста!", "" , MessageBoxButton.OK );
                //  throw new ArgumentNullException("Поля должны быть заполнены!");
                return;
            }
            else if(!(int.TryParse(AgeTextBox.Text , out int result))  && !(float.TryParse(Weight.Text , out float result2) ) )
            {
                MessageBoxResult resull = MessageBox.Show("Заполните поля веса и возраста корректно!", "", MessageBoxButton.OK);
                //  throw new ArgumentNullException("Поля должны быть заполнены!");
                return;
            }

            var context = new MyDbContext();
            Controller controller = new Controller(context);
            controller.WeightAndAgeClick(AgeTextBox, Weight, ChickenGrid);

        }

        private void CountEggClick(object sender, RoutedEventArgs e)
        {
            var context = new MyDbContext();
            Controller controller = new Controller(context);
            controller.CountEggClick(DateOne,DateTwo,ChickenGrid, ref result);
        }

        private void HowManyChickensEmployeeClick(object sender, RoutedEventArgs e)
        {
            var context = new MyDbContext();
            Controller controller = new Controller(context);
            controller.HowManyChickensEmployeeClick(ref ChickenGrid);
        }



        private void Button_Click(object sender, RoutedEventArgs e)
        {

            var context = new MyDbContext();
            Controller controller = new Controller(context);
            controller.ButtonClick(age,kind,wt,agemonth);
            
        }
    }
}
