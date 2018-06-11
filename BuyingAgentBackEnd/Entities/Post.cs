using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace BuyingAgentBackEnd.Entities
{
    public class Post
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Type { get; set; }

        public string Brand { get; set; }

        public int ExpectedTime { get; set; } //Business Days

        public decimal Price { get; set; } //Dollars/Kg

    }
}
