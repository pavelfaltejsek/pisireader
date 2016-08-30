//  
//  Copyright (c) 2013-2014 Simon Denier & Yannis Guedel
//  
using System;
using PISI.Net.Internal;

namespace PISI.Net
{
    public class InvalidMessage : Exception
    {
        private readonly SiMessage receivedMessage;

        public InvalidMessage(SiMessage receivedMessage)
        {
            this.receivedMessage = receivedMessage;
        }

        public SiMessage ReceivedMessage()
        {
            return receivedMessage;
        }
    }
}