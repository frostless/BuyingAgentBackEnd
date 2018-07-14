﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BuyingAgentBackEnd.Models
{
    public class VisitDto
    {
        [Required]
        public DateTime StartedTime { get; set; }

        public DateTime FinishedTime { get; set; }

        public int ShopId { get; set; }

    }
}
