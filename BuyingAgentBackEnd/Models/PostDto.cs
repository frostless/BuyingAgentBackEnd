using System.ComponentModel.DataAnnotations;

namespace BuyingAgentBackEnd.Models
{
    public class PostDto
    {
		[MaxLength(50)]
		public string Type { get; set; }

		[MaxLength(50)]
		public string Brand { get; set; }

        public int ExpectedTime { get; set; } //Business days

        public int Price { get; set; } //Dollor/kg
    }
}
