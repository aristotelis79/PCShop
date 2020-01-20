using System.Collections.Generic;
using System.Linq;
using PCShop.Data.Entities;

namespace PCShop.Models.Mapping
{
    /// <summary>
    /// Helper mapping Entities to view model class 
    /// </summary>
    public static class Map
    {
        /// <summary>
        /// Mapping product entity to product view model
        /// </summary>
        /// <param name="entity">Product</param>
        /// <returns>ProductViewModel</returns>
        public static ProductViewModel ToViewModel(this Product entity)
        {
            var model = new ProductViewModel
            {
                Id = entity.Id,
                Name = entity.Name
            };

            var allProductAttributes = GetsProductAttributesAndChildrenProductAttributes(entity.ProductComponent, new List<ProductAttribute>());

            foreach (var p in allProductAttributes)
            {
                model.Attributes.Add(new ProductAttributeViewModel
                {
                    Id = p.Id,
                    Name = p.Name,
                    Unit = p.Unit
                });
            }

            return model;
        }

        /// <summary>
        /// Mapping product entities to list of products view models
        /// </summary>
        /// <param name="entities">List of Products</param>
        /// <returns>List of ProductViewModel</returns>
        public static List<ProductViewModel> ToViewModels(this List<Product> entities)
        {
            return entities.Select(e => e.ToViewModel()).ToList();
        }

        /// <summary>
        /// Get all products attributes and children products attribute
        /// </summary>
        /// <param name="productComponent">Product component</param>
        /// <param name="productAttributes">List of product attributes</param>
        /// <returns>List of product attribute</returns>
        private static List<ProductAttribute> GetsProductAttributesAndChildrenProductAttributes(ProductComponent productComponent, List<ProductAttribute> productAttributes)
        {
            if (productComponent == null) return new List<ProductAttribute>();

            //Add parents product attributes to list
            productAttributes.AddRange(productComponent?.ProductAttributesMap.Select(s=>s.ProductAttribute).ToList());

            if (productComponent.ChildrenProductComponents == null) return productAttributes.Distinct().ToList();
            
            foreach (var childrenProductComponent in productComponent.ChildrenProductComponents)
            {
                // Get products children product attributes
                var childrenProductAttributes = childrenProductComponent?.ProductAttributesMap.Select(s => s.ProductAttribute).ToList();
                
                //add only new to products attribute  list
                productAttributes.AddRange(childrenProductAttributes?.Where(x => productAttributes.All(y => x.Id != y.Id)));

                if (childrenProductComponent?.ChildrenProductComponents != null &&
                    childrenProductComponent.ChildrenProductComponents.Any())
                {
                    //recursive call for children product attribute
                    GetsProductAttributesAndChildrenProductAttributes(childrenProductComponent, productAttributes);
                }
            }
            
            return productAttributes.Distinct().ToList();
        }
    }
}