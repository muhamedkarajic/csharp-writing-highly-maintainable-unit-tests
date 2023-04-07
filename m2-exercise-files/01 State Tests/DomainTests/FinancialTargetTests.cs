using System;
using Collections;
using Domain;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DomainTests
{
    [TestClass]
    public class FinancialTargetTests
    {
        [TestMethod]
        public void AddTargetPoints_EmptyArrayAndCount2_ArrayContains3And5()
        {
            // Arrange
            FinancialTarget target = new FinancialTarget();
            MyArray array = new MyArray();

            // Act
            target.AddTargetPoints(array, 2);

            // Assert
            Assert.AreEqual(3, array.GetElementAt(0));
            Assert.AreEqual(5, array.GetElementAt(1));
        }

        [TestMethod]
        public void AddTargetPoints_EmptyArrayAndCount2_ArrayHasLength2()
        {
            FinancialTarget target = new FinancialTarget();
            MyArray array = new MyArray();

            target.AddTargetPoints(array, 2);

            Assert.AreEqual(2, array.Length);
        }

        [TestMethod]
        public void InitializePoints_ReturnsNonNull()
        {
            MyArray array = new FinancialTarget().InitializePoints();
            Assert.IsNotNull(array);
        }

        [TestMethod]
        public void InitializePoints_ResultStartsWithValue3()
        {
            MyArray array = new FinancialTarget().InitializePoints();
            Assert.AreEqual(3, array.GetElementAt(0));
        }

        [TestMethod]
        public void InitializePoints_ReturnsNonEmptyArray()
        {
            MyArray array = new FinancialTarget().InitializePoints();
            Assert.IsTrue(array.Length > 0);
        }
    }
}
    