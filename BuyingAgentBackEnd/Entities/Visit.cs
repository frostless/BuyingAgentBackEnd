using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace BuyingAgentBackEnd.Entities
{
    public class Visit
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime StartedTime { get; set; }

        public DateTime FinishedTime { get; set; }

		//many-to-one relationship referencing Category table
		[ForeignKey("ShopId")]
		public Shop Shop { get; set; }
		public int? ShopId { get; set; }

	}
}
