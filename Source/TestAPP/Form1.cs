using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace TestAPP
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            webBrowser1.Url = new Uri(Path.Combine(Application.StartupPath, "index.html"));
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DataTable dt = DBUtility.MySQLHelper.Instance.GetDataTable("select * from t_test");
            string result = JsonConvert.SerializeObject(dt, new DataTableConverter());

            // 调用JavaScript的messageBox方法，并传入参数
            object[] objects = new object[1];
            objects[0] = result;
            this.webBrowser1.Document.InvokeScript("senddata", objects);
        }
    }
}
