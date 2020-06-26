using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class ImportExcel
    {
        public DataTable GetExcelDataTable(string filePath)
        {
            //定义了操作excel的多种方法和属性
            IWorkbook Workbook;
            DataTable table = new DataTable();
            try
            {
                //使用文件流，读取文件
                using (FileStream fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    //XSSFWorkbook 适用XLSX格式，HSSFWorkbook 适用XLS格式
                    string fileExt = Path.GetExtension(filePath).ToLower();
                    if (fileExt == ".xls")
                    {
                        Workbook = new HSSFWorkbook(fileStream);
                    }
                    else if (fileExt == ".xlsx")
                    {
                        Workbook = new XSSFWorkbook(fileStream);
                    }
                    else
                    {
                        Workbook = null;
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            //定位在第一个sheet
            ISheet sheet = Workbook.GetSheetAt(0);
            //第一行为标题行
            IRow headerRow = sheet.GetRow(0);
            //取标题行列数
            int cellCount = headerRow.LastCellNum;
            //取第一个sheet页中最后数据行数（及所有数据行）
            int rowCount = sheet.LastRowNum;

            //循环添加标题列
            for (int i = headerRow.FirstCellNum; i < cellCount; i++)
            {
                //StringCellValue取单元个中的值
                DataColumn column = new DataColumn(headerRow.GetCell(i).StringCellValue);
                //将添加的所有标题列添加datatable中
                table.Columns.Add(column);
            }

            //数据，FirstRowNum是第一行，为标题行，需+1排除标题行，剩下的行即为数据行
            for (int i = (sheet.FirstRowNum + 1); i <= rowCount; i++)
            {
                //获得sheet页的每行记录
                IRow row = sheet.GetRow(i);
                //定义datatable存储行数据的对象
                DataRow dataRow = table.NewRow();
                if (row != null)
                {
                    //如果行数据不为空，数据从第一列开始读取，并写入到datatable的行对象中
                    for (int j = row.FirstCellNum; j < cellCount; j++)
                    {
                        if (row.GetCell(j) != null)
                        {
                            //StringCellValue 列中数据值转为string类型
                            dataRow[j] = row.GetCell(j).ToString();
                        }
                    }
                }
                //再将整行数据插入到datatable中
                table.Rows.Add(dataRow);
            }

            return table;
        }
    }
}
