namespace AgentManager.WebApp.Models.Data
{
    public class Shipments
    {
        public String ShipmentId { get; set; }
        public int Quantity { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public String IngredientId { get; set; }
    }
}
