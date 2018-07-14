using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BuyingAgentBackEnd.Models
{
    public class ProductDto
    {

		[Required]
		[MaxLength(100)]
		public string Name { get; set; }

		[Required]
		[MaxLength(300)]
		public string Description { get; set; }

        public decimal Price { get; set; }

        public decimal Charged { get; set; }

        public string BarCode { get; set; }

		[MaxLength(100)]
		public string ImgUrl { get; set; }

        public int TransactionId { get; set; }

        public int CategoryId { get; set; }

        public decimal Profit { get; set; }

    }
}