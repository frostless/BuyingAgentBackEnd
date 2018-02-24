using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BuyingAgentBackEnd.Entities
{
    public class Transaction
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime TransactionTime { get; set; }

        //many-to-many relationshio to join table TransactionProduct

        public ICollection<TransactionProduct> TransactionProducts { get; set; }

        public decimal Price { get; set; }

        public decimal Profit { get; set; }

        public decimal Charged { get; set; }

        //many-to-one relationship referencing Customer table

        [ForeignKey("CustomerId")]

        public Customer Customer { get; set; }

        public int  CustomerId { get; set; }

        //many-to-one relationship referencing Visit table

        [ForeignKey("VisitId")]

        public Visit Visit { get; set; }

        public int VisitId { get; set; }

        //many-to-one relationship referencing Post table

        [ForeignKey("PostId")]

        public Post Post { get; set; }

        public int PostId { get; set; }
    }
}
