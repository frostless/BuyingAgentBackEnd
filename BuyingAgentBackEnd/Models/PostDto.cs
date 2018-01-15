using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyingAgentBackEnd.Models
{
    public class PostDto
    {
        public string Type { get; set; }

        public string Brand { get; set; }

        public int ExpectedTime { get; set; } //Business days

        public int Price { get; set; } //Dollor/kg
    }
}
