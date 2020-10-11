using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Common;
using MySqlX.XDevAPI.Relational;
using MySql.Data.MySqlClient;

namespace RegistrationAPI.Models
{
    public class OTPRequest : GeneralRequest
    {
        [Required(ErrorMessage = "Mobile Number Missing")]
        public string Mobile {get; set;}
        [Required(ErrorMessage = "Mobile Number Missing")]
        public string Email {get; set;}
        internal DBContext ParkDB { get; set; }

        public string _mobileotpCode;

        public string _emailotp;
        public string _reqid;
        public OTPRequest()
        { }

        internal OTPRequest(DBContext _db)
        {
            ParkDB = _db;
        }

        public async Task<int> InsertAsync(params DBParams[] param)
        {
            return await ParkDB.ExecureSTPAsync("stp_insertotp" , param);
        }

    }
}
