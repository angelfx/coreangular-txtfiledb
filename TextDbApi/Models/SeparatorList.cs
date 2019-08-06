using System.Collections.Generic;
using System.Collections;
using System.Linq;

namespace TextDbApi.Models
{
    /// <summary>
    /// Используется для разрешения разделителя, который выбирается из списка
    /// Пока разделители заданы во фронте и бэке. В дальнейшем необходимо сделать в качестве источника бэк
    /// </summary>
    public class SeparatorList : IEnumerable<Separator>
    {
        private readonly List<Separator> _separators = new List<Separator>();

        private string _currentSeprator = "tab";

        public SeparatorList()
        {
            _separators.Add(new Separator { Title = "space", Value = " " });
            _separators.Add(new Separator { Title = "tab", Value = "\t" });
            _separators.Add(new Separator { Title = "dotcomma", Value = ";" });
        }

        public Separator this[int index]
        {
            get
            {
                return _separators[index];
            }
        }

        public IEnumerator<Separator> GetEnumerator()
        {
            return _separators.GetEnumerator();
        }

        public string Separator
        {
            get
            {
                return _currentSeprator;
            }
            set
            {
                _currentSeprator = value;
            }
        }

        public string SeparatorValue
        {
            get
            {
                if (_separators.Any(x => x.Title == _currentSeprator))
                    return _separators.First(x => x.Title == _currentSeprator).Value;
                else
                    return _currentSeprator;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
    }
}
