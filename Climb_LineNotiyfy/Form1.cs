﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Globalization;
using AngleSharp;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TaskbarClock;

namespace Climb_LineNotiyfy
{
    
    public partial class Form1 : Form
    {
        climb_electric_plan climb = new climb_electric_plan();
        LineNotify lineNotify = new LineNotify();
         public Form1()
        {
            var date = DateTime.Now.ToString("dd");
            if (date == "01")
            {
                Task climb = task_runAsync();
            }
            else
            {
                lineNotify.Line_Notify();//已抓取直接查詢資料庫
            }


            InitializeComponent();
        }
        private async Task task_runAsync()
        {
            await Task.Run(() => climb.Main_test2Async());
            label1.Text = "已抓取完成!!";
            lineNotify.Line_Notify();//等待抓取完查詢資料庫
        }

    }
}
