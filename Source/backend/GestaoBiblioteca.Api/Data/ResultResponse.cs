using Newtonsoft.Json;
using System.Net;

namespace GestaoBiblioteca.Api.Data
{
    public class ResultResponse<T>
    {
        public bool Success { get; set; } = false;
        //public ExtratosFinanceiros Extratos {  get; set; }

        [JsonProperty(Required = Required.Always)]
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.BadRequest;

        public T Data { get; set; }

        public ICollection<string> Errors { get; set; }

        public ResultResponse()
        {
            Errors = new List<string>();
        }

        public ResultResponse(T data, bool success, HttpStatusCode statusCode)
        {
            Data = data;
            Success = success;
            StatusCode = statusCode;
            Errors = new List<string>();
        }

        public ResultResponse(string error, HttpStatusCode statusCode)
        {
            Success = false;
            StatusCode = statusCode;
            Errors = new List<string>() { error };
        }

        public ResultResponse(T data)
        {
            Data = data;
        }

        public void AddError(string error)
        {
            Errors.Add(error);
        }
    }
}
