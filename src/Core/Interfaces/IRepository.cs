using System.Collections.Generic;
using System.Threading.Tasks;
using Core.Entities;

namespace Core.Interfaces
{
    /// <summary>
    /// The Repository is an abstraction of our Data Access Layer.
    /// <para>This allows our logic to be "persistent ignorant"</para>
    /// <para>See <see cref="https://deviq.com/repository-pattern/"/> for more details</para>
    /// </summary>
    public interface IRepository
    {
        /// <summary>
        /// Gets a single entity by its <paramref name="id"/> asynchronously
        /// </summary>
        /// <typeparam name="T">The Type of the Entity</typeparam>
        /// <param name="id"></param>
        /// <returns>A single entity</returns>
        Task<T> GetByIdAsync<T>(int id) where T : BaseEntity;

        /// <summary>
        /// Gets a single entity by the <paramref name="spec"/> asynchronously
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="spec">The Specification for what to return</param>
        /// <returns>A single entity</returns>
        Task<T> GetSingleBySpecAsync<T>(ISpecification<T> spec) where T : BaseEntity;

        /// <summary>
        /// Lists all the entities of the type <typeparamref name="T"/> asynchronously
        /// </summary>
        /// <typeparam name="T">The type of the entity</typeparam>
        /// <returns>A list of all the entities</returns>
        Task<IReadOnlyList<T>> ListAllAsync<T>() where T : BaseEntity;

        /// <summary>
        /// Lists all the entities that match the <paramref name="spec"/> asynchronously
        /// </summary>
        /// <returns>The list of entities asynchronously.</returns>
        /// <param name="spec">The specification of entities. See <see cref="ISpecification{T}"/> </param>
        /// <typeparam name="T">The type of the entity</typeparam>
        Task<IReadOnlyList<T>> ListAsync<T>(ISpecification<T> spec) where T : BaseEntity;

        /// <summary>
        /// Counts the entities that match the <paramref name="spec"/> asynchronously
        /// </summary>
        /// <returns>The number of entities that match the <paramref name="spec"/></returns>
        /// <param name="spec">The specification of entities to count. See <see cref="ISpecification{T}"/> </param>
        /// <typeparam name="T">The type of the entity to count</typeparam>
        Task<int> CountAsync<T>(ISpecification<T> spec) where T : BaseEntity;

        /// <summary>
        /// Adds an entity to the repository Asynchronously
        /// </summary>
        /// <returns>The entity that was added.</returns>
        /// <param name="entity">The entity to add</param>
        /// <typeparam name="T">The type of the entity to add</typeparam>
        Task<T> AddAsync<T>(T entity) where T : BaseEntity;

        /// <summary>
        /// Adds a list of entities to the repository asynchronously
        /// </summary>
        /// <param name="entities">The list of entities to add</param>
        /// <typeparam name="T">The type of the entities to add</typeparam>
        Task AddAsync<T>(List<T> entities) where T : BaseEntity;

        /// <summary>
        /// Updates an entity in the repository asynchronously
        /// </summary>
        /// <param name="entity">The entity to update</param>
        /// <typeparam name="T">The type of the entity to update</typeparam>
        Task UpdateAsync<T>(T entity) where T : BaseEntity;

        /// <summary>
        /// Updates a list of entities asynchronously
        /// </summary>
        /// <param name="entities">The list of entities to update</param>
        /// <typeparam name="T">The type of the entities to update</typeparam>
        Task UpdateAsync<T>(List<T> entities) where T : BaseEntity;

        /// <summary>
        /// Deletes an entity from the repository asynchronously
        /// </summary>
        /// <param name="entity">The entity to delete</param>
        /// <typeparam name="T">The type of the entity to delete</typeparam>
        Task DeleteAsync<T>(T entity) where T : BaseEntity;

        /// <summary>
        /// Deletes a list of entities from the repository asynchronously
        /// </summary>
        /// <param name="entities">The list of entities to delete</param>
        /// <typeparam name="T">The type of the entity to delete</typeparam>
        Task DeleteAsync<T>(List<T> entities) where T : BaseEntity;
    }
}
