using BuyingAgentBackEnd.Entities;
using BuyingAgentBackEnd.Models;
using System.Collections.Generic;

namespace BuyingAgentBackEnd.Services
{
    public interface IBuyingAgentReports
    {
		decimal GetallProfit();

		int GetTranactionsNum();

		int GetVisitsNum();

		decimal GetFormulaProfit();

		decimal GetSupplementsProfit();

		IDictionary<string, string> GetTopCustomer();

		ICollection<IDictionary<string, string>> GetTopFiveCustomers();

		IDictionary<string, string> GetTopProduct();

		IDictionary<string, string> GetTopPost();

		IDictionary<string, string> GetTopVisit();

		ICollection<Transaction> GetAllTransactions();

		ICollection<Visit> GetAllVisits();

		ICollection<Product> GetAllProducts();

		ICollection<Category> GetAllCategories();

		ICollection<Customer> GetAllCustomers();

		InitialInfoDtos GetEntities();

		IDictionary<string, decimal> GetMonthsProfit(int year);

	}
}
