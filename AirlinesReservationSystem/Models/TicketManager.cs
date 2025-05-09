﻿namespace AirlinesReservationSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    using System.Linq;

    [Table("TicketManager")]
    public partial class TicketManager
    {
        private static Model1 db = new Model1();
        public const int STATUS_PAY = 0;
        public const int STATUS_CANCEL = 1;


        [DisplayName("ID")]
        public int id { get; set; }

        [DisplayName("FlightSchedule")]
        public int flight_schedules_id { get; set; }

        [DisplayName("User")]
        public int user_id { get; set; }

        [Range(0, 1, ErrorMessage = "sai mã trạng thái")]
        [DisplayName("Trạng thái")]
        public int status { get; set; }

        [DisplayName("Mã")]
        public string code { get; set; }    

        [DisplayName("Vị trí")]
        public int? seat_location { get; set; }

        [DisplayName("Mã thanh toán")]
        public int? pay_id { get; set; }

        public virtual FlightSchedule FlightSchedule { get; set; }

        public virtual User User { get; set; }

        [NotMapped] // Bỏ ánh xạ cột Admin_id trong DB
        public int AdminId { get; set; }

        public string getStatus()
        {
            string status = "";
            switch (this.status)
            {
                case TicketManager.STATUS_PAY:
                    status = "PAY";
                    break;
                case TicketManager.STATUS_CANCEL:
                    status = "CANCEL";
                    break;
            }
            return status;
        }

        public bool isCancel()
        {
            if(this.status == TicketManager.STATUS_CANCEL)
            {
                return true;
            }
            return false;
        }

        public string getLabelStatus()
        {
            string label = "";
            switch (this.status)
            {
                case TicketManager.STATUS_CANCEL:
                    label = "danger";
                    break;
                case TicketManager.STATUS_PAY:
                    label = "success";
                    break;
                default:
                    label = "success";
                    break;
            }
            return label;
        }

       
    }
}
