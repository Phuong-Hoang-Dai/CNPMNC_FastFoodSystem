﻿using System.ComponentModel.DataAnnotations;

namespace AgentManager.WebApp.Models.Data
{
    public class Position
    {
        [Display(Name ="Mã chức vụ")]
        public int PositionId { get; set; }
        [Required]
        [Display(Name = "Tên chức vụ")]
        public string? PositionName { get; set; }
        [Display(Name ="Danh sách nhân viên")]
        public ICollection<Staff>? Staffs { get; set; }
    }
}
