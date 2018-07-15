using System;
using System.ComponentModel.DataAnnotations;

namespace BuyingAgentBackEnd.Models
{
    public class CustomerDto
    {
		[Required]
		[MaxLength(200)]
		public string Name { get; set; }

		[MaxLength(200)]
		public string Province { get; set; }

		[MaxLength(200)]
		public string Gender { get; set; }

		[MaxLength(200)]
		public string Relationship { get; set; }

        public DateTime CustomerSince { get; set; }
    }
}
