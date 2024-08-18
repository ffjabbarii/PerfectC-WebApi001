#region usings -----------------------------------------------------------------

using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

#endregion

namespace ApiTemplate.Models
{
    /// <summary>
    /// Represents the content of a basic Product entity in the API.
    /// This model is validated upon receiving an HTTP request to ensure the provided data meets the API's requirements.
    /// </summary>
    public class Product
    {
        #region properties ------------------------------------------------------

        [Required(ErrorMessage = "The Product ID is required.")]
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [Required(ErrorMessage = "The Product Name is required.")]
        [StringLength(50, ErrorMessage = "The Product Name must be less than 50 characters.")]
        [JsonPropertyName("name")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "The Product Price is required.")]
        [Range(0.01, 999999.99, ErrorMessage = "The Product Price must be between 0.01 and 999999.99.")]
        [JsonPropertyName("price")]
        public decimal Price { get; set; }

        #endregion
    }
}