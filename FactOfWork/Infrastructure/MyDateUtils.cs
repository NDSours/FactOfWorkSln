using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactOfWork.Infrastructure
{
    public static class MyDateUtils
    {
        public static List<int> GetRangeYear(int startYear = 2021, int countYear = 2)
        {
            List<int> result = new List<int>();
            int tempYear = startYear; 
            while (tempYear != (DateTime.Now.Year + countYear)) //  
            {
                result.Add(tempYear);
                tempYear++;
            }

            return result;
        }
    }
}
