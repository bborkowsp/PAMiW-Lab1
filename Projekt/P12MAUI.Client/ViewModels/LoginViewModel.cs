using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using P12MAUI.Client;
using P12MAUI.Client.ViewModels;
using P06Shop.Shared.Auth;
using P06Shop.Shared.Services.AuthService;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using static System.Reflection.Metadata.BlobBuilder;
using System.Reflection;
using P06Library.Shared.Services.AuthService;
using Microsoft.AspNetCore.Components.Authorization;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace P12MAUI.Client.ViewModels
{
   
 public partial class LoginViewModel : ObservableObject
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IAuthService _authService;
        private readonly IMessageDialogService _mesageDialogService;

        private bool IsLoadingWebView;
        private bool IsLogin = false;

        public LoginViewModel(IServiceProvider serviceProvider, IAuthService authService, ITranslationsManager translationsManager,
            IMessageDialogService wpfMesageDialogService, AuthenticationStateProvider authenticationStateProvider)
        {
            UserRegisterDTO = new UserRegisterDTO();
            _serviceProvider = serviceProvider;
            _authService = authService;
            _translationsManager = translationsManager;
            _mesageDialogService = wpfMesageDialogService;
        }

        [ObservableProperty]
        private UserRegisterDTO userRegisterDTO;


    }
}
