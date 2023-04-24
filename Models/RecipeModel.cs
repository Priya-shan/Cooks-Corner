using Microsoft.Build.Framework;
using System.ComponentModel.DataAnnotations;
using RequiredAttribute = Microsoft.Build.Framework.RequiredAttribute;

namespace Online_Recipe_Website.Models
{
    public class RecipeModel
    {
        [Key]
        public int recipe_id { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public  string title { get; set; }
        public string image_url { get; set; }
        public string description { get; set; }
        public string ingredients { get; set; }
        public string instruction { get; set; }
        public string category { get; set; }
        public string difficulty_level { get; set;}
        public string cuisine { get; set;}
        public string cooking_time { get; set; }


    }
}
