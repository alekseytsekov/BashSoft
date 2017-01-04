namespace BashSoft.Testing
{
    using System;
    using System.Collections.Generic;
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using BashSoft.Contracts;
    using BashSoft.DataStructures;

    [TestClass]
    public class OrderedDataStructureTester
    {
        private ISimpleOrderBag<string> names;

        [TestInitialize]
        public void SetUp()
        {
            this.names = new SimpleSortedList<string>();
        }

        [TestMethod]
        public void TestJoinWorksFine()
        {
            this.names.Add("ivan");
            this.names.Add("koko");

            string result = this.names.JoinWith(", ");
            string expcetedResult = "ivan, koko";

            Assert.AreEqual(result, expcetedResult);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestJoinWithNull()
        {
            this.names.JoinWith(null);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestRemovingNullThrowsException()
        {
            this.names.Remove(null);
        }

        [TestMethod]
        public void TestRemoveValidElementRemovesSelectedOne()
        {
            this.names.Add("ivan");
            this.names.Add("nasko");

            this.names.Remove("ivan");

            foreach (var name in this.names)
            {
                Assert.AreNotEqual("ivan", name);
            }
        }

        [TestMethod]
        public void TestRemoveValidElementDecreasesSize()
        {
            this.names.Add("Al");
            this.names.Remove("Al");

            Assert.AreEqual(this.names.Size, 0);
        }

        [TestMethod]
        public void TestAddAllKeepsSorted()
        {
            string[] addedName = new[] { "Rosen", "Georgi", "Balkan" };
            this.names.AddAll(addedName);


            string result = this.names.JoinWith(",");

            string validResult = "Balkan," + "Georgi," + "Rosen";

            Assert.AreEqual(validResult, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAddingAllFromNullThrowsException()
        {
            this.names.AddAll(null);
        }

        [TestMethod]
        public void TestAddingAllFromCollectionIncreasesSize()
        {
            List<string> someNames = new List<string>();
            someNames.Add("ivan");
            someNames.Add("pesho");
            
            this.names.AddAll(someNames);
            
            Assert.AreEqual(this.names.Size, 2);    
        }

        [TestMethod]
        public void TestAddingMoreThanInitialCapacity()
        {
            for (int i = 1; i <= 17; i++)
            {
                this.names.Add(i.ToString());
            }

            Assert.AreEqual(this.names.Size, 17);
            Assert.AreNotEqual(this.names.Capacity, 16);
        }

        [TestMethod]
        public void TestAddUnsortedDataIsHeldSorted()
        {
            //string[] addedName = new[] {"Rosen", "Georgi", "Balkan"};
            this.names.Add("Rosen");
            this.names.Add("Georgi");
            this.names.Add("Balkan");

            
            string result = this.names.JoinWith(",");

            string validResult = "Balkan," + "Georgi,"  + "Rosen";

            Assert.AreEqual(validResult, result);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void TestAddNullThrowNullException()
        {
            this.names.Add(null);
        }

        [TestMethod]
        public void TestAddIncreasesSize()
        {
            this.names.Add("Al");
            Assert.AreEqual(1, this.names.Size);
        }

        [TestMethod]
        public void TestEmptyCtor()
        {
            this.names = new SimpleSortedList<string>();
            Assert.AreEqual(this.names.Capacity, 16);
            Assert.AreEqual(this.names.Size, 0);
        }

        [TestMethod]
        public void TestCtorWithInitCapacity()
        {
            this.names = new SimpleSortedList<string>(20);
            Assert.AreEqual(this.names.Capacity, 20);
            Assert.AreEqual(this.names.Size, 0);
        }

        [TestMethod]
        public void TestCtorWithAllParams()
        {
            this.names = new SimpleSortedList<string>(StringComparer.OrdinalIgnoreCase, 30);
            Assert.AreEqual(this.names.Capacity, 30);
            Assert.AreEqual(this.names.Size, 0);
        }

        [TestMethod]
        public void TestCtorWithInitComparer()
        {
            this.names = new SimpleSortedList<string>(StringComparer.OrdinalIgnoreCase);
            Assert.AreEqual(this.names.Capacity, 16);
            Assert.AreEqual(this.names.Size, 0);
        }
    }
}
