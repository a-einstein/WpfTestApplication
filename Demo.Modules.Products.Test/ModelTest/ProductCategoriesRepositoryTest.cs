﻿using Common.DomainClasses;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Demo.Modules.Products.Model.Test
{
    [TestClass()]
    public class ProductCategoriesRepositoryTest
    {
        [TestMethod()]
        public void ListTest()
        {
            var target = ProductCategoriesRepository.Instance;
            var element = new ProductCategory() { Name = "CategoryName" };

            target.Clear();
            target.List.Add(element);

            var result = target.List.Count;

            Assert.AreEqual(1, result);
        }
    }
}