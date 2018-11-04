namespace Sids.Prodesp.Infrastructure.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Configuration;
    using System.Data;
    using System.Data.SqlClient;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Reflection;

    internal static class DataHelper
    {
        public static TOut Cast<TOut>(object sourceIn) where TOut : new()
        {
            if (sourceIn == null) return default(TOut);

            var resultOut = new TOut();
            foreach (var propIn in sourceIn.GetType().GetProperties())
            {
                var propOut = typeof(TOut).GetProperty(propIn.Name);
                if (propOut != null)
                    propOut.SetValue(resultOut, propIn.GetValue(sourceIn));
            }
            return resultOut;
        }
        public static IEnumerable<TOut> CastList<TOut, TIn>(TIn[] sourceIn) where TOut : new()
        {
            foreach (var item in sourceIn)
            {
                var resultOut = new TOut();
                foreach (var propIn in typeof(TIn).GetProperties())
                {
                    var propOut = typeof(TOut).GetProperty(propIn.Name);
                    if (propOut != null)
                        propOut.SetValue(resultOut, propIn.GetValue(item));
                }
                yield return resultOut;
            }
        }

        #region Get
        public static T Get<T>(string query) where T : new()
        {
            return Get<T>(query, string.Empty);
        }
        public static T Get<T>(string query, params SqlParameter[] parameter) where T : new()
        {
            return Get<T>(query, string.Empty, parameter);
        }
        public static T Get<T>(string query, SqlParameterCollection parameter) where T : new()
        {
            return Get<T>(query, string.Empty, parameter.Cast<SqlParameter>().ToArray());
        }
        public static T Get<T>(string query, string catalog) where T : new()
        {
            return Get<T>(query, catalog, new SqlParameter[] { });
        }
        public static T Get<T>(string query, string catalog, params SqlParameter[] parameter) where T : new()
        {
            return Cast<T>(Get(query, catalog, parameter).Select().FirstOrDefault());
        }
        #endregion

        #region List
        public static IEnumerable<T> List<T>(string query, string catalog, params SqlParameter[] parameter) where T : new()
        {
            return from DataRow row in Get(query, catalog, parameter).Rows select Cast<T>(row);
        }
        public static IEnumerable<T> List<T>(string query) where T : new()
        {
            return List<T>(query, string.Empty);
        }
        public static IEnumerable<T> List<T>(string query, params SqlParameter[] parameter) where T : new()
        {
            return List<T>(query, string.Empty, parameter);
        }
        public static IEnumerable<T> List<T>(string query, SqlParameterCollection parameter) where T : new()
        {
            SqlParameter[] obj = new SqlParameter[parameter.Count];
            parameter.CopyTo(obj, 0);
            return List<T>(query, string.Empty, obj);
        }
        public static IEnumerable<T> List<T>(string query, string catalog) where T : new()
        {
            return List<T>(query, catalog, new SqlParameter[] { });
        }
        #endregion

        #region Generic's DataBase
        private static T Cast<T>(DataRow dr) where T : new()
        {
            var columnName = string.Empty;
            if (dr == null) return default(T);

            var source = new T();

            try
            {
                if (source is int)
                {
                    return (T)Convert.ChangeType(dr.Table.Rows[0][0], typeof(T));
                }

                foreach (DataColumn column in dr.Table.Columns)
                {
                    columnName = column.ColumnName;
                    var t = typeof(T);
                    var p = t.GetProperty(column.ColumnName) ??
                        t.GetProperties().FirstOrDefault(q => q.GetCustomAttributes(true).Any(x => x is ColumnAttribute && ((ColumnAttribute)x).Name == column.ColumnName));

                    if (p != null && dr[column.ColumnName].GetType().Name != "DBNull")
                    {
                        var val = dr[column.ColumnName];

                        p.SetValue(source, p.PropertyType.IsGenericType && p.PropertyType.GetGenericArguments()[0].BaseType == typeof(Enum)
                            ? Enum.Parse(p.PropertyType.GetGenericArguments()[0], val.ToString())
                            : val);
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message + " " + columnName);
                throw;
            }

            return source;
        }
        private static ConnectionStringSettings _connectionStringSettings;
        private static ConnectionStringSettings ConnectionString
        {
            get
            {
                return _connectionStringSettings ?? (_connectionStringSettings =
                    ConfigurationManager.ConnectionStrings[ConfigurationManager.ConnectionStrings.Count - 1]);
            }
        }
        [SuppressMessage("Microsoft.Security", "CA2100:Review SQL queries for security vulnerabilities")]
        private static DataTable Get(string query, string catalog, params SqlParameter[] parameter)
        {
            using (var dt = new DataTable())
            using (var cmd = new SqlCommand())
            {
                cmd.CommandTimeout = 300;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = query;

                if (parameter.Any()) cmd.Parameters.AddRange(parameter);

                using (var cn = new SqlConnection(ConnectionString.ConnectionString))
                {
                    cmd.Connection = cn;

                    cn.Open();

                    if (!string.IsNullOrEmpty(catalog)) cn.ChangeDatabase(catalog);

                    dt.Load(cmd.ExecuteReader());

                    return dt;
                }
            }
        }
        #endregion

        private static object GetValue(object aux)
        {
            return Convert.ToString(aux).Trim();
        }

        public static string GetColumn<T>(string propriety)
        {

            foreach (var prop in typeof(T).GetProperties())
            {
                object[] attrs = prop.GetCustomAttributes(true);
                if (attrs == null || attrs.Length == 0)
                    continue;

                string conds = "";

                if (prop.Name != propriety) continue;
                foreach (Attribute attr in attrs)
                {
                    if (!(attr is ColumnAttribute)) continue;

                    conds += (attr as ColumnAttribute).Name;
                    return $"@{conds}";
                }
            }

            return "";
        }
        public static SqlParameter[] GetSqlParameterList<T>(T entity)
        {
            return GetSqlParameterList(entity, new string[0]);
        }
        public static SqlParameter[] GetSqlParameterList<T>(T entity, string[] parametersToIgnore)
        {
            var properties = entity.GetType().GetProperties();
            var parames = new List<SqlParameter>();
            foreach (var propertyInfo in properties)
            {
                string column = GetColumn<T>(propertyInfo.Name);
                if (column != "")
                {
                    if (!parametersToIgnore.Contains(column))
                    {
                        var value = propertyInfo.GetValue(entity);
                        value = value is DateTime ? ((DateTime)value).ValidateDBNull() : propertyInfo.GetValue(entity);
                        var paramer = new SqlParameter(column, value);
                        parames.Add(paramer);
                    }
                }
            }

            return parames.ToArray();
        }

        internal static object GetMovCancelamento<T>(string v, SqlParameter paramId)
        {
            throw new NotImplementedException();
        }
    }
}
