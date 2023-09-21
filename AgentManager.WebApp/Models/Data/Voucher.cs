namespace AgentManager.WebApp.Models.Data
{
    public class Voucher
    {
        public String VoucherId { get; set; }
        public int Num {  get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public String State { get; set; }
        public Double Price { get; set; }
    }
}
