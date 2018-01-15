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

        public string Shop { get; set; }

    }
}
