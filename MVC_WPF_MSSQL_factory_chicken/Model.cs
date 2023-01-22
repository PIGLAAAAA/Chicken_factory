using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MVC_WPF_MSSQL_factory_chicken
{
    public class Chicken
    {
       public int  Id { get; set; }
       public int Age { get; set; }
       public float Weight { get; set; }
       public string kind { get; set; }
       public int AgeMonth { get; set; }
       public virtual Chicken_Factory Chicken_Factory{ get; set; }
    }



    public class Chicken_Factory
    {
        public int Id { get; set; }
        public DateTime This_Date { get; set; }
        public bool Egg_true_or_false { get; set; }

        public virtual ICollection<Chicken> Chickens { get; set;  }
        public virtual ICollection<Person> Persons { get; set; }
    }





    public class Person
    {
        public int Id{ get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }  
        public int money { get; set; }
        public virtual Chicken_Factory Chicken_Factory { get; set; }
    }
}
