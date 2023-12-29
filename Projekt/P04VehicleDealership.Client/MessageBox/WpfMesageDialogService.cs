using P06VehicleDealership.Shared.MessageBox;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P04VehicleDealership.Client.MessageBox
{
    class WpfMesageDialogService : IMessageDialogService
    {
        public void ShowMessage(string message)
        {
            System.Windows.MessageBox.Show(message);
        }
    }
}
