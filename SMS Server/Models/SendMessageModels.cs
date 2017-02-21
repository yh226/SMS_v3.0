using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SMS_Server.Models
{
    public class SendMessageModels
    {
        [Required(ErrorMessage ="not null")]
        public string MessageTo { get; set; }
        [Required]
        public string MessageFrom { get; set; }
        [Required]
        public string MessageText { get; set; }
        public string messagetype { get; set; }
        public string gateway { get; set; }
       public string userid { get; set; }
        public string userinfo { get; set; }
        public int priority { get; set; }
        public DateTime scheduled { get; set; }
        public int validityperiod { get; set; }
        public bool issent { get; set; }
        public bool isread { get; set; }
        //    
    }
}