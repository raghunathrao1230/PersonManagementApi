using PersonManagementApi.Models;

namespace PersonManagementApi.Helpers
{
    public class ResponseBuilder
    {
        public static ApiResponse<T> BuildResponse<T>(T data,bool isSuccess,List<string> messages=null)
        {
            if(isSuccess && messages==null)
            {
                messages = new List<string>() { "Successfully Processed"};

            }
            return new ApiResponse<T>()
            {
                 Data = data,
                 IsSuccess = isSuccess,
                 Messages = messages
            };
        }
    }
}
