using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BuyingAgentBackEnd.Entities
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public string Province { get; set; }

        public string Gender { get; set; }

        public string Relationship { get; set; }

        public DateTime CustomerSince { get; set; }

    }
}