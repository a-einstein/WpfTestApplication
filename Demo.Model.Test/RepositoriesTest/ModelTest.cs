﻿using Demo.ServiceClients.Products.ServiceReference;

namespace Demo.Model.Test
{
    public abstract class ModelTest
    {
        public static ProductsOverviewRowDto ProductsOverviewRowDto(int id, object noId)
        {
            // TODO Put definition of dto in other project and reuse in service client to remove that dependency ?
            var dto = new ProductsOverviewRowDto()
            {
                Color = "a Color",
                ListPrice = (decimal)id,
                Name = Format("Name", id),
                ProductCategory = Format("ProductCategory", id),
                ProductCategoryID = (int)noId,
                ProductID = id,
                ProductSubcategory = Format("ProductSubcategory", id),
                Size = id.ToString(),
                SizeUnitMeasureCode = "SUM",
                ThumbNailPhoto = new byte[0],
                WeightUnitMeasureCode = "WUM"
            };

            return dto;
        }

        public static string Format(string aString, int anInt)
        {
            return string.Format("{0} {1}", aString, anInt);
        }
    }
}