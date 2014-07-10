using System;
using AspectMVVMTests.TestOjbects;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace AspectMVVMTests
{
    [TestClass]
    public class LazyLoadTests
    {
        /// <summary>
        /// testOjbect should instantiate the List<int> so it is not null.
        /// </summary>
        [TestMethod]
        public void LazyLoad_Empty_Ctor_Test()
        {
            // Arrange
            // Act
            var testObject = new LazyLoadTestObject();

            // Assert
            Assert.IsNotNull(testObject.Numbers);
        }

        /// <summary>
        /// testObject should have TrueFalse set to true by LazyLoad(true)
        /// </summary>
        [TestMethod]
        public void LazyLoad_Object_Ctor_Test()
        {
            // Arrange
            // Act
            var testObject = new LazyLoadChangeDefaultValueObject();

            // Assert
            Assert.IsTrue(testObject.TrueFalse);
        }

        /// <summary>
        /// testObject should have TrueFalse set to true by LazyLoad(true)
        /// </summary>
        [ExpectedException(typeof(MissingMethodException))]
        [TestMethod]
        public void LazyLoad_Object_Ctor_NotEmpty_Test()
        {
            // Arrange
            var testObject = new LazyLoadPropertyCtorNotEmtpyObject();

            // Act
            object o = testObject.Name;

            // Assert
            // See ExpectedExeption attribute
        }

    }


}
