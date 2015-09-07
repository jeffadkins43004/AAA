Imports System.Web.Http
Imports System.Web.Routing

Public Class WebApiApplication
    Inherits System.Web.HttpApplication

    Sub Application_Start()
        WebApiConfig.Register()
    End Sub
End Class