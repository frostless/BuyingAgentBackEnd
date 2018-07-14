using BuyingAgentBackEnd.Entities;
using System.Collections.Generic;


namespace BuyingAgentBackEnd.Services
{
	public class BuyingAgentSave : IBuyingAgentSave
	{
		private BuyingAgentContext _buyingAgentContext;

		public BuyingAgentSave(BuyingAgentContext buyingAgentContext)
		{
			_buyingAgentContext = buyingAgentContext;
		}
		public void SaveNewEntity<T>(T newEntity) where T : class
		{
			_buyingAgentContext.Add(newEntity);
		}

		public void SaveNewTPWithTransaction(int transactionId, IDictionary<int, int> ProductsInfo)
		{
			if (ProductsInfo.Count == 0) return;

			List<TransactionProduct> transactionProducts = new List<TransactionProduct>();

			foreach (var info in ProductsInfo)
			{
				transactionProducts.Add(new TransactionProduct
				{
					TransactionId = transactionId,
					ProductId = info.Key,
					Qty = info.Value
				});
			}
			_buyingAgentContext.TransactionProducts.AddRange(transactionProducts);
		}
	}
}
