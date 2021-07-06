using System.Data.SqlClient;

namespace FinanceReportSearchTool.Task
{
    public class ConDb
    {
        /// <summary>
        /// 获取K3数据连接
        /// </summary>
        /// <returns></returns>
        public SqlConnection GetK3CloudConn()
        {
            var conn = new Conn();
            var sqlcon = new SqlConnection(conn.GetConnectionString());
            return sqlcon;
        }
    }
}
