using System.Collections.Generic;

namespace PCShop.Data.Entities
{
    /// <summary>
    /// Represents Product
    /// </summary>
    public class Product : BaseEntity<int>
    {
        /// <summary>
        /// Gets or sets the product name description
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Navigation property of product component identifier
        /// </summary>
        public int? ProductComponentId { get; set; }

        /// <summary>
        /// Navigation property of product component
        /// </summary>
        public virtual ProductComponent ProductComponent { get; set; }
    }
}