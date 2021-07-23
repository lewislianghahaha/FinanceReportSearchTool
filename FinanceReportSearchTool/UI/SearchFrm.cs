using System;
using System.Data;
using System.Threading;
using System.Windows.Forms;
using FinanceReportSearchTool.Task;

namespace FinanceReportSearchTool.UI
{
    public partial class SearchFrm : Form
    {
        TaskLogic taskLogic=new TaskLogic();
        Load load=new Load();

        #region 变量定义
        //返回DT类型
        private DataTable _resultTable;
        //返回查询条件信息
        private string _lbmessage;
        #endregion

        #region Get
        /// <summary>
        /// 返回DT
        /// </summary>
        public DataTable ResultTable => _resultTable;
        /// <summary>
        /// 返回查询条件信息
        /// </summary>
        public string Lbmessage => _lbmessage;
        #endregion

        public SearchFrm()
        {
            InitializeComponent();
            OnRegisterEvents();
        }

        private void OnRegisterEvents()
        {
            tmsearch.Click += Tmsearch_Click;
            tmclose.Click += Tmclose_Click;
        }

        /// <summary>
        /// 查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tmsearch_Click(object sender, EventArgs e)
        {
            try
            {
                //获取‘开始日期’
                var sdt = dtstart.Value.ToString("yyyy-MM-dd");
                //获取‘结束日期’
                var edt = dtend.Value.ToString("yyyy-MM-dd");
                //获取‘销售员’
                var salesman = txtsales.Text;
                //获取‘部门’
                var dep = txtdep.Text;
                //将各变量参数赋值给task变量
                taskLogic.Sdt = sdt;
                taskLogic.Edt = edt;
                taskLogic.Salesman = salesman;
                taskLogic.Dep = dep;

                //子线程调用
                new Thread(SearchPzstReport).Start();
                load.StartPosition = FormStartPosition.CenterScreen;
                load.ShowDialog();

                //记录查询条件
                _lbmessage = string.IsNullOrEmpty(salesman) ? $"查询条件=>开始日期:{sdt},结束日期:{edt}" : $"查询条件=>开始日期:{sdt},结束日期:{edt},销售员:{salesman}";

                //接收taskLogic.ResultTable,将赋给_resultdt,最后关闭本窗体
                _resultTable = taskLogic.ResultTable;
                
                //完成后关闭窗体
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, $"错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        /// 关闭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Tmclose_Click(object sender, EventArgs e)
        {
            try
            {
                //若返回的DT有值时，先将内容清空,再执行“关闭”(注:必须有值)
                if (_resultTable?.Rows.Count > 0)
                {
                    _resultTable.Rows.Clear();
                    _resultTable.Columns.Clear();
                }
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, $"错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        /// <summary>
        ///子线程使用(重:用于监视功能调用情况,当完成时进行关闭LoadForm)
        /// </summary>
        private void SearchPzstReport()
        {
            //查询历史记录
            taskLogic.SearchPzstReport();

            //当完成后将Load子窗体关闭
            this.Invoke((ThreadStart)(() =>
            {
                load.Close();
            }));
        }

    }
}
