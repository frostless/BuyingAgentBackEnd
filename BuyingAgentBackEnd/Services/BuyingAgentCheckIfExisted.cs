using BuyingAgentBackEnd.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyingAgentBackEnd.Services
{
	public class BuyingAgentCheckIfExisted : IBuyingAgentCheckIfExisted
	{
		private BuyingAgentContext _buyingAgentContext;
		public BuyingAgentCheckIfExisted(BuyingAgentContext buyingAgentContext)
		{
			_buyingAgentContext = buyingAgentContext;
		}
		public bool IfVisitExist(int visitId)
		{
			return _buyingAgentContext.Visits.Any(v => v.Id == visitId);
		}
		public bool IfProductExist(int productId)
		{
			return _buyingAgentContext.Products.Any(p => p.Id == productId);
		}
		public bool IfTransactionExist(int transactionId)
		{
			return _buyingAgentContext.Transactions.Any(t => t.Id == transactionId);
		}
		public bool IfCustomerExist(int customerId)
		{
			return _buyingAgentContext.Customers.Any(c => c.Id == customerId);
		}
		public bool IfMonthsProfitExit(int year)
		{
			return _buyingAgentContext.Transactions.Any(t => t.TransactionTime.Year == year);
		}
	}
}
