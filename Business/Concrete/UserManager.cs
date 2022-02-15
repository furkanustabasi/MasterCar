using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Core.Entities.Concrete;
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
    public class UserManager : IUserService
    {
        private IUserDal _userDal;

        public UserManager(IUserDal userDal)
        {
            _userDal = userDal;
        }

        public IResult Add(User user)
        {
            var rulesResult = BusinessRules.Run(CheckIfEmailExist(user.Email));
            if (!rulesResult.Success)
            {
                return rulesResult;
            }

            _userDal.Add(user);
            return new SuccessResult(Messages.AddedSuccess);
        }

        [SecuredOperation("admin,user.ops,user.delete")]
        public IResult Delete(User user)
        {
            if (!GetById(user.Id).Success)
            {
                return new ErrorResult(Messages.NotFound);
            }

            _userDal.Delete(user);
            return new SuccessResult(Messages.DeleteSuccess);
        }

        public IDataResult<List<User>> GetAll()
        {
            return new SuccessDataResult<List<User>>(_userDal.GetAll(), Messages.ListedSuccess);
        }

        public IDataResult<User> GetById(int id)
        {
            var result = _userDal.Get(x => x.Id == id);
            if (result != null)
            {
                return new SuccessDataResult<User>(result, Messages.ListedSuccess);
            }

            return new ErrorDataResult<User>(Messages.NotFound);
        }

        [SecuredOperation("admin,user.ops,user.update")]
        public IResult Update(User user)
        {
            var rulesResult = BusinessRules.Run(UpdateMailIfIsAvaible(user), CheckIfUserIdExist(user.Id));
            if (!rulesResult.Success)
            {
                return rulesResult;
            }

            _userDal.Update(user);
            return new SuccessResult(Messages.UpdateSuccess);
        }

        public IDataResult<User> GetUserByMail(string email)
        {
            var result = _userDal.Get(u => u.Email == email);
            if (result != null)
            {
                return new SuccessDataResult<User>(result);
            }

            return new ErrorDataResult<User>(Messages.NotFound);
        }


        public IDataResult<List<OperationClaim>> GetClaims(User user)
        {
            var rulesResult = BusinessRules.Run(CheckIfUserIdExist(user.Id));
            if (rulesResult != null)
            {
                return new ErrorDataResult<List<OperationClaim>>(rulesResult.Message);
            }

            return new SuccessDataResult<List<OperationClaim>>(_userDal.GetClaims(user));
        }

        private IResult CheckIfUserIdExist(int userId)
        {
            if (!_userDal.GetAll(u => u.Id == userId).Any())
            {
                return new ErrorResult(Messages.UserNotExist);
            }

            return new SuccessResult();
        }

        private IResult CheckIfEmailExist(string userEmail)
        {
            if (_userDal.GetAll(u => u.Email == userEmail).Any())
            {
                return new SuccessResult();
            }

            return new ErrorResult();
        }

        private IResult UpdateMailIfIsAvaible(User user)
        {
            if (!CheckIfEmailExist(user.Email).Success)
            {
                return new SuccessResult();
            }

            var result = _userDal.Get(u => u.Id == user.Id);

            if (result.Email == user.Email)
            {
                return new SuccessResult();
            }

            return new ErrorResult();
        }
    }
}
