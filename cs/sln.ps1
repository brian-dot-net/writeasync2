$ErrorActionPreference = 'Stop'

$dir = Get-ChildItem -Filter ".vs" | Select-Object -First 1
if (!$dir)
{
    $dir = New-Item -Path ".vs" -ItemType Directory
}

$proj = Get-ChildItem -Filter "*proj" | Select-Object -First 1
if (!$proj)
{
    throw "No project file found."
}

$name = [System.IO.Path]::GetFileNameWithoutExtension($proj.Name) + ".sln"
$slnPath = Join-Path -Path $($dir.FullName) -ChildPath $name

dotnet slngen -o $slnPath $($proj.FullName)
