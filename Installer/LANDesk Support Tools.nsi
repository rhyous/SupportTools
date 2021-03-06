; ProjName "Support Tools for LDMS"
Name "Support Tools for LDMS"

RequestExecutionLevel admin

SetCompressor lzma

# General Symbol Definitions
!define ProjName "Support Tools for LDMS"
!define ProjVersion "Beta_10.0"
!define REGKEY "SOFTWARE\${ProjName}"
!define VERSION 10.0
!define COMPANY "Rhyous (Jared Barneck)"
!define URL "http://www.rhyous.com/programming-development/landesk-add-ons/landesk-support-tools/"

# Global Variables
VAR /GLOBAL LDMAIN
VAR /GLOBAL ISCORE
VAR /GLOBAL ISCONSOLE
VAR /GLOBAL TOKENINSTALLED
VAR /GLOBAL PrevVersion

;MUI Header Configuration
!define MUI_HEADERIMAGE
!define MUI_HEADERIMAGE_BITMAP "Rhyous MUI2 Logo.bmp"
!define MUI_ABORTWARNING

# MUI Symbol Definitions
!define MUI_ICON SupportTools.ico
!define MUI_FINISHPAGE_NOAUTOCLOSE
!define MUI_UNICON SupportTools.ico
!define MUI_UNFINISHPAGE_NOAUTOCLOSE
!define MUI_LANGDLL_REGISTRY_ROOT HKLM
!define MUI_LANGDLL_REGISTRY_KEY ${REGKEY}
!define MUI_LANGDLL_REGISTRY_VALUENAME InstallerLanguage

# Included files
!include Sections.nsh
!include MUI2.nsh

# Reserved Files
!insertmacro MUI_RESERVEFILE_LANGDLL

# Installer pages
!insertmacro MUI_PAGE_WELCOME
!define MUI_PAGE_CUSTOMFUNCTION_LEAVE funcLicenseLeave
!insertmacro MUI_PAGE_LICENSE Managementsuite\SupportTools\LICENSE.txt
!insertmacro MUI_PAGE_COMPONENTS
!insertmacro MUI_PAGE_DIRECTORY
!insertmacro MUI_PAGE_INSTFILES
!insertmacro MUI_PAGE_FINISH
!insertmacro MUI_UNPAGE_CONFIRM
!insertmacro MUI_UNPAGE_INSTFILES

# Installer languages
!insertmacro MUI_LANGUAGE English
!insertmacro MUI_LANGUAGE SimpChinese
!insertmacro MUI_LANGUAGE TradChinese
!insertmacro MUI_LANGUAGE German
!insertmacro MUI_LANGUAGE Spanish
!insertmacro MUI_LANGUAGE French
!insertmacro MUI_LANGUAGE Italian
!insertmacro MUI_LANGUAGE Japanese
!insertmacro MUI_LANGUAGE Korean
!insertmacro MUI_LANGUAGE Portuguese
!insertmacro MUI_LANGUAGE Russian

# Installer attributes
OutFile "${ProjName}_${ProjVersion}.exe"
InstallDir "$INSTDIR"
CRCCheck on
XPStyle on
ShowInstDetails show
VIProductVersion 8.80.3.6
VIAddVersionKey /LANG=${LANG_ENGLISH} ProductName "$(^Name)"
VIAddVersionKey /LANG=${LANG_ENGLISH} ProductVersion "${VERSION}"
VIAddVersionKey /LANG=${LANG_ENGLISH} CompanyName "${COMPANY}"
VIAddVersionKey /LANG=${LANG_ENGLISH} FileVersion "${VERSION}"
VIAddVersionKey /LANG=${LANG_ENGLISH} FileDescription ""
VIAddVersionKey /LANG=${LANG_ENGLISH} LegalCopyright ""
InstallDirRegKey HKLM "${REGKEY}" Path
ShowUninstDetails show

