using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FactOfWork.Models
{
    public class RepositoryPerson : IRepositoryPerson
    {
        private readonly SqlDbContext _context;
        public RepositoryPerson(SqlDbContext ctx) => _context = ctx;
        public IEnumerable<Person> Persons => _context.Persons.ToArray();

        public IQueryable<Person> QuerablePersons => _context.Persons;

        public void AddPerson(Person person)
        {
            _context.Add(person);
             _context.SaveChanges();
        }

        public void AddRange(IEnumerable<Person> list)
        {
            foreach (var itemPerson in list)
                _context.Add(itemPerson);

            _context.SaveChanges();
        }

        public void Delete(Person person)
        {
            _context.Persons.Remove(person);
            _context.SaveChanges();
        }

        public Person GetPersonWithSubEntites(long Id)
        {
            return _context.Persons.Where(p => p.Id == Id).Include(p => p.Zaproses).FirstOrDefault();
        }

        public void Update(Person person)
        {
            _context.Persons.Update(person);
            _context.SaveChanges();
        }

        public Person GetPerson(long Id)
        {
            return _context.Persons.Find(Id);
        }
    }
}
