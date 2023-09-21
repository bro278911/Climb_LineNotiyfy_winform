using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Climb_LineNotiyfy
{
    internal class SQL_SIDU
    {
        public void Insert_SQL(string date, string start_time, string end_time, string area)
        {
            Method_electric method = new Method_electric();
            string re_ = method.electric_insert(date,start_time,end_time, area);
            Console.WriteLine(re_);//success or fail
        }
        public string Select_SQL(string current_area)
        {
            string area = "";
            string date = "";
            string start_time = "";
            string end_time = "";
            string all = "";
            Method_electric method = new Method_electric();
            DataTable re_ = method.electric_Select(current_area);
            try
            {
                date = Convert.ToDateTime(re_.Rows[0]["date"]).ToString("yyyy-MM-dd");
                start_time = re_.Rows[0]["start_time"].ToString();
                end_time = re_.Rows[0]["end_time"].ToString();
                area = re_.Rows[0]["area"].ToString();
                all = "停電日期："+date+ "停電開始時間：" + Convert.ToDateTime(start_time).ToString("HH:mm") + " 停電結束時間：" + Convert.ToDateTime(end_time).ToString("HH:mm") + "\n停電區域：" + area;
            }
            catch (Exception ex)
            {
                all = "目前沒有停電訊息";
            }
            
            return all;
        }
    }
}