# Installer sections
Section -Main SEC0000
    SetOutPath $INSTDIR\SupportTools
    SetOverwrite on
    File ManagementSuite\SupportTools\LICENSE.txt
    File ManagementSuite\SupportTools\README.txt

    SetOutPath $INSTDIR\SupportTools\Images
    SetOverwrite on
    File ManagementSuite\SupportTools\Images\SupportTools.png
    File ManagementSuite\SupportTools\Images\Folder.png

    WriteRegStr HKLM "${REGKEY}\Components" "Main" 1
    WriteRegStr HKLM SOFTWARE\LANDesk\ManagementSuite\Setup\Version\Patches "LANDesk Support Tools" "${VERSION}"
SectionEnd

Section "Support Tools" SEC0001
    SetOutPath $INSTDIR\Tools
    SetOverwrite on
    #File ManagementSuite\Tools\DebugLogEnabler.xml
    File ManagementSuite\Tools\ErrorTranslator.xml
    File ManagementSuite\Tools\LDCommunity.xml
    File ManagementSuite\Tools\LDDiscover.xml
    #File ManagementSuite\Tools\LDGatherLogs.xml
    File ManagementSuite\Tools\LDSupportGatewayRC.xml
    File ManagementSuite\Tools\LocalSystemCommandPrompt.xml
    File ManagementSuite\Tools\Zenmap.xml

    WriteRegStr HKLM "${REGKEY}\Components" "Support Tools" 1
SectionEnd

Section "Device Support Tools" SEC0002
    SetOutPath $INSTDIR
    SetOverwrite on
    File ManagementSuite\SupportTools.dll
    File ManagementSuite\SupportTools.DockingForm.dll

    SetOutPath $INSTDIR\Tools
    SetOverwrite on
    File ManagementSuite\Tools\SupportToolsAddon.xml  
    File ManagementSuite\Tools\SupportToolsDockingForm.xml 

    #
    # LDMain/SupportTools - Todo: Move it all to LDLogon eventually
    #
	
	SetOutPath $INSTDIR\SupportTools
    SetOverwrite on
    # Exes
    #File ManagementSuite\SupportTools\LDDebugLogEnabler.exe
    File ManagementSuite\SupportTools\LDErrorTranslator.exe
    File ManagementSuite\SupportTools\PAExec.exe
    File ManagementSuite\SupportTools\RemoteRegedit.exe
    File ManagementSuite\SupportTools\Rhyous.Agent.Ping.exe
    File ManagementSuite\SupportTools\Rhyous.ServiceManager.exe
    File ManagementSuite\SupportTools\ssh.exe
    File ManagementSuite\SupportTools\who.vbs
    File ManagementSuite\SupportTools\winscp.exe
    File ManagementSuite\SupportTools\winscp.ini
    
    # Dlls
    File ManagementSuite\SupportTools\AspectMVVM.dll
    File ManagementSuite\SupportTools\PostSharp.dll
    
    # Other
    File ManagementSuite\SupportTools\Services.xml
    File ManagementSuite\SupportTools\SupportTools.xml
    
    SetOutPath $INSTDIR\SupportTools\TightVNC
    SetOverwrite on
    File ManagementSuite\SupportTools\TightVNC\LICENSE.txt
    File ManagementSuite\SupportTools\TightVNC\tvnviewer.exe

    #
    # LDLogon/SupportTools
    #
    SetOutPath $INSTDIR\LDLogon\SupportTools
    SetOverwrite on
    # Exes
    File ManagementSuite\LDLogon\SupportTools\MessagePresenter.exe

    #
    # Scripts
    #
    SetOutPath $INSTDIR\Scripts
    SetOverwrite on
    # Exes
    File ManagementSuite\Scripts\MessagePresenter.ini
    
    
    WriteRegStr HKLM "${REGKEY}\Components" "Device Support Tools" 1
SectionEnd

Section "UDD Support Tools" SEC0003
    SetOutPath $INSTDIR
    SetOverwrite on
    File ManagementSuite\UDDCxExtender.xml
    File ManagementSuite\UnmanagedToManaged.exe

    WriteRegStr HKLM "${REGKEY}\Components" "UDD Support Tools" 1
SectionEnd

