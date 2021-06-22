using Microsoft.VisualStudio.TestTools.UnitTesting;
using Restaurant.Interface.table;
using Restaurant.Logic.table;
using Restaurant.Tests.TestDAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant.Tests
{
    [TestClass]
    public class TableTests
    {
        #region Add

        [TestMethod]
        [TestCategory("Add")]
        public void Add_Correct_True()
        {
            TableTestDAL dal = new();

            Table t = new(1, dal: dal);

            bool res = t.Add();

            Assert.IsTrue(res);
        }

        #endregion

        #region Update

        [TestMethod]
        [TestCategory("Update")]
        public void Update_NotExist_False()
        {
            TableTestDAL dal = new();

            Table t = new(1, dal: dal);

            bool res = t.Update(0);

            Assert.IsFalse(res);
        }

        [TestMethod]
        [TestCategory("Update")]
        public void Update_Correct_True()
        {
            TableTestDAL dal = new();

            Table t = new(1, dal: dal);

            t.Add();

            Table t2 = new(2, TableStatus.Delivered, dal: dal);

            t.Update(1);
        }

        #endregion

        #region AssignGuest

        [TestMethod]
        [TestCategory("AssignGuest")]
        public void AssignGuest_IncorrectTableNumber_False()
        {
            TableTestDAL dal = new();

            Table t = new(0, dal: dal);

            bool res = t.AssignGuest();

            Assert.IsFalse(res);
        }

        [TestMethod]
        [TestCategory("AssignGuest")]
        public void AssignGuest_CorrectTableNumber_True()
        {
            TableTestDAL dal = new();

            Table t = new(1, dal: dal);

            t.Add();

            bool res = t.AssignGuest();

            Assert.IsTrue(res);
        }

        #endregion

        #region RemoveGuest

        [TestMethod]
        [TestCategory("RemoveGuest")]
        public void RemoveGuest_BillNotExist_False()
        {
            TableTestDAL dal = new();

            Table t = new(0, dal: dal);

            t.Add();

            bool res = t.RemoveGuest(dal);

            Assert.IsFalse(res);
        }

        [TestMethod]
        [TestCategory("RemoveGuest")]
        public void RemoveGuest_IncorrectTableNumber_False()
        {
            TableTestDAL dal = new();

            Table t = new(0, dal: dal);

            bool res = t.RemoveGuest(dal);

            Assert.IsFalse(res);
        }

        [TestMethod]
        [TestCategory("RemoveGuest")]
        public void RemoveGuest_CorrectTableNumber_True()
        {
            TableTestDAL dal = new();

            Table t = new(1, dal: dal);

            t.Add();

            t.AssignGuest();

            bool res = t.RemoveGuest(dal);

            Assert.IsTrue(res);
        }

        #endregion
    }
}
