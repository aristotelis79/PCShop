namespace PCShop.Data.Entities
{
    /// <summary>
    /// Represents the product component attribute mapping 
    /// </summary>
    public class ProductComponentAttributeMap : BaseEntity<int>
    {
        /// <summary>
        /// Navigation property of product component identifier 
        /// </summary>
        public int ProductComponentId { get; set; }

        /// <summary>
        /// Navigation property for product component
        /// </summary>
        public virtual ProductComponent ProductComponent { get; set; }
 
        /// <summary>
        /// Navigation property of product attribute identifier 
        /// </summary>
        public int ProductAttributeId { get; set; }

        /// <summary>
        /// Navigation property for product attributes
        /// </summary>
        public virtual ProductAttribute ProductAttribute { get; set; } 
    }
}