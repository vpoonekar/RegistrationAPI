using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RegistrationAPI.Models;
using Common;
using RegistrationAPI.App_Code;

namespace RegistrationAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        OTPEmailSender sender;
        DBContext dBContext;
        public RegisterController(IEmailSender _sender , DBContext _dBContext)
        {
            sender = (OTPEmailSender)_sender;
            dBContext = _dBContext;
        }

        [HttpGet]
        public string Test()
        {
            return "Hi";
        }

        private string APIVERSION = "1.0";

        [HttpPost]
        public async Task<GeneralResponse> Send([FromBody] OTPRequest req)
        {
            switch (req.ReqCode)
            {
                case (int)REQUEST.OTPREQUEST:
                    return await RegistrationProcess.ProcessGenerateOTPRequest(req, sender , dBContext);
                    break;
                default:
                    return new GeneralResponse() { RespCode = (int)RESPONSE.INVALIDREQUEST, Version = APIVERSION };
            }
            //return new GeneralResponse(){Resp }            
        }
    }
}
