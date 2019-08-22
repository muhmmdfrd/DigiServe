using Core.Manager.MenuManager;
using System.Web.Http;
using MiniMenu.Models;
using System.Collections.Generic;
using System;
using Core.Manager.UserManager;
using WebApiTutorial.FarAuth;

namespace MiniMenu.Controllers
{
    [FarAuthStandar]
    public class MenuController : ApiController
    {
        ApiResponse<List<MenuDTO>> respList = new ApiResponse<List<MenuDTO>>();
        ApiResponse<MenuDTO> respSingle = new ApiResponse<MenuDTO>();

        [HttpPost]
        public IHttpActionResult Post([FromBody]MenuDTO dto)
        {
            var user = ActionContext.ActionArguments["UserDTO"] as UserDTO;

            if (user == null)
            {
                respSingle.Message = "user doesn't exist";
                respSingle.MessageCode = 500;
                respSingle.ErrorStatus = true;
                respSingle.ErrorCode = 1;
                return Ok(respSingle);
            }

            try
            {
                using (var manager = new MenuAdapter())
                {
                    var response = manager.Creator.Value.Save(dto, user.UserId);
                    var result = manager.Query.Value.TransformPost(response.MenuId);

                    respSingle.Message = "Data Inserted";
                    respSingle.MessageCode = 200;
                    respSingle.ErrorStatus = false;
                    respSingle.ErrorCode = 0;
                    respSingle.Data = result;
                }
            }
            catch (Exception ex)
            {
                respSingle.Message = ex.Message;
                respSingle.MessageCode = 500;
                respSingle.ErrorStatus = true;
                respSingle.ErrorCode = 1;
            }
            return Ok(respSingle);
        }

        [HttpPut]
        public IHttpActionResult Put([FromBody]MenuDTO dto)
        {
            var user = ActionContext.ActionArguments["UserDTO"] as UserDTO;

            if (user == null)
            {
                respSingle.Message = "user doesn't exist";
                respSingle.MessageCode = 500;
                respSingle.ErrorStatus = true;
                respSingle.ErrorCode = 1;
                return Ok(respSingle);
            }

            try
            {
                using (var manager = new MenuAdapter())
                {
                    var response = manager.Updater.Value.Update(dto, user.UserId);
                    var result = manager.Query.Value.TransformPost(response.MenuId);

                    respSingle.Message = "Data Updated";
                    respSingle.MessageCode = 200;
                    respSingle.ErrorStatus = false;
                    respSingle.ErrorCode = 0;
                    respSingle.Data = result;
                }
            }
            catch (Exception ex)
            {
                respSingle.Message = ex.Message;
                respSingle.MessageCode = 500;
                respSingle.ErrorStatus = true;
                respSingle.ErrorCode = 1;
            }
            return Ok(respSingle);
        }

        [HttpDelete]
        [Route("api/menu/{id}")]
        public IHttpActionResult Delete(long id)
        {
            try
            {
                using (var manager = new MenuAdapter())
                {
                    respSingle.Message = manager.Deleter.Value.Delete(id);
                    respSingle.MessageCode = 200;
                    respSingle.ErrorStatus = false;
                    respSingle.ErrorCode = 0;
                }
            }
            catch (Exception ex)
            {
                respSingle.Message = ex.Message;
                respSingle.MessageCode = 500;
                respSingle.ErrorStatus = true;
                respSingle.ErrorCode = 1;
            }
            return Ok(respSingle);
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            try
            {
                using (var manager = new MenuAdapter())
                {
                    respList.Message = "Data Founded";
                    respList.MessageCode = 200;
                    respList.ErrorStatus = false;
                    respList.ErrorCode = 0;
                    respList.Data = manager.Query.Value.Transform();
                   
                }
            }
            catch (Exception ec)
            {
                respList.Message = ec.Message;
                respList.MessageCode = 500;
                respList.ErrorStatus = true;
                respList.ErrorCode = 1;
            }
            return Ok(respList);
        }

        [HttpGet]
        [Route("api/menu/{id}")]
        public IHttpActionResult GetId(long id)
        {
            try
            {
                using (var manager = new MenuAdapter())
                {
                    respSingle.Message = "Data Founded";
                    respSingle.MessageCode = 200;
                    respSingle.ErrorStatus = false;
                    respSingle.ErrorCode = 0;
                    respSingle.Data = manager.Query.Value.TransformPost(id);

                }
            }
            catch (Exception ec)
            {
                respSingle.Message = ec.Message;
                respSingle.MessageCode = 500;
                respSingle.ErrorStatus = true;
                respSingle.ErrorCode = 1;
            }
            return Ok(respSingle);
        }

    }
}
