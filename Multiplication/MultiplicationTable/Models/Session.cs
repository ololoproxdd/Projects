using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MultiplicationTable.Models
{
    public class Session
    {
        //список множителей
        public List<int> MultiplierList { get; set; }

        //верхняя граница второго множителя
        public int UpperBound { get; set; }

        //количество упражнений в сессии
        public int Amount { get; set; }
    }
}