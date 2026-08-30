; Instalador offline de rebith
; Compilar este archivo con Inno Setup Compiler.

[Setup]
AppId={{B7F9E2A1-3C6D-4A8F-9E12-202608220001}}
AppName=rebith
AppVersion=1.0.0
DefaultDirName={autopf}\rebith
DefaultGroupName=rebith
OutputDir=Output
OutputBaseFilename=rebith_installer
Compression=lzma
SolidCompression=yes
PrivilegesRequired=admin
ArchitecturesInstallIn64BitMode=x64
WizardStyle=modern

[Files]
Source: "dist\rebith.exe"; DestDir: "{app}"; Flags: ignoreversion

[Icons]
Name: "{group}\rebith"; Filename: "{app}\rebith.exe"
Name: "{autodesktop}\rebith"; Filename: "{app}\rebith.exe"

[Run]
Filename: "{app}\rebith.exe"; Description: "Abrir rebith"; Flags: nowait postinstall skipifsilent
