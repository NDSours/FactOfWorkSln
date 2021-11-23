using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NPOI.SS.UserModel;

namespace FactOfWork.Infrastructure
{
    public class FileSetting
    {
        public int NumberRow { get; set; }
        public int Column_NumberArea { get; set; }
        public int Column_Surname { get; set; }
        public int Column_Name { get; set; }
        public int Column_Patr { get; set; }
        public int Column_SNILS { get; set; }
        public int Column_Category { get; set; }

        public FileSetting() {}
        public FileSetting(ISheet sheet)
        {
            for (int row = 0; row <= sheet.LastRowNum; row++)
            {
                var currentrow = sheet.GetRow(row);
                if (currentrow != null)
                {
                    var cell = currentrow.GetCell(0);
                    if (cell != null)
                    {
                        string value = NPOIUtils.GetStringValueFromCell(cell);
                        if (value.ToLower() == "регион")
                        {
                            this.NumberRow = row+1;
                            for (int numbercell = 1; numbercell <= currentrow.LastCellNum; numbercell++)
                            {
                                var currentcell = currentrow.GetCell(numbercell);
                                if (currentcell != null)
                                {
                                    string header = NPOIUtils.GetStringValueFromCell(currentcell);
                                    switch (header.ToLower())
                                    {
                                        case "район":
                                            this.Column_NumberArea = numbercell;
                                            break;
                                        case "фамилия":
                                            this.Column_Surname = numbercell;
                                            break;
                                        case "имя":
                                            this.Column_Name = numbercell;
                                            break;
                                        case "отчество":
                                            this.Column_Patr = numbercell;
                                            break;
                                        case "снилс":
                                            this.Column_SNILS = numbercell;
                                            break;
                                        case "категория":
                                            this.Column_Category = numbercell;
                                            break;
                                        default:
                                            break;
                                    }
                                }
                            }
                        }
                    }
                }
            }
        }

        

        public override bool Equals(object obj)
        {
            if (obj is FileSetting temp)
            {
                if (this.Column_NumberArea.Equals(temp.Column_NumberArea)
                    && this.Column_Category.Equals(temp.Column_Category)
                    && this.Column_Name.Equals(temp.Column_Name)
                    && this.Column_Patr.Equals(temp.Column_Patr)
                    && this.Column_SNILS.Equals(temp.Column_SNILS)
                    && this.Column_Surname.Equals(temp.Column_Surname)
                    && this.NumberRow.Equals(temp.NumberRow))
                    return true;
            }

            return false;
        }

        public override int GetHashCode()
        {
            throw new NotImplementedException();
        }
    }
}
