using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrintManagementSystem_Sapunov.Classes
{
    public class TypeOpertation
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public TypeOpertation(int id, string name, string description) 
        {
            this.id = id;
            this.name = name;
            this.description = description;
        }

        public static List<TypeOpertation> AllTypeOperation() 
        {
            List<TypeOpertation> allTypeOperation = new List<TypeOpertation>();

            allTypeOperation.Add(new TypeOpertation(1, "Печать", ""));
            allTypeOperation.Add(new TypeOpertation(2, "Копия", ""));
            allTypeOperation.Add(new TypeOpertation(3, "Сканирование", ""));
            allTypeOperation.Add(new TypeOpertation(4, "Ризограф", ""));

            return allTypeOperation;
        }
    }
}