Section -post SEC0004
    WriteRegStr HKLM "${REGKEY}" Path $INSTDIR
    WriteUninstaller $INSTDIR\SupportTools\uninstall.exe
    WriteRegStr HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\$(^Name)" DisplayName "$(^Name)"
    WriteRegStr HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\$(^Name)" DisplayVersion "${VERSION}"
    WriteRegStr HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\$(^Name)" Publisher "${COMPANY}"
    WriteRegStr HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\$(^Name)" DisplayIcon $INSTDIR\uninstall.exe
    WriteRegStr HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\$(^Name)" UninstallString $INSTDIR\SupportTools\uninstall.exe
    WriteRegStr HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\$(^Name)" QuietUninstallString "$INSTDIR\SupportTools\uninstall.exe /S"
    WriteRegDWORD HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\$(^Name)" NoModify 1
    WriteRegDWORD HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\$(^Name)" NoRepair 1
SectionEnd

Section /o "Agent ini" SEC0005
    #
    # LDLogon
    #
    SetOutPath $INSTDIR\LDLogon
    SetOverwrite on
    # Agent Ini
    File ManagementSuite\LDLogon\SupportTools.ini
    
    WriteRegStr HKLM "SOFTWARE\LANDESK\ManagementSuite\Stamping\Files" SupportTools '"$INSTDIR\LDLogon\SupportTools.ini"'
    ExecWait "$INSTDIR\stamper.exe"
    ExecWait '"$INSTDIR\CreateClientConfiguration.exe" /rebuild'
SectionEnd

Section /o "Trusted Token" SEC0006
    SetOutPath $INSTDIR
    SetOverwrite on
    # Coredbutil.exe xml
    File ManagementSuite\Rhyous.xml
    ExecWait '"$INSTDIR\CoreDbUtil.exe" /xml=Rhyous.xml /buildcomponents'
    
    WriteRegStr HKLM "${REGKEY}\Components" "Trusted Token" 1
SectionEnd

# Macro for selecting uninstaller sections
!macro SELECT_UNSECTION SECTION_NAME UNSECTION_ID
    #MessageBox MB_OKCANCEL "${SECTION_NAME} ${UNSECTION_ID}"
    Push $R0
    ReadRegStr $R0 HKLM "${REGKEY}\Components" "${SECTION_NAME}"
    #MessageBox MB_OKCANCEL "$R0"
    StrCmp $R0 1 0 next${UNSECTION_ID}
    !insertmacro SelectSection ${UNSECTION_ID}
    GoTo done${UNSECTION_ID}
    
    next${UNSECTION_ID}:
        #MessageBox MB_OKCANCEL "Next"
        !insertmacro UnselectSection ${UNSECTION_ID}
    
    done${UNSECTION_ID}:
        #MessageBox MB_OKCANCEL "Done"
        Pop $R0
!macroend

# Uninstaller sections
Section /o -un.Main UNSEC0000
    ;Don't delete GatherLogs.cfg
    ;Delete /REBOOTOK $INSTDIR\Utilities\GatherLogs\GatherLogs.cfg
    Delete /REBOOTOK $INSTDIR\SupportTools\README.txt
    Delete /REBOOTOK $INSTDIR\SupportTools\LICENSE.txt
    Delete /REBOOTOK  $INSTDIR\SupportTools\Images\Folder.png
    Delete /REBOOTOK  $INSTDIR\SupportTools\Images\SupportTools.png
    
    RmDir /REBOOTOK $INSTDIR\SupportTools\Images
    RmDir /REBOOTOK $INSTDIR\SupportTools

    DeleteRegValue HKLM "${REGKEY}\Components" "Support Tools"
    DeleteRegValue HKLM SOFTWARE\LANDesk\ManagementSuite\Setup\Version\Patches "LANDesk Support Tools"
SectionEnd

