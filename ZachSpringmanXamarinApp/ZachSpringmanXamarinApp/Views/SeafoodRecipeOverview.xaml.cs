using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ZachSpringmanXamarinApp.Models;
using Rg.Plugins.Popup.Services; // Import the Popup Services namespace

namespace ZachSpringmanXamarinApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class RecipeOverview : ContentPage
	{
		public RecipeOverview ()
		{
			InitializeComponent ();

			//LabelCount.Text = "Hello from Code Behind!";
			//Example of accessing a label in the code behind and changing the text value
		}

        private async void Frame_Tapped(object sender, System.EventArgs e)
        {
            // Get the tapped Frame
            Frame tappedFrame = (Frame)sender;

            // Get the data context of the tapped Frame (which should be a Recipe object)
            Recipe tappedRecipe = (Recipe)tappedFrame.BindingContext;

            // Create a new instance of your popup page (e.g., RecipePopupPage)
            var popupPage = new RecipePopupPage(tappedRecipe.strMeal);

            // Show the popup page as a modal and await its result
            await PopupNavigation.Instance.PushAsync(popupPage);

        }

        


    }
}