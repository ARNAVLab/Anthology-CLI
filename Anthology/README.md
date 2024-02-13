# Anthology
A C# translation for Anthology: a social simulation framework originally written in Typescript. Uses the .NET 6 framework.

# How to run
## Prerequisites
- Visual Studio 2017(or later). This is to ensure support for .NET 6. In the Visual Studio Installer, ensure that the Universal Windows Platform development package is installed.

## Build
Open the .sln file in Visual Studio. In the Build tab, click on Build Solution.
This will build the solution with the default configuration Debug|Any CPU.
You can also build by opening the terminal and running the command ``` dotnet build ``` relative to root directory. 

## Run
In Visual Studio, run the application via Start Debugging. Visual Studio will build the application before debugging
if there was no previous build done or if the solution was cleaned. You can also run by opening the terminal and running the command ``` dotnet run --project Anthology ``` relative to root directory. 

## Usage
As of now, there is no GUI implementation (the default index page is simply a deception right now, sorry). To interaction with the simulation, please adhere to the following:

### Editing the start state
- Actions are read from /Data/Actions.json. Location and People requirements are both stored as collections, although the current implementation only expects one requirement of these types, so any additional ones will simply be ignored.
- Agents are read from /Data/Agents.json, so any changes to the simulation's agents should be made here and adhere to the existing format.
- Locations are read from /Data/Locations.json. There is currently no support for multiple locations occupying the same coordinates.

### Running and view the simulation's state
In the browser open the port that is running in terminal. For instance, https://localhost:7031

Due to the lack of a GUI, for simplicity's sake all interactions are performed with the following route mappings. 
- **/Action/Print** displays all actions in the simulation
- **/Agent/Print** displays all agents in the simulation
- **/Location/Print** displays all locations in the simulation
- **/TempSim/Step/{n}** runs *n* iterations of the simulation, and displays all updated agent states. In addition, various higher resolution events such as action selection are logged in the command console.

# Dependencies
- Microsoft.NETCore
- Microsoft.AspNetCore

# Screenshots
![startup](https://github.com/alexo1110/Anthology/blob/e26d9942c985b641c672655007d1f6b88dc1adb0/screenshots/on_startup.png)
