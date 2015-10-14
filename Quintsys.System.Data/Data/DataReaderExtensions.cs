using System;
using System.Data;

namespace Quintsys.System.Data
{
    public static class DataReaderExtensions
    {
        public static T ColumnValue<T>(this IDataReader dataReader, int columnIndex)
        {
            return dataReader[columnIndex].ChangeType<T>();
        }

        public static T ColumnValue<T>(this IDataReader dataReader, string columnName)
        {
            return dataReader[columnName].ChangeType<T>();
        }

        public static T Single<T>(this IDataReader reader, Func<IDataReader, T> selector)
        {
            T result = default(T);

            if (reader.Read())
                result = selector(reader);

            if (reader.Read())
                throw new InvalidOperationException("Multiple rows returned!");

            return result;
        }
    }
}