using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace com.dug.Server.exceptions
{
    public class ServerException : Exception
    {
        public ServerException() : base()
        {
        }
        public ServerException(string message) : base(message)
        {

        }
        public ServerException(string message, Exception innerException) : base(message, innerException)
        {

        }

    }

}
