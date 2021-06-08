using System;
using System.Collections.Generic;
using System.Text;


namespace InheritanceTask
{
    public class Company 
    {
       
        private readonly Employee[] employees;
        

        public Company(Employee[] employees)        
        {
         this.employees = employees;
        }

        public void GiveEverybodyBonus(decimal companyBonus)          
        {
            for (int i = 0; i < employees.Length; i++) {
                employees[i].SetBonus(companyBonus);
            }
        }

        public decimal TotalToPay()
        {
            decimal sum = 0;
            for (int i = 0; i < employees.Length; i++)
            {
               sum += employees[i].ToPay();
            }
            return sum;
        }

        public string NameMaxSalary()
        {
            decimal max = 0;
            int index = 0;
            for (int i = 0; i < employees.Length; i++)
            {
                if (max < employees[i].ToPay()) {
                    max = employees[i].ToPay();
                    index = i;
                }
                
            }
            return employees[index].Name;
        }   
    }
}
