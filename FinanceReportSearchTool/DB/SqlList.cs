namespace FinanceReportSearchTool.DB
{
    public class SqlList
    {
        //根据SQLID返回对应的SQL语句  
        private string _result;

        /// <summary>
        /// 查询‘凭证销售统计报表’
        /// </summary>
        /// <param name="sdt">开始日期</param>
        /// <param name="edt">结束日期</param>
        /// <param name="saleman">销售员</param>
        /// <param name="dep">部门</param>
        /// <returns></returns>
        public string Get_SearchPZSTReport(string sdt,string edt,string saleman,string dep)
        {
            _result = $@"
                            if OBJECT_ID('tempdb..#temp1')is not null
		                    drop table #temp1

	                        CREATE TABLE #temp1(
	                                            部门 NVARCHAR(250),凭证日期 nvarchar(250),AA NVARCHAR(100),
	                                            科目编码 nvarchar(250),科目名称 nvarchar(250),摘要 nvarchar(250),
	                                            销售员 nvarchar(250),
	                                            本位币金额 DECIMAL(25,2),借方金额 DECIMAL(25,2),贷方金额 DECIMAL(25,2),
						                        余额方向 NVARCHAR(200)
	                                            )
	                     
	                        INSERT INTO #temp1(
	                                        部门,凭证日期,AA,
	                                        科目编码,科目名称,摘要,
	                                        销售员,
	                                        本位币金额,借方金额,贷方金额,
						                    余额方向)
	                        SELECT * 
	                        FROM (
	                        SELECT 
	                        DISTINCT
		                        T11.FNAME 部门
	                            ,CONVERT(varchar(100),t1.fdate,23) 凭证日期
		                        ,SUBSTRING(t8.FNUMBER,1,4) AA
	                            ,t8.FNUMBER 科目编码 --科目编码
	                            ,t5.FFULLNAME 科目名称 --科目名称
	                            ,t6.FEXPLANATION 摘要 --摘要
	                            ,case t2.FFLEXITEMPROPERTYID when 4 then t3.fname when 100017 then t4.fname else '' end 销售员 --销售员
	                            ,t6.FAMOUNT 本位币金额 --本位币金额
	                            ,t6.FDEBIT 借方金额    --借方金额
	                            ,t6.FCREDIT 贷方金额   --贷方金额
		                        ,t8.FDC   余额方向     --余额方向（1:借方 -1:贷方）

	                        FROM T_GL_VOUCHER t1
	                        INNER join T_GL_VOUCHERENTRY t6 on t1.FVOUCHERID=t6.FVOUCHERID
	                        INNER join T_BD_ACCOUNT t8 on t8.FACCTID=t6.FACCOUNTID
	                        INNER join T_BD_ACCOUNT_L t5 on t6.FACCOUNTID=t5.FACCTID
	                        INNER join T_BD_ACCOUNTFLEXENTRY t2 on t2.FACCTID=t5.FACCTID
	                        LEFT JOIN T_BD_FLEXITEMDETAILV t7 on t7.FID=t6.FDETAILID
	                        LEFT JOIN dbo.T_BD_DEPARTMENT_L T11 ON T7.FFLEX5=T11.FDEPTID AND T11.FLOCALEID=2052 --部门

	                        LEFT join T_HR_EMPINFO_L t3 on t3.FID=t7.FFLEX7     --员工
	                        LEFT join V_BD_SALESMAN_L t4 on t4.fid=t7.FF100017  --销售员
	                        LEFT JOIN dbo.T_HR_EMPINFO T9 ON T3.FID=T9.FID AND T9.FFORBIDSTATUS!='B'
	                        LEFT JOIN dbo.V_BD_SALESMAN T10 ON T4.fid=T10.fid AND T10.FFORBIDSTATUS!='B'

	                        WHERE (t8.FNUMBER like '6601.%' or t8.FNUMBER like '6001.%' or
	                                t8.FNUMBER like '6051.%' or t8.FNUMBER like '6401.%' or t8.FNUMBER like '6402.%')
	                        AND (t1.FDATE>='{sdt}' AND T1.FDATE<='{edt}')
	                        AND (case t2.FFLEXITEMPROPERTYID when 4 then t3.fname when 100017 then t4.fname else '' end)<>''
	                        )X
	                        WHERE (X.销售员='{saleman}' OR '{saleman}'='')
	                        AND (X.部门='{dep}' OR '{dep}'='')
	                        ORDER BY X.AA--,X.凭证日期

	                        --SELECT * FROM #temp1 order by aa
	 
	                        --动态将行内容转换为列显示(注:当#temp1有记录时才执行)
	                        DECLARE
	                        @COUNT INT,
	                        @SQL NVARCHAR(MAX);

	                        SELECT @COUNT=COUNT(*) FROM #temp1
	                        IF(@COUNT>0)
	                        BEGIN
		                        SET @SQL='SELECT AA,部门,科目编码,科目名称,SUM(CASE 余额方向 WHEN 1 THEN 借方金额 ELSE 贷方金额 END) 合计'

		                        SELECT @SQL=@SQL+',isnull (sum(case 销售员 when '''+销售员+''' then case 余额方向 when 1 then 借方金额 else 贷方金额 end end),0) as ['+销售员+']'
		                        FROM (SELECT DISTINCT 销售员 FROM #temp1) AS A

		                        SELECT @SQL=@SQL+'FROM #TEMP1 GROUP BY [AA],[部门],[科目编码],[科目名称] ORDER BY [AA]'
	  
		                        EXEC(@SQL)
	                        END 
                        ";
            return _result;
        }

    }
}
