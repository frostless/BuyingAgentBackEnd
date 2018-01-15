using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace BuyingAgentBackEnd.Services
{
    public class EnumToDicConverter
    {

        public IDictionary<string, string> Convert(IQueryable source)
        {
            IDictionary<string,string> dicToReturn = new Dictionary<string, string>();

            foreach (var item in source)
            {
                foreach (PropertyInfo property in item.GetType().GetProperties())
                {
                   dicToReturn[property.Name] = property.GetValue(item, null).ToString();
                }
            }
            return dicToReturn;
        }
    }
}
