using Core.Manager.PersonManager;
using Core.Manager.UserManager;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web.Http;
using WebApiTutorial.FarAuth;

namespace MiniMenu.Controllers
{
    [FarAuthStandar]
    public class UserController : ApiController
    {
        UserResponse<PersonUserDTO> resp = new UserResponse<PersonUserDTO>();
        UserResponse<List<PersonUserDTO>> respList = new UserResponse<List<PersonUserDTO>>();

        [HttpPost]
        public IHttpActionResult Post([FromBody]PersonUserDTO dto)
        {
            var user = ActionContext.ActionArguments["UserDTO"] as UserDTO;

            if (user == null)
            {
                resp.Message = "user doesn't exist";
                resp.MessageCode = 500;
                resp.ErrorStatus = !false;
                resp.ErrorCode = 1;
                return Ok(resp);
            }

            try
            {
                using (var manager = new UserAdapter())
                {
                    var response = manager.Creator.Value.Save(dto, user.UserId);
                    var result = manager.Query.Value.TransformJoinUser(response.UserId);

                    resp.Message = "Data Inserted";
                    resp.MessageCode = 200;
                    resp.ErrorStatus = false;
                    resp.ErrorCode = 0;
                    resp.Data = result;
                }
            }
            catch (Exception ex)
            {
                resp.Message = ex.Message;
                resp.MessageCode = 500;
                resp.ErrorStatus = true;
                resp.ErrorCode = 1;
            }
            return Ok(resp);
        }

        [HttpPut]
        public IHttpActionResult Put([FromBody]PersonUserDTO dto)
        {
            var user = ActionContext.ActionArguments["UserDTO"] as UserDTO;

            if (user == null)
            {
                resp.Message = "user doesn't exist";
                resp.MessageCode = 500;
                resp.ErrorStatus = !false;
                resp.ErrorCode = 1;
                return Ok(resp);
            }

            try
            {
                using (var manager = new UserAdapter())
                {
                    var response = manager.Updater.Value.Update(dto, user.UserId);
                    var result = manager.Query.Value.TransformPost(response.UserId);

                    resp.Message = "Data Updated";
                    resp.MessageCode = 200;
                    resp.ErrorStatus = false;
                    resp.ErrorCode = 0;
                    resp.Data = manager.Query.Value.TransformJoinUser(response.UserId);
                }
            }
            catch (Exception ex)
             {
                resp.Message = ex.Message;
                resp.MessageCode = 500;
                resp.ErrorStatus = true;
                resp.ErrorCode = 1;
            }
            return Ok(resp);
        }

        [HttpPut]
        [Route("api/user/changepass/{id}")]
        public IHttpActionResult UpdatePassword([FromBody] NewPassword dto, long id)
        {
            try
            {
                using (var manager = new UserAdapter())
                {
                    var response = manager.Updater.Value.UpdatePass(dto.OldPass, dto.NewPass, id);
                    var result = manager.Query.Value.TransformPost(response.UserId);

                    resp.Message = "Data Updated";
                    resp.MessageCode = 200;
                    resp.ErrorStatus = false;
                    resp.ErrorCode = 0;
                    resp.Data = manager.Query.Value.TransformJoinUser(response.UserId);
                }
            }
            catch (Exception ex)
            {
                resp.Message = ex.Message;
                resp.MessageCode = 500;
                resp.ErrorStatus = true;
                resp.ErrorCode = 1;
            }
            return Ok(resp);
        }

        [HttpDelete]
        [Route("api/user/{id}")]
        public IHttpActionResult Delete(long id)
        {
            var user = ActionContext.ActionArguments["UserDTO"] as UserDTO;

            if (user == null)
            {
                resp.Message = "user doesn't exist";
                resp.MessageCode = 500;
                resp.ErrorStatus = !false;
                resp.ErrorCode = 1;
                return Ok(resp);
            }

            if (user.UserId == id)
            {
                resp.Message = "you cannot delete yourself";
                resp.MessageCode = 500;
                resp.ErrorStatus = !false;
                resp.ErrorCode = 1;
                return Ok(resp);
            }

            try
            {
                using (var manager = new UserAdapter())
                {
                    using (var transac = new TransactionScope())
                    {
                        var userPerson = manager.Query.Value.All().FirstOrDefault(x => x.UserId == id);
                        var personId = manager.PersonManager.Value.Query.Value.All().FirstOrDefault(x => x.PersonId == userPerson.PersonId).PersonId;
                        var response = manager.Deleter.Value.Delete(id);
                        var responsePerson = manager.PersonManager.Value.Deleter.Value.Delete(personId);

                        if (response.ToLower() == "deleted")
                        {
                            resp.Message = "Data Deleted";
                            resp.MessageCode = 202;
                            resp.ErrorStatus = false;
                            resp.ErrorCode = 0;
                        }
                        transac.Complete();
                    }
                }
            }
            catch (Exception ex)
            {
                resp.Message = ex.Message;
                resp.MessageCode = 500;
                resp.ErrorStatus = true;
                resp.ErrorCode = 1;
            }
            return Ok(resp);
        }

        public IHttpActionResult Get()
        {
            try
            {
                using (var manager = new UserAdapter())
                {
                    respList.Message = "Data Founded";
                    respList.MessageCode = 200;
                    respList.ErrorStatus = false;
                    respList.ErrorCode = 0;
                    respList.Data = manager.Query.Value.TransformJoinAll();
                }
            }
            catch (Exception ex)
            {
                respList.Message = ex.Message;
                respList.MessageCode = 500;
                respList.ErrorStatus = true;
                respList.ErrorCode = 1;
            }
            return Ok(respList);
        }

        [HttpGet]
        [Route("api/user/{id}")]
        public IHttpActionResult Get(long id)
        {
            using (var manager = new UserAdapter())
            {
                resp.Data = manager.Query.Value.TransformJoinUser(id);
                if (resp.Data != null)
                {
                    resp.Message = "Data With id " + id + " Founded";
                    resp.MessageCode = 200;
                    resp.ErrorStatus = false;
                    resp.ErrorCode = 0;

                }
                else
                {
                    var err = new Exception("Data with id " + id + " not found");
                    resp.Message = err.ToString();
                    resp.MessageCode = 500;
                    resp.ErrorStatus = true;
                    resp.ErrorCode = 1;
                }
            }
            return Ok(resp);
        }

        [HttpPost]
        [Route("api/user/logout/{id}")]
        public IHttpActionResult LogoutUser(long id)
        {
            try
            {
                using (var manager = new UserAdapter())
                {
                    manager.Logout.Value.Logout(id);
                    resp.Message = "user logged out";
                    resp.MessageCode = 200;
                    resp.ErrorStatus = false;
                    resp.ErrorCode = 0;
                }
            }
            catch (Exception ex)
            {
                resp.Message = ex.Message;
                resp.MessageCode = 500;
                resp.ErrorStatus = true;
                resp.ErrorCode = 1;
            }
            return Ok(resp);
        }
    }
}