Section /o "-un.Support Tools" UNSEC0001
    Delete /REBOOTOK $INSTDIR\Tools\Zenmap.xml
    Delete /REBOOTOK $INSTDIR\Tools\LDValidate.xml
    Delete /REBOOTOK $INSTDIR\Tools\LDSupportGatewayRC.xml
    Delete /REBOOTOK $INSTDIR\Tools\LocalSystemCommandPrompt
    Delete /REBOOTOK $INSTDIR\Tools\SupportToolsServiceManager.xml
    Delete /REBOOTOK $INSTDIR\Tools\LDSelfServicePortal.xml
    #Delete /REBOOTOK $INSTDIR\Tools\LDGatherLogs.xml
    Delete /REBOOTOK $INSTDIR\Tools\LDDiscover.xml
    Delete /REBOOTOK $INSTDIR\Tools\LDCommunity.xml
    Delete /REBOOTOK $INSTDIR\Tools\ErrorTranslator.xml
    #Delete /REBOOTOK $INSTDIR\Tools\DebugLogEnabler.xml
    RmDir $INSTDIR\Tools
    
    DeleteRegValue HKLM "${REGKEY}\Components" "Support Tools"
SectionEnd

Section /o "-un.Device Support Tools" UNSEC0002
    
    Delete /REBOOTOK $INSTDIR\SupportTools\TightVNC\LICENSE.txt
    Delete /REBOOTOK $INSTDIR\SupportTools\TightVNC\tvnviewer.exe
    RmDir $INSTDIR\SupportTools\TightVNC
    
    # Scripts
    Delete /REBOOTOK $INSTDIR\Scripts\MessagePresenter.ini
    
    # LDLogon
    Delete /REBOOTOK $INSTDIR\LDLogon\SupportTools\MessagePresenter.exe    
    RmDir $INSTDIR\LDLogon\SupportTools
    
    # Exes
    #Delete /REBOOTOK $INSTDIR\SupportTools\LDDebugLogEnabler.exe
    Delete /REBOOTOK $INSTDIR\SupportTools\LDErrorTranslator.exe
    Delete /REBOOTOK $INSTDIR\SupportTools\PAExec.exe
    Delete /REBOOTOK $INSTDIR\SupportTools\RemoteRegedit.exe
    Delete /REBOOTOK $INSTDIR\SupportTools\Rhyous.Agent.Ping.exe
    Delete /REBOOTOK $INSTDIR\SupportTools\Rhyous.ServiceManager.exe
    Delete /REBOOTOK $INSTDIR\SupportTools\ssh.exe
    Delete /REBOOTOK $INSTDIR\SupportTools\who.vbs
    Delete /REBOOTOK $INSTDIR\SupportTools\winscp.exe
    Delete /REBOOTOK $INSTDIR\SupportTools\winscp.ini
    # Dlls
    
    # Dlls
    Delete /REBOOTOK $INSTDIR\SupportTools\AspectMVVM.dll
    Delete /REBOOTOK $INSTDIR\SupportTools\PostSharp.dll

    Delete /REBOOTOK $INSTDIR\SupportTools\Services.xml
    Delete /REBOOTOK $INSTDIR\SupportTools\SupportTools.xml
    
    Delete /REBOOTOK $INSTDIR\Tools\SupportToolsAddon.xml
    Delete /REBOOTOK $INSTDIR\Tools\SupportToolsDockingForm.xml

    Delete /REBOOTOK $INSTDIR\SupportTools.dll
    Delete /REBOOTOK $INSTDIR\SupportTools.DockingForm.dll
   
    DeleteRegValue HKLM "${REGKEY}\Components" "Device Support Tools"
SectionEnd

Section /o "-un.UDD Support Tools" UNSEC0003
    Delete /REBOOTOK $INSTDIR\UDDCxExtender.xml
    Delete /REBOOTOK $INSTDIR\UnmanagedToManaged.exe
    DeleteRegValue HKLM "${REGKEY}\Components" "UDD Support Tools"
SectionEnd

Section -un.post UNSEC0004
    DeleteRegKey HKLM "SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall\$(^Name)"
    Delete /REBOOTOK $INSTDIR\SupportTools\uninstall.exe
    DeleteRegValue HKLM "${REGKEY}" Path
    DeleteRegKey /IfEmpty HKLM "${REGKEY}\Components"
    DeleteRegKey /IfEmpty HKLM "${REGKEY}"
    RmDir /REBOOTOK $INSTDIR\SupportTools
    RmDir /REBOOTOK $INSTDIR\Tools
