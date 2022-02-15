using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
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
    public class ColorManager : IColorService
    {
        private IColorDal _colorDal;

        public ColorManager(IColorDal colorDal)
        {
            _colorDal = colorDal;
        }

        [SecuredOperation("admin, color.ops, color.add")]
        [ValidationAspect(typeof(ColorValidator))]
        public IResult Add(Color color)
        {
            _colorDal.Add(color);
            return new SuccessResult();
        }

        [SecuredOperation("admin, color.ops, color.delete")]
        [ValidationAspect(typeof(ColorValidator))]
        public IResult Delete(Color color)
        {
            var rulesResult = BusinessRules.Run(CheckIfExist(color.Id));
            if (rulesResult != null)
            {
                return rulesResult;
            }

            _colorDal.Delete(color);
            return new SuccessResult();
        }

        public IDataResult<List<Color>> GetAll()
        {
            return new SuccessDataResult<List<Color>>(_colorDal.GetAll());

        }

        public IDataResult<Color> GetById(int id)
        {
            var result=  _colorDal.Get(c => c.Id == id);

            if (result != null)
            {
                return new SuccessDataResult<Color>(result);
            }

            return new ErrorDataResult<Color>();
        }


        [SecuredOperation("admin, color.ops, color.update")]
        [ValidationAspect(typeof(ColorValidator))]
        public IResult Update(Color color)
        {
            var rulesResult = BusinessRules.Run(CheckIfExist(color.Id));
            if (rulesResult != null)
            {
                return rulesResult;
            }

            _colorDal.Update(color);
            return new SuccessResult();
        }

        private IResult CheckIfExist(int id)
        {
            var result = _colorDal.GetAll(c => c.Id == id).Any();
            if (!result)
            {
                return new ErrorResult(Messages.NotFound);
            }
            return new SuccessResult();
        }
    }
}
