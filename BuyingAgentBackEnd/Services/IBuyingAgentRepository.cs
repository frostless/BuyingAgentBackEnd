using BuyingAgentBackEnd.Entities;
using BuyingAgentBackEnd.Models;
using System.Collections.Generic;

namespace BuyingAgentBackEnd.Services
{
    public interface IBuyingAgentRepository
    {
        //expect unique ProductIds
        void SaveNewTPWithTransaction(int transactionId, IDictionary<int,int> ProductsInfo);
    
        bool Save();//helper method to check if the entity is being saved to DB successfully.

        bool IfVisitExist(int visitId);
        bool IfProductExist(int productId);
        bool IfTransactionExist(int transactionId);
        bool IfCustomerExist(int customerId);
        void SaveNewEntity<T>(T newEntity) where T : class;

        Visit GetVisit(int visitId);

        Product GetProduct(int productId);

        Customer GetCustomer(int customerId);

        Transaction GetTransaction(int transactionId);

        InitialInfoDtos GetEntities();

        IDictionary<string, decimal> GetMonthsProfit(int year);

        bool IfMonthsProfitExit(int year);

        IDictionary<string, string> GetTopCustomer();

        IDictionary<string, string> GetTopProduct();

        IDictionary<string, string> GetTopPost();

        IDictionary<string, string> GetTopVisit();

        ICollection<Transaction> GetAllTransactions();

        decimal GetallProfit();

        int GetTranactionsNum();

        int GetVisitsNum();

        void DeleteEntity(int Id,string entity);
    }
}
