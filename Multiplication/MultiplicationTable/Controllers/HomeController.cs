using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MultiplicationTable.Models;

namespace MultiplicationTable.Controllers
{
    public class HomeController : Controller
    {
        private void GetOperation(int A, int B, int Variant, out string Operation, out int? TrueValue)
        {
            int C = A * B;
            switch (Variant)
            {
                case 0:
                    Operation = A + " * " + B + " = ";
                    TrueValue = C;
                    break;

                case 1:
                    Operation = B + " * " + A + " = ";
                    TrueValue = C;
                    break;

                case 2:
                    Operation = C + " / " + A + " = ";
                    TrueValue = B;
                    break;

                case 3:
                    Operation = C + " / " + B + " = ";
                    TrueValue = A;
                    break;

                default:
                    Operation = null;
                    TrueValue = null;
                    break;
            }
        }
        private void RandomOperation(Random Rnd, Session Model, out string Operation, out int? TrueValue)
        {
            //случайно выбираем A
            int A = Model.MultiplierList[Rnd.Next(Model.MultiplierList.Count)];
            //случайно выбираем B
            int B = Rnd.Next(1, Model.UpperBound + 1);
            //случайно выбираем операцию
            int Variant = Rnd.Next(4);
            GetOperation(A, B, Variant, out Operation, out TrueValue);
        }
        private List<Exercise> GenerateList(Session Model)
        {
            List<Exercise> List = new List<Exercise>();
            Random Rnd = new Random();
            for (int i = 0; i < Model.Amount; i++)
            {
                string Operation;
                int? TrueValue;
                RandomOperation(Rnd, Model, out Operation, out TrueValue);
                List.Add(new Exercise(Operation, TrueValue));
            }
            return List;
        }

        [HttpGet]
        public ActionResult Results()
        {
            List<Exercise> List = (List<Exercise>)Session["List"];
            ViewBag.List = List;
            ViewBag.TrueValuesPercantage = (double)Session["TrueValuesCount"] / List.Count;
            return View();
        }

        [HttpGet]
        public ActionResult Exercises()
        {
            List<Exercise> List = (List<Exercise>)Session["List"];
            ViewBag.Operation = List[(int)Session["CurrentExercise"]].Operation;
            return View(); 
        }

        [HttpPost]
        public ActionResult Exercises(int? CurrentValue = null)
        {
            List<Exercise> List = (List<Exercise>)Session["List"];
            int CurrentExercise = (int)Session["CurrentExercise"];
            if (CurrentValue == List[CurrentExercise].TrueValue)
                Session["TrueValuesCount"] = (double)Session["TrueValuesCount"] + 1;
            List[CurrentExercise].CurrentValue = CurrentValue;
            Session["CurrentExercise"] = ++CurrentExercise;
            ViewBag.TrueAnswersPercantage = (double)0;
            if (CurrentExercise < List.Count)
                return RedirectToAction("Exercises");
            else
                return RedirectToAction("Results");
        }

        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(string StrMultiplierList, Session Model)
        {
            try
            {
                Model.MultiplierList = StrMultiplierList.Split(',').Select(x => int.Parse(x)).ToList();
            }
            catch
            {
                return RedirectToAction("Index");
            }
            List<Exercise> List = GenerateList(Model);
            Session["List"] = List;
            Session["CurrentExercise"] = 0;
            Session["TrueValuesCount"] = (double)0;
            return RedirectToAction("Exercises");
        } 
    }
}