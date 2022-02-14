using System.Collections.Generic;
using System.Linq;
using GraphQlApplication.DTO;
using GraphQlApplication.Interfaces;
using GraphQlApplication.Model;


namespace GraphQlApplication.Services
{
    public class ActionService : IActionService
    {
        private readonly IGenericRepository<Model.Action> _action;


        public ActionService(IGenericRepository<Action> action)
        {
            _action = action;
        }

        public void AddAction(ActionDto actionDto)
        {
            
        }

        public Action GetActionById(int id)
        {
            return _action.GetWithInclude(t => t.action_id == id, null)?.FirstOrDefault();
        }

        public void SaveAction(Action action)
        {
            if (action.action_id == 0)
            {
                _action.Insert(action);
            }
            else
            {
                _action.Update(action);
            }
        }

        public List<Action> GetActionsBySessionId(int sessionId, string status)
        {
            return _action.GetWithInclude(s => s.session_id == sessionId)?.Where(t => t.status == status).ToList();
        }

        public List<Action> GetAllActionBySessionId(int sessionId)
        {
            return _action.GetWithInclude(s => s.session_id == sessionId)?.ToList();
        }

        public List<Action> GetActionsBySubSessionId(int subSessionId, string status)
        {
            return _action.GetWithInclude(s => s.sub_session_id == subSessionId)?.Where(t => t.status == status).ToList();
        }

        public List<Action> GetAllActionsBySubSessionId(int subSessionId)
        {
            return _action.GetWithInclude(s => s.sub_session_id == subSessionId)?.ToList();
        }

        public void SaveActions(List<Action> actions)
        {
            foreach (var action in actions)
            {
                SaveAction(action);
            }
        }
    }
}
