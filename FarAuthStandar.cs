using Core.Manager.UserManager;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Http.Controllers;
using System.Web.Http;

namespace WebApiTutorial.FarAuth
{
    public class FarAuthStandar : AuthorizeAttribute
    {
        private const string BearerAuthResponseHeader = "Authorization";
        private const string BearerAuthResponseHeaderValue = "Bearer";
        private bool IsAuthenticated = false;

        public bool IsUseSession { get; set; }
        public bool IsReturnLoginDTO { get; set; }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            try
            {
                var authValue = actionContext.Request.Headers.Authorization;
                if (authValue != null
                    && !string.IsNullOrWhiteSpace(authValue.Parameter)
                    && authValue.Scheme == BearerAuthResponseHeaderValue)
                {
                    var credentials = ParseAuthorizationHeader(authValue);
                    if (credentials == null)
                    {
                        actionContext.Response = GetUnauthorizedResponse("Blank token", 103);
                        return;
                    }

                    using (var manager = new UserAdapter())
                    {
                        var userDTO = manager.Query.Value.GetUserByToken(credentials);

                        if (userDTO == null)
                        {
                            actionContext.Response = GetUnauthorizedResponse("User Not found", 103);
                            return;
                        }
                        else
                        {
                            IsAuthenticated = true;

                            actionContext.ActionArguments["UserDTO"] = userDTO;
                        }

                    }

                }
                else
                {
                    actionContext.Response = GetUnauthorizedResponse("Not secure request or mismatch authentication type", 101);
                    return;
                }
            }
            catch (Exception exp)
            {
                actionContext.Response = GetUnauthorizedResponse(exp.Message, 100);
                return;
            }
        }

        protected virtual string ParseAuthorizationHeader(AuthenticationHeaderValue authorization)
        {
            string authorizationHeader = null;
            if (authorization != null && authorization.Scheme == "Bearer")
                authorizationHeader = authorization.Parameter;
            if (string.IsNullOrEmpty(authorizationHeader))
                return null;
            return authorizationHeader;
        }

        private HttpResponseMessage GetUnauthorizedResponse(string message = "", int status = 100)
        {
            var response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            response.Headers.Add(BearerAuthResponseHeader, BearerAuthResponseHeaderValue);
            
            var v = new { message = message, innerMessage = "", statusCode = status };
            response.Content = new StringContent(JsonConvert.SerializeObject(v));
            response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
            return response;
        }

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            return IsAuthenticated;
        }
    }
}