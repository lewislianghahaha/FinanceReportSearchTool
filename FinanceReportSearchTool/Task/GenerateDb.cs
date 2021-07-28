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

            var result = sourcedt.Clone();
            //用于记录各‘科目编号’下的记录集(注:每次使用完会清除记录,然后在下一次循环时使用)
            var tempdt = sourcedt.Clone();

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

                        //将相关数据插入至tempdt临时表内
                        var newtemprow = tempdt.NewRow();
                        for (var colid = 0; colid < tempdt.Columns.Count; colid++)
                        {
                            newtemprow[colid] = dtlrows[i][colid];
                        }
                        tempdt.Rows.Add(newtemprow);
                    }

                    //最后将‘合计行’添加(包括每个员工的‘合计’)
                    result.Merge(InsertSumRow(result,tempdt));
                    //在完成一个科目的'总计'计算后,新建一个空白行
                    result.Merge(InsertEmptyRow(result));
                    //最后将mark值赋给oldmark变量中
                    oldmark = mark;   
                    //在结束循环后将tempdt表数据清空,表结构保留
                    tempdt.Rows.Clear();
                }
                else if (mark == oldmark)
                {
                    continue;
                }
            }
            //计算‘科目总合计’
            result.Merge(InsertTotalSumRow(result));
            //得出结果后将'第一列'去掉再返回
            result.Columns.RemoveAt(0);
            return result;
        }

        /// <summary>
        /// 创建并插入'合计'行,利用Columns.count循环获取各列的‘合计数’,从5列开始至最后
        /// </summary>
        /// <param name="sourcedt"></param>
        /// <param name="tempdt">临时表,累加当前‘科目编码’的各合计数时使用</param>
        /// <returns></returns>
        private DataTable InsertSumRow(DataTable sourcedt,DataTable tempdt)
        {
            //定义各列‘合计’变量
            decimal colsum = 0;

            var newrow = sourcedt.NewRow();
            for (var colid = 0; colid < sourcedt.Columns.Count; colid++)
            {
                if (colid <= 3)
                {
                    newrow[colid] = DBNull.Value;
                }
                else if (colid == 4)
                {
                    newrow[colid] = "科目合计";
                }
                //从第5列开始计算各列的合计数,需要使用tempdt,并指定列进行累加合计
                else
                {
                    for (var rowsid = 0; rowsid < tempdt.Rows.Count; rowsid++)
                    {
                        colsum += Convert.ToDecimal(tempdt.Rows[rowsid][colid]);
                    }
                    newrow[colid] = colsum;
                    //最后还原sum变量
                    colsum = 0;
                }
            }
            sourcedt.Rows.Add(newrow);
            return sourcedt;
        }

        /// <summary>
        /// 在完成一个科目的总计计算后,新建一个空白行
        /// </summary>
        /// <param name="sourcedt"></param>
        /// <returns></returns>
        private DataTable InsertEmptyRow(DataTable sourcedt)
        {
            var newrow = sourcedt.NewRow();
            for (var i = 0; i < sourcedt.Columns.Count; i++)
            {
                newrow[i] = DBNull.Value;
            }
            sourcedt.Rows.Add(newrow);
            return sourcedt;
        }

        /// <summary>
        /// 计算‘科目总合计’
        /// </summary>
        /// <param name="sourcedt"></param>
        /// <returns></returns>
        private DataTable InsertTotalSumRow(DataTable sourcedt)
        {
            //定义各列‘合计’变量
            decimal colsum = 0;

            var newrow = sourcedt.NewRow();

            for (var colid = 0; colid < sourcedt.Columns.Count; colid++)
            {
                if (colid <= 3)
                {
                    newrow[colid] = DBNull.Value;
                }
                else if (colid == 4)
                {
                    newrow[colid] = "科目总合计";
                }
                else
                {
                    for (var rowid = 0; rowid < sourcedt.Rows.Count; rowid++)
                    {
                        //若检测到[0]=DBNull.Value 就continue
                        if (sourcedt.Rows[rowid][0] == DBNull.Value) continue;
                        else
                        {
                            colsum += Convert.ToDecimal(sourcedt.Rows[rowid][colid]);  
                        }   
                    }
                    newrow[colid] = colsum;
                    //最后还原sum变量
                    colsum = 0;
                }
            }
            sourcedt.Rows.Add(newrow);
            return sourcedt;
        }
    }
}
