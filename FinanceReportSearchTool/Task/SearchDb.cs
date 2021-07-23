using System;
using System.Data;
using System.Data.SqlClient;
using FinanceReportSearchTool.DB;

namespace FinanceReportSearchTool.Task
{
    //查询
    public class SearchDb
    {
        SqlList sqlList=new SqlList();
        ConDb conDb=new ConDb();

        private string _sqlscript = string.Empty;

        /// <summary>
        /// 根据SQL语句查询得出对应的DT(公共方法)
        /// </summary>
        /// <param name="sqlscript">SQL语句</param>
        /// <returns></returns>
        public DataTable UseSqlSearchIntoDt(string sqlscript)
        {
            var resultdt=new DataTable();

            try
            {
                var sqlcon = conDb.GetK3CloudConn();
                var sqlDataAdapter=new SqlDataAdapter(sqlscript,sqlcon);
                sqlDataAdapter.Fill(resultdt);
            }
            catch (Exception)
            {
                resultdt.Rows.Clear();
                resultdt.Columns.Clear();
            }
            return resultdt;
        }

        /// <summary>
        /// 查询‘凭证销售统计报表’
        /// </summary>
        /// <returns></returns>
        public DataTable SearchPzst(string sdt,string edt,string salesman,string dep)
        {
            _sqlscript = sqlList.Get_SearchPZSTReport(sdt,edt,salesman,dep);
            return UseSqlSearchIntoDt(_sqlscript);
        }

    }
}
