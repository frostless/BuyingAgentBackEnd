using BuyingAgentBackEnd.Entities;
using System.Linq;


namespace BuyingAgentBackEnd.Services
{
	public class BuyingAgentDelete : IBuyingAgentDelete
	{
		private BuyingAgentContext _buyingAgentContext;

		public BuyingAgentDelete(BuyingAgentContext buyingAgentContext,
								 EnumToDicConverter converter)
		{
			_buyingAgentContext = buyingAgentContext;
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
			else if (entity == "transaction")
			{
				var entityToDelete = _buyingAgentContext.Transactions.FirstOrDefault(t => t.Id == Id);
				if (entityToDelete == null) return;
				_buyingAgentContext.Transactions.Remove(entityToDelete);
			}

		}
	}
}
