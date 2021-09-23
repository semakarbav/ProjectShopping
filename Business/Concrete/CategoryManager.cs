using Business.Abstract;
using Bussiness.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        ICategoryDal _categorDal;

        public CategoryManager(ICategoryDal categoryDal)
        {
            _categorDal = categoryDal;
        }

        public IResult Add(Category category)
        {
            _categorDal.Add(category);
            return new SuccessResult(Messages.CategoryAdded);
        }

        public IResult Delete(Category category)
        {
            _categorDal.Delete(category);
            return new SuccessResult(Messages.CategoryDeleted);
        }

        public IDataResult<List<Category>> GetAll()
        {
            return new SuccessDataResult<List<Category>>(_categorDal.GetAll(),Messages.CategoriesListed);
        }

        public IDataResult<Category> GetById(int categoryId) //kategori filtreleme
        {
            return new SuccessDataResult<Category>(_categorDal.Get(c=>c.CategoryId==categoryId));
        }

        public IResult Update(Category category)
        {
            _categorDal.Update(category);
            return new SuccessResult(Messages.CategoryUpdated);
        }
    }
}
