using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

/// <summary>
/// Method_Fish 的摘要描述
/// </summary>
public class Method_electric
{
    public static string cn = $"Data Source=LAPTOP-LI1PKLM3;Uid=sa;pwd=teco11332202";
    public static string cnStr = cn + ";database=test";
    SqlConnection conn;
    public Method_electric()
    {
        conn = new SqlConnection(cnStr);
    }

    #region 爬蟲資料新增(建)
    public string electric_insert(string date, string start_time, string end_time, string area)
    {
        string result = "";
        SqlCommand cmd = new SqlCommand
       (@"Insert into [test].[dbo].[stop_electric_plan](date,start_time,end_time,area) VALUES (@date,@start_time,@end_time,@area)");

        cmd.Parameters.Add("@date", SqlDbType.Date).Value = date;
        cmd.Parameters.Add("@start_time", SqlDbType.Time).Value = start_time;
        cmd.Parameters.Add("@end_time", SqlDbType.Time).Value = end_time;
        cmd.Parameters.Add("@area", SqlDbType.VarChar).Value = area;
        int check_num = electric_WEB.SqlHelper.cmdCheck(cmd);
        result = (check_num != 0) ? "success" : "fail";
        return result;
    }
    #endregion
    public DataTable electric_Select(string current_area)
    {
        //Console.WriteLine(current_area);
        SqlCommand cmd = new SqlCommand(@"SELECT TOP(1) *  FROM [test].[dbo].[stop_electric_plan] WHERE area like @area AND Cast(date as varchar(max))+ ' ' +Cast(start_time as varchar(max)) >= SYSDATETIME()");
        
        cmd.Parameters.Add("@area", SqlDbType.VarChar).Value = current_area;
        //Console.WriteLine(cmd);
        DataTable dt = electric_WEB.SqlHelper.cmdTable(cmd);
        Console.WriteLine(dt);
        return dt;

    }
}