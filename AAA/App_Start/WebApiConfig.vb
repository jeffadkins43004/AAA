Imports System
Imports System.Collections.Generic
Imports System.Collections.ObjectModel
Imports System.Data.Entity
Imports System.Web.Http
Imports Microsoft.WindowsAzure.Mobile.Service

Public Module WebApiConfig
    Public Sub Register()
        ' Use this class to set configuration options for your mobile service
        Dim options As ConfigOptions = New ConfigOptions()

        ' Use this class to set WebAPI configuration options 
        Dim config As HttpConfiguration = ServiceConfig.Initialize(New ConfigBuilder(options))

        ' To display errors in the browser during development, uncomment the following
        ' line. Comment it out again when you deploy your service for production use.
        ' config.IncludeErrorDetailPolicy = IncludeErrorDetailPolicy.Always

        Database.SetInitializer(New MobileServiceInitializer())

    End Sub
End Module

Public Class MobileServiceInitializer
    Inherits DropCreateDatabaseIfModelChanges(Of MobileServiceContext)

    Protected Overrides Sub Seed(ByVal context As MobileServiceContext)
        Dim todoItems As List(Of TodoItem) = New List(Of TodoItem) From
            {
                New TodoItem With {.Id = Guid.NewGuid().ToString(), .Text = "First item", .Complete = False},
                New TodoItem With {.Id = Guid.NewGuid().ToString(), .Text = "Second item", .Complete = False}
            }

        For Each todoItem As TodoItem In todoItems
            context.Set(Of TodoItem).Add(todoItem)
        Next

        MyBase.Seed(context)

    End Sub

End Class



