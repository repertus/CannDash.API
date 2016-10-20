using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Twilio;

namespace CannDash.API.HelperFunctions
{
    public class TwilioSMS
    {
        public static string SendSms(string telephone, string messageContents)
        {
            string smsOutcome = "Sms sent successfully";

            var accountSid = "AC2c5bd0b7419f7d04849878e836bd0fa9";
            var authToken = "b65eeb763423711680e573a38bca7ef1";
            var twilioNumber = "+19152065638";

            var twilio = new TwilioRestClient(accountSid, authToken);
            var message = twilio.SendMessage(
                twilioNumber,
                telephone,
                messageContents
                );

            return smsOutcome;
        }
    }
}