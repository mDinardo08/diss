using System.Collections.Generic;

namespace Models
{
    public class ApiResult<T>
    {
    public T Model { get; set; }
    public List<string> ValidationErrors { get; set; } = new List<string>();
    }
}