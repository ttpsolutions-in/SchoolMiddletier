using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using Twilio.TwiML;
using Twilio.Types;
using Twilio;
using Twilio.AspNet.Mvc;
using System.Threading.Tasks;
using Twilio.AspNet.Common;
using Twilio.Rest.Api.V2010.Account;

namespace schools.Controllers
{
    public class MessageController : TwilioController
    {
        public TwiMLResult Incoming(SmsRequest incomingMessage)
        {
            var messagingResponse = new MessagingResponse();
            messagingResponse.Message($"Hey there, this is our first whatapp response to { incomingMessage.Body}");
            var task = new Task(() => SendWhatsAppMessage(incomingMessage.To, incomingMessage.From));
            task.Start();
            return TwiML(messagingResponse);
        }
        public void SendWhatsAppMessage(string to, string from)
        {
            Thread.Sleep(5000);
            Twilio.TwilioClient.Init("AC1a0ee62d686f2fd71642008bf1dc1c33", "304dda2e22b1ec03a424a697c44968e3");
            MessageResource.Create(
                body: "This is a new whatsapp message",
                from: new Twilio.Types.PhoneNumber(to),
                to: new Twilio.Types.PhoneNumber(from)
                );
        }
        public void Alert()
        {
            var accountSid = "AC1a0ee62d686f2fd71642008bf1dc1c33";
            var authToken = "304dda2e22b1ec03a424a697c44968e3";
            TwilioClient.Init(accountSid, authToken);

            var messageOptions = new CreateMessageOptions(
                new PhoneNumber("whatsapp:+918974098031"));
            messageOptions.From = new PhoneNumber("whatsapp:+14155238886");
            messageOptions.Body = "Your appointment is coming up on July 21 at 3PM";

            var message = MessageResource.Create(messageOptions);
            Console.WriteLine(message.Body);
        }
        public void SendwithFile()
        {
            var accountSid = "AC1a0ee62d686f2fd71642008bf1dc1c33";
            var authToken = "304dda2e22b1ec03a424a697c44968e3";

            TwilioClient.Init(accountSid, authToken);

            var mediaUrl = new[] {
            new Uri("https://c1.staticflickr.com/3/2899/14341091933_1e92e62d12_b.jpg")
            }.ToList();

            var message = MessageResource.Create(
                body: "This is the ship that made the Kessel Run in fourteen parsecs?",
                from: new Twilio.Types.PhoneNumber("+15017122661"),
                mediaUrl: mediaUrl,
                to: new Twilio.Types.PhoneNumber("+918974098031")
            );

            Console.WriteLine(message.Sid);

        }
        public void MultipleRecipients()
        {
            var numbersToMessage = new List<string>
                {
                    "+15558675310",
                    "+14158141829",
                    "+15017122661"
                };

            foreach (var number in numbersToMessage)
            {
                var message = MessageResource.Create(
                    body: "Hello from my Twilio number!",
                    from: new Twilio.Types.PhoneNumber("+15017122662"),
                    to: new Twilio.Types.PhoneNumber(number)
                );

                Console.WriteLine($"Message to {number} has been {message.Status}.");
            }
        }
    }
}
