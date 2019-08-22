using Core.Manager.MenuHistoryManager;
using Core.Manager.MenuManager;
using Core.Manager.UserManager;
using MiniMenu.Models;
using System;
using System.Collections.Generic;
using System.Web.Http;
using WebApiTutorial.FarAuth;

namespace MiniMenu.Controllers
{
    [FarAuthStandar]
    public class MenuHistoryController : ApiController
    {
        ApiResponse<List<MenuHistoryDTO>> respList = new ApiResponse<List<MenuHistoryDTO>>();
        ApiResponse<MenuHistoryDTO> respSingle = new ApiResponse<MenuHistoryDTO>();

        [HttpGet]
        public IHttpActionResult Get()
        {
            try
            {
                using (var manager = new MenuHistoryAdapter())
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

        [HttpPost]
        public IHttpActionResult Post([FromBody]MenuHistoryDTO dto)
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
                using (var manager = new MenuHistoryAdapter())
                {
                    var response = manager.Creator.Value.Save(dto, user.UserId);
                    var result = manager.Query.Value.TransformSingle(response.MenuHistoryId);

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

        [HttpDelete]
        public IHttpActionResult Delete(long id)
        {
            try
            {
                using (var manager = new MenuHistoryAdapter())
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
    }
}
