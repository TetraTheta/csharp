@echo off
cd /d "%~dp0"
mkdir ".bin" 2>nul
move ".\GUI\.bin\Release\net48\ConvertScreenshotGUI.exe" ".\.bin\"
move ".\Launcher\.bin\Release\net48\ConvertScreenshot.exe" ".\.bin\"
pause
