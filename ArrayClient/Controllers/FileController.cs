using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ArrayClient.Models;
using TextSerializer;
using TextSerializer.Abstract;

namespace ArrayClient.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private IArrayRepository _repo;

        private FileSettings _fileSettings = new FileSettings();

        public FileController(IArrayRepository repository)
        {
            _repo = repository;
            _fileSettings.WorkingDir = "Data";
        }

        [HttpGet]
        public IEnumerable<string> GetFiles()
        {
            //string[] files = Directory.GetFiles("Data").Select(s => Path.GetFileName(s)).ToArray();
            string[] files = Directory.GetFiles("Data");

            return files;
        }

        [HttpPost]
        public void CreateFile(FileSettings fileSettings)
        {
            _repo.Path = $"{_fileSettings.WorkingDir}\\{fileSettings.FileName}";
            _repo.CreateFile($"{_fileSettings.WorkingDir}\\{fileSettings.FileName}", fileSettings.ColsNumber, fileSettings.SeparatorValue);
        }
    }
}
