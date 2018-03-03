using Microsoft.Azure.Documents;

namespace TemperatureFunctions.CQRS
{
    public interface IQuery
    {
        SqlQuerySpec Sql { get; }
    }
}