using System;
using System.Collections.Generic;
using System.Linq;

namespace BlazorHomeTask.Data.Models
{
    public abstract class MessagePlayer
    {
        public MessagePlayer(int maxWaitableTimeMsg = -1)
        {
            MaxWaitableTimeMsg = maxWaitableTimeMsg;
            MessagesToPlayQueue = new Queue<Message>();
            MessagePlayedLog = new List<Message>();
            IsPlaying = false;
            LastPlayedMessage = null;
        }

        // If the log could be very big so we can save it on data base like noSql and do quarry to CRUD actions
        protected List<Message> MessagePlayedLog { get; set; }
        protected Queue<Message> MessagesToPlayQueue { get; set; }

        // set the maximum time out for waiting message in sec
        protected int MaxWaitableTimeMsg { get; set; }
        public Message LastPlayedMessage { get; set; }

        // check if is on play mode
        public bool IsPlaying { get; set; }

        public void AddMessageToLog(Message message)
        {
            MessagePlayedLog.Add(message);
        }

        // n is the index and start whit 1, not found throw out of range expiation
        public Message GetMessagesFromLog(int n = 1)
        {
            try
            {
                return MessagePlayedLog.ElementAt(n - 1);
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public void AddMessagesToPlayQueue(Message message)
        {
            MessagesToPlayQueue.Enqueue(message);
        }

        // need to implement by the inerrant instance by is platform
        protected abstract void PlayMessage(string message);
    }
}
