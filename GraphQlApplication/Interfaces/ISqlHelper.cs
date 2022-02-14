using System.Data;

namespace GraphQlApplication.Interfaces
{
    public interface ISqlHelper
    {
        DataTable ExecuteQuery(string query);
    }
}
