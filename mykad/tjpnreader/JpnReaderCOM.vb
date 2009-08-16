<ComClass(JpnReaderCOM.ClassId, JpnReaderCOM.InterfaceId, JpnReaderCOM.EventsId)> _
Public Class JpnReaderCOM

#Region "COM GUIDs"
    ' These  GUIDs provide the COM identity for this class 
    ' and its COM interfaces. If you change them, existing 
    ' clients will no longer be able to access the class.
    Public Const ClassId As String = "270fd7af-ebed-4979-aca9-4acbffba6c9d"
    Public Const InterfaceId As String = "6683080e-348b-47f3-b7d2-ccb3797c92b7"
    Public Const EventsId As String = "7b975b16-56bd-48cf-9dec-8f6cbf436aa3"
#End Region

    Private service As JpnReaderService = New JpnReaderService()
    ' A creatable COM class must have a Public Sub New() 
    ' with no parameters, otherwise, the class will not be 
    ' registered in the COM registry and cannot be created 
    ' via CreateObject.
    Public Sub New()
        MyBase.New()
    End Sub

    Public Function JPN_init() As Integer
        Return service.init()
    End Function

    Public Sub JPN_cleanup()
        service.cleanUp()
    End Sub

    Public Function readTextInfo() As Boolean
        Return service.readTextInfo()
    End Function

    Public Function getId() As String
        Return service.icno
    End Function
End Class


