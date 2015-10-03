using Microsoft.VisualStudio.TestTools.UnitTesting;
using BlaBlaRunProject.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BlaBlaRunProject.DataPopulator;
using BlaBlaRunProject.Domain.Concrete;
using BlaBlaRunProject.DataAccess.Abstract;

namespace BlaBlaRunProject.Controllers.Tests
{
    [TestClass()]
    public class WorkoutsOldsControllerTests
    {
        [TestMethod()]
        public void PopulateWorkoutsOldsDB()
        {
            var list = WorkoutsOldInitializer.PopulateData();

            EFDBContextContainer oEFDBContextContainer = new EFDBContextContainer();
            UnitOfWork oUnitOfWork = new UnitOfWork(oEFDBContextContainer);


            WorkoutsOldsController controller = new WorkoutsOldsController(oUnitOfWork);
            foreach (var item in list)
            {
                controller.Create(item);

            }
        }
    }
}