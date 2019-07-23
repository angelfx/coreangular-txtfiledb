using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ArrayClient.Models
{
    public class DataModel
    {
        public DataModel()
        {
            FileParameters = new FileSettings();
        }

        public FileSettings FileParameters { get; set; }

        public string[] Columns { get; set; }

        public string[][] Data { get; set; }
    }
}
