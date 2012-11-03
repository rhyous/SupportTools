Option Explicit
Dim objWMIService, StrComputer, SuccessfullyAccessedRemotePC
Dim colNamedArguments, colComputer, objComputer, args

'Set this to default to false
SuccessfullyAccessedRemotePC = false

Set colNamedArguments = WScript.Arguments.Named
If colNamedArguments.Exists("S") Then
	strComputer = colNamedArguments.Item("S")
Elseif WScript.Arguments.Count = 0 Then
	Dim objNetwork
	Set objNetwork = WScript.CreateObject("WScript.Network")
	strComputer = objNetwork.ComputerName
Else
 Wscript.Echo "Usage: Who.vbs /S:ComputerName"
 Wscript.Quit
End If

On Error Resume Next
GetLoggedInUser

If Not SuccessfullyAccessedRemotePC Then
	Wscript.Echo vbCr & _
	"Could not access " & strComputer & "." & vbCr & vbCr & _
	"Make sure the the computer is on and" & vbCr & _
	"accessible via RPC." & vbCr & vbCr
End If

Sub GetLoggedInUser()
	Set objWMIService = GetObject("winmgmts:" _
		& "{impersonationLevel=impersonate}!\\" _
		& strComputer _
		& "\root\cimv2")

	Set colComputer = objWMIService.ExecQuery _
		("Select * from Win32_ComputerSystem")

	SuccessfullyAccessedRemotePC = true
	
	For Each objComputer in colComputer
		If not objComputer.UserName = "" Then
			Wscript.Echo vbCr & _
			"Machine Name: " & strComputer & vbCr & _
			"Logged-on user: " & objComputer.UserName & vbCr & vbCr
		Else 
			Wscript.Echo vbCr & _
			"No one is currently logged into '" & StrComputer & "'" & _
			vbCr & vbCr
		End If
	Next
	
End Sub
