# Robot Control

Technical assignment C# .NET 6

Requires dotnet runtime 6.0.
https://dotnet.microsoft.com/en-us/download/dotnet/6.0

`dotnet-test.bat` for running tests with installed dotnet version.

`dotnet-run.bat` to run command prompt.

* Configure a rectangular or circular grid of size with a robot placed.
* Select language for the robot to interpret.
* Enter commands, i.e. RFLFRRFFLR for english, HGVGHHGGVH in swedish (*Capital case is important*).

R/H = Turn right.
L/V = Turn left.
F/G = Move forward.

After each command, the program outputs its new position and the direction the robot is facing (East/Öst, West/Väst, North/Norr, South/Söder).


###Limits
Cannot move forward standing by the edge facing the "void" direction.
