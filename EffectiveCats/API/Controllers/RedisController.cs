using Microsoft.AspNetCore.Mvc;
using StackExchange.Redis;

namespace API.Controllers
{
    [Route("api/[controller]")]
    public class RedisController : ControllerBase
    {
        private IDatabase _database;

        public RedisController(IDatabase database)
        {
            _database = database;
        }

        [HttpGet]
        public string Get([FromHeader] string key)
        {
            return _database.StringGet(key);
        }

        [HttpPost]
        public void Post([FromBody] KeyValuePair<string, string> kv)
        {
            WeakReference wr = new WeakReference(kv.Value);
            _database.StringSet(kv.Key, kv.Value);
        }
    }
}
