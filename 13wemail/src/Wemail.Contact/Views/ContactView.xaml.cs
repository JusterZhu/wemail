using System.Windows.Controls;
using Wemail.Common.MVVM;
using Wemail.Contact.ViewModels;

namespace Wemail.Contact.Views
{
    /// <summary>
    /// Interaction logic for ViewA.xaml
    /// </summary>
    public partial class ContactView : UserControl, IView
    {
        public ContactView()
        {
            InitializeComponent();
            (DataContext as ContactViewModel).View = this;
        }

        public bool Launch()
        {
            var view = new NewContactView();
            view.ShowDialog();
            return true;
        }

        public bool Shutdown()
        {
            return true;
        }
    }
}
