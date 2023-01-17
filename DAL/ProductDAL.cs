using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UASWebApp2001092046.Models;

namespace UASWebApp2001092046.DAL
{
    public class ProductDAL : IProduct
    {
        private readonly UasDbContext _dbContext;
        public ProductDAL(UasDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Add(Product product)
        {
            try
            {
                _dbContext.Add(product);
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {
                
                throw new Exception(ex.Message);
            }
        }

        public void Delete(int id)
        {
            var delProduct = Get(id);
            if(delProduct!=null)
            {
                _dbContext.Remove(delProduct);
                _dbContext.SaveChanges();
            }
            else
            {
                throw new Exception($"Id{id} data tidak ditemukan");
            }
        }

        public Product Get(int id)
        {
            var result = _dbContext.Products.FirstOrDefault(t=>t.Id==id);
            if(result==null) throw new Exception($"Id{id} data tidak ditemukan");
            return result;
        }

        public IEnumerable<Product> GetAll()
        {
            var result = _dbContext.Products.OrderBy(t=>t.Name);
            return result;
        }

        public void Update(int id, Product product)
        {
           var updateProduct = Get(id);
            if(updateProduct!=null)
            {
                updateProduct.Name = product.Name;
                updateProduct.Stock = product.Stock;
                updateProduct.Price = product.Price;
                _dbContext.SaveChanges();
            }
            else
            {
                throw new Exception($"Data Id:{id} data tidak ditemukan");
            }
        }
    }
}