# Labloader

Labloader is an experimental plugin loader and framework for SCP: Labrat.

## Installing on server

- Create a folder called `Frameworks` in the SCP: Labrat server files
- Put the `Labloader.dll` file in the `Frameworks` folder
- Launch the server and it will finish creating the files for Labloader

## Building from source

### Automatic setup

- Install .NET 5.0 SDK
- Run `setup.ps1`
- Open the project with IDE of choice

### Manual setup

- Install https://github.com/iRebbok/APublicizer via releases
- Copy `Managed` folder from SCP: Labrat's Dedicated Server tool (Steam AppId is `2261800`) here, rename it to `LABRAT_DEPS`
- Publicize `Assembly-CSharp.dll` and `RiptideNetworking.dll` files in the `LABRAT_DEPS` folder
- Open the project with IDE of choice