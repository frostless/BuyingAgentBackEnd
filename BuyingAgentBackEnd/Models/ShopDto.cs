using System.ComponentModel.DataAnnotations;

namespace BuyingAgentBackEnd.Models
{
    public class ShopDto
    {
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
