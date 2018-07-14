using BuyingAgentBackEnd.Entities;

namespace BuyingAgentBackEnd.Services
{
    public class BuyingAgentCheckIfSaved : IBuyingAgentCheckIfSaved
    {
		private BuyingAgentContext _buyingAgentContext;
		public BuyingAgentCheckIfSaved(BuyingAgentContext buyingAgentContext)
		{
			_buyingAgentContext = buyingAgentContext;

		}
		public bool Save()
		{
			return (_buyingAgentContext.SaveChanges() >= 0);
		}
	}
}
