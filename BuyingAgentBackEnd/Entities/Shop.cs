using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BuyingAgentBackEnd.Entities
{
    public class Shop
    {
		[Key]
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		public int Id { get; set; }

		[Required]
		[MaxLength(100)]
		public string Name { get; set; }

		[MaxLength(300)]
		public string Address { get; set; }

		[MaxLength(100)]
		public string ContactNo { get; set; }

		[MaxLength(100)]
		public string WeChatNo { get; set; }
	}
}
