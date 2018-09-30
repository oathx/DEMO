@echo off

set VS=12
if "%configuration%"=="2015" (set VS=14)
if "%configuration%"=="2013" (set VS=12)

if not defined platform set platform=x64
if "%platform%" EQU "x64" (set VS=%VS% Win64)

cmake -H. -Bbuild -G"Visual Studio %VS%"
cmake --build build --config Release
mkdir ..\bin
copy build\Release\luv.dll ..\bin
copy build\Release\luajit.exe ..\bin
