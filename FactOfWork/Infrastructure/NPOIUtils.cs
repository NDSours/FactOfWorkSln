using NPOI.SS.UserModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactOfWork.Infrastructure
{
    public static class NPOIUtils
    {
        public static string GetStringValueFromCell(ICell cell)
        {
            switch (cell.CellType)
            {
                case CellType.String:
                    return cell.StringCellValue;
                case CellType.Numeric:
                    return cell.NumericCellValue.ToString();
                case CellType.Unknown:
                    return cell.StringCellValue;
                default:
                    return "";
            }
        }
    }
}
