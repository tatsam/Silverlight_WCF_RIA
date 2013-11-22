
namespace TasksManager.Web
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.ServiceModel.DomainServices.Hosting;
    using System.ServiceModel.DomainServices.Server;


    // TODO: создайте методы, содержащие собственную логику приложения.
    public class Foo
    {
        [Key]
        public int FooId { get; set; }
        public string Name { get; set; }
    }

    [EnableClientAccess()]
    public class MyPOCODomainService : DomainService
    {
        public IEnumerable<Foo> GetFoos()
        {
            return new List<Foo> { new Foo { FooId = 42, Name = "Fred" } };
        }
    }

}


