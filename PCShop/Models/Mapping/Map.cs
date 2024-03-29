﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

            var productAttributes = GetsProductAttributesAndChildrenComponentProductAttributes(entity.ProductComponent, new List<ProductAttribute>());

            foreach (var p in productAttributes)
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
        /// <returns>ProductViewModel</returns>
        public static List<ProductViewModel> ToViewModels(this List<Product> entities)
        {
            return entities.Select(e => e.ToViewModel()).ToList();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="productComponent">ProductComponent</param>
        /// <param name="productAttributes"></param>
        /// <returns></returns>
        private static List<ProductAttribute> GetsProductAttributesAndChildrenComponentProductAttributes(ProductComponent productComponent, List<ProductAttribute> productAttributes)
        {
            if (productComponent == null) return new List<ProductAttribute>();

            //Add parents product attributes to list
            productAttributes.AddRange(productComponent?.ProductAttributesMap.Select(s=>s.ProductAttribute).ToList());

            if (productComponent?.ChildrenProductComponents != null)
            {
                foreach (var childrenProductComponent in productComponent?.ChildrenProductComponents)
                {
                    // Get products children product attributes
                    var childrenProductAttributes = childrenProductComponent.ProductAttributesMap.Select(s => s.ProductAttribute).ToList();
                    
                    //add only new to products attribute  list
                    productAttributes.AddRange(childrenProductAttributes.Where(x => productAttributes.All(y => x.Id != y.Id)));

                    if (childrenProductComponent?.ChildrenProductComponents != null &&
                        childrenProductComponent.ChildrenProductComponents.Any())
                    {
                        GetsProductAttributesAndChildrenComponentProductAttributes(childrenProductComponent, productAttributes);
                    }
                }
            }

            return productAttributes.Distinct().ToList();
        }
    }
}