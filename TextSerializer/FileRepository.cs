using System.IO;
using System.Linq;
using TextSerializer.Abstract;

namespace TextSerializer
{
    /// <summary>
    /// Репозиторий для работы с данными в файле
    /// </summary>
    public class FileRepository : IArrayRepository
    {
        /// <summary>
        /// Сериализатор
        /// </summary>
        private ITextSerializer _serializer;

        /// <summary>
        /// Файловый процессор
        /// </summary>
        private TextFileIO _fileIO;

        /// <summary>
        /// Путь до файла
        /// </summary>
        private string _path;

        /// <summary>
        /// Разделитель
        /// </summary>
        private string _separator;

        /// <summary>
        /// Массив
        /// </summary>
        private string[][] _array;

        /// <summary>
        /// Столбцы
        /// </summary>
        private string[] _columns;

        /// <summary>
        /// Первая строка столбцы
        /// </summary>
        public bool FirstRowIsColumns { get; set; }

        /// <summary>
        /// Количество строк
        /// </summary>
        public int Length
        {
            get
            {
                if (_array == null || _array.Length == 0)
                    return 0;
                else
                    return _array.Length;
            }
        }

        /// <summary>
        /// Получение столбцов
        /// </summary>
        public string[] Columns
        {
            get
            {
                return this._columns;
            }
        }

        /// <summary>
        /// Получение массива и его создание
        /// </summary>
        public string[][] Array
        {
            get
            {
                ReadArray();
                return this._array;
            }
            set
            {

                this._array = value;
            }
        }

        /// <summary>
        /// Задание и получение разделителя
        /// </summary>
        public string Separator
        {
            get { return this._separator; }
            set { this._separator = value; }
        }

        /// <summary>
        /// Путь
        /// </summary>
        public string Path
        {
            get { return this._path; }
            set { this._path = value; }
        }

        public FileRepository()
        {
            this._fileIO = new TextFileIO();
            this._serializer = new TextSerializer();
        }

        /// <summary>
        /// Создание файла
        /// </summary>
        /// <returns></returns>
        public bool CreateFile(string path, int colsNumber, string separator)
        {
            _path = path;
            Separator = separator;
            _array = new string[1][];
            FirstRowIsColumns = false;
            if (!File.Exists(_path))
            {
                using (var fStream = File.Create(_path))
                {

                }

                _array[0] = new string[colsNumber];
                for (int i = 0; i < colsNumber; i++)
                {
                    _array[0][i] = $"col{i}";
                }

                Save();


                return true;
            }
            return false;
        }

        /// <summary>
        /// Получить столбцы
        /// </summary>
        /// <returns></returns>
        public string[] GetColumns()
        {
            return Columns;
        }

        /// <summary>
        /// Получить строку по номеру
        /// </summary>
        /// <param name="n"></param>
        /// <returns></returns>
        public string[] GetRow(int n)
        {
            ReadArray();
            if (n < Length)
            {
                return _array[n];
            }
            return null;
        }

        /// <summary>
        /// Получить все данные из файла
        /// </summary>
        /// <returns></returns>
        public string[][] GetAll()
        {
            ReadArray();
            return _array;
        }

        /// <summary>
        /// Обновить строку
        /// </summary>
        /// <param name="row"></param>
        /// <param name="n"></param>
        public void UpdateRow(string[] row, int n)
        {
            if (Length == 0)
                ReadArray();

            if (Length > 0 && n < Length)
                _array[n] = row;
        }

        /// <summary>
        /// Добавить новую строку
        /// </summary>
        /// <param name="row"></param>
        public void AddRow(string[] row, bool first = false)
        {
            if (Length == 0)
                ReadArray();

            if (Length == 0) //Если файл пустой, то создаем первую строку
            {
                _array = new string[1][];
                _array[0] = row;
            }
            else
            {
                string[][] newArray = new string[_array.Length + 1][]; //Создаем новый массив размерности на один больше
                int i = 0;
                if (first) //Если добавляем в начало массива
                {
                    i = 1; //Сдвиг
                    newArray[0] = row;
                }
                else //Иначе в конец
                    newArray[_array.Length] = row;

                //Копируем массив
                _array.CopyTo(newArray, i);

                _array = newArray;
            }
        }

