@echo off
cd /d "%~dp0"
mkdir ".bin" 2>nul
move ".\GUI\.bin\Debug\net48\ConvertScreenshotGUI.exe" ".\.bin\"
move ".\Launcher\.bin\Debug\net48\ConvertScreenshot.exe" ".\.bin\"
