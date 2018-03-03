using System;
using Microsoft.Azure.Documents;

namespace TemperatureFunctions.CQRS.Queries
{
    public class TimeSeriesQuery : IQuery
    {
        public TimeSeriesQuery(DateTimeOffset timeFrom, DateTimeOffset timeTo, long maxNumberOfItems)
        {
            Sql = new SqlQuerySpec("SELECT TOP @maxNumberOfItems c.temperature, c._ts FROM c WHERE c._ts >= @timestampFrom AND c._ts <= @timestampTo ORDER BY c._ts DESC",
                new SqlParameterCollection
                {
                    new SqlParameter("@maxNumberOfItems", maxNumberOfItems),
                    new SqlParameter("@timestampFrom", timeFrom.ToUnixTimeSeconds()),
                    new SqlParameter("@timestampTo", timeTo.ToUnixTimeSeconds())
                });
        }

        public SqlQuerySpec Sql { get; }
    }
}