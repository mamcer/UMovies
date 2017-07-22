using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UMovies.Core.Test
{
    [TestClass]
    public class MovieTest
    {
        [TestMethod]
        public void ConstructorShouldSetMovieFiles()
        {
            // Arrange 
            Movie movie;

            // Act
            movie = new Movie();

            // Assert
            Assert.IsNotNull(movie.MovieFiles);
        }
    }
}
