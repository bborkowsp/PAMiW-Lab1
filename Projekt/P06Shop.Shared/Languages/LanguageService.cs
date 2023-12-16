namespace P06Shop.Shared.Languages {
   public class LanguageService: ILanguageService {
      public Dictionary < string, Dictionary < string, string >> loadedLanguages = new Dictionary < string, Dictionary < string, string >> ();

      public LanguageService() {
         LoadLanguages();
      }

      public string GetLanguage(string language, string keyWord) {
         if (keyWord == null) {
            return keyWord;
         }

         if (loadedLanguages.TryGetValue(language, out
               var languageTranslations)) {
            if (languageTranslations.TryGetValue(keyWord, out
                  var translation)) {
               return translation;
            }
         }

         return keyWord;
      }

      public void LoadLanguages() {
         loadedLanguages = new Dictionary < string, Dictionary < string, string >> {
            {
               "english",
               new Dictionary < string,
               string > {
                  {
                     "WelcomeTitle",
                     "Welcome to the car dealership!"
                  },
                  {
                     "DealershipDescription",
                     "Discover a modern car dealership where innovative design meets advanced technology. Our experienced advisors will help you find the perfect vehicle, offering you a unique shopping experience in complete comfort and elegance."
                  },
                  {
                     "LoginTitle",
                     "Login"
                  },
                  {
                     "EmailLabel",
                     "Email:"
                  },
                  {
                     "PasswordLabel",
                     "Password:"
                  },
                  {
                     "LoginButton",
                     "Login In"
                  },
                  {
                     "RegisterTitle",
                     "Register"
                  },
                  {
                     "UsernameLabel",
                     "User name:"
                  },
                  {
                     "ConfirmPasswordLabel",
                     "Confirm Password:"
                  },
                  {
                     "RegisterButton",
                     "Register"
                  },
                  {
                     "SettingsTitle",
                     "Settings"
                  },
                  {
                     "ChooseLanguageLabel",
                     "Choose your language:"
                  },
                  {
                     "PolishOption",
                     "Polish"
                  },
                  {
                     "EnglishOption",
                     "English"
                  },
                  {
                     "SaveButton",
                     "Save"
                  },
                  {
                     "CreateVehicleTitle",
                     "Create Vehicle"
                  },
                  {
                     "EditVehicleTitle",
                     "Edit Vehicle"
                  },
                  {
                     "DeleteButton",
                     "Delete"
                  },
                  {
                     "LoadingVehicles",
                     "Loading vehicles..."
                  },
                  {
                     "VehiclesListTitle",
                     "Full list of vehicles"
                  },
                  {
                     "CreateNewVehicleLink",
                     "Create new vehicle"
                  },
                  {
                     "SearchButton",
                     "Search"
                  },
                  {
                     "EditButton",
                     "Edit"
                  },
                  {
                     "PreviousButton",
                     "Previous"
                  },
                  {
                     "NextButton",
                     "Next"
                  },
                  {
                     "ModelLabel",
                     "Model"
                  },
                  {
                     "FuelLabel",
                     "Fuel"
                  },
                  {
                     "UpdateButton",
                     "Update"
                  },

               }
            },
            {
               "polish",
               new Dictionary < string,
               string > {
                  {
                     "WelcomeTitle",
                     "Witamy w salonie samochodowym!"
                  },
                  {
                     "DealershipDescription",
                     "Odkryj nowoczesny salon samochodowy, gdzie innowacyjny design spotyka się z zaawansowaną technologią. Nasi doświadczeni doradcy pomogą Ci znaleźć idealny pojazd, oferując wyjątkowe doświadczenie zakupowe w pełnym komforcie i elegancji."
                  },
                  {
                     "LoginTitle",
                     "Logowanie"
                  },
                  {
                     "EmailLabel",
                     "Email:"
                  },
                  {
                     "PasswordLabel",
                     "Hasło:"
                  },
                  {
                     "LoginButton",
                     "Zaloguj"
                  },
                  {
                     "RegisterTitle",
                     "Rejestracja"
                  },
                  {
                     "UsernameLabel",
                     "Nazwa użytkownika:"
                  },
                  {
                     "ConfirmPasswordLabel",
                     "Potwierdź hasło:"
                  },
                  {
                     "RegisterButton",
                     "Zarejestruj"
                  },
                  {
                     "SettingsTitle",
                     "Ustawienia"
                  },
                  {
                     "ChooseLanguageLabel",
                     "Wybierz język:"
                  },
                  {
                     "PolishOption",
                     "Polski"
                  },
                  {
                     "EnglishOption",
                     "Angielski"
                  },
                  {
                     "SaveButton",
                     "Zapisz"
                  },
                  {
                     "CreateVehicleTitle",
                     "Utwórz Pojazd"
                  },
                  {
                     "EditVehicleTitle",
                     "Edytuj Pojazd"
                  },
                  {
                     "DeleteButton",
                     "Usuń"
                  },
                  {
                     "LoadingVehicles",
                     "Ładowanie pojazdów..."
                  },
                  {
                     "VehiclesListTitle",
                     "Pełna lista pojazdów"
                  },
                  {
                     "CreateNewVehicleLink",
                     "Utwórz nowy pojazd"
                  },
                  {
                     "SearchButton",
                     "Szukaj"
                  },
                  {
                     "EditButton",
                     "Edytuj"
                  },
                  {
                     "PreviousButton",
                     "Poprzedni"
                  },
                  {
                     "NextButton",
                     "Następny"
                  },
                  {
                     "ModelLabel",
                     "Model"
                  },
                  {
                     "FuelLabel",
                     "Paliwo"
                  },
                  {
                     "UpdateButton",
                     "Aktualizuj"
                  },
               }
            }
         };
      }
   }
}