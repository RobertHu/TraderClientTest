@ECHO OFF

REM The following directory is for .NET 2.0
set DOTNETFX2=C:\Windows\Microsoft.NET\Framework\v4.0.30319
set PATH=%PATH%;%DOTNETFX2%

echo Installing WindowsService...
echo ---------------------------------------------------
InstallUtil /i WindowsService.exe
echo ---------------------------------------------------
echo Done.