SectionEnd

Section -un.post UNSEC0005
    Delete /REBOOTOK $INSTDIR\LDLogon\SupportTools.ini
    DeleteRegValue HKLM "SOFTWARE\LANDESK\ManagementSuite\Stamping\Files" "SupportTools"
SectionEnd

Section /o "-un.Trusted Token" UNSEC0006
    Delete /REBOOTOK $INSTDIR\Rhyous.xml
    DeleteRegValue HKLM "${REGKEY}\Components" "Trusted Token"
    DeleteRegKey /IfEmpty HKLM "${REGKEY}\Components"
    DeleteRegKey /IfEmpty HKLM "${REGKEY}"
SectionEnd
# Installer functions
Function .onInit
    InitPluginsDir
    !insertmacro MUI_LANGDLL_DISPLAY
    SetRegView 64
    ReadRegStr $LDMAIN HKLM SOFTWARE\LANDesk\ManagementSuite\Setup "LDMainPath"
    ReadRegStr $ISCORE HKLM SOFTWARE\LANDesk\LANDeskSoftware\Features "Core"
    ReadRegStr $ISCONSOLE HKLM SOFTWARE\LANDesk\LANDeskSoftware\Features "Console"
    ReadRegStr $TOKENINSTALLED HKLM "SOFTWARE\Support Tools for LDMS\Components" "Trusted Token"
    StrCmp $ISCORE "1" ISCORE
    StrCmp $ISCONSOLE "1" defaultdir
        MessageBox MB_YESNO $(NoCoreConsole) /SD IDYES IDNO donotinstall
            StrCpy $LDMAIN "$PROGRAMFILES64\LANDesk\Managementsuite"
            Goto next
        donotinstall:
            Quit
        ISCORE:
            StrCmp $TOKENINSTALLED 1 defaultdir
            !insertmacro SelectSection ${SEC0006} 
        defaultdir:
            StrCpy $LDMAIN "$LDMAIN" -1
    next:
    StrCpy $INSTDIR "$LDMAIN"
    
    IfFileExists "$INSTDIR\SupportTools\*.*" prev_version no_prev_version
        prev_version:
            StrCpy $PrevVersion "true"
            goto end_prev_version
        no_prev_version:
            StrCpy $PrevVersion "false"
        end_prev_version:
FunctionEnd

Function funcLicenseLeave
        StrCmp $PrevVersion "true" RunUninstaller DoNotRunUninstaller
        RunUninstaller:
        MessageBox MB_OKCANCEL $(PrevVersion) /SD IDOK IDCANCEL doQuit
                 IfSilent doSilentUninstall doUninstall
                    doSilentUninstall:
                        ExecWait '"$INSTDIR\SupportTools\uninstall.exe" /S'
                        goto uninstallran
                    doUninstall:
                        ExecWait "$INSTDIR\SupportTools\uninstall.exe"
                        goto uninstallran
                    doQuit:
                        Quit
                    uninstallran:
        DoNotRunUninstaller:
FunctionEnd

# Uninstaller functions
Function un.onInit
    #MessageBox MB_OKCANCEL "Starting Uninstall"
    SetRegView 64
    ReadRegStr $INSTDIR HKLM "${REGKEY}" Path
    #MessageBox MB_OKCANCEL $INSTDIR
    StrCmp $INSTDIR "" 0 +2
        StrCpy $INSTDIR  $EXEPATH
    !insertmacro MUI_UNGETLANGUAGE
    !insertmacro SELECT_UNSECTION Main ${UNSEC0000}
    !insertmacro SELECT_UNSECTION "Support Tools" ${UNSEC0001}
    !insertmacro SELECT_UNSECTION "Device Support Tools" ${UNSEC0002}
    !insertmacro SELECT_UNSECTION "UDD Support Tools" ${UNSEC0003}
    !insertmacro SELECT_UNSECTION "Trusted Token" ${UNSEC0006}
FunctionEnd

