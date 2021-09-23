using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfProductDal : EfEntityRepositoryBase<Product, ShoppingContext>, IProductDal
    {

       
        public List<ProductDetailDto> GetProductDetails(Expression<Func<ProductDetailDto, bool>> filter = null)
        {
            using (ShoppingContext context=new ShoppingContext())
            {
                var result = from p in context.Products
                             join c in context.Categories on p.CategoryId equals c.CategoryId
                             

                             select new ProductDetailDto
                             {
                                 ProductId = p.ProductId,
                                 ProductName = p.ProductName,
                                 CategoryName = c.CategoryName,
                                 Price = p.Price,
                                 UnitsInStock = p.UnitsInStock,
                                 ImagePath =  (from pi in context.ProductImages
                                                         where (p.ProductId == pi.ProductId)
                                                         select new ProductImage { Id = pi.Id, 
                                                             ProductId = p.ProductId, Date = pi.Date,
                                                             ImagePath = pi.ImagePath }).ToList()
                                
                             };
               
                    return filter == null ?result.ToList() : result.Where(filter).ToList();



            }
        }

        

    }
}
