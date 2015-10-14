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
                case SqlDbType.BigInt:
                case SqlDbType.Binary:
                case SqlDbType.Decimal:
                case SqlDbType.Float:
                case SqlDbType.Image:
                case SqlDbType.Int:
                case SqlDbType.Money:
                case SqlDbType.Real:
                case SqlDbType.UniqueIdentifier:
                case SqlDbType.SmallDateTime:
                case SqlDbType.SmallInt:
                case SqlDbType.SmallMoney:
                case SqlDbType.Timestamp:
                case SqlDbType.TinyInt:
                case SqlDbType.VarBinary:
                case SqlDbType.Variant:
                case SqlDbType.Udt:
                case SqlDbType.Structured:
                default:
                    parameterValue = parameter.Value.ToString();
                    break;
            }
            return parameterValue;
        }
    }
}