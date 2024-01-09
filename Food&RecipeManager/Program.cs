using System.Threading.Channels;
using Newtonsoft.Json;

namespace Food_RecipeManager
{
    internal class Program
    {
        static void Main(string[] args)
        {
           // store recipe, food
            Dictionary<int, Recipe> Recipes = new Dictionary<int, Recipe>();

            HashSet<Food> Foods = new HashSet<Food>();

            // Display a welcome message
            Console.WriteLine("==================================================");
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Console.WriteLine($"=============Food and Recipe Manager==============");
            Console.ResetColor();
            Console.WriteLine("==================================================");

            LoadData();
            DisplayMenu();


            // load foods/recipes from JSON file
            void LoadData()
            {
                try
                {
                    string foodJson = File.ReadAllText("foods.json");
                    IEnumerable<Food> foods = JsonConvert.DeserializeObject<IEnumerable<Food>>(foodJson);
                    Foods = new HashSet<Food>(foods);

                    string recipeJson = File.ReadAllText("recipes.json");
                    IEnumerable<Recipe> recipes = JsonConvert.DeserializeObject<IEnumerable<Recipe>>(recipeJson);
                    Recipes = new Dictionary<int, Recipe>();
                    foreach (var recipe in recipes)
                    {
                        Recipes.Add(recipe.id, recipe);
                    }
                }
                catch (IOException e)
                {
                    Console.WriteLine(e.Message);
                }
            }


            //display recipes
            void DisplayRecipes()
            {
                foreach (Recipe recipe in Recipes.Values)
                {
                    Console.WriteLine($" {recipe.id}| Name: {recipe.name}");
                    Console.WriteLine("--------------------------------------------------\n");
                }
            }

            void DisplayAllRecipe(string option)
            {
                foreach (KeyValuePair<int, Recipe> recipe in Recipes)
                {
                    if (recipe.Key.ToString() == option)
                    {
                        Console.ForegroundColor = ConsoleColor.DarkCyan;
                        Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (recipe.Value.name.Length / 2)) + "}", recipe.Value.name + " " + recipe.Value.prepTime+"min"));
                        string stars = "*************************";
                        Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (stars.Length / 2)) + "}", stars));

                        Console.WriteLine($"Ingredients: {string.Join("; ", recipe.Value.Ingredients)}");
                        Console.WriteLine($"Method: {recipe.Value.description}");
                        Console.ResetColor();
                    }
                }
                Console.WriteLine("Press any key to return to the main menu.");
                Console.ReadKey();
                Console.Clear();
                DisplayMenu();
            }

            //view food
            void ViewFood()
            {
                foreach (Food food in Foods.ToList())
                {
                    Console.WriteLine($"Name: {food.name}| Category: {food.category}| Calories: {food.calories}");
                    Console.WriteLine("--------------------------------------------------\n");
                }
                Console.WriteLine("Press any key to return to the main menu.");
                Console.ReadKey();
                Console.Clear();
                DisplayMenu();
            }

            //search for recipes
            void SearchRecipes(string ingredient)
            {
                foreach (Recipe recipe in Recipes.Values)
                {
                    if (recipe.Ingredients.Contains(ingredient))
                    {
                        Console.WriteLine($"{recipe.id}| Name: {recipe.name}");
                        Console.WriteLine("--------------------------------------------------\n");
                    }
                }
                Console.WriteLine("Press any key to return to the main menu.");
                Console.ReadKey();
                Console.Clear();
                DisplayMenu();
            }

            //search for food
            void SearchByCookingTime()
            {
                Console.WriteLine("Select an option:\n");
                Console.WriteLine("a.Short(<20min)");
                Console.WriteLine("b.Medium(20-30min)");
                Console.WriteLine("c.Long(+30min)");
                string input = Console.ReadLine();

                if(input == "a")
                {
                    IEnumerable<Recipe> shortPrepTime = Recipes.Values.Where(r => r.prepTime < 20);
                    foreach(Recipe recipe in shortPrepTime)
                    {
                        Console.WriteLine($"Name: {recipe.name}  |  PrepTime: {recipe.prepTime} min");
                    }

                }
                if(input == "b")
                {
                    IEnumerable<Recipe> mediumPrepTime = Recipes.Values.Where(r => r.prepTime >= 20 && r.prepTime <= 30);
                    foreach (Recipe recipe in mediumPrepTime)
                    {
                        Console.WriteLine($"Name: {recipe.name}  |  PrepTime: {recipe.prepTime} min");
                    }
                }
                if(input == "c")
                {
                    IEnumerable<Recipe> longPrepTime = Recipes.Values.Where(r => r.prepTime > 30);
                    foreach (Recipe recipe in longPrepTime)
                    {
                        Console.WriteLine($"Name: {recipe.name}  |   PrepTime: {recipe.prepTime} min");
                    }
                }
                Console.WriteLine("--------------------------------------------------\n");

                Console.WriteLine("Press any key to return to the main menu.");
                Console.ReadKey();
                Console.Clear();
                DisplayMenu();
            }

            // Display a menu
            void DisplayMenu()
            {
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine("Please choose an option:");
                Console.WriteLine("1. Display all recipes");
                Console.WriteLine("2. Search for recipes by ingredient");
                Console.WriteLine("3. View calorie values");
                Console.WriteLine("4. Search by cooking time");
                Console.WriteLine("5. Exit");
                Console.ResetColor();
                Console.WriteLine("--------------------------------------------------\n");
            }

             void HandleChoice(int choice)
            {
                switch (choice)
                {
                    case 1:
                        DisplayRecipes();
                        Console.WriteLine("");
                        Console.WriteLine("Please choose a recipe: ");
                        string option = Console.ReadLine();
                        Console.WriteLine("");
                        DisplayAllRecipe(option);
                        break;
                    case 2:
                        Console.WriteLine("Please enter an ingredient: ");
                        string ingredient = Console.ReadLine();
                        Console.WriteLine("");
                        SearchRecipes(ingredient);
                        break;
                    case 3:
                        ViewFood();
                        break;
                    case 4:
                        SearchByCookingTime();
                        break;
                    case 5:
                        Console.ForegroundColor= ConsoleColor.DarkCyan;
                        string bye = "Thank you! See you soon!";
                        Console.WriteLine(String.Format("{0," + ((Console.WindowWidth / 2) + (bye.Length / 2)) + "}", bye));
                        Console.ResetColor();
                        Environment.Exit(0);
                        break;
                    default:
                        // Display an error message
                        Console.WriteLine("Invalid option. Please try again.");
                        break;

                }
             }
            while (true)
            {

                string input = Console.ReadLine();
                int choice;
                bool success = int.TryParse(input, out choice);

                if (success)
                {
                    HandleChoice(choice);
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a number.");
                }
            }
        }
    }
}