namespace UMovies.Data
{
    public interface IRepository<TEntity, TKey> where TEntity : class
    {
        TEntity GetById(TKey id);

        void Create(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);
    }
}