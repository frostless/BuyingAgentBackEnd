﻿using BuyingAgentBackEnd.Entities;
using BuyingAgentBackEnd.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace BuyingAgentBackEnd.Services
{
	public class BuyingAgentReports : IBuyingAgentReports
	{
		private BuyingAgentContext _buyingAgentContext;
		private EnumToDicConverter _converter;
		public BuyingAgentReports(BuyingAgentContext buyingAgentContext,
							      EnumToDicConverter converter)
		{
			_buyingAgentContext = buyingAgentContext;
			_converter = converter;
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

		public InitialInfoDtos GetEntities()
		{
			InitialInfoDtos resultList = new InitialInfoDtos
			{
				customers = _buyingAgentContext.Customers.OrderBy(c => c.Id).ToList(),
				posts = _buyingAgentContext.Posts.OrderBy(p => p.Id).ToList(),
				products = _buyingAgentContext.Products.OrderBy(p => p.Id).ToList(),
				categories = _buyingAgentContext.Categories.OrderBy(c => c.Id).ToList(),
				shops = _buyingAgentContext.Shops.OrderBy(s => s.Id).ToList()
			};
			return resultList;
		}

		public ICollection<Transaction> GetAllTransactions()
		{

			return _buyingAgentContext.Transactions.OrderBy(t => t.Id).ToList();

		}

		public ICollection<Visit> GetAllVisits()
		{

			return _buyingAgentContext.Visits.OrderBy(t => t.Id).ToList();

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
			IDictionary<string, string> customerToReturn = _converter.Convert(topCustomer).First();
			return customerToReturn;
		}

		public ICollection<IDictionary<string, string>> GetTopFiveCustomers()
		{
			var topFiveCustomer = _buyingAgentContext.Transactions
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
							  .Take(5);
			ICollection<IDictionary<string, string>> customersToReturn = _converter.Convert(topFiveCustomer);	
			return customersToReturn;
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
			IDictionary<string, string> productToReturn = _converter.Convert(topProduct).First();
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
			IDictionary<string, string> postToReturn = _converter.Convert(topPost).First();
			return postToReturn;
		}

		public IDictionary<string, string> GetTopVisit()
		{
			var topVisit = (from t in _buyingAgentContext.Transactions
							join v in _buyingAgentContext.Visits
							on t.VisitId equals v.Id
							join s in _buyingAgentContext.Shops
							on v.ShopId equals s.Id
							group t by new { visit = v.Id, shopName = s.Name, v.StartedTime, v.FinishedTime }
							into g
							select new
							{
								visit = g.Key.visit,
								shop = g.Key.shopName,
								profit = g.Sum(p => p.Profit),
								timeElapsed = g.Key.FinishedTime - g.Key.StartedTime,
								date = g.Key.StartedTime.Date
							}).OrderByDescending(x => x.profit)
							  .Take(1);
			IDictionary<string, string> visitToReturn = _converter.Convert(topVisit).First();
			return visitToReturn;
		}

		public decimal GetFormulaProfit()
		{
			int categoryId = _buyingAgentContext.Categories.Where(c => c.Name == "baby formula").First().Id;

			var groupedTP = (from tp in _buyingAgentContext.TransactionProducts
							 group tp by new { tp.TransactionId }
							 into g
							 select new
							 {
								 TransactionId = g.Key.TransactionId,
								 ProductId = g.Min(TransactionProduct => TransactionProduct.ProductId)
							 }
						);

			decimal formulaProfit = (from tp in groupedTP
									 join t in _buyingAgentContext.Transactions
									 on tp.TransactionId equals t.Id
									 join p in _buyingAgentContext.Products
									 on tp.ProductId equals p.Id
									 join c in _buyingAgentContext.Categories
									 on p.CategoryId equals c.Id
									 where c.Id == categoryId
									 group t by new { c.Id }
									 into g
									 select new { profit = g.Sum(t => t.Profit) }).First().profit;
			return formulaProfit;
		}

		public decimal GetSupplementsProfit()
		{
			int categoryId = _buyingAgentContext.Categories.Where(c => c.Name == "Supplements").First().Id;

			var groupedTP = (from tp in _buyingAgentContext.TransactionProducts
							 group tp by new { tp.TransactionId }
							 into g
							 select new
							 {
								 TransactionId = g.Key.TransactionId,
								 ProductId = g.Min(TransactionProduct => TransactionProduct.ProductId)
							 }
						 );

			decimal supplementsProfit = (from tp in groupedTP
										 join t in _buyingAgentContext.Transactions
										 on tp.TransactionId equals t.Id
										 join p in _buyingAgentContext.Products
										 on tp.ProductId equals p.Id
										 join c in _buyingAgentContext.Categories
										 on p.CategoryId equals c.Id
										 where c.Id == categoryId
										 group t by new { c.Id }
										 into g
										 select new { profit = g.Sum(t => t.Profit) }).First().profit;
			return supplementsProfit;
		}
	}
}
