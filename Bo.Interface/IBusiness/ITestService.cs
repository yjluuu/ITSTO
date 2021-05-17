using Routine.Models.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Bo.Interface.IBusiness
{
    public interface ITestService
    {
        List<Test> GetTests();
        void Insert();
    }
}
