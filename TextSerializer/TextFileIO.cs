using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using TextSerializer.Abstract;

namespace TextSerializer
{
    /// <summary>
    /// Логика для доступа к файлу
    /// </summary>
    public class TextFileIO
    {
        Encoding win1251; //Кодрировка

        public TextFileIO()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            win1251 = Encoding.GetEncoding(1251);
        }

        /// <summary>
        /// Прочитать весь текст
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string ReadAllText(string path)
        {
            try
            {
                string text = System.IO.File.ReadAllText(path, win1251);
                return text;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        /// <summary>
        /// Записать вес текст
        /// </summary>
        /// <param name="text"></param>
        /// <param name="path"></param>
        public string WriteText(string text, string path)
        {
            try
            {
                System.IO.File.WriteAllText(path, text, win1251);
                return string.Empty;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
