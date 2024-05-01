using MvvmHelpers;
using MvvmHelpers.Commands;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using ZachSpringmanXamarinApp.Models;
//using Xamarin.Forms;

namespace ZachSpringmanXamarinApp.ViewModels
{
    public class SeafoodOverviewViewModel : ViewModelBase
    {
        public ObservableRangeCollection<Recipe> Recipe { get; set; }
        public ObservableRangeCollection<Grouping<string, Recipe>> RecipeGroups { get; set; } //allows to have a "list of lists" with a key
        public AsyncCommand RefreshCommand { get; }
        public SeafoodOverviewViewModel()
        {
            Title = "Seafood Recipes";

            Recipe = new ObservableRangeCollection<Recipe>();
            RecipeGroups = new ObservableRangeCollection<Grouping<string, Recipe>>();

            //-------------BELOW IS THE MANUAL WAY I ADDED RECIPES TO TEST... I MADE A FUNCTION THAT CALLS THE API TO GRAB ALL SEAFOOD RECIPES

            //var image = "https://www.themealdb.com//images//media//meals//ustsqw1468250014.jpg"; //just an image of pasta

            //Recipe.Add(new Recipe { idMeal = 1, strMeal = "meal one", strArea = "area one", strCategory = "category one", strMealThumb = image });
            //Recipe.Add(new Recipe { idMeal = 2, strMeal = "meal two", strArea = "area two", strCategory = "category two", strMealThumb = image });
            //Recipe.Add(new Recipe { idMeal = 3, strMeal = "meal three", strArea = "area three", strCategory = "category three", strMealThumb = image });
            //Recipe.Add(new Recipe { idMeal = 4, strMeal = "meal four", strArea = "area four", strCategory = "category four", strMealThumb = image });
            //Recipe.Add(new Recipe { idMeal = 5, strMeal = "meal five", strArea = "area five", strCategory = "category five", strMealThumb = image });
            //Recipe.Add(new Recipe { idMeal = 6, strMeal = "meal six", strArea = "area six", strCategory = "category six", strMealThumb = image });
            //Recipe.Add(new Recipe { idMeal = 7, strMeal = "meal seven", strArea = "area seven", strCategory = "category seven", strMealThumb = image });

            //RecipeGroups.Add(new Grouping<string, Recipe>("meal one", new[]));

            FillData();
            RefreshCommand = new AsyncCommand(Refresh);
        }

        async Task Refresh()
        {
            IsBusy = true;
            await Task.Delay(2000);
            IsBusy = false;
        }

        async Task FillData()
        {
            var httpClient = new HttpClient();
            var apiUrl = "https://www.themealdb.com/api/json/v1/1/filter.php?c=Seafood";
           
            try
            {
                var response = await httpClient.GetAsync(apiUrl);
                response.EnsureSuccessStatusCode(); // Throw if not successful
                var jsonString = await response.Content.ReadAsStringAsync();
                var apiResponse = JsonConvert.DeserializeObject<ApiResponse>(jsonString);
                // Clear existing recipes
                Recipe.Clear();

                // Add recipes from API response
                foreach (var meal in apiResponse.Meals)
                {
                    Recipe.Add(new Recipe
                    {
                        idMeal = meal.idMeal,
                        strMeal = meal.strMeal,
                        strArea = meal.strArea,
                        strCategory = meal.strCategory,
                        strMealThumb = meal.strMealThumb
                    });
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
            


        
    }
}
