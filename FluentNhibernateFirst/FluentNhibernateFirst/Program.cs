using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;

namespace FluentNhibernateFirst
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var tran = session.BeginTransaction())
                {
                    var person = new Person { ID=Guid.NewGuid(),FirstName="Robert", LastName="Hu" };
                    session.Save(person);
                    tran.Commit();
                }
                
            }
        }
    }

    public class Person 
    {
        public virtual Guid ID { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
    }

    public class PersonMap:ClassMap<Person>
    {
        public PersonMap()
        {
            Id(x => x.ID, "ID");
            Map(x => x.FirstName, "firstName");
            Map(x => x.LastName, "lastName");
        }
    }
}
