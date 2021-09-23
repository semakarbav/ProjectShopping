using Business.Abstract;
using Bussiness.Constants;
using Core.Business;
using Core.Helpers;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class ProductImageManager : IProductImageService
    {
        IProductImageDal _productImageDal;

        public ProductImageManager(IProductImageDal productImageDal)
        {
           _productImageDal = productImageDal;
        }

        public IResult Add(IFormFile file, ProductImage productImage)
        {
            var imageResult = FileHelper.Upload(file);

            if (!imageResult.Success)
            {
                return new ErrorResult(imageResult.Message);
            }
            productImage.ImagePath = imageResult.Message;
            productImage.Date = DateTime.Now;
            _productImageDal.Add(productImage);

            return new SuccessResult(Messages.ProductImageAdded);
        }

        public IResult Delete(ProductImage productImage)
        {
            var image = _productImageDal.Get(c => c.Id == productImage.Id);
            if (image == null)
            {
                return new ErrorResult("Resim Bulunamadı");
            }

            FileHelper.Delete(image.ImagePath);
            _productImageDal.Delete(productImage);
            return new SuccessResult(Messages.ProductImageDeleted);
        }

        public IDataResult<ProductImage> Get(int id)
        {
            return new SuccessDataResult<ProductImage>(_productImageDal.Get(p => p.Id == id));
        }

        public IDataResult<List<ProductImage>> GetAll()
        {
            return new SuccessDataResult<List<ProductImage>>(_productImageDal.GetAll());
        }

        public IDataResult<List<ProductImage>> GetAllByProductId(int productId)
        {
            return new SuccessDataResult<List<ProductImage>>(_productImageDal.GetAll(c => c.Id == productId));
        }

        public IDataResult<List<ProductImage>> GetImagesByProductId(int id)
        {
            IResult result = BusinessRules.Run(CheckIfCarImageNull(id));

            if (result != null)
            {
                return new ErrorDataResult<List<ProductImage>>(result.Message);
            }

            return new SuccessDataResult<List<ProductImage>>(CheckIfCarImageNull(id).Data);
        }

        public IResult Update(IFormFile file, ProductImage productImage)
        {
            var isImage = _productImageDal.Get(c => c.Id == productImage.Id);
            if (isImage == null)
            {
                return new ErrorResult("Resim bulunamadı");
            }

            var updatedFile = FileHelper.Update(file, isImage.ImagePath);
            if (!updatedFile.Success)
            {
                return new ErrorResult(updatedFile.Message);
            }
            productImage.ImagePath = updatedFile.Message;
            _productImageDal.Update(productImage);
            return new SuccessResult(Messages.ProductImageUpdated);
        }

        private IDataResult<List<ProductImage>> CheckIfCarImageNull(int id)
        {
            try
            {
                string path = @"\images\logo.jpg";
                var result = _productImageDal.GetAll(p => p.ProductId == id).Any();
                if (!result)
                {
                    List<ProductImage> productimage = new List<ProductImage>();
                    productimage.Add(new ProductImage { ProductId = id, ImagePath = path, Date = DateTime.Now });
                    return new SuccessDataResult<List<ProductImage>>(productimage);
                }
            }
            catch (Exception exception)
            {

                return new ErrorDataResult<List<ProductImage>>(exception.Message);
            }

            return new SuccessDataResult<List<ProductImage>>(_productImageDal.GetAll(p => p.ProductId == id).ToList());
        }

    }
}
