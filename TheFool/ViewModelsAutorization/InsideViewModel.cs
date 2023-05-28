using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheFool.Tools;

namespace TheFool.ViewModelsAutorization
{
    internal class InsideViewModel : BaseViewModel
    {
        private object _currentPage;
        public object CurrentPage
        {
            get { return _currentPage; }
            set
            {
                _currentPage = value;
                SignalChanged("CurrentPage");
            }
        }

    }
}
