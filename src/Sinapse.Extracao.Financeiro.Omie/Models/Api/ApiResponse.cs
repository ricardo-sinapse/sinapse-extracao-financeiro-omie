namespace Sinapse.Extracao.Financeiro.Omie.Models.Api
{
    public class ApiResponse
    {
        public ApiResponseState State { get; set; }
        public string Message { get; set; }
        public Object Data { get; set; }

        public ApiResponse()
        {

        }

        public ApiResponse(ApiResponseState state, string message, Object data)
        {
            State = state;
            Message = message;
            Data = data;
        }

        public ApiResponse(ApiResponseState state, string message)
        {
            State = state;
            Message = message;
        }

        public ApiResponse(ApiResponseState state, Object data)
        {
            State = state;
            Data = data;
        }
    }
}
