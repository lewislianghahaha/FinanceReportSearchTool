using System.Data;

namespace FinanceReportSearchTool.Task
{
    //中转代码
    public class TaskLogic
    {
        SearchDb searchDb=new SearchDb();
        ExportDb exportDb=new ExportDb();

        #region 变量定义

        private string _sdt;        //开始日期
        private string _edt;       //结束日期
        private string _salesman; //销售员
        private string _dep;      //部门


        private DataTable _resultTable; //返回Dt记录集

        private DataTable _exportdt;    //获取Dt记录集(用于导出至EXCEL)
        private string _add;           //导出地址
        private bool _resultmark;      //返回是否成功标记
        #endregion

        #region Set(获取外部值)
        /// <summary>
        /// 开始日期
        /// </summary>
        public string Sdt { set { _sdt = value; } }
        /// <summary>
        /// 结束日期
        /// </summary>
        public string Edt { set { _edt = value; } }
        /// <summary>
        /// 销售员
        /// </summary>
        public string Salesman { set { _salesman = value; } }
        /// <summary>
        /// 部门
        /// </summary>
        public string Dep { set { _dep = value; } }

        /// <summary>
        /// 获取Dt记录集(用于导出至EXCEL)
        /// </summary>
        public DataTable Exportdt { set { _exportdt = value; } }
        /// <summary>
        /// 导出地址
        /// </summary>
        public string Add { set { _add = value; } }
        #endregion

        #region Get(返回值至外部)
        /// <summary>
        ///返回DataTable至主窗体
        /// </summary>
        public DataTable ResultTable => _resultTable;
        /// <summary>
        /// 返回是否成功标记
        /// </summary>
        public bool Resultmark => _resultmark;
        #endregion

        /// <summary>
        /// ‘凭证销售统计表’使用
        /// </summary>
        public void SearchPzstReport()
        {
            _resultTable = searchDb.SearchPzst(_sdt, _edt, _salesman,_dep);
        }

        /// <summary>
        /// 导出
        /// </summary>
        public void ExportDtToExcel()
        {
            _resultmark = exportDb.ExportDtToExcel(_add, _exportdt);
        }

    }
}
