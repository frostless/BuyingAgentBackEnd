using System.ComponentModel.DataAnnotations;

namespace BuyingAgentBackEnd.Models
{
    public class CategoryDto
    {
		[MaxLength(200)]
		[Required]
		public string Name { get; set; }

    }
}
