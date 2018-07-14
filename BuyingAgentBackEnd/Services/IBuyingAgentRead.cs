using BuyingAgentBackEnd.Entities;

namespace BuyingAgentBackEnd.Services
{
    public interface IBuyingAgentRead
    {
		Visit GetVisit(int visitId);

		Product GetProduct(int productId);

		Customer GetCustomer(int customerId);

		Transaction GetTransaction(int transactionId);
	}
}
