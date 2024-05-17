using System;

namespace SalesDemo.Core.Models.Concrete
{
    public class Result<T>
    {

        /// <summary>
        /// hepsini el ile atama yapackasan
        /// </summary>
        /// <param name="statusCode"></param>
        /// <param name="message"></param>
        /// <param name="data"></param>
        /// <param name="time"></param>
        public Result(int statusCode, string message, T data, DateTime time)
        {
            StatusCode = statusCode;
            Message = message;
            Data = data;
            Time = time;
        }
        /// <summary>
        /// boş constructor
        /// </summary>
        public Result()
        {

        }




        public int StatusCode { get; set; } = 200;
        public string Message { get; set; }

        public DateTime Time { get; set; } = DateTime.Now;
        public T Data { get; set; }
    }

}
