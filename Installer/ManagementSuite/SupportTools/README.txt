LANDesk Support Tools 
========================
Created by Jared Barneck

Though I didn't create all the tools, only some, I am putting them all in one installer and making them accessible from one Core Server.
So many poeple have helped me it is difficult to thank them all.

Version: 9.0.3.1 
( 9.0.x.x Matches our current release of LDMS, though this is not really version dependent.)

1. Added LANDesk.Agent.Ping.exe for use with LD Ping instead of explorer.
2. Added UnmanagedToManaged.exe so a device can be made managed.
3. Fixed UDDCxExtender.xml to assume commands will be run with a working directory of ManagementSuite (this may need and post SP3 patch).
4. Updated the ldmg.landesk.com.exe file for LANDesk remote control.
5. Removed LDValidate.
6. Updated gatherlogs config.


Version 9.0.2.7b
=================
1. Compiled using LDMS 9 SP2
2. Changed the OSType xml object to search for the value as an exact match and as a contains. (This resolve an issue where in 9.0 Windows Workstations just say Workstation and Windows Servers just say Server, but 8.8 agents might scan in and say XP Workstation, 2008 Server, so by just putting "Workstation, Server" all windows machines should be obtained.)
3. Added a trick to handle the different the patch to the LDClient directory, which is now much more necessary since %ProgramFiles% doesn't point to "c:\program files (X86)" on 64 bit machines. (I use this: %LDMS_LOCAL_DIR%\..\)

WARNING: This version is not tested on 8.8 and it is recommend you continue with version beta 6 for 8.8.

Version 8.80.2.6
===================
1. Mostly rewrote the SupportTools.dll
    a. Now the SupportTools.xml can be updated and the changes are seen in the console immediately without restarting.
    b. It is now using OS Type instead of OS Name for a filter.
    c. Filtered OS Types no longer show the option disabled, the option is now removed and I didn't leave in the grayed out option.
    d. Only remote executes check for Remote Control rights, instead of everything checking for those rights.
    e. I slightly altered the XML format.
    f. Moved the SupportTools.xml file to the SupportTools directory.
    g. The Support Tools now are listed in the order placed in the XML, and not automatically sorted, so if you want to change the order you can.
2. Slightly enhanced who.vbs to not crash, but instead give a friendly error for disconnected devices.
3. Added a few options that weren't there before.
4. Fixed typos in the XML.
5. Added RemoteRegistry.exe (an autoit script recommended to me by user Rich Sterling from community.landesk.com.  Thanks Rich.)

Version 8.80.2.5
===================
1. Completely re-wrote the installer using the Eclipse NSIS plugin.
	a. Took steps to add language support.
	b. Added entry in Add/Remove Programs.
	c. Old version is uninstalled as part of the new version's install process.
	d. Registry key in HKLM\Software\LANDesk Support Tools now exists and shows components installed.
	e. In the Console, Help | About | More info will show if the LANDesk Support Tools is installed.
2. Added winscp418.exe.
	See this web site to enable the SSH port on the Linux firewall.
	http://www.redhat.com/docs/manuals/linux/RHL-9-Manual/install-guide/s1-firewallconfig.html
3. Updated to use ldmg.landesk.com.exe for requesting LANDesk Support Remote Control.

Version 8.80.2.4
===================
1. Changed RemCom.exe command line to show the computername as part of the prompt.
2. Stopped using cxExtender.dll in favor of SupportTools.dll.
	a. The install of this version will delete cxExtender.dll and delete Tools\MenuExtender.xml.
	b. SupportTools.dll is added and Tools\SupportTools.xml is added.
3. Started launching Core Side executables from the ManagementSuite directory so the %ldmain% variable is no longer necessary.
4. Added an option to send a message.
5. Added an option to defrag the C drive.
6. Added an option the remote executes a command through the agent that opens RPC through a Vista firewall.  Not XP yet.
7. Fixed bug where command line environment variables were being replaced with the BNF process and evaluating to {0}, even if they were not database BNFs, but were intended to be environment variables such as %windir%.  Now I check if there is a period character "." as all BNFs have period characters.  By default no environment variable name contains a period character ".". 
8. Improved the installer
9. Fixed some options to be able to be launched by IP address and host name.

Version 8.80.2.3
===================
1. Added TightVNC Viewer so that Linux devices with VNC enabled can be remote controlled with vncviewer.exe.

   See this web site to enable Red Hat to use VNC
   http://www.redhat.com/docs/manuals/enterprise/RHEL-4-Manual/desktop-guide/ch-ddg-remote-desktop.html

   See this web site to enable the VNC port on the Linux firewall.
   http://www.redhat.com/docs/manuals/linux/RHL-9-Manual/install-guide/s1-firewallconfig.html

2. Fixed the bug where this README.txt was not getting deleted on uninstall.


