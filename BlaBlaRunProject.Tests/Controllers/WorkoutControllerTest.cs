using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using VMSWebApplication;
using VMSWebApplication.WebUI.Controllers;

using VMSWebApplication.Domain.Concrete;
using System.Data.Entity;
using VMSWebApplication.Tests.Common;
using VMSWebApplication.Tests.Controllers;

namespace VMSWebApplication.Tests.Controllers
{
    [TestClass]
    public class WorkoutControllerTest : EntityControllerTest<Workout, WorkoutsController>
    {
        #region DB
        [TestMethod]
        [TestCategory("VesselsController")]
        public void IndexWithDB()
        {
            base.IndexWithDB();
        }

        [TestMethod]
        [TestCategory("VesselsController")]
        public void DetailsWithDB()
        {
            base.DetailsWithDB();
        }

        [TestMethod]
        [TestCategory("VesselsController")]
        public void CreateWithDB()
        {
            base.CreateWithDB(TestCreator);
        }

        [TestMethod]
        [TestCategory("VesselsController")]
        public void EditWithDB()
        {
            base.EditWithDB();
        }

        [TestMethod]
        [TestCategory("VesselsController")]
        public void EditWithPushDB()
        {
            base.EditWithPushDB(TestEditEntity);
        }
        #endregion

        #region Mock

        [TestMethod]
        [TestCategory("VesselsController")]
        public void Index()
        {
            base.Index(GenericMethods.SetupVesselList);
        }


        [TestMethod]
        [TestCategory("VesselsController")]
        public void Details()
        {
            base.Details(GenericMethods.SetupVesselList);
        }



        [TestMethod]
        [TestCategory("VesselsController")]
        public void Create()
        {
            base.Create(GenericMethods.SetupVesselList, TestCreator);
        }

        [TestMethod]
        [TestCategory("VesselsController")]
        public void Edit()
        {
            base.Edit(GenericMethods.SetupVesselList);
        }

        [TestMethod]
        [TestCategory("VesselsController")]
        public void EditPost()
        {
            base.EditPost(GenericMethods.SetupVesselList, TestEditEntity);
        }

        #endregion

        public Vessel TestCreator()
        {
            Vessel oVessel = new Vessel()
            {
                VesselName = "TestVessel"

            };

            return oVessel;
        }


        public void TestEditEntity(Vessel oVessel)
        {
            oVessel.VesselName = "TestVesselEdited";

        }
    }
}
