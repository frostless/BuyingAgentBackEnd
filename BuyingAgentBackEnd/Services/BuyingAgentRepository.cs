using BuyingAgentBackEnd.Entities;
using System.Collections.Generic;
using System.Linq;
using BuyingAgentBackEnd.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace BuyingAgentBackEnd.Services
{
	public class BuyingAgentRepository : IBuyingAgentRepository
	{
		private BuyingAgentContext _buyingAgentContext;
		private EnumToDicConverter _converter;

		public BuyingAgentRepository(BuyingAgentContext buyingAgentContext,
									EnumToDicConverter converter)
		{
			_buyingAgentContext = buyingAgentContext;
			_converter = converter;
		}
		public bool Save()
		{
			return (_buyingAgentContext.SaveChanges() >= 0);
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

		public void SaveNewEntity<T>(T newEntity) where T : class
		{
			_buyingAgentContext.Add(newEntity);
		}

		public InitialInfoDtos GetEntities()
		{
			InitialInfoDtos resultList = new InitialInfoDtos
			{
				customers = _buyingAgentContext.Customers.OrderBy(c => c.Id).ToList(),
				posts = _buyingAgentContext.Posts.OrderBy(p => p.Id).ToList(),
				products = _buyingAgentContext.Products.OrderBy(p => p.Id).ToList(),
				categories = _buyingAgentContext.Categories.OrderBy(c => c.Id).ToList()
			};
			return resultList;
		}

		public IDictionary<string, decimal> GetMonthsProfit(int year)
		{
			IDictionary<string, decimal> monthsProfit = new Dictionary<string, decimal>();
			for (int i = 1; i <= 12; i++)
			{
				var monthProfit = _buyingAgentContext.Transactions
									.Where(t => t.TransactionTime.Year == year)
									.Where(t => t.TransactionTime.Month == i);
				//decimal profit = monthProfit.Sum(x => x.Charged) - monthProfit.Sum(x => x.Price);
				decimal profit = monthProfit.Sum(x => x.Profit);
				monthsProfit[i.ToString()] = profit;
			}
			return monthsProfit;
		}

		public bool IfMonthsProfitExit(int year)
		{
			return _buyingAgentContext.Transactions.Any(t => t.TransactionTime.Year == year);
		}

		public IDictionary<string, string> GetTopCustomer()
		{
			var topCustomer = _buyingAgentContext.Transactions
							.Include(e => e.Customer)
							.GroupBy(e => e.Customer)
							.Select(e =>
							new
							{
								//profit = e.Sum(s => s.Charged) - e.Sum(s => s.Price),
								profit = e.Sum(s => s.Profit),
								name = e.Key.Name,
								province = e.Key.Province,
								relationship = e.Key.Relationship,
								gender = e.Key.Gender,
								customerSince = e.Key.CustomerSince
							}).OrderByDescending(x => x.profit)
							  .Take(1);
			IDictionary<string, string> customerToReturn = _converter.Convert(topCustomer);
			return customerToReturn;
		}

		public IDictionary<string, string> GetTopProduct()
		{
			var topProduct = (from p in _buyingAgentContext.Products
							  join tp in _buyingAgentContext.TransactionProducts
							  on p.Id equals tp.ProductId
							  join c in _buyingAgentContext.Categories
							  on p.CategoryId equals c.Id
							  group tp by new { tp.ProductId, productName = p.Name, categoryName = c.Name, p.Profit, p.Charged, p.Price, p.BarCode, p.ImgUrl }
							  into g
							  select new
							  {
								  productName = g.Key.productName,
								  categoryName = g.Key.categoryName,
								  charged = g.Key.Charged,
								  price = g.Key.Price,
								  barCode = g.Key.BarCode,
								  imgUrl = g.Key.ImgUrl,
								  profit = g.Sum(q => q.Qty) * g.Key.Profit
							  }).OrderByDescending(x => x.profit)
								.Take(1);
			IDictionary<string, string> productToReturn = _converter.Convert(topProduct);
			return productToReturn;
		}

		public IDictionary<string, string> GetTopPost()
		{
			var topPost = (from t in _buyingAgentContext.Transactions
						   join p in _buyingAgentContext.Posts
						   on t.PostId equals p.Id
						   group p by new { p.Brand, p.Type, p.ExpectedTime, p.Price }
						   into g
						   select new
						   {
							   postBrand = g.Key.Brand,
							   postType = g.Key.Type,
							   price = g.Key.Price,
							   expectedTime = g.Key.ExpectedTime,
							   transactionTimes = g.Count()
						   }).OrderByDescending(x => x.transactionTimes)
							 .Take(1);
			IDictionary<string, string> postToReturn = _converter.Convert(topPost);
			return postToReturn;
		}

		public IDictionary<string, string> GetTopVisit()
		{
			var topVisit = (from t in _buyingAgentContext.Transactions
							join v in _buyingAgentContext.Visits
							on t.VisitId equals v.Id
							group t by new { visit = v.Id, v.Shop, v.StartedTime, v.FinishedTime }
							into g
							select new
							{
								visit = g.Key.visit,
								shop = g.Key.Shop,
								profit = g.Sum(p => p.Profit),
								timeElapsed = g.Key.FinishedTime - g.Key.StartedTime,
								date = g.Key.StartedTime.Date
							}).OrderByDescending(x => x.profit)
							  .Take(1);
			IDictionary<string, string> visitToReturn = _converter.Convert(topVisit);
			return visitToReturn;
		}

		public decimal GetallProfit()
		{
			var transactions = _buyingAgentContext.Transactions;
			decimal allProfit = transactions.Sum(t => t.Profit);
			return allProfit;
		}

		public int GetTranactionsNum()
		{
			return _buyingAgentContext.Transactions.Count();
		}

		public int GetVisitsNum()
		{
			return _buyingAgentContext.Visits.Count();
		}

		public ICollection<Transaction> GetAllTransactions()
		{

			return _buyingAgentContext.Transactions.OrderBy(t => t.Id).ToList();

		}

		public void DeleteEntity(int Id, string entity)
		{
			if (entity == "product")
			{
				var entityToDelete = _buyingAgentContext.Products.FirstOrDefault(p => p.Id == Id);
				if (entityToDelete == null) return;
				_buyingAgentContext.Products.Remove(entityToDelete);
			}
			else if (entity == "customer")
			{
				var entityToDelete = _buyingAgentContext.Customers.FirstOrDefault(c => c.Id == Id);
				if (entityToDelete == null) return;
				_buyingAgentContext.Customers.Remove(entityToDelete);

			}
			else if (entity == "category")
			{
				var entityToDelete = _buyingAgentContext.Categories.FirstOrDefault(c => c.Id == Id);
				if (entityToDelete == null) return;
				_buyingAgentContext.Categories.Remove(entityToDelete);

			}
			else if (entity == "post")
			{
				var entityToDelete = _buyingAgentContext.Posts.FirstOrDefault(P => P.Id == Id);
				if (entityToDelete == null) return;
				_buyingAgentContext.Posts.Remove(entityToDelete);
			}
			else if (entity == "visit")
			{
				var entityToDelete = _buyingAgentContext.Visits.FirstOrDefault(v => v.Id == Id);
				if (entityToDelete == null) return;
				_buyingAgentContext.Visits.Remove(entityToDelete);
			}

		}
	}
}
