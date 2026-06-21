using System.Collections.Generic;

namespace CollectionConsumer.Models
{
    public class Collection
    {
        public string Name { get; set; } = string.Empty;
        public string? IconPath { get; set; }
        public List<Card> Cards { get; set; } = new();
    }
}