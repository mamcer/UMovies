using System;

namespace UMovies.Data
{
    public interface IUnitOfWork : IDisposable
    {
        void SaveChanges();
    }
}