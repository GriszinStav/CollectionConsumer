using System.Collections.Generic;

namespace CollectionConsumer.Models
{
    public class AppData
    {
        public List<Collection> Collections { get; set; } = new();
        public string CurrentTheme { get; set; } = "Light";
    }
}