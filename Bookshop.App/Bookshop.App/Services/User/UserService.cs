using AutoMapper;
using Bookshop.App.Models.Auth;
using Bookshop.App.Models.User;
using Bookshop.Data.Extensions;
using Bookshop.Data.Model;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Bookshop.App.Services.User
{
    public class UserService: BaseService
    {
        protected IPasswordHasher<Data.Model.User> Hasher { get; }

      

        public UserService(IPasswordHasher<Data.Model.User> hasher,BaseContext context, IMapper mapper) : base(mapper, context)
        {
            Hasher = hasher;
        }
        public Data.Model.User GetByEmail(string email)
        {
            var user = Context.Users.Where(x => x.Email == email).FirstOrDefault();
            return user;
        }
        #region Create()
        public Data.Model.User Create(RegisterFormModel model)
        {
            var entity = Mapper.Map<Data.Model.User>(model);
            try
            {
                SetPassword(entity, model.Password);
                SetEntity(entity);
                Context.Add<Data.Model.User>(entity);
                Context.SaveChanges();
            }
            catch (Exception ex)
            {
                if (ex.InnerException.Message.Contains("IX_Email"))
                    throw new Exception("Podany adres email jest zajęty");
                if (ex.InnerException.Message.Contains("IX_UserName"))
                    throw new Exception("Podany adres email jest zajęty");
            }
            return entity;
          
           
        }
        #endregion
        #region SetPassword()
        public bool SetPassword(Data.Model.User user, string password)
        {
            if (user != null)
            {
                user.PasswordHash = Hasher.HashPassword(user, password);
                return true;
            }

            return false;
        }
        #endregion
        public bool SetEntity(Data.Model.User user)
        {
            if (user != null)
            {
                user.PublicId= Guid.NewGuid().ToString();
                user.UserName= user.Email;
                user.DateCreatedUtc= DateTime.Now;
                user.DateModifiedUtc= DateTime.Now;

                return true;
            }

            return false;
        }

      
    }
}