# Section Descriptions
!insertmacro MUI_FUNCTION_DESCRIPTION_BEGIN
!insertmacro MUI_DESCRIPTION_TEXT ${SEC0001} $(SEC0001_DESC)
!insertmacro MUI_DESCRIPTION_TEXT ${SEC0002} $(SEC0002_DESC)
!insertmacro MUI_DESCRIPTION_TEXT ${SEC0003} $(SEC0003_DESC)
!insertmacro MUI_FUNCTION_DESCRIPTION_END

# Installer Language Strings
# TODO Update the Language Strings with the appropriate translations.

LangString SEC0001_DESC ${LANG_ENGLISH} "Support tools added to the LANDesk Console menu."
LangString SEC0002_DESC ${LANG_ENGLISH} "Support Tools added to the options when right-clicking on a device in the Network View."
LangString SEC0003_DESC ${LANG_ENGLISH} "Adds tools to the options when right-clicking an Unmanaged Nodes in Unmanaged Device Discovery window in the LANDesk Console."
LangString NoCoreConsole ${LANG_ENGLISH} "No Core or Console found.  Do you want to install anyway?"
LangString PrevVersion   ${LANG_ENGLISH} "A previous version was detected.  Click OK to uninstall it now or Cancel to stop the installer."

LangString SEC0001_DESC ${LANG_SIMPCHINESE} "Support tools added to the LANDesk Console menu."
LangString SEC0002_DESC ${LANG_SIMPCHINESE} "Support Tools added to the options when right-clicking on a device in the Network View."
LangString SEC0003_DESC ${LANG_SIMPCHINESE} "Adds tools to the options when right-clicking an Unmanaged Nodes in Unmanaged Device Discovery window in the LANDesk Console."
LangString NoCoreConsole ${LANG_SIMPCHINESE} "No Core or Console found.  Do you want to install anyway?"
LangString PrevVersion  ${LANG_SIMPCHINESE} "A previous version was detected.  Click OK to uninstall it now or Cancel to stop the installer."


LangString SEC0001_DESC ${LANG_TRADCHINESE} "Support tools added to the LANDesk Console menu."
LangString SEC0002_DESC ${LANG_TRADCHINESE} "Support Tools added to the options when right-clicking on a device in the Network View."
LangString SEC0003_DESC ${LANG_TRADCHINESE} "Adds tools to the options when right-clicking an Unmanaged Nodes in Unmanaged Device Discovery window in the LANDesk Console."
LangString NoCoreConsole ${LANG_TRADCHINESE} "No Core or Console found.  Do you want to install anyway?"
LangString PrevVersion  ${LANG_TRADCHINESE} "A previous version was detected.  Click OK to uninstall it now or Cancel to stop the installer."

LangString SEC0001_DESC ${LANG_GERMAN} "Support tools added to the LANDesk Console menu."
LangString SEC0002_DESC ${LANG_GERMAN} "Support Tools added to the options when right-clicking on a device in the Network View."
LangString SEC0003_DESC ${LANG_GERMAN} "Adds tools to the options when right-clicking an Unmanaged Nodes in Unmanaged Device Discovery window in the LANDesk Console."
LangString NoCoreConsole ${LANG_GERMAN} "No Core or Console found.  Do you want to install anyway?"
LangString PrevVersion  ${LANG_GERMAN} "A previous version was detected.  Click OK to uninstall it now or Cancel to stop the installer."

LangString SEC0001_DESC ${LANG_SPANISH} "Support tools added to the LANDesk Console menu."
LangString SEC0002_DESC ${LANG_SPANISH} "Support Tools added to the options when right-clicking on a device in the Network View."
LangString SEC0003_DESC ${LANG_SPANISH} "Adds tools to the options when right-clicking an Unmanaged Nodes in Unmanaged Device Discovery window in the LANDesk Console."
LangString NoCoreConsole ${LANG_SPANISH} "No Core or Console found.  Do you want to install anyway?"
LangString PrevVersion ${LANG_SPANISH} "A previous version was detected.  Click OK to uninstall it now or Cancel to stop the installer."

