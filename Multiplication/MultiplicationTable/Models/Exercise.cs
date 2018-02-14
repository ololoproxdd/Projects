using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MultiplicationTable.Models
{
    public class Exercise
    {
        public Exercise(string operation, int? trueValue)
        {
            Operation = operation;
            TrueValue = trueValue;
        }

        //Операция 
        public string Operation { get; set; }
        
        //Правильный ответ
        public int? TrueValue { get; set; }  

        //Ответ пользователя
        public int? CurrentValue { get; set; }
    }
}