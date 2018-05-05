using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BuyingAgentBackEnd.Models
{
    public class TransactionDto
    {

        public DateTime TransactionTime { get; set; }

        [Required]
        public IDictionary<int,int> ProductInfo { get; set; }

        public decimal Price { get; set; }

        public decimal Profit { get; set; }

        public decimal Charged { get; set; }

        public int CustomerId { get; set; }

        public int VisitId { get; set; }

        public int PostId { get; set; }

    }
}
