using System;

namespace BlazorHomeTask.Data.Models
{
    public class Message
    {
        public Message(String messageSrc = "", String messageDest = "")
        {
            MessageSrc = messageSrc;
            MessageDest = messageDest;
        }

        public String MessageSrc { get; set; }
        public String MessageDest { get; set; }
    }
}
