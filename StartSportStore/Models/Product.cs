namespace StartSportStore.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        //public string Category { get; set; }
        //public decimal PurchasePrice { get; set; }
        public decimal RetailPrice { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }

    }
}
