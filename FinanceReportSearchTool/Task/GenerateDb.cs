using System;
using System.Data;

namespace FinanceReportSearchTool.Task
{
    public class GenerateDb
    {
        /// <summary>
        /// 作用:根据查询得出的DT进行数据整理;对相同'科目编号'的数据进行整合,并新增一行‘合计’计算相同'科目编号'的'合计'值
        /// </summary>
        /// <param name="sourcedt"></param>
        /// <returns></returns>
        public DataTable GenerateDt(DataTable sourcedt)
        {
            //定义‘旧标记科目编码’变量
            var oldmark = "";
            //定义‘标记科目编码’变量
            var mark = "";
            //定义‘合计’变量
            decimal sum = 0;

            var result = sourcedt.Clone();

            //循环查询结果DT
            foreach (DataRow rows in sourcedt.Rows)
            {
                //获取‘科目编号’标记
                mark = Convert.ToString(rows[0]);
                if (mark == "" || mark != oldmark)
                {
                    //针对获取的‘科目编号’标记作为条件,进行查询,若有记录即将记录插入至result内,并在插入完成后,计算及新增‘合计行’
                    var dtlrows = sourcedt.Select("AA='" +mark+ "'");
                    for (var i = 0; i < dtlrows.Length; i++)
                    {
                        //将数值插入至result内
                        var newrow = result.NewRow();
                        for (var j = 0; j < result.Columns.Count; j++)
                        {
                            newrow[j] = dtlrows[i][j];
                        }
                        result.Rows.Add(newrow);
                        //最后累加'合计'项
                        sum += Convert.ToDecimal(dtlrows[i][4]);
                    }
                    //最后将‘合计行’添加
                    result.Merge(InsertSumRow(result,sum));
                    //最后还原sum变量,并将mark值赋给oldmark变量中
                    oldmark = mark;
                    sum = 0;
                }
                else if (mark == oldmark)
                {
                    continue;
                }
            }
            //得出结果后将'第一列'去掉再返回
            result.Columns.RemoveAt(0);
            return result;
        }

        /// <summary>
        /// 创建并插入'合计'行
        /// </summary>
        /// <param name="sourcedt"></param>
        /// <param name="sumqty">合计数</param>
        /// <returns></returns>
        private DataTable InsertSumRow(DataTable sourcedt,decimal sumqty)
        {
            var newrow = sourcedt.NewRow();
            for (var i = 0; i < sourcedt.Columns.Count; i++)
            {
                switch (i)
                {
                    case 3:
                        newrow[i] = "科目合计";
                        break;
                    case 4:
                        newrow[i] = sumqty;
                        break;
                    default:
                        newrow[i] = DBNull.Value;
                        break;
                }
            }
            sourcedt.Rows.Add(newrow);
            return sourcedt;
        }
    }
}
