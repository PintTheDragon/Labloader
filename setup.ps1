$extra_install_flags = ""
# $extra_install_flags = "-beta unstable-beta"

$work_dir = "./workspace/"
$nuget_exe = $work_dir + "nuget.exe"
$game_dir = $work_dir + "serverfiles/"
$apublicizer_dir = $work_dir + "apublicizer/"
$apublicizer_zip = $work_dir + "apublicizer.zip"
$steamcmd_dir = $work_dir + "steamcmd/"
$steamcmd_zip = $work_dir + "steamcmd.zip"
$steamcmd = $steamcmd_dir + "steamcmd.exe"

if (!(Test-Path -Path $work_dir -PathType Any)) {
    New-Item -Path $work_dir -ItemType Directory
}
if (!(Test-Path -Path $steamcmd_dir -PathType Any)) {
    New-Item -Path $steamcmd_dir -ItemType Directory
}
Write-Progress -Activity "Installing SteamCMD"
if (!(Test-Path -Path $steamcmd_zip -PathType Any)) {
    Invoke-WebRequest "https://steamcdn-a.akamaihd.net/client/installer/steamcmd.zip" -OutFile $steamcmd_zip
    Expand-Archive -Path $steamcmd_zip -DestinationPath $steamcmd_dir
}

Write-Progress -Activity "Installing SCP: Labrat Dedicated Server tool"
& $steamcmd ("+force_install_dir ../serverfiles/") "+login anonymous" ("+app_update 2261800 " + $extra_install_flags + " validate") "+quit"
if (!(Test-Path -Path "./LABRAT_DEPS/" -PathType Any)) {
    New-Item -Path "./LABRAT_DEPS/" -ItemType Directory
}
Copy-Item -Path ($game_dir + "SCP Labrat_Data/Managed/*") -Destination "./LABRAT_DEPS/" -Recurse

Write-Progress -Activity "Installing APublicizer-1.0.3"
if (!(Test-Path -Path $apublicizer_dir -PathType Any)) {
    New-Item -Path $apublicizer_dir -ItemType Directory
}
if (!(Test-Path -Path $apublicizer_zip -PathType Any)) {
    Invoke-WebRequest "https://github.com/iRebbok/APublicizer/archive/refs/tags/1.0.3.zip" -OutFile $apublicizer_zip
    Expand-Archive -Path $apublicizer_zip -DestinationPath $apublicizer_dir
}

dotnet run -c Release --project "./workspace/apublicizer/APublicizer-1.0.3/APublicizer.csproj" "./LABRAT_DEPS/Assembly-CSharp.dll"
dotnet run -c Release --project "./workspace/apublicizer/APublicizer-1.0.3/APublicizer.csproj" "./LABRAT_DEPS/RiptideNetworking.dll"

Write-Progress -Activity "Installing Latest NuGet Package Manager"
if (!(Test-Path -Path $nuget_exe -PathType Any)) {
    Invoke-WebRequest "https://dist.nuget.org/win-x86-commandline/latest/nuget.exe" -OutFile $nuget_exe
}
& $nuget_exe restore Labloader.sln

Write-Output "Project is setup and ready!"
