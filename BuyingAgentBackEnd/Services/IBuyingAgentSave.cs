using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuyingAgentBackEnd.Services
{
    public interface IBuyingAgentSave
    {
		//expect unique ProductIds
		void SaveNewTPWithTransaction(int transactionId, IDictionary<int, int> ProductsInfo);

		void SaveNewEntity<T>(T newEntity) where T : class;
	}
}
