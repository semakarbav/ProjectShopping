using Business.Abstract;
using Business.Validations.FluentValidation;
using Bussiness.Constants;
using Core.Aspect.Autofac.Logging;
using Core.Aspect.Autofac.Validation;
using Core.CrossCuttingConcerns.Logging.Log4Net.Loggers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal;
        IProductImageDal _productImageDal;

        public ProductManager(IProductDal productDal,IProductImageDal productImageDal)
        {
            _productDal = productDal;
            _productImageDal = productImageDal;
        }

        [ValidationAspect(typeof(ProductValidator))]

        public IResult Add(Product product)
        {
           
            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);
        }

        public IResult Delete(Product product)
        {
           
            _productDal.Delete(product);
           
            return new SuccessResult(Messages.ProductDeleted);
        }


        public IDataResult<Product> GetById(int productId)
        {
            return new SuccessDataResult<Product> (_productDal.Get(p => p.ProductId == productId));
        }

        
        public IResult Update(Product product)
        {
            _productDal.Update(product);
            return new SuccessResult(Messages.ProductUpdated);
        }

        public IDataResult<List<Product>> GetAll()
        {
            return new SuccessDataResult<List<Product>> (_productDal.GetAll(),Messages.ProductsListed);
        }

       // [LogAspect(typeof(FileLogger))]
        public IDataResult<List<Product>> GetAllCategoryId(int id)
        {
            return new SuccessDataResult<List<Product>>  (_productDal.GetAll(p => p.CategoryId == id));
        }

        public IDataResult<ProductDetailDto> GetProductDetails(int productId)
        {
            
            return new SuccessDataResult<ProductDetailDto>(_productDal.GetProductDetails(p=>p.ProductId== productId).FirstOrDefault());
        }
    }
}
