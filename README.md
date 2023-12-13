# Kotono

## Setup :

1. Install .NET Framework in [Visual Studio](https://visualstudio.microsoft.com/downloads/) and [.NET 8.0 SDK](https://dotnet.microsoft.com/download)
2. Install [OpenAl](https://openal.org/downloads/oalinst.zip) (run the installer in the zip)
3. Clone Kotono in Visual Studio (click "<> Code" then "Open with Visual Studio")
4. Build Kotono and add Kotono/Kotono/bin/Debug/net8.0/Kotono.dll to your project's Project References
5. Add Kotono to your projects's Project Dependencies and put Kotono before your project in your project's Project Build Order,
   so that everytime a change is made to Kotono, Kotono.dll will be updated
6. Change Kotono and your project's path in your project's Program.cs

If you want my TestApp for Kotono, clone [Kotono-TestApp](https://github.com/laracIette/Kotono-TestApp) in Visual Studio (and do step 4 for Kotono-TestApp)
