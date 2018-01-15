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
        public string Name { get; set; }
        
        [Required]
        [MaxLength(200)]
        public string Description { get; set; }

        public decimal Price { get; set; }

        public decimal Charged { get; set; }

        public string BarCode { get; set; }

        public string ImgUrl { get; set; }

        //many-to-many relationshio to join table TransactionProduct

        public ICollection<TransactionProduct> TransactionProducts { get; set; }

        //many-to-one relationship referencing Category table

        [ForeignKey("CategoryId")]

        public Category Category { get; set; }

        public int CategoryId { get; set; }

    }
}