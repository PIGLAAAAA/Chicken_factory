using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Media.Media3D;

namespace MVC_WPF_MSSQL_factory_chicken
{
    public  class Controller
    {
        MyDbContext Context { get; set; }

        public Controller (MyDbContext context)
        {
            Context = context;
        }



        public void WeightAndAgeClick(TextBox AgeTextBox, TextBox Weight , DataGrid ChickenGrid)
        {
            int agetextbox = Convert.ToInt32(AgeTextBox.Text);
            float weight = float.Parse(Weight.Text);
            var weightandage = Context.Chickens.Where(x => x.Age == agetextbox).Where(y => y.Weight == weight);


            ClearDataGrid(ChickenGrid);


            DataGridTextColumn textColumn = new DataGridTextColumn();
            textColumn.Header = "Порода";
            textColumn.Binding = new Binding("kind");
            ChickenGrid.Columns.Add(textColumn);

            DataGridTextColumn textColumn3 = new DataGridTextColumn();
            textColumn3.Header = "Возраст";
            textColumn3.Binding = new Binding("Age");
            ChickenGrid.Columns.Add(textColumn3);

            DataGridTextColumn textColumn2 = new DataGridTextColumn();
            textColumn2.Header = "Вес/гр";
            textColumn2.Binding = new Binding("Weight");
            ChickenGrid.Columns.Add(textColumn2);


            foreach (var item in weightandage)
            {
                ChickenGrid.Items.Add(new Chicken() { kind = item.kind, Age = item.Age, Weight = item.Weight });
            }
        }

        public void CountEggClick(TextBox DateOne , TextBox DateTwo, DataGrid ChickenGrid , ref TextBox result)
        {
            DateTime beginDate = DateTime.Parse(DateOne.Text);
            DateTime lastDate = DateTime.Parse(DateTwo.Text);
            int costEgg = 25;
            var countEgg = Context.Chickens_Factory
                .Where(x => x.This_Date > beginDate)
                .Where(y => y.This_Date < lastDate)
                .Where(d => d.Egg_true_or_false == true).Count();
            result.Text = $"Получено яиц -{countEgg},Общая цена -{countEgg * costEgg}";
        }


        public void HowManyChickensEmployeeClick( ref DataGrid  ChickenGrid)
        {

            ClearDataGrid(ChickenGrid);
            using (var context = new MyDbContext())
            {

                var HowManyChickensEmployee = context.Chickens_Factory.Join(context.Persons, w => w.Id, b => b.Chicken_Factory.Id, (w, b) =>
                new
                {
                    b.FirstName,
                    b.SecondName,
                    w.Id

                });
                var result = HowManyChickensEmployee.ToList();


                DataGridTextColumn textColumn = new DataGridTextColumn();
                textColumn.Header = "Имя";
                textColumn.Binding = new Binding("FirstName");
                ChickenGrid.Columns.Add(textColumn);

                DataGridTextColumn textColumn3 = new DataGridTextColumn();
                textColumn3.Header = "Фамилия";
                textColumn3.Binding = new Binding("SecondName");
                ChickenGrid.Columns.Add(textColumn3);

                DataGridTextColumn textColumn2 = new DataGridTextColumn();
                textColumn2.Header = "Кол-во обслуживаемых куриц";

                textColumn2.Binding = new Binding("Id");
                ChickenGrid.Columns.Add(textColumn2);



                for (int i = 0; i < result.Count();)
                {
                    int count = 0;
                    for (int j = 0; j < result.Count(); j++)
                    {
                        if (result[i].SecondName == result[j].SecondName) { count++; }
                    }
                    ChickenGrid.Items.Add(new Person { FirstName = result[i].FirstName, SecondName = result[i].SecondName, Id = count });
                    i = i + count;
                }

            }
        }