Version: 8.80.2.2
===================
1. Updated version of LDValidate.exe (Jan 27, 2009) and updated the XML.  Sorry, the xml is still for 8.8 flat and there is not an 8.8 SP2a version of the XML yet.
2. On upgrade, if the Console is open an error replacing cxExtender.dll occurs even if cxExtender is not a newer version and doesn't need to be replaced.  Fixed installer to ignore this error unless this file needs updated. 
3. Added who.vbs to see who is logged in to a remote system.
4. Added RemCom.exe to be able to open a remote command prompt.
5. Added the ability in cxExtender.dll to use %ldmain% in the command line of cxExtender.xml.  %ldmain% will evaluate to the path to the ManagementSuite directory, which can be different depending on where you install LANDesk.
6. Updated right-click ability in UDD to have the same features as right-click in all devices.  It does have one more feature in UDD for Remote Desktop Protocol.  Since it is an Unmanaged Node, you may not have the LANDesk agent yet so LANDesk Remote Control may not be available yet.


Version: 8.80.2.1
===================
1. Improved the installer
2. Added LANDesk logos to the installer.


Files
===========

	ManagementSuite\SupportTools.dll - From the LANDesk SDK to extend Console functionality.
	ManagementSuite\SupportToolsAddon.xml - From the LANDesk SDK to extend Console functionality.
	ManagementSuite\UDDCxExtender.xml - From the LANDesk SDK to extend Console functionality.

	ManagementSuite\SupportTools\LDDebugLogEnabler.exe - Custom written.
	ManagementSuite\SupportTools\LDErrorTranslator.exe - Custom written.
	ManagementSuite\SupportTools\LDSupportRC.exe - Custom written.
	ManagementSuite\SupportTools\RemCom.exe - - Open Source utility like PsExec
	ManagementSuite\SupportTools\ssh.exe - Open Source utility (PuTTY.exe renamed).
	ManagementSuite\SupportTools\vncviewer.exe - GPL Open Source utility (TightVNC).
	ManagementSuite\SupportTools\winscp418.exe - GPL Open Source utility (WinSCP).
	ManagementSuite\SupportTools\Who.vbs - WMI/VBSCript that gets remote logged in user.

	ManagementSuite\Tools\LDBKMs.xml - An xml file to add a feature to the Console.
	ManagementSuite\Tools\LDCommunity.xml - An xml file to add a feature to the Console.
	ManagementSuite\Tools\LDDebugLogEnabler.xml - An xml file to add a feature to the Console.
	ManagementSuite\Tools\LDDiscover.xml - An xml file to add a feature to the Console.
	ManagementSuite\Tools\LDErrorTranslator.xml - An xml file to add a feature to the Console.
	ManagementSuite\Tools\LDGatherLogs.xml - An xml file to add a feature to the Console.
	ManagementSuite\Tools\LDSelfServicePortal.xml - An xml file to add a feature to the Console.
	ManagementSuite\Tools\LDSupportGatewayRC.xml - An xml file to add a feature to the Console.
	ManagementSuite\Tools\MenuExtender.xml - An xml file to add a feature to the Console.
	ManagementSuite\Tools\Zenmap.xml - An xml file to add a feature to the Console.

	ManagementSuite\Utilities\GatherLogs\GatherLogs.cfg - An update to the current gatherlogs.cfg

Project Creation Notes
===============================

  The following files are modifications and enhancement to LANDesk SDK files and are now only compatible with 8.8 and later.

	1. ManagementSuite\SupportTools.dll
	2. ManagementSuite\SupportToolsAddon.xml
	3. ManagementSuite\UDDCxExtender.xml

  The following open source programs are used.
	
	1. NSIS - to create the installer.
	   http://nsis.sourceforge.net/
	   
	2. PuTTY - to add an option to ssh to Linux agents.
	   http://www.chiark.greenend.org.uk/~sgtatham/putty/
	   License: MIT License 

	3. wxWidgets - C++ library to write some simple tools.
	
	4. wxDevCpp - A RAD to write the simple tools.

	5. RemCom - An open source tool similar to PsExec.
	   http://sourceforge.net/projects/rce
	   License: BSD License

	6. TightVNC (vncviewer.exe).
	   http://www.tightvnc.com/
	   License: GPL 	

	7. Who.vbs is a simple vbs script. It is just an improvement of the one on Microsoft's site:
	   http://www.microsoft.com/technet/scriptcenter/scripts/desktop/logon/dmlgvb02.mspx?mfr=true
	   
	8. WinSCP is a secure copy tools that is great for copying files using port 22.  Usually used with Linux/Unix.  
	   The file winscp418.exe is the portable version.
	   http://winscp.net
	   License GPL version 2
	   
  The following applications were written with wxWidgets and WxDevCpp

	1. LDErrorTranslator.exe
	2. LDDebugLogEnabler.exe
	
  The Following tool was written/gathered by Joe Nunes and includes RClient.exe from the Management Gateway as well as a .HTA file.

	1. ldmg.landesk.com.exe



To do
======== 

  Features to add
	1. Find a better way than an autoit script to open Remote Registry.
	2. Gui front-end to LDServices.exe and LDProcesses.exe.
		a. Transfer the file if not there.
		b. run restartmon if file has to be transfered.
  Development
	1. Develop a GUI remote task manager tool.
	2. Develop a GUI for GatherLogs.exe.
	3. Remote Execute Option to enable RPC on XP & windows 7 if disabled (works on Vista).
	4. Remote Execute Option to open RPC on the XP Firewalls (works on Vista).
	5. Integrate with LANDesk providers, ldservices.exe ldprocess.exe
	6. Create my own provider dynamically.
	7. Create a tool that displays the user information, both logged into the OS, and logged into the Console using CBA instead of RPC.
