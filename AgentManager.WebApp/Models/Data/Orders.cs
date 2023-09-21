namespace AgentManager.WebApp.Models.Data
{
    public class Orders
    {
        public String OrderId {  get; set; }
        public DateTime Date { get; set; }
        public double Cash { get; set; }
        public String StaffId { get; set; }
        public String TableId { get; set; }
        public String VoucherId { get; set; }

    }
}
