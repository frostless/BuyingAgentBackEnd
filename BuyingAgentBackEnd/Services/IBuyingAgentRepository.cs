using BuyingAgentBackEnd.Entities;
using BuyingAgentBackEnd.Models;
using System.Collections.Generic;

namespace BuyingAgentBackEnd.Services
{
    public interface IBuyingAgentRepository
    {
        //expect unique ProductIds
        void SaveNewTPWithTransaction(int transactionId, ICollection<int> ProductIds);
    
        bool Save();//helper method to check if the entity is being saved to DB successfully.

        bool IfVisitExist(int visitId);

        void SaveNewEntity<T>(T newEntity) where T : class;

        Visit GetVisit(int visitId);

        InitialInfoDtos GetEntities();

        IDictionary<string, decimal> GetMonthsProfit(int year);

        bool IfMonthsProfitExit(int year);

        IDictionary<string, string> GetTopCustomer();

        IDictionary<string, string> GetTopProduct();

        IDictionary<string, string> GetTopPost();

        IDictionary<string, string> GetTopVisit();

        decimal GetallProfit();

        int GetTranactionsNum();

        int GetVisitsNum();

        void DeleteEntity(int Id,string entity);
    }
}
