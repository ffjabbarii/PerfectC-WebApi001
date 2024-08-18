#region usings -----------------------------------------------------------------

using ApiTemplate.Models;
using EmailTemplateAPI.Models;
using MimeKit;

#endregion

namespace ApiTemplate.Services.Interfaces
{
    /// <summary>
    /// Defines the general product service within the application.
    /// </summary>
    public interface IProductService
    {
        /// <summary>
        /// Retrieves a collection of products asynchronously.
        /// </summary>
        /// <returns> A collection of <see cref="Product"/> entities. </returns>
        Task<IEnumerable<Product>> GetProducts();

        /// <summary>
        /// Retrieves a product by its unique identifier asynchronously.
        /// </summary>
        /// <param name="id"> The unique identifier of the product to retrieve. </param>
        /// <returns> The <see cref="Product"/> entity with the specified identifier or null if not found. </returns>
        Task<Product?> GetProductById(int id);

        /// <summary>
        /// Adds a new product to the collection asynchronously.
        /// </summary>
        /// <param name="product"> The product to add to the collection. </param>
        Task AddProduct(Product product);

        /// <summary>
        /// Updates an existing product in the collection asynchronously.
        /// </summary>
        /// <param name="product"> The product to update in the collection. </param>
        /// <returns> The updated <see cref="Product"/> entity. </returns>
        Task<Product> UpdateProduct(Product product);

        /// <summary>
        /// Deletes a product from the collection asynchronously.
        /// </summary>
        /// <param name="id"> The unique identifier of the product to delete. </param>
        /// <returns> A boolean value indicating whether the product was successfully deleted. </returns>
        Task<bool> DeleteProduct(int id);
    }
}