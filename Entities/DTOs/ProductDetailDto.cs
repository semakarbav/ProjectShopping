using Core.Entities.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities.DTOs
{
    //Data Transformation object 
    public class ProductDetailDto :IDto
    {
       
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string CategoryName { get; set; }
        public short UnitsInStock { get; set; }
        public decimal Price { get; set; }
        public List<ProductImage> ImagePath { get; set; }
    }
}
