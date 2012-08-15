@echo off
REM
REM The System.Windows assembly that ships with Silverlight needs to be 
REM referenced by the design-time experience DLLs. This batch file locates the 
REM latest Silverlight assembly and copies it into the Binaries folder
REM to simplify project references and the build process.

SET THIS_DIR=%~dp0
REM IF EXIST %THIS_DIR%..\Microsoft.Cui.Controls\ClientBin\System.Windows.dll GOTO Finished

SET LocalProgramFiles=%ProgramFiles(x86)%
IF "%LocalProgramFiles%" == "" SET LocalProgramFiles=%ProgramFiles%

SET SilverlightDirectory="%LocalProgramFiles%\Microsoft Silverlight\"
for /f %%a IN ('dir /b/d %SilverlightDirectory%\3.*') do SET SystemWindowsLocation=%%a

copy %SilverlightDirectory%%SystemWindowsLocation%\System.Windows.dll "%THIS_DIR%..\Binaries\"
REM copy "%LocalProgramFiles%\Microsoft Expression\Blend 2\Microsoft.Expression.Framework.dll" "%THIS_DIR%..\Binaries\"

:Finished