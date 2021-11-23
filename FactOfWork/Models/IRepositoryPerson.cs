using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FactOfWork.Models
{
    public interface IRepositoryPerson
    {
        public IEnumerable<Person> Persons { get; }
        public IQueryable<Person> QuerablePersons { get; }
        public void AddRange(IEnumerable<Person> list);
        public void AddPerson(Person person);
        public Person GetPersonWithSubEntites(long Id);
        public Person GetPerson(long Id);
        public void Update(Person person);
        public void Delete(Person person);

    }
}
