using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TextSerializer;
using TextDbApi.Models;
using TextSerializer.Abstract;

namespace TextDbApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ArrayDataController : ControllerBase
    {
        private IArrayRepository _repo;

        private FileSettings _fileSettings = new FileSettings();

        public ArrayDataController(IArrayRepository repository)
        {
            _repo = repository;
            //_repo.Path = "Data\\f_tn_table_supplier.txt";
            _fileSettings.WorkingDir = "Data";
        }

        [HttpGet]
        public FileSettings Index()
        {
            var model = _fileSettings;
            model.FileName = "f_tn_table_supplier.txt";
            if (System.IO.File.Exists($"{_fileSettings.WorkingDir}\\f_tn_table_supplier.txt"))
            {

                model.FilePath = $"{_fileSettings.WorkingDir}\\f_tn_table_supplier.txt";
            }
            model.FirstRowIsColumns = true;
            model.Separator = "tab";

            return model;
        }

        [HttpGet]
        public DataModel GetArray(string filePath, bool firstRowIsColumns, string separator)
        {
            var model = new DataModel();
            try
            {
                FileSettings fileSet = _fileSettings;
                fileSet.Separator = separator;
                _repo.Path = filePath;
                _repo.FirstRowIsColumns = firstRowIsColumns;
                _repo.Separator = fileSet.SeparatorValue;

                model.Data = _repo.GetAll();
                model.Columns = _repo.GetColumns();
            }
            catch (Exception ex)
            {
                model.FileParameters.FileName = ex.Message; //Заглушка
                //Логирование или вывод пользователю. Но это уже в продакшене
            }
            return model;
        }

        [HttpPut]
        public void SaveColumns(RowModel model)
        {
            try
            {
                _repo.Path = model.FileParameters.FilePath;
                _repo.Separator = model.FileParameters.SeparatorValue;
                _repo.FirstRowIsColumns = model.FileParameters.FirstRowIsColumns;

                _repo.SaveColumns(model.Row);
            }
            catch (Exception ex)
            {
                //Логирование или вывод пользователю. Но это уже в продакшене
            }
        }

        [HttpPut]
        public void UpdateRow(RowModel model)
        {
            try
            {
                _repo.Path = model.FileParameters.FilePath;
                _repo.Separator = model.FileParameters.SeparatorValue;
                _repo.FirstRowIsColumns = model.FileParameters.FirstRowIsColumns;

                _repo.UpdateRow(model.Row, model.IndexRow);
                _repo.Save();
            }
            catch (Exception ex)
            {
                //Логирование или вывод пользователю. Но это уже в продакшене
            }
        }

        [HttpPut]
        public void AddRow(RowModel model)
        {
            try
            {
                _repo.Path = model.FileParameters.FilePath;
                _repo.Separator = model.FileParameters.SeparatorValue;
                _repo.FirstRowIsColumns = model.FileParameters.FirstRowIsColumns;

                _repo.AddRow(model.Row);
                _repo.Save();
            }
            catch (Exception ex)
            {
                //Логирование или вывод пользователю. Но это уже в продакшене
            }
        }

        [HttpPost]
        public void RemoveRow(RowModel model)
        {
            try
            {
                _repo.Path = model.FileParameters.FilePath;
                _repo.Separator = model.FileParameters.SeparatorValue;
                _repo.FirstRowIsColumns = model.FileParameters.FirstRowIsColumns;

                _repo.RemoveRow(model.IndexRow);
                _repo.Save();
            }
            catch (Exception ex)
            {
                //Логирование или вывод пользователю. Но это уже в продакшене
            }
        }
    }
}