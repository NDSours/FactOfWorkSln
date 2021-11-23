using System.Collections.Generic;
using System.Linq;
using Xunit;
using FactOfWork.Infrastructure;

namespace FactOfWork.Tests
{
    public class FileSettingTests
    {
        [Fact]
        public void TestCreateFileSetting()
        {
            //задание начальных данных
            string filename = @"C:\Users\Developer\Desktop\FactOfWorkSln\Тестовые файлы\ухаживающие_22.xls";
            FileSetting TargetSetting = new FileSetting()
            {
                NumberRow = 7,
                Column_NumberArea = 1,
                Column_Surname = 2,
                Column_Name = 3,
                Column_Patr = 4,
                Column_SNILS = 5,
                Column_Category = 6
            };
            var list = FileLoader.ReadFile(filename, @"C:\Users\Developer\Desktop\FactOfWorkSln","1",11,2021,11,2021);

            var lastPerson = list.Last();

            Assert.True(FileLoader.Setting.Equals(TargetSetting));
            Assert.True(list.Count() == 5);
            Assert.True(lastPerson.Surname.ToLower() == "кочеткова");
        }
    }
}
