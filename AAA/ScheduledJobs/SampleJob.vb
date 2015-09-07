Imports System.Threading.Tasks
Imports System.Web.Http
Imports Microsoft.WindowsAzure.Mobile.Service

Public Class SampleJob
    Inherits ScheduledJob

    ' A simple scheduled job which can be invoked manually by submitting an HTTP
    ' POST request to the path "/jobs/sample".
    Public Overrides Function ExecuteAsync() As Task
        Services.Log.Info("Hello from scheduled job!")
        Return Task.FromResult(True)
    End Function
End Class