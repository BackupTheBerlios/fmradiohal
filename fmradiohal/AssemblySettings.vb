

' Original code and credits
' Mike Woodring
' http://staff.develop.com/woodring
'
' VB.NET port by Darrell Norton
' http://dotnetjunkies.com/weblog/darrell.norton/
' 
Imports System
Imports System.Reflection
Imports System.Collections
Imports System.Xml
Imports System.Configuration

' AssemblySettings usage:
'
' If you know the keys you're after, the following is probably
' the most convenient:
'
'   C# code
'      AssemblySettings settings = new AssemblySettings();
'      string someSetting1 = settings["someKey1"];
'      string someSetting2 = settings["someKey2"];
'   VB.NET code
'       Dim settings As AssemblySettings = New AssemblySettings()
'       Dim someSetting1 As String = settings("someKey1")
'       Dim someSetting2 As String = settings("someKey2")
'
'
' If you want to enumerate over the settings (or just as an
' alternative approach), you can do this too:
'
'   C# code
'      IDictionary settings = AssemblySettings.GetConfig();
'      foreach( DictionaryEntry entry in settings )
'      {
'          // Use entry.Key or entry.Value as desired...
'      }
'   VB.NET Code
'       Dim settings As IDictionary = AssemblySettings.GetConfig()
'       Dim entry As DictionaryEntry
'       For Each entry In  settings
'           ' Use entry.Key or entry.Value as desired...
'       Next entry 
'
'
' In either of the above two scenarios, the calling assembly
' (the one that called the constructor or GetConfig) is used
' to determine what file to parse and what the name of the
' settings collection element is.  For example, if the calling
' assembly is c:\foo\bar\TestLib.dll, then the configuration file
' that's parsed is c:\foo\bar\TestLib.dll.config, and the
' configuration section that's parsed must be named <assemblySettings>.
'
' To retrieve the configuration information for an arbitrary assembly,
' use the overloaded constructor or GetConfig method that takes an
' Assembly reference as input.
'
' If your assembly is being automatically downloaded from a web
' site by an "href-exe" (an application that's run directly from a link
' on a web page), then the enclosed web.config shows the mechanism
' for allowing the AssemblySettings library to download the
' configuration files you're using for your assemblies (while not
' allowing web.config itself to be downloaded).
'
' If the assembly you are trying to use this with is installed in, and loaded
' from, the GAC then you'll need to place the config file in the GAC directory where
' the assembly is installed.  On the first release of the CLR, this directory is
' <windir>\assembly\gac\libName\verNum__pubKeyToken]]>.  For example,
' the assembly "SomeLib, Version=1.2.3.4, Culture=neutral, PublicKeyToken=abcd1234"
' would be installed to the c:\winnt\assembly\gac\SomeLib\1.2.3.4__abcd1234 diretory
' (assuming the OS is installed in c:\winnt).  For future versions of the CLR, this
' directory scheme may change, so you'll need to check the <code>CodeBase</code> property
' of a GAC-loaded assembly in the debugger to determine the correct directory location.

'<ClassInterface(ClassInterfaceType.None)> _


<System.Runtime.InteropServices.ClassInterface(Runtime.InteropServices.ClassInterfaceType.None)> _
    Friend Class AssemblySettings

    Private settings As IDictionary

    Public Sub New()
        MyClass.New([Assembly].GetExecutingAssembly())

    End Sub

    Public Sub New(ByVal asm As [Assembly])
        settings = GetConfig(asm)
    End Sub

    Default Public ReadOnly Property Item(ByVal key As String) As String
        Get
            Dim settingValue As String = Nothing

            If Not (settings Is Nothing) Then
                settingValue = settings(key)
            End If

            If (settingValue Is Nothing) Then
                Return ""
            Else
                Return settingValue
            End If
        End Get
    End Property

    Public Overloads Shared Function GetItem(ByRef key As String) As String
        Return CStr(GetConfig.Item(key))
    End Function

    Public Overloads Shared Function GetConfig() As IDictionary
        Return GetConfig([Assembly].GetExecutingAssembly())
    End Function

    Public Overloads Shared Function GetConfig(ByVal asm As [Assembly]) As IDictionary
        ' Open and parse configuration file for specified
        ' assembly, returning collection to caller for future
        ' use outside of this class.
        '
        Try
            Dim cfgFile As String = asm.CodeBase + ".config"
            Const nodeName As String = "assemblySettings"

            Dim doc As New XmlDocument
            doc.Load(New XmlTextReader(cfgFile))

            Dim nodes As XmlNodeList = doc.GetElementsByTagName(nodeName)

            Dim node As XmlNode
            For Each node In nodes
                If node.LocalName = nodeName Then
                    Dim handler As New DictionarySectionHandler
                    Return CType(handler.Create(Nothing, Nothing, node), IDictionary)
                End If
            Next node
        Catch
            MsgBox(Err.Description)
        End Try

        Return Nothing
    End Function
End Class
