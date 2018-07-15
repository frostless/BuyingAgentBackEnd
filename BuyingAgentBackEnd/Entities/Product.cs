using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace BuyingAgentBackEnd.Entities
{
    public class Product
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
		[MaxLength(300)]
		public string Name { get; set; }
        
        [Required]
        [MaxLength(500)]
        public string Description { get; set; }

        public decimal Price { get; set; }

        public decimal Charged { get; set; }

		[MaxLength(100)]
		public string BarCode { get; set; }

        public decimal Profit { get; set; }

		[MaxLength(500)]
		public string ImgUrl { get; set; }

        //many-to-many relationshiop to join table TransactionProduct

        public ICollection<TransactionProduct> TransactionProducts { get; set; }

        //many-to-one relationship referencing Category table

        [ForeignKey("CategoryId")]

        public Category Category { get; set; }

        public int CategoryId { get; set; }

    }
}