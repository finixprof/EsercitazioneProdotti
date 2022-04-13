using System;
using System.Collections.Generic;
using System.Text;

namespace EsercitazioneProdotti.Models.Exception
{ 
    public class CustomException : System.Exception
    {
        public int StatusCode { get; private set; }

        private string _message;
        public new string Message
        {
            get
            {
                return _message;
            }
        }

        //public Exception Exception { get; set; } idem sopra

        public CustomException(int statusCode, string message, System.Exception innerException) : base(message, innerException)
        {

            if (innerException.GetType().Equals(typeof(CustomException)))
            {
                _message = message + " " + innerException.Message;
                StatusCode = ((CustomException)innerException).StatusCode;
            }
            else
            {
                StatusCode = statusCode;
            }
        }

        public CustomException(int statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
            _message = message;
        }
    }
}
