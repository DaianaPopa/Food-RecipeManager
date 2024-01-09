using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;


namespace Food_RecipeManager
{
    //food, recipe info
    internal class Food
    {
        private double Calories;
        private string Name;
        private string Category;
        
        public double calories { get; set; }
        public string name { get; set; }
        public string category { get; set; }

        public Food(string name, string category, double calories)
        {
            this.name = name;
            this.category = category;
            this.calories = calories;
        }
    }

    class Recipe
    {
        private int ID;
        private string Name;
        private int PrepTime;
        private int Servings;
        private string Description;

        public int id { get; set; }
        public string name { get; set; }
        public int prepTime { get; set; }
        public int servings { get; set; }
        public string description { get; set; }
        public List<string> Ingredients { get; set; }

        public Recipe(int id, string name, int prepTime, int servings, string description, List<string> ingredients)
        {
           this.id = id;
           this.name = name;
           this.prepTime = prepTime;
           this.servings = servings;
           this.description = description;
            Ingredients = ingredients;
        }
    }


   




















}
