using BuyingAgentBackEnd.Entities;

using System.Collections.Generic;


namespace BuyingAgentBackEnd.Models
{
    public class InitialInfoDtos
    {
        public ICollection<Customer> customers { get; set; }
        public ICollection<Product> products { get; set; }
        public ICollection<Category> categories { get; set; }
        public ICollection<Post> posts { get; set; }
		public ICollection<Shop> shops { get; set; }
	}
}
