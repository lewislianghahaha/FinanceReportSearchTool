using System.Configuration;

namespace FinanceReportSearchTool
{
    public class Conn
    {
        /// <summary>
        /// 获取连接字符串
        /// </summary>
        /// <returns></returns>
        public string GetConnectionString()
        {
            var strcon = string.Empty;
            //读取App.Config配置文件中的Connstring节点    
            var pubs = ConfigurationManager.ConnectionStrings["Connstring"];
            strcon = pubs.ConnectionString;
            return strcon;
        }
    }
}
