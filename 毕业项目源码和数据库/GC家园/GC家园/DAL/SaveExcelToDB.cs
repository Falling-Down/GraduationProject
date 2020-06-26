using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models;

namespace DAL
{
    public class SaveExcelToDB
    {
        //静态方法只能调用静态属性，所以必须把上下文对象定义为静态的
        GCHomeEntitiesDB stuSystemDB = new GCHomeEntitiesDB();
        /// <summary>
        /// 将DataTable中数据写入数据库中
        /// </summary>
        /// <param name="dt"></param>
        /// <param name="TableName">表名</param>
        /// <returns></returns>
        public string InsertDataToDB(DataTable dt)
        {
            int ret = 0;
            //datatable中是否有行数据
            if (dt == null || dt.Rows.Count == 0)
            {
                return "Excel无内容";
            }

            try
            {
                //循环datatable中的数据行
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    //定义对象，接收第i行的0-4列的数据
                    Student stu = new Student()
                    {
                        StuNumber = dt.Rows[i][0].ToString(),
                        StuName = dt.Rows[i][1].ToString(),
                        StuCount = dt.Rows[i][2].ToString(),
                        StuPwd = dt.Rows[i][3].ToString(),
                        StuSex = int.Parse(dt.Rows[i][4].ToString()),
                        StuAge = int.Parse(dt.Rows[i][5].ToString()),
                        OccupancyStatus = 0,
                        IsDelete = 0
                    };
                    //添加
                    stuSystemDB.Student.Add(stu);
                    //保存
                    ret = stuSystemDB.SaveChanges();
                    if (ret <= 0)
                    {
                        return "Excel导入失败，请检查匹配";
                    }
                }
            }
            catch (Exception e)
            {

                string strOuput = string.Format("向数据库中写数据失败,错误信息:{0},异常{1}\n", e.Message, e.InnerException);
                return strOuput;
            }

            return "Excel导入成功";
        }

    }
}
