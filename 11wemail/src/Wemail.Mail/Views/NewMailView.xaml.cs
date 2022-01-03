using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Wemail.Common.MVVM;
using Wemail.Mail.ViewModels;

namespace Wemail.Mail.Views
{
    /// <summary>
    /// Interaction logic for NewMailView.xaml
    /// </summary>
    public partial class NewMailView : Window, IView
    {
        public NewMailView()
        {
            InitializeComponent();
            (DataContext as NewMailViewModel).View = this;
        }

        public bool Launch()
        {
            return false;
        }

        public bool Shutdown()
        {
            Close();
            return true;
        }
    }
}
