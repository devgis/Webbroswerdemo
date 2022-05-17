using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using MySql.Data.MySqlClient;
using System.IO;

namespace DBUtility
{
    public class MySQLHelper
    {
        private MySqlConnection StyleConnection;

        #region 构造方法
        private MySQLHelper()
        {
            #region 初始化连接信息
            string strConStr = System.Configuration.ConfigurationManager.AppSettings["MySQL"].ToString();
            StyleConnection = new MySqlConnection(strConStr);
            #endregion
        }
        #endregion

        #region 单例
        private static MySQLHelper _instance = null;

        /// <summary>
        /// PGHelper的实例
        /// </summary>
        public static MySQLHelper Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new MySQLHelper();
                }
                return _instance;
            }
        }
        #endregion

        #region 公有方法
        /// <summary>
        /// 从型号基础库获取数据
        /// </summary>
        /// <param name="SQL">查询的SQL语句</param>
        /// <returns>查询的结果</returns>
        public DataTable GetDataTable(String SQL)
        {
            MySqlCommand cmd = new MySqlCommand(SQL, StyleConnection);
            MySqlDataAdapter da = new MySqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }

        /// <summary>
        /// 执行单个SQL
        /// </summary>
        /// <param name="Sql">需要执行的SQL</param>
        /// <returns>执行结果</returns>
        public bool ExecuteSql(String Sql)
        {
            StyleConnection.Open();
            try
            {
                MySqlCommand oc = new MySqlCommand(Sql, StyleConnection);
                if (oc.ExecuteNonQuery() > 0)
                {
                    return true;
                }
            }
            catch
            {
            }
            finally
            {
                StyleConnection.Close();
            }
            return false;
        }
        #endregion

    }
}
