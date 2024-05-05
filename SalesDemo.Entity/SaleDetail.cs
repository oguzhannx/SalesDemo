namespace SalesDemo.Entities
{
    public class SaleDetail : BaseModel
    {
        public Product product { get; set; }
        public int Count { get; set; }
        public double Total { get; set; }
    }
}
