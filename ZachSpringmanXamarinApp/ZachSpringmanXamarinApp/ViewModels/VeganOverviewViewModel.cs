using MvvmHelpers.Commands;
using MvvmHelpers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using ZachSpringmanXamarinApp.Models;

namespace ZachSpringmanXamarinApp.ViewModels
{
    class VeganOverviewViewModel : ViewModelBase
    {
        public ObservableRangeCollection<Recipe> Recipe { get; set; }
        public ObservableRangeCollection<Grouping<string, Recipe>> RecipeGroups { get; set; } //allows to have a "list of lists" with a key
        public AsyncCommand RefreshCommand { get; }
        public VeganOverviewViewModel()
        {
            Title = "Vegan Recipes";

            Recipe = new ObservableRangeCollection<Recipe>();
            RecipeGroups = new ObservableRangeCollection<Grouping<string, Recipe>>();

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
            var apiUrl = "https://www.themealdb.com/api/json/v1/1/filter.php?c=Vegan";

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
