using Core.Utilities.Results;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IProductImageService
    {
        IDataResult<List<ProductImage>> GetAll();
        IDataResult<ProductImage> Get(int id);
        IDataResult<List<ProductImage>> GetAllByProductId(int productId);
        IDataResult<List<ProductImage>> GetImagesByProductId(int id); //ürüne ait resim var mı kontrolü
        IResult Add(IFormFile file, ProductImage productImage);
        IResult Delete(ProductImage productImage);
        IResult Update(IFormFile file, ProductImage productImage);
    }
}
