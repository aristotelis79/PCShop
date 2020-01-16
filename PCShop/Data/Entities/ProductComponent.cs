using System.Collections.Generic;

namespace PCShop.Data.Entities
{
    /// <summary>
    /// Represents product component
    /// </summary>
    public class ProductComponent : BaseEntity<int>
    {
        /// <summary>
        /// Gets or sets the product component name description
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the parent product component identifier
        /// </summary>
        public int? ParentProductComponentId { get; set; }

        /// <summary>
        /// Navigation property of parent product component
        /// </summary>
        public virtual ProductComponent ParentProductComponent { get; set; }

        /// <summary>
        /// Navigation property of product components 
        /// </summary>
        public virtual ICollection<ProductComponent> ChildrenProductComponents { get; set; }

        /// <summary>
        /// Navigation property of product
        /// </summary>
        public virtual ICollection<Product> Products { get; set; }

        /// <summary>
        /// Navigation property for products component attributes map
        /// </summary>
        public virtual ICollection<ProductComponentAttributeMap> ProductAttributesMap { get; set; }
    }
}