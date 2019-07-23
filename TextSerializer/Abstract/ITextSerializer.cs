using System;
using System.Collections.Generic;
using System.Reflection;

namespace TextSerializer.Abstract
{
    /// <summary>
    /// ЛОгика для разбора текста с массив и обратно
    /// </summary>
    public interface ITextSerializer
    {
        /// <summary>
        /// Разбираем строку
        /// </summary>
        /// <param name="row"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        string[] RowDeserialize(string row, string separator);

        /// <summary>
        /// Разбираем строки
        /// </summary>
        /// <param name="text"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        string[][] MultirowDeserialize(string text, string separator);

        /// <summary>
        /// Собираем массив в строку
        /// </summary>
        /// <param name="row"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        string RowSerialize(string[] row, string separator);

        /// <summary>
        /// Собираем массив в строку
        /// </summary>
        /// <param name="multirow"></param>
        /// <param name="separator"></param>
        /// <returns></returns>
        string MultirowSerialize(string[][] multirow, string separator);
    }
}
