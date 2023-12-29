using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using P04WeatherForecastAPI.Client.Commands;
using P06Shop.Shared.MessageBox;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace P04WeatherForecastAPI.Client.ViewModels
{
    // przekazywanie wartosci do innego formularza 
    public partial class LoggedInViewModel : ObservableObject
    {
    private readonly IServiceProvider _serviceProvider;
    private readonly IMessageDialogService _messageDialogService;

           private string _message;

        public string Message
        {
            get { return _message; }
            set
            {
                if (_message != value)
                {
                    _message = value;
                    OnPropertyChanged(nameof(Message));
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public void setMessage(string _message)
        {
            Message = _message;
        }

        public LoggedInViewModel(

            IServiceProvider serviceProvider, IMessageDialogService messageDialogService)
        {

            _serviceProvider = serviceProvider;

            _messageDialogService = messageDialogService;

        }

        [RelayCommand]
        public void CloseWindow()
        {
            Window currentWindow = Application.Current.Windows.OfType<LoggedInView>().FirstOrDefault();
            currentWindow?.Close();

        }
    }
}