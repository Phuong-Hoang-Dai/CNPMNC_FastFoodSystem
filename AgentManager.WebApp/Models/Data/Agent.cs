using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Serialization;
using System.ComponentModel.DataAnnotations;

namespace AgentManager.WebApp.Models.Data
{
    public class Agent
    {
        [Display(Name = "Mã đại lý")]
        public int AgentId { get; set; }
        [Required]
        [StringLength(100)]
        [Display(Name = "Tên đại lý")]
        public string? AgentName { get; set; }
        [Required]
        [StringLength(100)]
        [Display(Name = "Địa chỉ")]
        public string? Address { get; set; }
        [Required]
        [StringLength(100)]
        [Display(Name = "SĐT")]
        [DataType (DataType.PhoneNumber)]
        public string? Phone { get; set; }
        [Display(Name = "Ngày tiếp nhận")]
        [DataType (DataType.DateTime)]
        public DateTime ReceptionDate { get; set; }
        [Display(Name = "Danh sách phiếu xuất")]
        public ICollection<DeliveryNote>? DeliveryNotes { get; set; }
        [Display(Name = "Danh sách phiếu thu")]
        public ICollection<Receipt>? Receipts { get; set; }
        public int DistrictId { get; set; }
        [Display(Name = "Quận")]
        public District? District { get; set; }
        public int AgentCategoryId { get; set; }
        [Display(Name = "Loại đại lý")]
        public AgentCategory? AgentCategory { get; set; }
    }
}
