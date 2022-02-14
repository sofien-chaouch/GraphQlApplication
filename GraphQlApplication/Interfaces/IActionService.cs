using System.Collections.Generic;
using GraphQlApplication.DTO;
using GraphQlApplication.Model;


namespace GraphQlApplication.Interfaces
{
    public interface IActionService
    {
        Action GetActionById(int id);
        void AddAction(ActionDto actionDto);
        void SaveAction(Action action);
        void SaveActions(List<Action> actions);
        List<Action> GetAllActionBySessionId(int sessionId);
        List<Action> GetAllActionsBySubSessionId(int subSessionId);
        List<Action> GetActionsBySessionId(int sessionId,string status);
        List<Action> GetActionsBySubSessionId(int subSessionId,string status);
    }
}
