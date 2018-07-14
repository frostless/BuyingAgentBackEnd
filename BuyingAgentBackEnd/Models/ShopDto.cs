using System.ComponentModel.DataAnnotations;

namespace BuyingAgentBackEnd.Models
{
    public class ShopDto
    {
		[Required]
		[MaxLength(20)]
		public string Name { get; set; }
		[MaxLength(100)]
		public string Address { get; set; }
		[MaxLength(50)]
		public string ContactNo { get; set; }
		[MaxLength(50)]
		public string WeChatNo { get; set; }
	}
}
