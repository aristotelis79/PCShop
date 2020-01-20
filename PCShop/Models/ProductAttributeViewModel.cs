namespace PCShop.Models
{
    /// <summary>
    /// Represents product attribute view model
    /// </summary>
    public class ProductAttributeViewModel
    {
        /// <summary>
        /// Gets or sets the product attribute view model identifier
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the product attribute view model name description 
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Get or set the product attribute view model unit 
        /// </summary>
        public string Unit { get; set; }

        /// <summary>
        /// Get or set the product attribute view model value 
        /// </summary>
        public int Value { get; set; } = 1;
    }
}