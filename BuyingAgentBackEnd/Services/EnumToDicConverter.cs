using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace BuyingAgentBackEnd.Services
{
	public class EnumToDicConverter
	{
		public ICollection<IDictionary<string, string>> Convert(IQueryable source)
		{
			ICollection<IDictionary<string, string>> collectionToReturn = new List<IDictionary<string, string>>();

			foreach (var item in source)
			{
				IDictionary<string, string> dicToReturn = new Dictionary<string, string>();
				foreach (PropertyInfo property in item.GetType().GetProperties())
				{
					var val = property.GetValue(item, null) != null ? property.GetValue(item, null) : null;
					if (val != null)
					{
						dicToReturn[property.Name] = property.GetValue(item, null).ToString();

					}
				}
				collectionToReturn.Add(dicToReturn);
			}

			return collectionToReturn;
		}
	}
}
