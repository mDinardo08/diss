using System;
using System.Linq;
using System.Net.Http;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace Controllers
{
    [ResponseCache(Location = ResponseCacheLocation.None, NoStore = true)]
    public class BaseController : Controller
    {

        public BaseController()
        {
        }

        protected IActionResult ExecuteApiAction<T>(Func<ApiResult<T>> function, string uri = "")
        {
            try
            {
                var result = function();

                if (result.ValidationErrors.Any())
                    return BadRequest(result.ValidationErrors);

                if (result.Model == null)
                {
                    return NotFound();
                }

                if (String.Equals(HttpContext.Request.Method, HttpMethod.Post.ToString(), StringComparison.CurrentCultureIgnoreCase))
                {
                    return Created(uri, result.Model);
                }
                return Ok(result.Model);
            }
            catch (Exception e)
            {
                throw; //will return a 500 internal server error
            }
        }
    }
}
