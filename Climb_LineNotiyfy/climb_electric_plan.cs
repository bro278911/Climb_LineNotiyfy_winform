using AngleSharp;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Climb_LineNotiyfy
{
    internal class climb_electric_plan
    {
        SQL_SIDU SQL_SIDU = new SQL_SIDU();
        public async Task Main_test2Async()
        {
            var context = BrowsingContext.New(Configuration.Default.WithDefaultLoader());
            // 目標連結:
            string url = "https://branch.taipower.com.tw/d110/xcnotice?xsmsid=0M242581319136855888";
            // 取得目標網頁的 HTML 內容
            var document = await context.OpenAsync(url);
            // 選擇所有包含屬性的元素
            var dateElements = document.QuerySelectorAll("table caption");
            var timeElements = document.QuerySelectorAll(".time");
            var areaElements = document.QuerySelectorAll(".note");
            var date = "";
            var time = "";
            var area = "";
            int j = -1;
            var week_date = DateTime.Now.AddDays(7);
            //Console.WriteLine(timeElements.Length);
            //Console.WriteLine(week_date); 
            //Console.WriteLine(areaElements[305].TextContent);
            // 遍歷每個 tr 元素，並抽取 id、rank 和 title 屬性
            for (int i = 0; i < areaElements.Length; i++)
            {
                if (timeElements[i].TextContent == "停電時段")
                {
                    j++;
                }
                else
                {
                    date = (dateElements[j].TextContent).Split('：')[1];
                    time = timeElements[i].TextContent;
                    area = areaElements[i].TextContent;
                    string time_clear = time.Split('至')[0].Replace('時', ':').Replace(" ", "").Trim('分').Trim('自');//解析時間字串
                    string time2_clear = time.Split('至')[1].Replace('時', ':').Replace(" ", "").Trim('分');//解析時間字串
                    DateTime dateTime1 = DateTime.ParseExact(time_clear, "H:m", CultureInfo.InvariantCulture);
                    DateTime dateTime2 = DateTime.ParseExact(time2_clear, "H:m", CultureInfo.InvariantCulture);
                    string start_time = dateTime1.ToString("HH:mm");
                    string end_time = dateTime2.ToString("HH:mm");
                    Console.WriteLine(start_time, end_time);
                    CultureInfo culture = new CultureInfo("zh-TW");
                    culture.DateTimeFormat.Calendar = new TaiwanCalendar();
                    var Transfer_date = DateTime.Parse(date, culture);
                    var full_ymd = Transfer_date.ToString("yyyy-MM-dd");
                    int result = DateTime.Compare(Transfer_date, week_date);
                 Console.WriteLine(Transfer_date.ToString(), week_date, result);
                    if (result == 1)
                    {
                        break;
                    }
                    else
                    {
                        SQL_SIDU.Insert_SQL(full_ymd, start_time, end_time, area);
                    }                    
                }
            }
            
        }
    }
}