        /// <summary>
        /// Удалить строку
        /// </summary>
        /// <param name="n"></param>
        public void RemoveRow(int n)
        {
            if (Length == 0)
                ReadArray();

            if (Length > 0)
            {
                if (Length == 1)
                {
                    _array = null; //Обнуляем массив
                    return;
                }

                string[][] newArray = new string[_array.Length - 1][]; //Создаем новый массив размерности на один меньше

                //Копируем массив, пропуская удаляемую строку
                newArray = _array.Select((s, i) => new { s, i })
                                .Where(t => t.i != n)
                                .Select(t => t.s).ToArray();
                _array = newArray;
            }
        }

        /// <summary>
        /// Сохранить со стобцами
        /// </summary>
        private void SaveWithColumns()
        {
            FirstRowIsColumns = true;
            Save();
            FirstRowIsColumns = false;
        }

        /// <summary>
        /// Сохранить столбцы
        /// </summary>
        /// <param name="columns"></param>
        public void SaveColumns(string[] columns)
        {
            if (Length == 0)
                ReadArray();

            _columns = columns;
            SaveWithColumns();
        }

        /// <summary>
        /// Сохрвнить данные
        /// </summary>
        /// <param name="colSave">Задает принудительное сохранения названия стобцов. 
        /// Используется в том случае, если изначально в файле не было столбцов</param>
        public void Save()
        {
            if (Length > 0 || _columns != null)
            {
                if (string.IsNullOrEmpty(_path))
                    throw new System.Exception("Path is empty");
                if (string.IsNullOrEmpty(_separator))
                    throw new System.Exception("Separator is empty");
                if (FirstRowIsColumns)
                {
                    AddRow(Columns, true);
                }
                _fileIO.WriteText(this._serializer.MultirowSerialize(this._array, this._separator), this._path);
                ReadArray();
            }
        }

        /// <summary>
        /// Прочитать данные
        /// </summary>
        private void ReadArray()
        {
            if (string.IsNullOrEmpty(_path))
                throw new System.Exception("Path is empty");
            if (string.IsNullOrEmpty(_separator))
                throw new System.Exception("Separator is empty");
            this._array = _serializer.MultirowDeserialize(this._fileIO.ReadAllText(this._path), this._separator);
            if (this.FirstRowIsColumns)
            {
                _columns = _array[0];
                RemoveRow(0);
            }
            else
            {
                var cnt = _array.Max(x => x.Length);
                _columns = new string[cnt];
                for (int i = 0; i < cnt; i++)
                {
                    _columns[i] = $"col{i}";
                }
            }
            NormilizeArray();
        }

        /// <summary>
        /// Нормальзация строк массива
        /// </summary>
        private void NormilizeArray()
        {
            if (_array != null && _array.Any(x => x.Length < _columns.Length)) //Если есть строки, у которых дина меньше количества столбцов
            {
                for (int ii = 0; ii < _array.Length; ii++) //Идем по строкам
                {
                    if (_array[ii].Length < _columns.Length) //Если длина отличается от количества стобцов
                    {
                        var newRow = new string[_columns.Length]; //Создаем новую строку с длиной по количеству столбцов
                        int i = 0;
                        for (; i < _array[ii].Length; i++) //Заполняем строки
                        {
                            newRow[i] = _array[ii][i];
                        }
                        int diff = _columns.Length - i;
                        if (diff > 1) //Если есть различие, то забиваем постумы данными
                        {
                            diff--;
                            for (; diff < _columns.Length; diff++)
                            {
                                newRow[diff] = "";
                            }
                        }
                        _array[ii] = newRow;
                    }
                }
            }
        }
    }
}
