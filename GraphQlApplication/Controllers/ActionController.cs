using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GraphQlApplication.Interfaces;
using GraphQlApplication.Model;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Teamosphere.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActionController : ControllerBase
    {

        private readonly IActionService _action;

        public ActionController(IActionService action)
        {
            _action = action;
        }

        [HttpGet("GetActionById/{actionid}", Name = "GetActionById")]
        public IActionResult GetActionById(int actionid)
        {
            return new OkObjectResult(_action.GetActionById(actionid));
        }

        [HttpPost]
        public IActionResult SaveAction([FromBody] Action action)
        {
            if (action != null)
            {
                _action.SaveAction(action);
                return CreatedAtAction(nameof(GetActionById), new { actionId = action.action_id }, action);
            }
            return new NoContentResult();
        }

        [HttpGet("GetActionsBySessionId/{sessionId}/{status}", Name = "GetActionsBySessionId")]
        public IActionResult GetActionsBySessionId(int sessionId, string status)
        {
            return new OkObjectResult(_action.GetActionsBySessionId(sessionId, status));
        }

        [HttpGet("GetAllActionsBySessionId/{sessionId}", Name = "GetAllActionBySessionId")]
        public IActionResult GetAllActionBySessionId(int sessionId)
        {
            return new OkObjectResult(_action.GetAllActionBySessionId(sessionId));
        }

        [HttpGet("GetActionsBySubSessionId/{subSessionId}/{status}", Name = "GetActionsBySubSessionId")]
        public IActionResult GetActionsBySubSessionId(int subSessionId, string status)
        {
            return new OkObjectResult(_action.GetActionsBySubSessionId(subSessionId, status));
        }

        [HttpGet("GetAllActionsBySubSessionId/{subSessionId}", Name = "GetAllActionsBySubSessionId")]
        public IActionResult GetAllActionsBySubSessionId(int subSessionId)
        {
            return new OkObjectResult(_action.GetAllActionsBySubSessionId(subSessionId));
        }

        [HttpPost("SaveActions", Name = "SaveActions")]
        public IActionResult SaveActions([FromBody] List<Action> actions)
        {
            if (actions.Count != 0)
            {
                _action.SaveActions(actions);
                return new OkResult();
            }
            return new NoContentResult();
        }
    }
}
