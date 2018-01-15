﻿namespace BuyingAgentBackEnd.Entities
{
    public class TransactionProduct
    {
        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int TransactionId { get; set; }
        public Transaction Transaction { get; set; }
    }
}