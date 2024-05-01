using System;
using System.Collections.Generic;
using System.Text;

namespace ZachSpringmanXamarinApp.Models
{
    public class ApiResponse
    {
        public List<Meal> Meals { get; set; }
    }

    public class Meal
    {
        //API LINK - https://www.themealdb.com/api.php
        public int idMeal { get; set; }
        public string strMeal { get; set; }
        public string strArea { get; set; }
        public string strCategory { get; set; }
        public string strMealThumb { get; set; }
        public string strInstructions { get; set; }
    }
}
