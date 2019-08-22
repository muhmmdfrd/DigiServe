using Core.Manager;
using Core.Manager.OrderDetailManager;
using Core.Manager.OrderPaymentManager;
using Core.Manager.UserManager;
using MiniMenu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web.Http;
using WebApiTutorial.FarAuth;

namespace MiniMenu.Controllers
{
    [FarAuthStandar]
    public class OrderDetailController : ApiController
    {
        ApiResponse<List<OrderDetailDTO>> respList = new ApiResponse<List<OrderDetailDTO>>();
        ApiResponse<OrderDetailDTO> respSingle = new ApiResponse<OrderDetailDTO>();
        ApiResponse<List<OrderDetailIdDTO>> respId = new ApiResponse<List<OrderDetailIdDTO>>();
        ApiResponse<List<OrderDetailGroupDTO>> respGroup = new ApiResponse<List<OrderDetailGroupDTO>>();
        ApiResponse<List<OrderDetailMenuGroupDTO>> respMenu = new ApiResponse<List<OrderDetailMenuGroupDTO>>();

        [HttpPost]
        public IHttpActionResult Post([FromBody]OrderDetailDTO dto)
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
                using (var manager = new OrderDetailAdapter())
                {
                    using (var transac = new TransactionScope())
                    {
                        var response = manager.Creator.Value.Save(dto, user.UserId);
                        var listPay = manager.OrderPaymentManager.Value.Query.Value.All();

                        if (response != null)
                        {
                            var newOrderPayment = new OrderPaymentDTO
                            {
                                OrderPaymentDate = DateTime.Now,
                                PaymentMethod = 1,
                                OrderId = response.OrderId,
                                UserId = response.UserId
                            };

                            var savePayment = manager.OrderPaymentManager.Value.Creator.Value.Save(newOrderPayment, user.UserId);

                            transac.Complete();
                        }

                        respSingle.Message = "Data Inserted";
                        respSingle.MessageCode = 200;
                        respSingle.ErrorStatus = false;
                        respSingle.ErrorCode = 0;
                    }
                }
            }
            catch (Exception ex)
            {
                respSingle.Message = ex.Message;
                respSingle.MessageCode = 500;
                respSingle.ErrorStatus = !false;
                respSingle.ErrorCode = 1;
            }
            return Ok(respSingle);
        }

        [HttpPut]
        public IHttpActionResult Put([FromBody]OrderDetailDTO dto)
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
                using (var manager = new OrderDetailAdapter())
                {
                    using (var transac = new TransactionScope())
                    {
                        var response = manager.Updater.Value.Update(dto, user.UserId);
                        var orderPayment = new OrderPaymentDTO { OrderId = dto.OrderId, UserId = dto.UserId };

                        manager.OrderPaymentManager.Value.Updater.Value.Update(orderPayment, user.UserId);

                        transac.Complete();
                    }
                    respSingle.Message = "Data Updated";
                    respSingle.MessageCode = 200;
                    respSingle.ErrorStatus = false;
                    respSingle.ErrorCode = 0;
                }
            }
            catch (Exception ex)
            {
                respSingle.Message = ex.Message;
                respSingle.MessageCode = 500;
                respSingle.ErrorStatus = !false;
                respSingle.ErrorCode = 1;
            }
            return Ok(respSingle);
        }

        [HttpDelete]
        public IHttpActionResult Delete(long id)
        {
            try
            {
                using (var manager = new OrderDetailAdapter())
                {
                    var response = manager.Deleter.Value.Delete(id);
                    respSingle.Message = "deleted";
                    respSingle.MessageCode = 200;
                    respSingle.ErrorStatus = false;
                    respSingle.ErrorCode = 0;

                    var myListTransac = manager.Query.Value.All()
                        .Where(scope => scope.OrderId == response.OrderId && scope.UserId == response.UserId);

                    var myListCount = myListTransac.Select(x => x.UserId).Count();

                    if (myListCount == 0)
                    {
                        var paymentDelete = manager.OrderPaymentManager.Value.Query.Value.All()
                        .FirstOrDefault(x => x.OrderId == response.OrderId && x.UserId == response.UserId).OrderPaymentId;

                        manager.OrderPaymentManager.Value.Deleter.Value.Delete(paymentDelete);
                    }
                }
            }
            catch (Exception ex)
            {
                respSingle.Message = ex.Message;
                respSingle.MessageCode = 500;
                respSingle.ErrorStatus = !false;
                respSingle.ErrorCode = 1;
            }
            return Ok(respSingle);
        }

        [HttpGet]
        public IHttpActionResult Get()
        {
            try
            {
                using (var manager = new OrderDetailAdapter())
                {
                    respList.Message = "Data Founded";
                    respList.MessageCode = 200;
                    respList.ErrorStatus = false;
                    respList.ErrorCode = 0;
                    respList.Data = manager.Query.Value.Join();
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
        [Route("api/orderdetail/byid/{id}")]
        public IHttpActionResult GetOrderDetailById(long id)
        {
            try
            {
                using (var manager = new OrderDetailAdapter())
                {
                    respId.Message = "Data Founded";
                    respId.MessageCode = 200;
                    respId.ErrorStatus = false;
                    respId.ErrorCode = 0;
                    respId.Data = manager.Query.Value.OrderJoinId(id);
                }
                if (respId.Data == null)
                {
                    throw new Exception("Data Order Detail for today not found");
                }
            }
            catch (Exception ex)
            {
                respId.Message = ex.Message;
                respId.MessageCode = 500;
                respId.ErrorStatus = !false;
                respId.ErrorCode = 1;
            }
            return Ok(respId);
        }

        [HttpGet]
        [Route("api/orderdetail/today")]
        public IHttpActionResult GetOrderDetailByDate()
        {
            try
            {
                using (var manager = new OrderDetailAdapter())
                {
                    respId.Message = "Data Founded";
                    respId.MessageCode = 200;
                    respId.ErrorStatus = false;
                    respId.ErrorCode = 0;
                    respId.Data = manager.Query.Value.OrderToday();
                }
                if (respId.Data == null)
                {
                    throw new Exception("Data Order Detail for today not found");
                }
            }
            catch (Exception ex)
            {
                respId.Message = ex.Message;
                respId.MessageCode = 500;
                respId.ErrorStatus = !false;
                respId.ErrorCode = 1;
            }
            return Ok(respId);
        }

        [HttpGet]
        [Route("api/orderdetail/byid-user/{id}")]
        public IHttpActionResult GetOrderByUser(long id)
        {
            try
            {
                using (var manager = new OrderDetailAdapter())
                {
                    respId.Message = "Data Founded";
                    respId.MessageCode = 200;
                    respId.ErrorStatus = false;
                    respId.ErrorCode = 0;
                    respId.Data = manager.Query.Value.OrderByUser(id);
                }
                if (respId.Data == null)
                {
                    throw new Exception("Data Order Detail for today not found");
                }
            }
            catch (Exception ex)
            {
                respId.Message = ex.Message;
                respId.MessageCode = 500;
                respId.ErrorStatus = !false;
                respId.ErrorCode = 1;
            }
            return Ok(respId);
        }

        [HttpGet]
        [Route("api/orderdetail/history/{userId}")]
        public IHttpActionResult GetOrderByHistory(long id)
        {
            try
            {
                using (var manager = new OrderDetailAdapter())
                {
                    respId.Message = "Data Founded";
                    respId.MessageCode = 200;
                    respId.ErrorStatus = false;
                    respId.ErrorCode = 0;
                    respId.Data = manager.Query.Value.OrderByHistory(id);
                }
                if (respId.Data == null)
                {
                    throw new Exception("You've never Booked");
                }
            }
            catch (Exception ex)
            {
                respId.Message = ex.Message;
                respId.MessageCode = 500;
                respId.ErrorStatus = !false;
                respId.ErrorCode = 1;
            }
            return Ok(respId);
        }

        [HttpGet]
        [Route("api/orderdetail/{userId}/{orderId}")]
        public IHttpActionResult GetOrderByHistoryDay(long userId, long orderId)
        {
            try
            {
                using (var manager = new OrderDetailAdapter())
                {
                    respId.Message = "Data Founded";
                    respId.MessageCode = 200;
                    respId.ErrorStatus = false;
                    respId.ErrorCode = 0;
                    respId.Data = manager.Query.Value.OrderByHistoryInDay(userId, orderId);
                }
                if (respId.Data == null)
                {
                    throw new Exception("You've never Booked");
                }
            }
            catch (Exception ex)
            {
                respId.Message = ex.Message;
                respId.MessageCode = 500;
                respId.ErrorStatus = !false;
                respId.ErrorCode = 1;
            }
            return Ok(respId);
        }

        [HttpGet]
        [Route("api/orderdetail/group/{orderId}")]
        public IHttpActionResult GetOrderGroup(long orderId)
        {
            try
            {
                using (var manager = new OrderDetailAdapter())
                {
                    respGroup.Message = "data founded";
                    respGroup.MessageCode = 200;
                    respGroup.ErrorStatus = false;
                    respGroup.ErrorCode = 0;
                    respGroup.Data = manager.Query.Value.TransformToGroupAll(orderId);

                    if (respGroup.Data.Count == 0)
                    {
                        respGroup.Message = "data is empty";
                        respGroup.MessageCode = 200;
                        respGroup.ErrorStatus = false;
                        respGroup.ErrorCode = 0;
                        respGroup.Data = null;
                        return Ok(respGroup);
                    }
                }
            }
            catch (Exception ex)
            {
                respGroup.Message = ex.Message;
                respGroup.MessageCode = 500;
                respGroup.ErrorStatus = !false;
                respGroup.ErrorCode = 1;
            }
            return Ok(respGroup);
        }

        [HttpGet]
        [Route("api/orderdetail/group-user/{userId}/{orderId}")]
        public IHttpActionResult GetOrderOthersGroup(long userId, long orderId)
        {
            try
            {
                using (var manager = new OrderDetailAdapter())
                {
                    respGroup.Message = "data founded";
                    respGroup.MessageCode = 200;
                    respGroup.ErrorStatus = false;
                    respGroup.ErrorCode = 0;
                    respGroup.Data = manager.Query.Value.TransformToGroupUser(userId, orderId);

                    if (respGroup.Data.Count == 0)
                    {
                        respGroup.Message = "data is empty";
                        respGroup.MessageCode = 200;
                        respGroup.ErrorStatus = false;
                        respGroup.ErrorCode = 0;
                        respGroup.Data = null;
                        return Ok(respGroup);
                    }
                }
            }
            catch (Exception ex)
            {
                respGroup.Message = ex.Message;
                respGroup.MessageCode = 500;
                respGroup.ErrorStatus = !false;
                respGroup.ErrorCode = 1;
            }
            return Ok(respGroup);
        }

        [HttpGet]
        [Route("api/orderdetail/group-others/{userId}/{orderId}")]
        public IHttpActionResult GetOrderGroupOthers(long userId, long orderId)
        {
            try
            {
                using (var manager = new OrderDetailAdapter())
                {
                    respGroup.Message = "data founded";
                    respGroup.MessageCode = 200;
                    respGroup.ErrorStatus = false;
                    respGroup.ErrorCode = 0;
                    respGroup.Data = manager.Query.Value.TransformToGroupOthers(userId, orderId);

                    if (respGroup.Data.Count == 0)
                    {
                        respGroup.Message = "data is empty";
                        respGroup.MessageCode = 200;
                        respGroup.ErrorStatus = false;
                        respGroup.ErrorCode = 0;
                        respGroup.Data = null;
                        return Ok(respGroup);
                    }
                }
            }
            catch (Exception ex)
            {
                respGroup.Message = ex.Message;
                respGroup.MessageCode = 500;
                respGroup.ErrorStatus = !false;
                respGroup.ErrorCode = 1;
            }
            return Ok(respGroup);
        }

        [HttpGet]
        [Route("api/orderdetail/group-menu/{orderId}")]
        public IHttpActionResult GetOrderGroupMenu(long orderId)
        {
            try
            {
                using (var manager = new OrderDetailAdapter())
                {
                    respMenu.Message = "data founded";
                    respMenu.MessageCode = 200;
                    respMenu.ErrorStatus = false;
                    respMenu.ErrorCode = 0;
                    respMenu.Data = manager.Query.Value.OrderGroupMenuByUser(orderId);

                    if (respMenu.Data.Count == 0)
                    {
                        respMenu.Message = "data is empty";
                        respMenu.MessageCode = 200;
                        respMenu.ErrorStatus = false;
                        respMenu.ErrorCode = 0;
                        respMenu.Data = null;
                        return Ok(respMenu);
                    }
                }
            }
            catch (Exception ex)
            {
                respMenu.Message = ex.Message;
                respMenu.MessageCode = 500;
                respMenu.ErrorStatus = !false;
                respMenu.ErrorCode = 1;
            }
            return Ok(respMenu);
        }
    }
}
