using Newtonsoft.Json;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZachSpringmanXamarinApp.Models;

namespace ZachSpringmanXamarinApp.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RecipePopupPage : PopupPage
    {
        public RecipePopupPage(string recipeName)
        {
            InitializeComponent();

            // Set the recipe name as the content of the Label
            recipeLabel.Text = recipeName;

            LoadInstructions(recipeName);
        }

        private async void LoadInstructions(string recipeName)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    // Construct the API URL with the recipe name
                    string url = $"https://www.themealdb.com/api/json/v1/1/search.php?s={Uri.EscapeDataString(recipeName)}";

                    // Make the GET request
                    var response = await client.GetAsync(url);

                    // Check if the request was successful
                    if (response.IsSuccessStatusCode)
                    {
                        // Read the response content
                        var content = await response.Content.ReadAsStringAsync();

                        // Deserialize the JSON response
                        var result = JsonConvert.DeserializeObject<ApiResponse>(content);

                        // Extract instructions from the response
                        string instructions = result?.Meals?[0]?.strInstructions;

                        // Display the instructions
                        // Update the instructions label on the UI thread
                        Device.BeginInvokeOnMainThread(() =>
                        {
                            instructionsLabel.Text = instructions;
                        });
                    }
                    else
                    {
                        instructionsLabel.Text = "Failed to fetch instructions.";
                    }
                }
            }
            catch (Exception ex)
            {
                instructionsLabel.Text = $"Error: {ex.Message}";
            }
        }

        private async void OnCloseButtonClicked(object sender, EventArgs e)
        {
            // Close the popup
            await PopupNavigation.Instance.PopAsync();
        }
    }
}