LangString SEC0001_DESC ${LANG_FRENCH} "Support tools added to the LANDesk Console menu."
LangString SEC0002_DESC ${LANG_FRENCH} "Support Tools added to the options when right-clicking on a device in the Network View."
LangString SEC0003_DESC ${LANG_FRENCH} "Adds tools to the options when right-clicking an Unmanaged Nodes in Unmanaged Device Discovery window in the LANDesk Console."
LangString NoCoreConsole ${LANG_FRENCH} "No Core or Console found.  Do you want to install anyway?"
LangString PrevVersion ${LANG_FRENCH} "A previous version was detected.  Click OK to uninstall it now or Cancel to stop the installer."

LangString SEC0001_DESC ${LANG_ITALIAN} "Support tools added to the LANDesk Console menu."
LangString SEC0002_DESC ${LANG_ITALIAN} "Support Tools added to the options when right-clicking on a device in the Network View."
LangString SEC0003_DESC ${LANG_ITALIAN} "Adds tools to the options when right-clicking an Unmanaged Nodes in Unmanaged Device Discovery window in the LANDesk Console."
LangString NoCoreConsole ${LANG_ITALIAN} "No Core or Console found.  Do you want to install anyway?"
LangString PrevVersion ${LANG_ITALIAN} "A previous version was detected.  Click OK to uninstall it now or Cancel to stop the installer."

LangString SEC0001_DESC ${LANG_JAPANESE} "Support tools added to the LANDesk Console menu."
LangString SEC0002_DESC ${LANG_JAPANESE} "Support Tools added to the options when right-clicking on a device in the Network View."
LangString SEC0003_DESC ${LANG_JAPANESE} "Adds tools to the options when right-clicking an Unmanaged Nodes in Unmanaged Device Discovery window in the LANDesk Console."
LangString NoCoreConsole ${LANG_JAPANESE} "No Core or Console found.  Do you want to install anyway?"
LangString PrevVersion ${LANG_JAPANESE} "A previous version was detected.  Click OK to uninstall it now or Cancel to stop the installer."

LangString SEC0001_DESC ${LANG_KOREAN} "Support tools added to the LANDesk Console menu."
LangString SEC0002_DESC ${LANG_KOREAN} "Support Tools added to the options when right-clicking on a device in the Network View."
LangString SEC0003_DESC ${LANG_KOREAN} "Adds tools to the options when right-clicking an Unmanaged Nodes in Unmanaged Device Discovery window in the LANDesk Console."
LangString NoCoreConsole ${LANG_KOREAN} "No Core or Console found.  Do you want to install anyway?"
LangString PrevVersion ${LANG_KOREAN} "A previous version was detected.  Click OK to uninstall it now or Cancel to stop the installer."

LangString SEC0001_DESC ${LANG_PORTUGUESE} "Support tools added to the LANDesk Console menu."
LangString SEC0002_DESC ${LANG_PORTUGUESE} "Support Tools added to the options when right-clicking on a device in the Network View."
LangString SEC0003_DESC ${LANG_PORTUGUESE} "Adds tools to the options when right-clicking an Unmanaged Nodes in Unmanaged Device Discovery window in the LANDesk Console."
LangString NoCoreConsole ${LANG_PORTUGUESE} "No Core or Console found.  Do you want to install anyway?"
LangString PrevVersion ${LANG_PORTUGUESE} "A previous version was detected.  Click OK to uninstall it now or Cancel to stop the installer."

LangString SEC0001_DESC ${LANG_RUSSIAN} "Support tools added to the LANDesk Console menu."
LangString SEC0002_DESC ${LANG_RUSSIAN} "Support Tools added to the options when right-clicking on a device in the Network View."
LangString SEC0003_DESC ${LANG_RUSSIAN} "Adds tools to the options when right-clicking an Unmanaged Nodes in Unmanaged Device Discovery window in the LANDesk Console."
LangString NoCoreConsole ${LANG_RUSSIAN} "No Core or Console found.  Do you want to install anyway?"
LangString PrevVersion ${LANG_RUSSIAN} "A previous version was detected.  Click OK to uninstall it now or Cancel to stop the installer."
