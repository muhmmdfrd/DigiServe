using Core.Manager.OrderPaymentManager;
using Core.Manager.UserManager;
using MiniMenu.Models;
using System;
using System.Collections.Generic;
using System.Transactions;
using System.Web.Http;
using WebApiTutorial.FarAuth;

namespace MiniMenu.Controllers
{
    [FarAuthStandar]
    public class OrderPaymentController : ApiController
    {
        ApiResponse<OrderPaymentDTO> resp = new ApiResponse<OrderPaymentDTO>();
        ApiResponse<List<OrderPaymentDTO>> respList = new ApiResponse<List<OrderPaymentDTO>>();

        [HttpPost]
        public IHttpActionResult Post([FromBody]OrderPaymentDTO dto)
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
                using (var manager = new OrderPaymentAdapter())
                {
                    using (var transac = new TransactionScope())
                    {
                        var response = manager.Creator.Value.Save(dto, user.UserId);
                        transac.Complete();

                        var result = manager.Query.Value.TranformSingle(response.OrderPaymentId);
                        resp.Message = "Data Inserted";
                        resp.MessageCode = 200;
                        resp.ErrorStatus = false;
                        resp.ErrorCode = 0;
                        resp.Data = result;
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

        [HttpPut]
        public IHttpActionResult Put([FromBody]OrderPaymentDTO dto)
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
                using (var manager = new OrderPaymentAdapter())
                {
                    using (var transac = new TransactionScope())
                    {
                        var response = manager.Updater.Value.Update(dto, user.UserId);

                        transac.Complete();

                        resp.Message = "Data Updated";
                        resp.MessageCode = 200;
                        resp.ErrorStatus = false;
                        resp.ErrorCode = 0;
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

        [HttpPut]
        [Route("api/orderpayment/submit")]
        public IHttpActionResult SubmitPayment(OrderPaymentDTO dto)
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
                using (var manager = new OrderPaymentAdapter())
                {
                    var response = manager.Updater.Value.Submit(dto, user.UserId);

                    resp.Message = "Data Updated";
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

        [HttpDelete]
        public IHttpActionResult Delete(long id)
        {
            try
            {
                using (var manager = new OrderPaymentAdapter())
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

        [HttpGet]
        public IHttpActionResult Get()
        {
            try
            {
                using (var manager = new OrderPaymentAdapter())
                {
                    respList.Message = "Data Founded";
                    respList.MessageCode = 200;
                    respList.ErrorStatus = false;
                    respList.ErrorCode = 0;
                    respList.Data = manager.Query.Value.TransformToList();
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

        [HttpGet]
        [Route("api/orderpayment/{userId}/{orderId}")]
        public IHttpActionResult GetUserId(long userId, long orderId)
        {
            try
            {
                using (var manager = new OrderPaymentAdapter())
                {
                    respList.Message = "Data Founded";
                    respList.MessageCode = 200;
                    respList.ErrorStatus = false;
                    respList.ErrorCode = 0;
                    respList.Data = manager.Query.Value.TransformUserList(userId, orderId);
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

        [HttpGet]
        [Route("api/orderpayment/{orderId}")]
        public IHttpActionResult GetOrderId(long orderId)
        {
            try
            {
                using (var manager = new OrderPaymentAdapter())
                {
                    respList.Message = "Data Founded";
                    respList.MessageCode = 200;
                    respList.ErrorStatus = false;
                    respList.ErrorCode = 0;
                    respList.Data = manager.Query.Value.TransformOrderList(orderId);
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
