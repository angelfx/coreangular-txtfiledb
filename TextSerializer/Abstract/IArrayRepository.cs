using System;
using System.Collections.Generic;
using System.Text;

namespace TextSerializer.Abstract
{
    public interface IArrayRepository
    {
        /// <summary>
        /// Пусть до файла
        /// </summary>
        string Path { get; set; }

        /// <summary>
        /// Первая строка столбцы
        /// </summary>
        bool FirstRowIsColumns { get; set; }

        /// <summary>
        /// Задает сепаратор
        /// </summary>
        string Separator { get; set; }

        /// <summary>
        /// Создать файл данных
        /// </summary>
        /// <returns></returns>
        bool CreateFile(string path, int colsNumber, string separator);


        /// <summary>
        /// Получить строку
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        string[] GetRow(int n);

        /// <summary>
        /// Получить все строки
        /// </summary>
        /// <returns></returns>
        string[][] GetAll();

        /// <summary>
        /// Получить столбцы
        /// </summary>
        /// <returns></returns>
        string[] GetColumns();

        /// <summary>
        /// Обновить строку
        /// </summary>
        /// <param name="row"></param>
        /// <param name="n"></param>
        void UpdateRow(string[] row, int n);

        /// <summary>
        /// Добавить новую строку
        /// </summary>
        /// <param name="row"></param>
        void AddRow(string[] row, bool first = false);
        
        /// <summary>
        /// Удалить строку
        /// </summary>
        /// <param name="n"></param>
        void RemoveRow(int n);

        /// <summary>
        /// Сохрвнить данные
        /// </summary>
        void Save();

        /// <summary>
        /// Сохранение названий столбцов
        /// </summary>
        /// <param name="columns"></param>
        void SaveColumns(string[] columns);
    }
}
