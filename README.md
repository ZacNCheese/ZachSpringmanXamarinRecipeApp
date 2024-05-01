This is my first attempt at a Xamarin application with the MVVM structure.

I tested on an android simulator that simulated the Pixel 6.

It is a basic recipe catalog using the following free online API: https://www.themealdb.com/api.php.

There is a home page, and then on the left hand side you can open up the hamburger menu and navigate to different food types.

On the actual food type pages you can scroll through and select any recipe. Once a recipe is selected - the instructions will pop up on the screen.

This application is very simple, and only contains two different models: one to store the recipe, and one to store the API results.

The ViewModels are used to communicate to the views and display the recipes for each given category.

On the view side I have a backend action attached to each card that grabs the instructions based on the name of meal that was selected. This helps with performance as it is only grabbing one set of instructions when it is needed instead of grabbing ALL of the instructions upon the intial API request.
