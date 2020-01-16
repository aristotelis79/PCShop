using System.Collections;
using System.Collections.Generic;

namespace PCShop.Data.Entities
{
    /// <summary>
    /// Represents the product attribute 
    /// </summary>
    public class ProductAttribute : BaseEntity<int>
    {
        /// <summary>
        /// Gets or sets the product attribute name description
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Get or set the product attribute unit
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// Navigation property for products components attributes map
        /// </summary>
        public virtual ICollection<ProductComponentAttributeMap> ProductAttributesMap { get; set; }
    }
}