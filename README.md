# Dissertation

Ai for games: Being a good dad. This project aims to develop an adaptive ai that will play not to win but to provide fun and challenging games. I will also develop a easy and difficult ai that will be used as comparisons between the "Good Dad".

# Getting the Project Running

You will need to have .Net sdk installed on your machine.
This can be found here -> https://dotnet.microsoft.com/learn/dotnet/hello-world-tutorial

once installed navigate to the ./UltimateTicTacToe directory in your perferred terminal 
and running the command "dotnet run". The print out will tell you which port the server is now listening on
(it should be 5000). After another few minutes the ai will be initalised and you will be able to visit the site.
To kill the process press ctrl+c while in the command prompt.

To run the front end by itself first install the angular command line tool.
This can be done by first downloading npm at -> https://nodejs.org/en/download/
and running the command npm install -g @angular/cli this will install the angular cli globally on your machine.
Navigate to the ./UltimateTicTacToe/ClientApp directory and run the "ng start" command. This will spin up an
instance of the angular app without the server backend. 

# Testing the project

To run the test project navigate to the ./UltimateTicTacToeTests directory and run the "dotnet test" command
this will run the tests in the command prompt and print out the results

To run the tests for the front end navigate to the ./UltimateTicTacToe/ClientApp run the "ng test" command. 
This will run the tests in your command prompt once.
To run the tests in debug mode run the "ng testDebug" command mode.
This will run the tests in a chrome browser, and will monitor changes to the front end
so you can see how changes in the code affect the test results 