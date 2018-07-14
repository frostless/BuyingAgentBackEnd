using System.ComponentModel.DataAnnotations;

namespace BuyingAgentBackEnd.Models
{
    public class CategoryDto
    {
		[MaxLength(100)]
		[Required]
		public string Name { get; set; }

    }
}
