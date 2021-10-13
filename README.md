#Illinois COVID-19 Data Scraper
## Len Washington

A bot created to scrape data related to COVID-19 daily from the Illinois Department of Public Health.

### Create Spreadsheets
- In Google Sheets, have a spreadsheet that you want to use ready.
- In the link, find the id of the spreadsheet
  - The id for https://docs.google.com/spreadsheets/d/1ofE0QnPLQu9fN387-_hq3RBmOkf3u4sPJVK72YYgJ2c/edit is ```1ofE0QnPLQu9fN387-_hq3RBmOkf3u4sPJVK72YYgJ2c``` 
- Take that id, open the ```Link``` class, and set the value of the proper value to the id

### Create Google API Credentials
- In the top left, go to ```Select a project```, and then ```NEW PROJECT```.
- Click on the menu button on the left, select ```APIs & Services``` then ```Library```.
- In the search bar, search for ```Google Sheets API```, click it, then select ```Enable```.
- In the menu, go to ```APIs & Services``` then ```Credentials```.
- Click ```+ CREATE CREDENTIALS```, select ```OAuth client ID```.
  - The ```Application type``` is ```Desktop app```.
  - Name it whatever you want.
- When the client is created, download the json file, put it in the CoronaScraper folder, and save it as ```credentials.json```.


### Run the project
- When running the project, the program needs to know where the credentials file is. There are two options for telling it where it is.
  1. In the command line, add the arguments ```-c {location_of_credentials.json}```
     - Ex: >> ```CoronaScraper.exe -c {credentials_file.json}```
  2. In the project, add the credentials to the project resources.
     - In Visual Studio, click on ```Project```, then ```CoronaScraper Properties```.
     - Click on ```Resources```, click on the dropdown next to ```Strings```, switch it to ```Files```.
     - Click ```Add Resource```, ```Add Existing File```, find your credentials file and select it.
     - Rename the resource to ```credentials```


### My spreadsheets
- General
  - [Overall Data](https://docs.google.com/spreadsheets/d/1LvheiEFwA4m46WRGadCwPZ-PLquTvyRCtIkyIsaMPdo/edit#gid=0)
  - [Gender](https://docs.google.com/spreadsheets/d/1c9N0tnfnrOdho1Wumxj1Jbf4WBQW4n1PjvJoyKZFZrI/edit#gid=0)
- Age/Race Correlated Data
  - [Confirmed Cases](https://docs.google.com/spreadsheets/d/1ofE0QnPLQu9fN387-_hq3RBmOkf3u4sPJVK72YYgJ2c/edit#gid=550283834)
  - [Tested](https://docs.google.com/spreadsheets/d/1_GaWoyq4oJKr08QwPeq8CM0OcpnMLBJHSVQAOoS0M1Q/edit#gid=2094382044)
  - [Deaths](https://docs.google.com/spreadsheets/d/11HqcIBD2wpZTUaOQQEKIja6rINEIY2EXK19UJHiufuE/edit#gid=1038546542)

### Notes
- The Illinois Department of Public Health does not updated data on weekends and federal holidays.
- Users cannot edit the data in my spreadsheets, do not use those ids for the links
