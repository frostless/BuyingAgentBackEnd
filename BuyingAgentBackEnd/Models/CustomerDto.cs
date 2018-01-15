using System;
using System.ComponentModel.DataAnnotations;

namespace BuyingAgentBackEnd.Models
{
    public class CustomerDto
    {

        [Required]
        public string Name { get; set; }

        public string Province { get; set; }

        public string Gender { get; set; }

        public string Relationship { get; set; }

        public DateTime CustomerSince { get; set; }
    }
}
