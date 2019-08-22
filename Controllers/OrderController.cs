using Core.Manager.OrderManager;
using Core.Manager.OrderPaymentManager;
using Core.Manager.UserManager;
using MiniMenu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using WebApiTutorial.FarAuth;

namespace MiniMenu.Controllers
{
    [FarAuthStandar]
    public class OrderController : ApiController
    {
        ApiResponse<List<OrderDTO>> respList = new ApiResponse<List<OrderDTO>>();
        ApiResponse<OrderDTO> respSingle = new ApiResponse<OrderDTO>();
        ApiResponseCheckOrder<OrderDTO> resp = new ApiResponseCheckOrder<OrderDTO>();
        ApiResponse<List<OnHoldChangeDTO>> respChange = new ApiResponse<List<OnHoldChangeDTO>>();

        [HttpPost]
        public IHttpActionResult Post([FromBody]OrderDTO dto)
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
                using (var manager = new OrderAdapter())
                {
                    var response = manager.Creator.Value.Save(dto, user.UserId);
                    var result = manager.Query.Value.TranformSingle(response.OrderId);

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
                respSingle.ErrorStatus = !false;
                respSingle.ErrorCode = 1;
            }
             return Ok(respSingle);
        }

        [HttpPut]
        public IHttpActionResult Put([FromBody]OrderDTO dto)
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
                using (var manager = new OrderAdapter())
                {
                    var response = manager.Updater.Value.Update(dto, user.UserId);
                    var result = manager.Query.Value.TranformSingle(response.OrderId);

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
                respSingle.ErrorStatus = !false;
                respSingle.ErrorCode = 1;
            }
            return Ok(respSingle);
        }

        [HttpDelete]
        public IHttpActionResult Delete(long id)
        {
            var user = ActionContext.ActionArguments["UserDTO"] as UserDTO;

            if (user == null)
            {
                respSingle.Message = "user doesn't exist";
                respSingle.MessageCode = 500;
                respSingle.ErrorStatus = !false;
                respSingle.ErrorCode = 1;

                return Ok(respSingle);
            }

            try
            {
                using (var manager = new OrderAdapter())
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
                respSingle.ErrorStatus = !false;
                respSingle.ErrorCode = 1;
            }
            return Ok(respSingle);
        }

        public IHttpActionResult Get()
        {
           try
           {
                using (var manager = new OrderAdapter())
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

        [HttpPost]
        [Route("api/order/open")]
        public IHttpActionResult OpenOrder()
        {
            try
            {
                using (var manager = new OrderAdapter())
                {
                    var dateObject = manager.Query.Value.TransformDate();

                    if (dateObject.CreateDate.Value.Day != DateTime.Now.Day)
                    {
                        var responseClose = manager.Updater.Value.CloseOrder();
                        if (responseClose == null)
                        {
                            manager.Creator.Value.CreateOrder();
                            respSingle.Message = "Order Created";
                            respSingle.MessageCode = 200;
                            respSingle.ErrorStatus = false;
                            respSingle.ErrorCode = 0;
                        }
                        else
                        {
                            throw new Exception("please close the order first");
                        }
                    }
                    else if (dateObject == null)
                    {
                        manager.Creator.Value.CreateOrder();
                        respSingle.Message = "Order Created";
                        respSingle.MessageCode = 200;
                        respSingle.ErrorStatus = false;
                        respSingle.ErrorCode = 0;
                    }
                    else
                    {
                        if (dateObject.CreateDate.Value.Day == DateTime.Now.Day)
                        {
                            respSingle.Message = "You have already opened an order today";
                            respSingle.MessageCode = 201;
                            respSingle.ErrorStatus = true;
                            respSingle.ErrorCode = 1;
                        }
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

        [HttpPost]
        [Route("api/order/start")]
        public IHttpActionResult StartOrder()
        {
            try
            {
                using (var manager = new OrderAdapter())
                {
                    manager.Updater.Value.StartOrder();
                    respSingle.Message = "order started";
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

        [HttpGet]
        [Route("api/order/byid/{id}")]
        public IHttpActionResult GetOrderById(long id)
        {
            try
            {
                using (var manager = new OrderAdapter())
                {
                    respSingle.Message = "Data Founded";
                    respSingle.MessageCode = 200;
                    respSingle.ErrorStatus = false;
                    respSingle.ErrorCode = 0;
                    respSingle.Data = manager.Query.Value.TransformGetDTO(id, true);
                }
                if (respSingle.Data == null)
                {
                    throw new Exception("Data with id " + id + " not found");
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
        [Route("api/order/check")]
        public IHttpActionResult CheckOrder()
        {
            try
            {
                using (var manager = new OrderAdapter())
                { 
                    var existOrder = manager.Query.Value.TransformToList().OrderByDescending(x => x.CreateDate).FirstOrDefault();

                    if (existOrder == null)
                    {
                        resp.Message = "order service available";
                        resp.MessageCode = 200;
                        resp.ErrorStatus = false;
                        resp.ErrorCode = 0;
                        return Ok(resp);
                    }

                    var dateCreate = existOrder.CreateDate.Value.Day;
                    var idToday = existOrder.OrderId;
                    var status = existOrder.Status;

                    if (dateCreate != DateTime.Now.Day)
                    {
                        resp.Message = "order service available";
                        resp.MessageCode = 200;
                        resp.ErrorStatus = false;
                        resp.ErrorCode = 0;
                    }
                    else if (status == Core.Enum.OrderState.Close)
                    {
                        resp.Message = "order have been close";
                        resp.MessageCode = 201;
                        resp.ErrorStatus = false;
                        resp.ErrorCode = 0;
                    }
                    else
                    {
                        resp.Message = "You have already opened an order today";
                        resp.MessageCode = 202;
                        resp.ErrorStatus = false;
                        resp.ErrorCode = 0;
                        resp.Id = idToday;
                    }
                }
            }
            catch (Exception ex)
            {
                resp.Message = ex.Message;
                resp.MessageCode = 500;
                resp.ErrorStatus = !false;
                resp.ErrorCode = 1;
            }
            return Ok(resp);
        }

        [HttpPost]
        [Route("api/order/close")]
        public IHttpActionResult CloseOrder()
        {
            try
            {
                using (var manager = new OrderAdapter())
                {
                    var response = manager.Updater.Value.CloseOrder();

                    if (response == null)
                    {
                        respChange.Message = "order has been closed";
                        respChange.MessageCode = 200;
                        respChange.ErrorStatus = false;
                        respChange.ErrorCode = 0;
                    }
                    else
                    {
                        respChange.Message = "order cannot be closed caused people on hold change state";
                        respChange.MessageCode = 300;
                        respChange.ErrorStatus = !false;
                        respChange.ErrorCode = 1;
                        respChange.Data = response;
                    }
                }
            }
            catch (Exception ex)
            {
                respChange.Message = ex.Message;
                respChange.MessageCode = 500;
                respChange.ErrorStatus = !false;
                respChange.ErrorCode = 1;
            }
            return Ok(respChange);
        }

    }
}
