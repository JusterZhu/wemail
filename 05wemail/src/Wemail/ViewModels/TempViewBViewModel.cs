using Prism.Regions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Wemail.ViewModels
{
    public class TempViewBViewModel : IConfirmNavigationRequest
    {
        public void ConfirmNavigationRequest(NavigationContext navigationContext, Action<bool> continuationCallback)
        {
            bool result = true;

            // this is demo code only and not suitable for production. It is generally
            // poor practice to reference your UI in the view model. Use the Prism
            // IDialogService to help with this.
            if (MessageBox.Show("Do you to navigate?", "Navigate?", MessageBoxButton.YesNo) == MessageBoxResult.No)
                result = false;

            continuationCallback(result);
        }

        public bool IsNavigationTarget(NavigationContext navigationContext)
        {
            //是否创建新示例。为true的时候表示不创建新示例，页面还是之前的；如果为false，则创建新的页面。
            return true;
        }

        public void OnNavigatedFrom(NavigationContext navigationContext)
        {
            //导航离开当前页面前。
        }

        public void OnNavigatedTo(NavigationContext navigationContext)
        {
            //导航到当前页面前, 此处可以传递过来的参数以及是否允许导航等动作的控制。
        }
    }
}
