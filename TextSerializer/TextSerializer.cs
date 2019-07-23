using System;
using TextSerializer.Abstract;

namespace TextSerializer
{
    public class TextSerializer : ITextSerializer
    {
        /// <summary>
        /// Разбиваем строку по сепаратору в массив
        /// </summary>
        /// <param name="row"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public string[] RowDeserialize(string row, string separator)
        {
            var array = row.Trim().Split(separator, StringSplitOptions.RemoveEmptyEntries);

            return array;
        }

        /// <summary>
        /// Разбиваем многострочный текст на строки
        /// </summary>
        /// <param name="text"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public string[][] MultirowDeserialize(string text, string separator)
        {
            //if (!text.Contains(Environment.NewLine) && !text.Contains(separator))
            //    return null;

            var rows = text.Trim().Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries);
            string[][] resultArray = new string[rows.Length][];
            for (int i = 0; i < rows.Length; i++)
            {
                var array = RowDeserialize(rows[i], separator);
                resultArray[i] = new string[array.Length];
                for (int j = 0; j < array.Length; j++)
                {
                    resultArray[i][j] = array[j];
                }
            }

            return resultArray;
        }

        /// <summary>
        /// Собираем строку из массива
        /// </summary>
        /// <param name="row"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public string RowSerialize(string[] row, string separator)
        {
            if (row == null)
                return "";

            string res = String.Join(separator, row);

            return res;
        }

        /// <summary>
        /// Собираем строки из массива в большой текст
        /// </summary>
        /// <param name="multirow"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        public string MultirowSerialize(string[][] multirow, string separator)
        {
            string[] rows = new string[multirow.Length];
            for(int i = 0; i<multirow.Length; i++)
            {
                rows[i] = RowSerialize(multirow[i], separator);
            }

            return String.Join(Environment.NewLine, rows);
        }

    }
}
