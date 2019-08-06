using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TextDbApi.Models
{
    public class RowModel
    {
        public RowModel()
        {
            FileParameters = new FileSettings();
        }

        public FileSettings FileParameters { get; set; }

        public int IndexRow { get; set; }

        public string[] Row { get; set; }
    }
}
