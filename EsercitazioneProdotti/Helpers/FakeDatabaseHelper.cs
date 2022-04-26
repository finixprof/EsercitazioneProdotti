using EsercitazioneProdotti.Models;
using EsercitazioneProdotti.Models.Exception;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EsercitazioneProdotti.Helpers
{
    public static class FakeDatabaseHelper
    {
        private static Product[] products = new Product[]
            {
                new Product { Id = 1, Name = "Tomato Soup", Category = "Groceries", Price = 1 },
                new Product { Id = 2, Name = "Yo-yo", Category = "Toys", Price = 3.75M },
                new Product { Id = 3, Name = "Hammer", Category = "Hardware", Price = 16.99M }
            };

        public static IEnumerable<Product>  GetAllProducts()
        {
            return products;
        }

        public static Product GetProductById(int id)
        {
            if (!products.Any(t => t.Id == id))
                throw new CustomException(404,$"Non esiste il prodotto con id {id}");
            var product = products.First(t=>t.Id==id);
            return product;
        }

        public static bool Delete(int id)
        {
            if (!products.Any(t => t.Id == id))
                throw new CustomException(404, $"Non esiste il prodotto con id {id}");
            var p = products.FirstOrDefault(t => t.Id == id);
            var list = products.ToList();
            if (list.Remove(p))
            {
                products = list.ToArray();
                return true;
            }
            return false;
        }

        internal static bool Save(Product product)
        {
            if (product.Id>0)
            {
                return Update(product);
            }
            return Insert(product);
        }

        private static bool Insert(Product product)
        {
            var list = products.ToList();
            product.Id = list.Max(t => t.Id) + 1;
            list.Add(product);
            products = list.ToArray();
            return true;
        }

        private static bool Update(Product product)
        {
            if (!products.Any(t => t.Id == product.Id))
                throw new CustomException(404, $"Non esiste il prodotto con id {product.Id}");
            var p = products.FirstOrDefault(t => t.Id == product.Id);
            p.Name = product.Name;
            p.Category = product.Category;
            p.Price = product.Price;
            return true;
        }
    }
}
