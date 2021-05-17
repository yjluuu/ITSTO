using Bo.Interface.IBusiness;
using Bo.Interface.IRepository;
using Routine.Models.Entity;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Bo.Business
{
    public class TestService : BaseService, ITestService
    {
        public TestService(IRepositoryFactory repositoryFactory, DBContext _dbContext) : base(repositoryFactory, _dbContext)
        {
        }

        public List<Test> GetTests()
        {
            var service = this.CreateService<Test>();
            return service.GetAll().ToList();
        }

        public void Insert()
        {
            var service = this.CreateService<Test>();
            Test t = new Test() { Age = 100, Name = "snwang" };
            service.Add(t);
        }
    }
}
