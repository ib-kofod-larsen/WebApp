using ikl.web.Shared;
using Microsoft.AspNetCore.Mvc;

namespace ikl.web.Server.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class DataController : ControllerBase
    {
        private readonly Data _data;

        public DataController(Data data)
        {
            _data = data;
        }
        
        [HttpGet]
        public Data Get()
        {
            return _data;
        }
    }
}