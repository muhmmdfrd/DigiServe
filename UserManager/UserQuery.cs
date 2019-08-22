using Core.Manager.PersonManager;
using MiniMenu.Models;
using Repository1;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Core.Manager.UserManager
{
    public class UserQuery : AsistanceBase<UserAdapter, User>
    {
        public UserQuery(UserAdapter manager) : base(manager)
        {
            // code here
        }

        public IQueryable<User> All(bool includeDetail = false)
        {
            IQueryable<User> query = includeDetail ? Manager.Database.Users.AsQueryable().Include(s => s.Person) : Manager.Database.Users;

            return query;
        }

        public IQueryable<UserDTO> ALlDTO()
        {
            return (from u in All()
                    select new UserDTO()
                    {
                        UserId = u.UserId,
                        Username = u.Username,
                        IMEI = u.IMEI,
                        Status = u.Status,
                        RoleId = u.RoleId,
                        PersonId = u.PersonId,
                        Token = u.Token,
                        CreatedBy = u.CreatedBy,
                        CreatedDate = u.CreatedDate,
                        UpdatedBy = u.UpdatedBy,
                        UpdatedDate = u.UpdatedDate
                    });
        }



       

        public User Validate(string usr, bool includeDetail = false)
        {
            return All(includeDetail).FirstOrDefault(scope => scope.Username == usr);
        }

        public Person WithPerson(long responsePersonId)
        {
            return Manager.PersonManager.Value.Database.People.FirstOrDefault(scope => scope.PersonId == responsePersonId);
        }

        public User ValidateLoginImei(string imei, bool includeDetail = false)
        {
            return All(includeDetail).FirstOrDefault(scope => scope.IMEI == imei);
        }
        public User CheckToken(string token)
        {
            var TokenValue = Manager.Database.Users.FirstOrDefault(scope => scope.Token == token);
            if (TokenValue == null)
            {
                return TokenValue;
            }
            else
            {
                return null;
            }
        }

        public UserDTO GetUserByToken(string token)
        {
            return Transform().FirstOrDefault(scope => scope.Token == token);

        }

        public User GetId(long id)
        {
            var exist = Manager.Query.Value.All().FirstOrDefault(scope => scope.UserId == id);
            if (exist == null)
            {
                throw new Exception("User doesn't exist");
            }

            return exist;
        }

        public UserDTO UserId(long id)
        {
            //return new UserDTO()
            //{
            //    UserId = GetId(id).UserId,
            //    Username = GetId(id).Username,
            //    IMEI = GetId(id).IMEI,
            //    Status = GetId(id).Status,
            //    RoleId = GetId(id).RoleId,
            //    PersonId = GetId(id).PersonId,
            //    Token = GetId(id).Token,
            //    CreatedBy = GetId(id).CreatedBy,
            //    CreatedDate = GetId(id).CreatedDate,
            //    UpdatedBy = GetId(id).UpdatedBy,
            //    UpdatedDate = GetId(id).UpdatedDate
            //};

            var user = GetId(id);
            if (user == null)
                return null;

            return new UserDTO()
            {
                UserId = user.UserId,
                Username = user.Username,
                IMEI = user.IMEI,
                Status = user.Status,
                RoleId = user.RoleId,
                PersonId = user.PersonId,
                Token = user.Token,
                CreatedBy = user.CreatedBy,
                CreatedDate = user.CreatedDate,
                UpdatedBy = user.UpdatedBy,
                UpdatedDate = user.UpdatedDate
            };
        }

        public UserDTO LetIt(string usr)
        {
            var user = Validate(usr, true);
            if (user == null)
            {
                return null;
            }

            return new UserDTO()
            {
                UserId = user.UserId,
                Name = user.Person.Name,
                Username = user.Username,
                Password = user.Password,
                IMEI = user.IMEI,
                Status = user.Status,
                RoleId = user.RoleId,
                PersonId = user.PersonId,
                Token = user.Token,
                CreatedBy = user.CreatedBy,
                CreatedDate = user.CreatedDate,
                UpdatedBy = user.UpdatedBy,
                UpdatedDate = user.UpdatedDate
            };
        }

        public UserDTO LoginImei(string imei)
        {
            var user = ValidateLoginImei(imei, true);
            if (user == null)
            {
                return null;
            }

            return new UserDTO()
            {
                UserId = user.UserId,
                Name = user.Person.Name,
                Username = user.Username,
                Password = user.Password,
                IMEI = user.IMEI,
                Status = user.Status,
                RoleId = user.RoleId,
                PersonId = user.PersonId,
                Token = user.Token,
                CreatedBy = user.CreatedBy,
                CreatedDate = user.CreatedDate,
                UpdatedBy = user.UpdatedBy,
                UpdatedDate = user.UpdatedDate
            };
        }

        public UserDTO TransformPost(long id)
        {
            return (from transform in All()
                    where transform.UserId == id
                    select new UserDTO()
                    {
                        UserId = transform.UserId,
                        Username = transform.Username,
                        IMEI = transform.IMEI,
                        Status = transform.Status,
                        RoleId = transform.RoleId,
                        PersonId = transform.PersonId,
                        Token = transform.Token,
                        CreatedBy = transform.CreatedBy,
                        CreatedDate = transform.CreatedDate,
                        UpdatedBy = transform.UpdatedBy,
                        UpdatedDate = transform.UpdatedDate
                    }).FirstOrDefault();
        }

        public PersonUserDTO TransformJoinUser(long id)
        {
            return (from user in All(true)
                    where user.UserId == id
                    select new PersonUserDTO()
                    {
                        UserId = user.UserId,
                        Username = user.Username,
                        IMEI = user.IMEI,
                        Status = user.Status,
                        RoleId = user.RoleId,
                        PersonId = user.Person.PersonId,
                        Name = user.Person.Name,
                        Address = user.Person.Address,
                        DateOfBirth = user.Person.DateOfBirth,
                        Gender = user.Person.Gender,
                        Token = user.Token,
                        CreatedBy = user.CreatedBy,
                        CreatedDate = user.CreatedDate,
                        UpdatedBy = user.UpdatedBy,
                        UpdatedDate = user.UpdatedDate
                    }).FirstOrDefault();
        }

        public List<PersonUserDTO> TransformJoinAll()
        {
            return (from user in All(true)
                    select new PersonUserDTO()
                    {
                        UserId = user.UserId,
                        Username = user.Username,
                        IMEI = user.IMEI,
                        Status = user.Status,
                        RoleId = user.RoleId,
                        PersonId = user.Person.PersonId,
                        Name = user.Person.Name,
                        Address = user.Person.Address,
                        DateOfBirth = user.Person.DateOfBirth,
                        Gender = user.Person.Gender,
                        Token = user.Token,
                        CreatedBy = user.CreatedBy,
                        CreatedDate = user.CreatedDate,
                        UpdatedBy = user.UpdatedBy,
                        UpdatedDate = user.UpdatedDate
                    }).ToList();
        }

        public List<UserDTO> Transform()
        {
            return (from transform in All()
                    select new UserDTO()
                    {
                        UserId = transform.UserId,
                        Username = transform.Username,
                        IMEI = transform.IMEI,
                        Status = transform.Status,
                        RoleId = transform.RoleId,
                        PersonId = transform.PersonId,
                        Token = transform.Token,
                        CreatedBy = transform.CreatedBy,
                        CreatedDate = transform.CreatedDate,
                        UpdatedBy = transform.UpdatedBy,
                        UpdatedDate = transform.UpdatedDate
                    }).ToList();
        }

    }
}
