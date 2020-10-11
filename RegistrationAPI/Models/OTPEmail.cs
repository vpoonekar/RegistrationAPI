using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Common;
using Org.BouncyCastle.Asn1;

namespace RegistrationAPI.Models
{
    public class OTPEmail : EmailMessage 
    {
        public OTPEmail(string _to, string _subject, string _otp)
        {
            
        }
    }
}
