@echo off
cd "%~dp0"
@REM Remove .bin, .obj, .vs
for /d /r %%i in (.bin .obj .vs) do (
  if exist "%%i" (
    echo Deleting %%i
    rd /s /q "%%i"
  )
)
@REM Remove empty directories
for /f "delims=" %%d in ('dir /s /b /ad ^| sort /r') do (
  dir /b "%%d" | findstr /r . >nul 2>nul || (
    echo Deleting %%d
    rd "%%d"
  )
)
pause
