Public Class CO_PR_PLE

    Private Sub CO_PR_PLE_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Call CargarCombo_ConMeses(uce_Mes)
        une_Ayo.Value = gDat_Fecha_Sis.Year
        uce_Mes.Value = gDat_Fecha_Sis.Month
    End Sub


    Private Sub Tool_Salir_Click(sender As System.Object, e As System.EventArgs) Handles Tool_Salir.Click
        Me.Close()
    End Sub

    Private Sub Tool_Generar_Click(sender As System.Object, e As System.EventArgs) Handles Tool_Generar.Click
        Call Generar_Archivos_ple()
    End Sub


    Private Sub Generar_Archivos_ple()

        If uchk_RegCompras.Checked Then Call Generar_Archivo_Compras()
        If uchk_RegVentas.Checked Then Call Generar_Archivo_Ventas()
        If uchk_LibDiario.Checked Then Call Generar_Archivo_Libro_Diario()
        If uchk_LibMayor.Checked Then Call Generar_Archivo_Libro_Mayor()

        Call Avisar("Listo!")

    End Sub

    Private Sub Generar_Archivo_Compras()
        Dim reportesBL As New BL.ContabilidadBL.SG_CO_Reportes_Registros
        reportesBL.get_PLE_RegCompras(une_Ayo.Value, uce_Mes.Value, gInt_IdEmpresa, gStr_RutaRep & "\PLE")
        reportesBL = Nothing
    End Sub

    Private Sub Generar_Archivo_Ventas()
        Dim reportesBL As New BL.ContabilidadBL.SG_CO_Reportes_Registros
        reportesBL.get_PLE_RegVentas(une_Ayo.Value, uce_Mes.Value, gInt_IdEmpresa, gStr_RutaRep & "\PLE")
        reportesBL = Nothing
    End Sub

    Private Sub Generar_Archivo_Libro_Diario()
        Dim reportesBL As New BL.ContabilidadBL.SG_CO_Reportes_Registros
        reportesBL.get_PLE_Diario(une_Ayo.Value, uce_Mes.Value, gInt_IdEmpresa, gStr_RutaRep & "\PLE")
        reportesBL = Nothing
    End Sub

    Private Sub Generar_Archivo_Libro_Mayor()
        Dim reportesBL As New BL.ContabilidadBL.SG_CO_Reportes_Registros
        reportesBL.get_PLE_Mayor(une_Ayo.Value, uce_Mes.Value, gInt_IdEmpresa, gStr_RutaRep & "\PLE")
        reportesBL = Nothing
    End Sub


    Private Sub Tool_Abrir_Click(sender As System.Object, e As System.EventArgs) Handles Tool_Abrir.Click
        Process.Start("explorer.exe", gStr_RutaRep & "\PLE")
    End Sub

End Class