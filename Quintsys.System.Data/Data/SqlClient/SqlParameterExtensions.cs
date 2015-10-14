using System;
using System.Data;
using System.Data.SqlClient;

namespace Quintsys.System.Data.SqlClient
{
    public static class SqlParameterExtensions
    {
        /// <summary>
        /// Formats the <paramref name="parameter" /> value.
        /// </summary>
        /// <param name="parameter">A <see cref="T:System.Data.SqlClient.SqlParameter" /> object.</param>
        /// <returns></returns>
        public static string FormattedValue(this SqlParameter parameter)
        {
            if (parameter.Direction != ParameterDirection.Input)
                return $"{parameter.ParameterName} OUTPUT";

            return parameter.Value == DBNull.Value
                ? "NULL"
                : FormatParameterValue(parameter);
        }

        private static string FormatParameterValue(SqlParameter parameter)
        {
            string parameterValue;

            // ReSharper disable once SwitchStatementMissingSomeCases
            switch (parameter.SqlDbType)
            {
                case SqlDbType.Char:
                case SqlDbType.NChar:
                case SqlDbType.NText:
                case SqlDbType.NVarChar:
                case SqlDbType.Text:
                case SqlDbType.Time:
                case SqlDbType.VarChar:
                case SqlDbType.Xml:
                case SqlDbType.Date:
                case SqlDbType.DateTime:
                case SqlDbType.DateTime2:
                case SqlDbType.DateTimeOffset:
                    parameterValue = $"'{parameter.Value}'";
                    break;
                case SqlDbType.Bit:
                    parameterValue = ((bool)parameter.Value) ? "1" : "0";
                    break;
                default:
                    parameterValue = parameter.Value.ToString();
                    break;
            }
            return parameterValue;
        }
    }
}