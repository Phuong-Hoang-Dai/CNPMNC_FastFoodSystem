namespace AgentManager.WebApp.Models.Data
{
    public class Receipt
    {
        public int ReceiptId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Cash { get; set; }
        public string? StaffId { get; set; }
        public Staff? Staff { get; set; }
        public int AgentId { get; set; }
        public Agent? Agent { get; set; }
    }
}
