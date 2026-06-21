using System;

namespace CollectionConsumer.Models
{
    public class Card
    {
        public string Name { get; set; } = string.Empty;
        public string Rarity { get; set; } = string.Empty;
        public decimal? Price { get; set; }
        public DateTime? AcquisitionDate { get; set; }
        public string? ImagePath { get; set; }
    }
}