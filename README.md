# Projekty 1-8 
Linki do nagrań na Youtube:  
Projekt 1: https://youtu.be/TJevwzwHb_Y  
Projekt 2: https://youtu.be/5Wmrv9hBwks  
Projekt 3: https://youtu.be/dooIFopIZ6g  
Projekt 4: https://youtu.be/lTgUGtSlB9Y  
Projekt 5: https://youtu.be/buRsKujNYn0  
Projekt 6: https://youtu.be/oz8q7JuyQSo  
Projekt 7: https://youtu.be/-SHStBwj-xo  
Projekt 8: https://youtu.be/dOkMk_DCObU  
# Projekt końcowy
## Zrealizowane zadania 
## Hosting API na publicznym serwerze
API zostało przeniesione na hosting w chmurze Google Cloud. Do zahostowania API wykorzystałem usługę Google App Engine.
![image](https://github.com/bborkowsp/PAMiW-Lab/assets/95755487/abafd0e9-d653-4704-a826-69525b63cfac)

Do obsługi bazy danych została utworzona instancja SQL Server'a.

![image](https://github.com/bborkowsp/PAMiW-Lab/assets/95755487/90a6e96e-adcb-44ac-ae73-95effbd1546f)

Na platformie Google Cloud App Engine została wdrożona aplikacja zgodnie z konfiguracją określoną w pliku app.yaml
```yaml
runtime: aspnetcore
env: flex

runtime_config:
  operating_system: ubuntu22

# This sample incurs costs to run on the App Engine flexible environment. 
# The settings below are to reduce costs during testing and are not appropriate
# for production use. For more information, see:
# https://cloud.google.com/appengine/docs/flexible/dotnet/configuring-your-app-with-app-yaml
manual_scaling:
  instances: 1
resources:
  cpu: 1
  memory_gb: 0.5
  disk_size_gb: 10

#env_variables:
#  ASPNETCORE_ENVIRONMENT: production
```
Przykład poprawnego działania zahostowanego API.
```powershell
PS > curl pamiw-409617.lm.r.appspot.com/api/Vehicle
{"data":[{"id":1,"model":"Colorado","fuel":"Diesel"},{"id":2,"model":"Focus","fuel":"Electric"},...
```
### Kompatybilność Aplikacji Mobilnej
Aplikacja mobilna napisana w framework'u MAUI poprawnie uruchamia się na urządzeniu z systemem Android.
![image](https://github.com/bborkowsp/PAMiW-Lab/assets/95755487/b663645f-641c-4fad-9ad9-8a6a3948817c)

### Udoskonalenie Interfejsu
#### Aplikacja mobilna
Zostały zastosowane jednolie kolory, czcionki oraz przyciski w całym ekosystemie.
Długie operacja sygnalizowane są ikoną ładowania.
![image](https://github.com/bborkowsp/PAMiW-Lab/assets/95755487/59d08a73-43bf-4e11-8451-6f02e694cfe0)

Dodatkowo, użytkownik jest informowany o wszelkich błędach jakie wystąpiły, lub o poprawnym zakończeniu operacji.
![image](https://github.com/bborkowsp/PAMiW-Lab/assets/95755487/33eb69b7-aa37-43dc-8c4f-ee4a4e79d289)
![image](https://github.com/bborkowsp/PAMiW-Lab/assets/95755487/d32c76c2-fd1a-4050-a836-83180c4b26c6)
![image](https://github.com/bborkowsp/PAMiW-Lab/assets/95755487/28a96990-0c41-453f-a1c7-ef979ccaf06b)

#### Aplikacja webowa
Zostały zastosowane jednolie kolory, czcionki oraz przyciski w całym ekosystemie.
![image](https://github.com/bborkowsp/PAMiW-Lab/assets/95755487/fb3a3b8b-2cd2-4e18-86b0-88800ae4d5c5)

Długie operacja sygnalizowane są ikoną ładowania.
![image](https://github.com/bborkowsp/PAMiW-Lab/assets/95755487/f43e19b1-b700-4f58-82a8-028b649c65c9)

Dodatkowo, użytkownik jest informowany o wszelkich błędach jakie wystąpiły, lub o poprawnym zakończeniu operacji.
![image](https://github.com/bborkowsp/PAMiW-Lab/assets/95755487/8d4eab66-4016-41f5-bdc0-d04842ec6349)

### Ustawienia użytkownika
#### Aplikacja mobilna
Dodana jest obsluga języka angielskiego oraz polskiego.

![image](https://github.com/bborkowsp/PAMiW-Lab/assets/95755487/bf4cd6b9-2732-42f3-bf4f-30dad45054c4)
![image](https://github.com/bborkowsp/PAMiW-Lab/assets/95755487/882e5598-0701-4939-a21c-6191a4fe226a)
![image](https://github.com/bborkowsp/PAMiW-Lab/assets/95755487/1d9cf54a-5685-41ea-9099-bd0c63ff7be4)

Użytkownik ma również możliwość wyboru między trybem ciemnym a jasnym

![image](https://github.com/bborkowsp/PAMiW-Lab/assets/95755487/4bf19527-4a23-479f-a8ff-1bd96112cca2)

#### Aplikacja webowa
Dodana jest obsluga języka angielskiego oraz polskiego.
![image](https://github.com/bborkowsp/PAMiW-Lab/assets/95755487/748d887d-e86d-486d-8653-cd77a0b2a0bc)
![image](https://github.com/bborkowsp/PAMiW-Lab/assets/95755487/ffcc6541-3fb7-4a2e-baca-6a01e0a2a937)

Użytkownik ma również możliwość wyboru między trybem ciemnym a jasnym
![image](https://github.com/bborkowsp/PAMiW-Lab/assets/95755487/6354be4e-0c62-48d2-b395-793fccb5da93)

### Warstwa Serwisów Dla Aplikacji Webowej i Mobilnej
Zaimplementowana została warstwa serwisów wspólnych dla aplikacji mobilnej oraz webowej. Poprzez tę warstwę kierowane są zapytania do zahostowanego API. W tej warstwie znajduje się również klasa LanguageService odpowiedzialna za przechowywanie tekstów w dwóch językach oraz udostępnianie ich klientom.

![image](https://github.com/bborkowsp/PAMiW-Lab/assets/95755487/4155a283-f05f-41be-91b8-9984fa974232)

## Podsumowanie
### Zrealizowane zadania 
#### 1. Hosting API na publicznym serwerze
#### 2. Kompatybilność aplikacji mobilnej
#### 3. Udoskonalenie interfejsu aplikacji mobilnej i webowej
#### 4. Ustawienia użytkownika (wielojęzyczność, tryb jasny/ciemny)
#### 5. Warstwa serwisów dla aplikacji webowej i mobilnej

### Niezrealizowane zadania
#### 1. Opcje logowania/rejestracji poprzez zaufaną platformę.
#### 2. Dostęp do zasobów sprzętowych.

