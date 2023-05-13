dotnet build -c Debug

$work_dir = "./workspace/"
$game_dir = $work_dir + "serverfiles/"
$frameworks_dir = $game_dir + "Frameworks/"

if (!(Test-Path -Path $frameworks_dir -PathType Any)) {
    New-Item -Path $frameworks_dir -ItemType Directory
}

Copy-Item -Path "./Labloader/bin/Debug/Labloader.dll" -Destination $frameworks_dir

Push-Location $game_dir
& "./SCPLR_rcon.exe"
Pop-Location
