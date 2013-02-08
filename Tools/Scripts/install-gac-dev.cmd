			    set REMLIBSERVER=..\..\lib\Server

				"%ProgramFiles(x86)%\Microsoft SDKs\Windows\v7.0A\Bin\NETFX 4.0 Tools\gacutil.exe" /if "%REMLIBSERVER%\C32Gen\IKVM.OpenJDK.Core.dll"
				"%ProgramFiles(x86)%\Microsoft SDKs\Windows\v7.0A\Bin\NETFX 4.0 Tools\gacutil.exe" /if "%REMLIBSERVER%\C32Gen\IKVM.Runtime.dll"
				"%ProgramFiles(x86)%\Microsoft SDKs\Windows\v7.0A\Bin\NETFX 4.0 Tools\gacutil.exe" /if "%REMLIBSERVER%\Apelon\DTS-SDK\IKVM.Runtime.dll" 
	
				"%ProgramFiles(x86)%\Microsoft SDKs\Windows\v7.0A\Bin\NETFX 4.0 Tools\gacutil.exe" /if "%REMLIBSERVER%\Koogra\Ionic.Utils.Zip.dll" 
				"%ProgramFiles(x86)%\Microsoft SDKs\Windows\v7.0A\Bin\NETFX 4.0 Tools\gacutil.exe" /if "%REMLIBSERVER%\Koogra\Net.SourceForge.Koogra.dll"
				        
      