        public void NumberOfCollectedEggs (ref DataGrid ChickenGrid)
        {
            ClearDataGrid(ChickenGrid);



            using (var context = new MyDbContext())
            {


                var NumberOfCollectedEggs = context.Chickens_Factory.Join(context.Persons, w => w.Id, b => b.Chicken_Factory.Id, (w, b) => new
                {
                    w.This_Date,
                    w.Id,
                    w.Egg_true_or_false,
                    b.FirstName,
                    b.SecondName

                });

                var NumberOfCollectedEggs2 = NumberOfCollectedEggs.Where(x => x.Egg_true_or_false == true).Distinct();
                var fullname = NumberOfCollectedEggs.Select(x => new
                {
                    FirstName = x.FirstName,
                    SecondName = x.SecondName

                });


                DataGridTextColumn textColumn = new DataGridTextColumn();
                textColumn.Header = "Имя";
                textColumn.Binding = new Binding("FirstName");
                ChickenGrid.Columns.Add(textColumn);

                DataGridTextColumn textColumn3 = new DataGridTextColumn();
                textColumn3.Header = "Фамилия";
                textColumn3.Binding = new Binding("SecondName");
                ChickenGrid.Columns.Add(textColumn3);

                DataGridTextColumn textColumn2 = new DataGridTextColumn();
                textColumn2.Header = "Кол-во собранных яиц";

                textColumn2.Binding = new Binding("Id");
                ChickenGrid.Columns.Add(textColumn2);



                var name = NumberOfCollectedEggs2.ToList();


                for (int i = 0; i < name.Count();)
                {
                    int count = 0;
                    for (int j = 0; j < name.Count(); j++)
                    {
                        if (name[i].SecondName == name[j].SecondName) { count++; }
                    }
                    ChickenGrid.Items.Add(new Person { FirstName = name[i].FirstName, SecondName = name[i].SecondName, Id = count });
                    i = i + count;
                }






            }
        }


        public void BiggerEggClick( ref TextBox BigEgg)
        {
            Context = new MyDbContext();
            
                List<Chicken> collectionChiken = new List<Chicken>();
                var maxegg = Context.Chickens.Select(x => x.AgeMonth);
                var result = Context.Chickens.Single(x => x.AgeMonth == maxegg.Max());
                BigEgg.Text = result.Id.ToString();


            
        }



        public void SmallEggClick(ref DataGrid ChickenGrid)
        {
            ClearDataGrid(ChickenGrid);

             Context = new MyDbContext();
            
                int count = 0;
                var chickersCollection = Context.Chickens;
                foreach (var item in chickersCollection)
                    count += item.AgeMonth;
                count = count / chickersCollection.Count();

                var result = chickersCollection.Where(x => x.AgeMonth < count);
                //ChickenGrid.ItemsSource = result.ToList();


                DataGridTextColumn textColumn = new DataGridTextColumn();
                textColumn.Header = "Яйценоскость";
                textColumn.Binding = new Binding("AgeMonth");
                ChickenGrid.Columns.Add(textColumn);

                DataGridTextColumn textColumn3 = new DataGridTextColumn();
                textColumn3.Header = "Возраст";
                textColumn3.Binding = new Binding("Age");
                ChickenGrid.Columns.Add(textColumn3);

                DataGridTextColumn textColumn2 = new DataGridTextColumn();
                textColumn2.Header = "Порода";

                textColumn2.Binding = new Binding("kind");
                ChickenGrid.Columns.Add(textColumn2);


                foreach (var item in result)
                {
                    ChickenGrid.Items.Add(new Chicken() { Age = item.Age, AgeMonth = item.AgeMonth, kind = item.kind });
                }


        }

        public void ButtonClick(TextBox age, TextBox kind , TextBox wt, TextBox agemonth)
        {
          
                var chicken = new Chicken()
                {
                    Age = int.Parse(age.Text),
                    kind = kind.Text,
                    Weight = float.Parse(wt.Text),
                    AgeMonth = int.Parse(agemonth.Text)
                };

                Context.Chickens.Add(chicken);
                Context.SaveChanges();
           
        }


        static private void ClearDataGrid(DataGrid ChickenGrid)
        {

            ChickenGrid.Columns.Clear();
            ChickenGrid.Items.Clear();
        }
    }
}
