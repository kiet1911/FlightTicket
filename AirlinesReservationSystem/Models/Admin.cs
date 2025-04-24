namespace AirlinesReservationSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("Admin")]
    public partial class Admin
    { 
        [DisplayName("ID")]
        public int id { get; set; }

        [DisplayName("Tên")]
        [StringLength(255)]
        public string name { get; set; }

        [Required]
        [DisplayName("Email")]
        [StringLength(255)]
        public string email { get; set; }

        [DisplayName("CCCD")]
        [StringLength(255)]
        public string cccd { get; set; }

        [DisplayName("Địa chỉ")]
        [StringLength(255)]
        public string address { get; set; }

        [DisplayName("SĐT")]
        [StringLength(255)]
        public string phone_number { get; set; }

        [Required]
        [DisplayName("Mật khẩu")]
        [StringLength(255)]
        public string password { get; set; }

        [DisplayName("Phân loại")]
        public int user_type { get; set; }

    }
}
