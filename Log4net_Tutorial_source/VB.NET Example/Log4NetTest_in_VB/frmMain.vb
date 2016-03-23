'Here is the once-per-application setup information
<Assembly: log4net.Config.XmlConfigurator(Watch:=True)> 

Public Class frmMain

    'Here is the once-per-class call to initialize the log object
    Private Shared ReadOnly log As log4net.ILog = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType)

    Private Sub frmMain_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        'You should try to call the logger as soon as possible in your application
        log.Debug("Application started")
    End Sub

    'This method hooks to the button on the start-up form.  When
    'you click the button, you call every type of log method available
    Private Sub butRunTest_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles butRunTest.Click

        'Test all normal logging methods
        log.Debug("Debug logging")
        log.Info("Info logging")
        log.Warn("Warn logging")
        log.Error("Error logging")
        log.Fatal("Fatal logging")

        'Test the logging in a different class
        Dim testObject As New OtherClass
        testObject.TestLogger()

        'Tests the logging methods with the optional exception parameter
        Try
            Throw New System.IO.FileNotFoundException()
        Catch ex As Exception
            log.Debug("Debug error logging", ex)
            log.Info("Info error logging", ex)
            log.Warn("Warn error logging", ex)
            log.Error("Error error logging", ex)
            log.Fatal("Fatal error logging", ex)
        End Try

    End Sub

End Class

'This is our second class so that we can show how the logger can act differently when we change classes or namespaces
Public Class OtherClass

    'Here is the once-per-class call to initialize the log object
    Private Shared ReadOnly log As log4net.ILog = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType)

    'This is a test method to show how the logger can listen at different levels to different classes or namespaces
    Public Sub TestLogger()

        log4net.GlobalContext.Properties("testProperty") = "This is my test property information"

        log.Debug("Other Class - Debug logging")
        log.Info("Other Class - Info logging")
        log.Warn("Other Class - Warn logging")
        log.Error("Other Class - Error logging")
        log.Fatal("Other Class - Fatal logging")

    End Sub

End Class