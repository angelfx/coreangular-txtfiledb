using System.IO;
using System.Text.RegularExpressions;

namespace ArrayClient.Models
{
    public class FileSettings
    {
        private readonly SeparatorList _separators = new SeparatorList();

        private string _fileName;

        public string FileName
        {
            get
            {
                return _fileName;
            }
            set
            {
                //string illegal = "\"M\"\\a/ry/ h**ad:>> a\\/:*?\"| li*tt|le|| la\"mb.?";
                string regexSearch = new string(Path.GetInvalidFileNameChars()) + new string(Path.GetInvalidPathChars());
                Regex r = new Regex(string.Format("[{0}]", Regex.Escape(regexSearch)));
                _fileName = r.Replace(value, "");

                //_fileName = value; }
            }
        }

        public string FilePath { get; set; }

        public bool FirstRowIsColumns { get; set; }

        public int ColsNumber { get; set; }

        public string WorkingDir { get; set; }

        public string Separator
        {
            get
            {
                return _separators.Separator;
            }
            set
            {
                _separators.Separator = value;
            }
        }

        public string SeparatorValue
        {
            get
            {
                return _separators.SeparatorValue;
            }
        }
    }
}
