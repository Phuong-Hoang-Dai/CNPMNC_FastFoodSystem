﻿using System.ComponentModel.DataAnnotations;

namespace AgentManager.WebApp.Models.Data
{
    public class FFSDeliveryRecievedNote
    {

		[Display(Name = "Mã hóa đơn nhập/xuất kho")]
		[Required]
		public string FFSDeliveryRecievedNoteId { get; set; }

		[Required]
        [Display (Name = "Trạng thái")]
        public string State { get; set; }

        [Required]
        [Display (Name = "Mã nhân viên")]
        public string StaffId  { get; set; }
		public Staff? Staff { get; set; }

		public ICollection<FFSShipment> FFSShipments { get; set; }

	}
}