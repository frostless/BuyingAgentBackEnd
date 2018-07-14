using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyingAgentBackEnd.Services
{
    public interface IBuyingAgentCheckIfExisted
    {
		bool IfVisitExist(int visitId);
		bool IfProductExist(int productId);
		bool IfTransactionExist(int transactionId);
		bool IfCustomerExist(int customerId);
		bool IfMonthsProfitExit(int year);
	}
}
