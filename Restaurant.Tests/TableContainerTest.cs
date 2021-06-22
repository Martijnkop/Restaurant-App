using Microsoft.VisualStudio.TestTools.UnitTesting;
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
    public class TableContainerTest
    {

        #region FindByTableNumber

        [TestMethod]
        [TestCategory("FindByTableNumber")]
        public void FindByTableNumber_IncorrectTableNumber_Null()
        {
            TableTestDAL dal = new();

            Table t = new TableContainer(dal).FindByTableNumber(1);

            Assert.IsNull(t);
        }

        [TestMethod]
        [TestCategory("FindByTableNumber")]
        public void FindByTableNumber_DoesExist_Table()
        {
            TableTestDAL dal = new();

            Table t = new(1, dal: dal);

            t.Add();

            Table table = new TableContainer(dal).FindByTableNumber(1);

            Assert.IsNotNull(table);

        }

        #endregion

        #region GetAll

        [TestMethod]
        [TestCategory("GetAll")]
        public void GetAll_NoneInList_EmptyList()
        {
            TableTestDAL dal = new();

            List<Table> t = new TableContainer(dal).GetAll();

            Assert.AreEqual(t.Count, 0);
        }

        [TestMethod]
        [TestCategory("GetAll")]
        public void GetAll_InList_Return()
        {
            TableTestDAL dal = new();

            Table table = new(1, dal: dal);

            table.Add();

            List<Table> t = new TableContainer(dal).GetAll();

            Assert.AreEqual(t.Count, 1);
        }
        #endregion

    }
}
