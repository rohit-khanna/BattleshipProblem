using System;

namespace Game.Model
{
    public class ErrorResponse
    {
        public string Message { get; set; }

        public override string ToString()
        {
            return "Error:" + Message + "  " + DateTime.Now;
        }
    }
}
