using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Climb_LineNotiyfy
{
    internal class LineNotify
    {
        SQL_SIDU SQL_SIDU = new SQL_SIDU();
        public void Line_Notify()
        {
            var current_area = "%觀音區%";
            string result = SQL_SIDU.Select_SQL(current_area);
            HttpClient httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/x-www-form-urlencoded"));
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "ztZlrGBh1AOeZD4KcP1Ag1uoE7KuCbhv7bsj4JC9y7b");
            var content = new Dictionary<string, string>();
            content.Add("message", result);
            httpClient.PostAsync("https://notify-api.line.me/api/notify", new FormUrlEncodedContent(content));
        }
    }
}
