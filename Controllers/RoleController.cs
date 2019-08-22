using Core.Manager.RoleManager;
using Core.Manager.UserManager;
using MiniMenu.Models;
using System;
using System.Collections.Generic;
using System.Web.Http;
using WebApiTutorial.FarAuth;

namespace MiniMenu.Controllers
{
    [FarAuthStandar]
    public class RoleController : ApiController
    {
        ApiResponse<RoleDTO> resp = new ApiResponse<RoleDTO>();
        ApiResponse<List<RoleDTO>> respList = new ApiResponse<List<RoleDTO>>();

        [HttpPost]
        public IHttpActionResult Post([FromBody]RoleDTO dto)
        {
            var user = ActionContext.ActionArguments["UserDTO"] as UserDTO;

            if (user == null)
            {
                resp.Message = "user doesn't exist";
                resp.MessageCode = 500;
                resp.ErrorStatus = true;
                resp.ErrorCode = 1;
                return Ok(resp);
            }

            try
            {
                using (var manager = new RoleManager())
                {
                    var response = manager.Creator.Value.Save(dto, user.UserId);
                    var result = manager.Query.Value.TransformSingle(response.RoleId);

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
        public IHttpActionResult Put([FromBody]RoleDTO dto)
        {
            var user = ActionContext.ActionArguments["UserDTO"] as UserDTO;

            if (user == null)
            {
                resp.Message = "user doesn't exist";
                resp.MessageCode = 500;
                resp.ErrorStatus = true;
                resp.ErrorCode = 1;
                return Ok(resp);
            }

            try
            {
                using (var manager = new RoleManager())
                {
                    var response = manager.Updater.Value.Update(dto, user.UserId);
                    var result = manager.Query.Value.TransformSingle(response.RoleId);

                    resp.Message = "Data Updated";
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

        [HttpDelete]
        public IHttpActionResult Delete(long id)
        {

            try
            {
                using (var manager = new RoleManager())
                {
                    resp.Message = manager.Deleter.Value.Delete(id);
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

        public IHttpActionResult Get()
        {
            try
            {
                using (var manager = new RoleManager())
                {
                    respList.Message = "Data Founded";
                    respList.MessageCode = 200;
                    respList.ErrorStatus = false;
                    respList.ErrorCode = 0;
                    respList.Data = manager.Query.Value.Transform();
                }
            }
            catch (Exception ex)
            {
                respList.Message = ex.Message;
                respList.MessageCode = 500;
                respList.ErrorStatus = !false;
                respList.ErrorCode = 1;
            }
            return Ok(respList);
        }
    }
}
