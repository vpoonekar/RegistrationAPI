#define DIRECTMAIL
#if !DIRECTMAIL
    #define QUEUE
#endif
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using Newtonsoft.Json;
using RegistrationAPI.Models;
using Renci.SshNet.Messages;

namespace RegistrationAPI.App_Code
{
    public class RegistrationProcess
    {
        public static async Task<GeneralResponse> ProcessGenerateOTPRequest(OTPRequest _req , OTPEmailSender sender , DBContext _dBContext)
        {
            OTPRequest _otpReq = _req;
            GeneralResponse _resp = null;

            //write request to queue.
            //string _msg = JsonConvert.SerializeObject(_req);

            //Generate OTP here
            _req._reqid = UniqueCodeGenerator.GetUniqueCodeForRequest();
            _req._emailotp = GenerateOTP();
            _req._mobileotpCode = GenerateOTP();
            _req.ParkDB = _dBContext;
            
            DBParams param1 = new DBParams();
            param1.FieldName = "fsReqId";
            param1.FieldValue = _req._reqid;

            DBParams param2 = new DBParams();
            param2.FieldName = "fsMobileNo";
            param2.FieldValue = _req.Mobile;
            
            DBParams param3 = new DBParams();
            param3.FieldName = "fsEmail";
            param3.FieldValue = _req.Email;

            DBParams param4 = new DBParams();
            param4.FieldName = "fsMobileOTP";
            param4.FieldValue = _req._mobileotpCode;

            DBParams param5 = new DBParams();
            param5.FieldName = "fsEmailOTP";
            param5.FieldValue = _req._emailotp;

            int _retCode = await _req.InsertAsync(param1, param2, param3, param4, param5);
            
#if DIRECTMAIL            
            EmailMessage _message = new EmailMessage();
            _message.To = _req.Email;
            _message.Subject = "Account Activatio Code from StarParkz";
            _message.Body = _req._emailotp;
            await sender.SendEmailAsync(_message);

#endif
            //QM.WriteStringToQueue(_msg);

            //var t = Task.Run(() => _resp = new GeneralResponseMsg() { RespCode = (int)RESPONSE.OTPRESPONSE, RespMsg = PREDEFINEDMESSAGE.MOBILE_OTP_GENERATED, Data = "OK" });
            //t.Wait();
            return _resp;
            //validation to check if user exist or not. If exist return message that user already exist else proceed with OTP generation.
            //we are going to write the code to generate the OTP.
            // User the API to send the otp to the mobile number
            // also save the OTP in the database against mobile number. There has to be a time also for OTP expiration.

            //throw new Exception("Error");
        }

        //public static GeneralResponseMsg ProcessValidateOTPRequest(GeneralRequestMsg _req)
        //{
        //    OTPValidationReq _validateotp = JsonConvert.DeserializeObject<OTPValidationReq>(_req.Data.ToString());
        //    return new GeneralResponseMsg() { RespCode = (int)RESPONSE.VALIDATEOTPRESPONSE, RespMsg = PREDEFINEDMESSAGE.SUCCESS, Data = "OK" };
        //    //we will not store this data because on the front-end just after OTP validation we are going to ask user to register
        //    // and we will storing the registration value so this is just a validtion step.
        //    // so if the user validate the OTP and close the app witout registration he/she has to do it again when the app is started again.
        //}

        //public static GeneralResponseMsg ProcessRegisterUserRequest(GeneralRequestMsg _req)
        //{
        //    RegistrationReq _register = JsonConvert.DeserializeObject<RegistrationReq>(_req.Data.ToString());
        //    //this is where we will validate the User if it exist.
        //    // if exist we will return existing user details.
        //    // if not then we will create the unique id with date (YYYYMMDD at the end)
        //    RegistrationResp _resp = new RegistrationResp() { CN = _register.Mobile, UID = Guid.NewGuid().ToString("D") + DateTime.Now.ToString("YYYYMMDD") };
        //    return new GeneralResponseMsg() { RespCode = (int)RESPONSE.REGISTRATIONRESPONSE, RespMsg = PREDEFINEDMESSAGE.REGISTRATION_COMPLETE, Data = _resp };
        //}

        //private void ValidateStructure()
        //{
        //}

        private static string GenerateOTP()
        {
            Random rnd = new Random();
            return rnd.Next(1000, 9999).ToString();            
        }
    }
}
