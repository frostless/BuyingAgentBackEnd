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
		[MaxLength(100)]
		public string Name { get; set; }

		[MaxLength(100)]
		public string Province { get; set; }

		[MaxLength(50)]
		public string Gender { get; set; }

		[MaxLength(50)]
		public string Relationship { get; set; }

        public DateTime CustomerSince { get; set; }

    }
}