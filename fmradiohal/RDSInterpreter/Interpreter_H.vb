Option Strict On
Namespace RDSInterpreter
    Public Interface I_Interpreter
        Property BlocksCounter() As Long()
        Property CurrentProgram() As RDSInterpreter.ProgramIdentifier
        Sub decode(ByRef rdsGroupData As FMRadio.FMRadioHAL.stRDSRAWMessage)
        ReadOnly Property EEONList() As ProgramListDictionary
    End Interface 'I_Interpreter

    <System.Runtime.InteropServices.InterfaceTypeAttribute(Runtime.InteropServices.ComInterfaceType.InterfaceIsIDispatch)> _
    Public Interface I_Interpreter_Events
        Sub ProgramTypeNameChange(ByVal oldText As Char())
        Sub RadioTextChange(ByVal oldText As Char())
        'Sub AnyEvent(ByRef Anything As Object) 'this Event is for expandibility (if you need a special Event for your frontend which your radio supports!)

    End Interface

End Namespace
