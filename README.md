# SCCInterviewProject

A minimal api in C# that saves a list of favourite Star Wars planets.

**Also includes a frontend web app to interact with the API.
To open the web app, just open the base url.**

To build the project, execute `dotnet run` in the command line

The project uses .NET 8.0

There is also a built version in `/bin/net8.0`

Run `starwarsapi.exe` to start the API server

## Routes:

### GET a list of Planets from the SWAPI API (https://swapi.dev)
GET /planets/{page}
Requires page as a parameter. From 1 to 6.

![image](https://github.com/miguel4521/SCCInterviewProject/assets/109853127/ab26b8e3-4385-425f-a782-f2d29fa9e23c)

### GET a list of favourite Planets
GET /favouriteplanets

![image](https://github.com/miguel4521/SCCInterviewProject/assets/109853127/577dba26-325c-4cc1-8129-7c5fb2457b45)

### POST a favourite Planet and save to an in memory entity framework database - Planets can only be favourited once.
POST /favouriteplanets

![image](https://github.com/miguel4521/SCCInterviewProject/assets/109853127/5e5420a5-06a5-4977-9e63-9aaad34fc5ac)
![image](https://github.com/miguel4521/SCCInterviewProject/assets/109853127/0971684c-0346-435a-b524-9d21ce2df42c)

### DELETE a favourite planet.
POST /favouriteplanets

To DELETE a favourite planet, just use the /favouriteplanets route the name and url of a planet that is already a favourite

### GET a random planet that has not yet been favourited from the SWAPI API. (https://swapi.dev)
GET /newrandomplanet

![image](https://github.com/miguel4521/SCCInterviewProject/assets/109853127/5c78b307-6651-4bed-823e-a597f08def9c)

## The webpage
![image](https://github.com/miguel4521/SCCInterviewProject/assets/109853127/76094b1d-15b6-46fa-b095-d974cc79cbaf)

