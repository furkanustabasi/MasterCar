using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class BrandManager : IBrandService
    {
        private IBrandDal _brandDal;

        public BrandManager(IBrandDal brandDal)
        {
            _brandDal = brandDal;
        }

        [ValidationAspect(typeof(BrandValidator))]
        [SecuredOperation("admin, brand.ops, brand.add")]
        [CacheRemoveAspect("IBrandService.Get")]
        public IResult Add(Brand brand)
        {
            var result = BusinessRules.Run(CheckIfBrandNameExist(brand.Name));
            if (result.Success)
            {
                return new ErrorResult(result.Message);
            }

            _brandDal.Add(brand);
            return new SuccessResult(Messages.AddedSuccess);
        }

        [ValidationAspect(typeof(BrandValidator))]
        [SecuredOperation("admin, brand.ops, brand.delete")]
        [CacheRemoveAspect("IBrandService.Get")]
        public IResult Delete(Brand brand)
        {
            _brandDal.Delete(brand);
            return new SuccessResult(Messages.DeleteSuccess);
        }


        [CacheAspect(10)]
        public IDataResult<List<Brand>> GetAll()
        {
            return new SuccessDataResult<List<Brand>>(_brandDal.GetAll(), Messages.ListedSuccess);
        }

        [CacheAspect(10)]
        public IDataResult<Brand> GetById(int id)
        {
            return new SuccessDataResult<Brand>(_brandDal.Get(x => x.Id == id), Messages.ListedSuccess);

        }


        [ValidationAspect(typeof(BrandValidator))]
        [SecuredOperation("admin, brand.ops, brand.update")]
        [CacheRemoveAspect("IBrandService.Get")]
        public IResult Update(Brand brand)
        {
            var result = BusinessRules.Run(CheckIfBrandNameExist(brand.Name));
            if (!result.Success)
            {
                return new ErrorResult(Messages.NotFound);
            }

            _brandDal.Update(brand);
            return new SuccessResult(Messages.UpdateSuccess);
        }


        private IResult CheckIfBrandNameExist(string brandName)
        {
            var result = _brandDal.GetAll(b => Equals(b.Name, brandName)).Any();
            if (result)
            {
                return new SuccessResult(Messages.IsExist);
            }
            return new ErrorResult();
        }
    }
}
