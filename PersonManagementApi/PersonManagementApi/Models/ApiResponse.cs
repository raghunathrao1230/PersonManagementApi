namespace PersonManagementApi.Models
{
    public class ApiResponse<T>
    {
        public bool IsSuccess { get; set; }
        public T Data { get; set; }
        public List<string> Messages { get; set; }
    }
}
