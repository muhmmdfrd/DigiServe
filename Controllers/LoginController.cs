using System;
using System.Web.Http;
using Core.Manager.LoginManager;
using Core.Manager.UserManager;
using MiniMenu.Models;

namespace MiniMenu.Controllers
{
    public class LoginController : ApiController
    {
        [HttpPost]
        public IHttpActionResult Post(AuthRequest prop)
        {
            ApiResponse<UserDTO> resp = new ApiResponse<UserDTO>();
            try
            {
                using (var manager = new UserAdapter())
                {
                    if (string.IsNullOrEmpty(prop.Username) || string.IsNullOrEmpty(prop.Password))
                    {
                        resp.Message = "Please fill all form field";
                        resp.MessageCode = 406;
                        resp.ErrorStatus = true;
                        resp.ErrorCode = 1;
                        resp.Data = null;
                    }
                    else
                    {
                        var check = manager.Query.Value.LetIt(prop.Username);

                        if (check != null)
                        {
                            var hashedPass = manager.Encrypt.Value.Encrypt(prop.Password);

                            if (hashedPass == check.Password)
                            {
                                if (check.RoleId == 2)
                                {
                                    throw new Exception("you don't have permission to login");
                                }

                                var token = Guid.NewGuid().ToString();

                                check.Token = manager.Updater.Value.UpdateToken(token, check.UserId);
                                resp.Message = "Login Success";
                                resp.MessageCode = 200;
                                resp.ErrorStatus = false;
                                resp.ErrorCode = 0;
                                resp.Data = check;
                            }
                            else
                            {
                                resp.Message = "username or password is incorrect";
                                resp.MessageCode = 406;
                                resp.ErrorStatus = true;
                                resp.ErrorCode = 1;
                                resp.Data = null;
                            }
                        }
                        else
                        {
                            resp.Message = "username or password is incorect";
                            resp.MessageCode = 406;
                            resp.ErrorStatus = true;
                            resp.ErrorCode = 1;
                            resp.Data = null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resp.Message = ex.Message;
                resp.MessageCode = 500;
                resp.ErrorStatus = true;
                resp.ErrorCode = 1;
                resp.Data = null;
            }
            return Ok(resp);
        }

        [HttpPost]
        [Route("api/login/mobile")]
        public IHttpActionResult IMEI(AuthRequest prop)
        {
            ApiResponse<UserDTO> resp = new ApiResponse<UserDTO>();

            try
            {
                using (var manager = new UserAdapter())
                {
                    if (string.IsNullOrEmpty(prop.IMEI))
                    {
                        resp.Message = "Please register your IMEI";
                        resp.MessageCode = 406;
                        resp.ErrorStatus = true;
                        resp.ErrorCode = 1;
                        resp.Data = null;
                    }
                    else
                    {
                        var check = manager.Query.Value.LoginImei(prop.IMEI);

                        if (check != null)
                        {
                            if (prop.IMEI == check.IMEI)
                            {
                                var token = Guid.NewGuid().ToString();

                                check.Token = manager.Updater.Value.UpdateToken(token, check.UserId);
                                resp.Message = "Login Success";
                                resp.MessageCode = 200;
                                resp.ErrorStatus = false;
                                resp.ErrorCode = 0;
                                resp.Data = check;
                            }
                            else
                            {
                                resp.Message = "IMEI Not Registered Yet";
                                resp.MessageCode = 406;
                                resp.ErrorStatus = true;
                                resp.ErrorCode = 1;
                                resp.Data = null;
                            }
                        }
                        else
                        {
                            resp.Message = "IMEI Not Registered Yet.";
                            resp.MessageCode = 406;
                            resp.ErrorStatus = true;
                            resp.ErrorCode = 1;
                            resp.Data = null;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resp.Message = ex.Message;
                resp.MessageCode = 500;
                resp.ErrorStatus = true;
                resp.ErrorCode = 1;
                resp.Data = null;
            }
            return Ok(resp);
        }
    }
}
