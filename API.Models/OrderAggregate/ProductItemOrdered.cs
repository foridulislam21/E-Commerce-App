namespace API.Models.OrderAggregate
{
    public class ProductItemOrdered
    {
        public ProductItemOrdered()
        {
        }

        public ProductItemOrdered(int productItemId, string productName, string productUrl)
        {
            ProductItemId = productItemId;
            ProductName = productName;
            ProductUrl = productUrl;
        }

        public int ProductItemId { get; set; }
        public string ProductName { get; set; }
        public string ProductUrl { get; set; }
    }
}