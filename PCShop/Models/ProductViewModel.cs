using System.Collections.Generic;

namespace PCShop.Models
{
    /// <summary>
    /// Represents product view model
    /// </summary>
    public class ProductViewModel
    {
        /// <summary>
        /// Gets or sets the product view model identifier
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the product view model name description 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Represents the products attributes view model 
        /// </summary>
        public List<ProductAttributeViewModel> Attributes { get; set; } = new List<ProductAttributeViewModel>();

    }
}