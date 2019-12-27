using System;
using System.IO;
using System.Text;

namespace com.pmp.test
{
    class WordConvert
    {
        private string appRoot = AppDomain.CurrentDomain.BaseDirectory;
        private string rootPath = @"C:\Users\Colin\Desktop\log\";
        private string wordFilePath = @"E:\bak-desktop\LED多媒体信息发布平台方案.docx";

        public void Convert()
        {
            var path = GetPathByDocToHTML(wordFilePath);
            Console.WriteLine(path);
        }


        private string GetPathByDocToHTML(string sourceFilePath)
        {
            if (string.IsNullOrEmpty(sourceFilePath))
            {
                return "0";//没有文件
            }
            var flagName = DateTime.Now.ToOADate();

            Microsoft.Office.Interop.Word.ApplicationClass word = new Microsoft.Office.Interop.Word.ApplicationClass();
            Type wordType = word.GetType();
            Microsoft.Office.Interop.Word.Documents docs = word.Documents;

            // 打开文件  
            Type docsType = docs.GetType();
            var doc = (Microsoft.Office.Interop.Word.Document)docsType.InvokeMember("Open", System.Reflection.BindingFlags.InvokeMethod, null, docs, new Object[] { sourceFilePath, true, true });

            // 转换格式，另存为html  
            Type docType = doc.GetType();

            string newDir = $@"{rootPath}{flagName}\";
            // 判断指定目录下是否存在文件夹，如果不存在，则创建 
            if (!Directory.Exists(newDir))
            {
                // 创建文件夹 
                Directory.CreateDirectory(newDir);
            }

            //被转换的html文档保存的位置 
            var saveFileName = $"{newDir}{flagName}.html";

            /*下面是Microsoft Word 9 Object Library的写法，如果是10，可能写成： 
              * docType.InvokeMember("SaveAs", System.Reflection.BindingFlags.InvokeMethod, null, doc, new object[]{saveFileName, Word.WdSaveFormat.wdFormatFilteredHTML}); 
              * 其它格式： 
              * wdFormatHTML 
              * wdFormatDocument 
              * wdFormatDOSText 
              * wdFormatDOSTextLineBreaks 
              * wdFormatEncodedText 
              * wdFormatRTF 
              * wdFormatTemplate 
              * wdFormatText 
              * wdFormatTextLineBreaks 
              * wdFormatUnicodeText 
            */
            docType.InvokeMember("SaveAs", System.Reflection.BindingFlags.InvokeMethod, null, doc, new object[] { saveFileName, Microsoft.Office.Interop.Word.WdSaveFormat.wdFormatFilteredHTML });

            //关闭文档  
            docType.InvokeMember("Close", System.Reflection.BindingFlags.InvokeMethod, null, doc, new object[] { null, null, null });
            // 退出 Word  
            wordType.InvokeMember("Quit", System.Reflection.BindingFlags.InvokeMethod, null, word, null);

            //转化HTML页面统一编码格式
            TransHTMLEncoding(saveFileName);

            return $"{flagName}\\{flagName}.html";
        }
        private void TransHTMLEncoding(string strFilePath)
        {
            try
            {
                System.IO.StreamReader reader = new System.IO.StreamReader(strFilePath, Encoding.GetEncoding(0));
                string html = reader.ReadToEnd();
                reader.Close();
                html = System.Text.RegularExpressions.Regex.Replace(html, @"<meta[^>]*>", "<meta http-equiv=Content-Type content='text/html; charset=gb2312'>", System.Text.RegularExpressions.RegexOptions.IgnoreCase);
                System.IO.StreamWriter writer = new System.IO.StreamWriter(strFilePath, false, Encoding.Default);
                writer.Write(html);
                writer.Close();
            }
            catch (Exception ex)
            {
                // Page.ClientScript.RegisterStartupScript(Page.ClientScript.GetType(), "myscript", "<script>alert('" + ex.Message + "')</script>");
            }
        }

    }
}
