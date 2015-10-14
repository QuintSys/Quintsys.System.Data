using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace Quintsys.System.Data.SqlClient
{
    public static class SqlCommandExtensions
    {
        /// <summary>
        /// Transforms the <paramref name="sqlCommand" /> to a SQL statement string. Use it for logging purposes.
        /// </summary>
        /// <param name="sqlCommand">A <see cref="T:System.Data.SqlClient.SqlCommand" /> object.</param>
        /// <returns></returns>
        public static string SqlStatement(this SqlCommand sqlCommand)
        {
            if (sqlCommand.CommandType != CommandType.StoredProcedure)
                return sqlCommand.CommandText;

            var stringBuilder = new StringBuilder();
            stringBuilder.AppendFormat("EXEC [{0}] ", sqlCommand.CommandText);
            foreach (SqlParameter sqlParameter in sqlCommand.Parameters)
            {
                stringBuilder.AppendFormat("{0} = {1}, ", sqlParameter.ParameterName, sqlParameter.FormattedValue());
            }

            return stringBuilder.ToString().TrimEnd(' ').TrimEnd(','); 
        }
    }
}