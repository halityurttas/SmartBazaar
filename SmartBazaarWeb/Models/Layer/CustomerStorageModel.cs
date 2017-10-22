namespace SmartBazaar.Web.Models.Layer
{
    public class CustomerStorageModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int? GroupId { get; set; }
        public short PriceIndex { get; set; }
    }
}