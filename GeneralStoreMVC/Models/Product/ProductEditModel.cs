namespace GeneralStoreMVC.Models.Product
{
    public class ProductEditModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int QuantityInStock { get; set; }
        public double Price { get; set; }
    }
}
