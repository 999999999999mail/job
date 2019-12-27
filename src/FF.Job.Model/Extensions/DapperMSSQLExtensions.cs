using Dapper;
using FF.Job.Infrastructure;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FF.Job.Model.Extensions
{
    public static class DapperMSSQLExtensions
    {
        private const string PAGE_SQL = "WITH __CTE AS ({0}), __CTE2 AS (SELECT COUNT(*) AS __RecordCount FROM __CTE) SELECT * FROM __CTE, __CTE2 ORDER BY {1} OFFSET {2} * {3} ROWS FETCH NEXT {3} ROWS ONLY;";

        public static async Task<(IEnumerable<T> Rows, int RecordCount)> QueryPageAsync<T>(this IDbConnection connection, string sql, object param, string sortName, string sortOrder = "ASC", int pageInex = 0, int pageSize = 15, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null) where T : EntityRoot
        {
            if (string.IsNullOrEmpty(sortName))
            {
                throw new CustomException("Missing sort field");
            }
            var orderByClause = $"[{sortName}] {(sortOrder?.ToUpper() == "DESC" ? "DESC" : "ASC")}";
            sql = string.Format(PAGE_SQL, sql, orderByClause, pageInex, pageSize);
            var rows = await connection.QueryAsync<T>(sql, param, transaction, commandTimeout, commandType);
            var recordCount = 0;
            if (rows?.Count() > 0)
            {
                recordCount = rows.First().__RecordCount;
            }
            return (rows, recordCount);
        }

        public static async Task<(IEnumerable<TRow> Rows, int RecordCount)> QueryPageAsync<TRow>(this IDbConnection connection, string sql, PageParameter param, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null) where TRow : EntityRoot
        {
            return await connection.QueryPageAsync<TRow>(sql, param, param.SortName, param.SortOrder, param.PageIndex, param.PageSize, transaction, commandTimeout, commandType);
        }

        public static async Task<(IEnumerable<TRow> Rows, int RecordCount)> QueryPageAsync<TRow, TQuery>(this IDbConnection connection, string sql, QueryParameter<TQuery> param, IDbTransaction transaction = null, int? commandTimeout = null, CommandType? commandType = null) where TRow : EntityRoot
        {
            return await connection.QueryPageAsync<TRow>(sql, param.Data, param.SortName, param.SortOrder, param.PageIndex, param.PageSize, transaction, commandTimeout, commandType);
        }

        public static async Task<bool> BulkCopyAsync(this IDbConnection connection, DataTable dataTable, string destinationTableName, IDbTransaction transaction = null)
        {
            if (!(connection is SqlConnection sqlConnection))
            {
                return false;
            }
            SqlTransaction sqlTransaction = null;
            if (transaction != null)
            {
                sqlTransaction = transaction as SqlTransaction;
                if (sqlTransaction == null)
                {
                    return false;
                }
            }
            if (dataTable == null)
            {
                return false;
            }
            if (string.IsNullOrEmpty(destinationTableName))
            {
                return false;
            }
            using SqlBulkCopy sqlBulkCopy = new SqlBulkCopy(sqlConnection, SqlBulkCopyOptions.Default, sqlTransaction)
            {
                DestinationTableName = destinationTableName,
            };
            await sqlBulkCopy.WriteToServerAsync(dataTable);
            return true;
        }
    }

    public abstract class PageParameter
    {
        /// <summary>
        /// 页码
        /// </summary>
        public int PageIndex { get; set; } = 0;

        /// <summary>
        /// 每页行数
        /// </summary>
        public int PageSize { get; set; } = 15;

        /// <summary>
        /// 排序字段
        /// </summary>
        public string SortName { get; set; }

        /// <summary>
        /// 排序顺序 ASD|DESC
        /// </summary>
        public string SortOrder { get; set; }
    }

    public class QueryParameter<T> : PageParameter
    {
        public T Data { get; set; }
    }
}
