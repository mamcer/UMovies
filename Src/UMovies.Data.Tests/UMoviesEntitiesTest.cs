using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UMovies.Data.Tests
{
    [TestClass]
    public class UMoviesEntitiesTest
    {
        [TestMethod]
        public void ConstructorShouldSetDatabaseName()
        {
            // Arrange
            var entities = new UMoviesEntities();
            string dbName;

            // Act
            dbName = entities.Database.Connection.Database;

            // Assert
            Assert.AreEqual("UMovies", dbName);
        }
    }
}