using System;

namespace com.dug.Server.vo
{

    [Serializable]
    public class CRUDResult
    {
        public enum ResultType
        {
            SUCCESS = 0, FAILED = 1
        }

        public ResultType resultType;
        public string message;
    }
}


