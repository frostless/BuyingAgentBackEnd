using BuyingAgentBackEnd.Entities;
using System.Linq;

namespace BuyingAgentBackEnd.Services
{
    public class BuyingAgentRead : IBuyingAgentRead
    {
		private BuyingAgentContext _buyingAgentContext;
		public BuyingAgentRead(BuyingAgentContext buyingAgentContext)
		{
			_buyingAgentContext = buyingAgentContext;
		}
		public Visit GetVisit(int visitId)
		{
			return _buyingAgentContext.Visits.FirstOrDefault(v => v.Id == visitId);
		}

		public Product GetProduct(int productId)
		{
			return _buyingAgentContext.Products.FirstOrDefault(v => v.Id == productId);
		}

		public Transaction GetTransaction(int transactionId)
		{
			return _buyingAgentContext.Transactions.FirstOrDefault(t => t.Id == transactionId);

		}

		public Customer GetCustomer(int customerId)
		{
			return _buyingAgentContext.Customers.FirstOrDefault(c => c.Id == customerId);

		}
	}
}
