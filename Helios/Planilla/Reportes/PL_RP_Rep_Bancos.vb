Public Class PL_RP_Rep_Bancos
    Dim comenzar As Boolean = False

    Private Sub PL_RP_Rep_Bancos_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        une_ayo.Value = gDat_Fecha_Sis.Year
        Call Formatear_Grilla_Selector(ug_bancos)
        Call CargarCombo_ConMeses(uce_Mes)
        Call Cargar_TipoPersonal()
        uce_TipoPersonal.SelectedIndex = 0
        comenzar = Not comenzar
    End Sub

    Private Sub Tool_Salir_Click(sender As System.Object, e As System.EventArgs) Handles Tool_Salir.Click
        Me.Close()
    End Sub

    Private Sub Tool_imprimir_Click(sender As System.Object, e As System.EventArgs) Handles Tool_imprimir.Click


        If uce_Mes.SelectedIndex = -1 Then
            Avisar("Seleccione un mes")
            uce_Mes.Focus()
            Exit Sub
        End If

        If uce_TipoPersonal.SelectedIndex = -1 Then
            Avisar("Seleccione un Tipo de Trabajador")
            uce_Mes.Focus()
            Exit Sub
        End If

        Me.Cursor = Cursors.WaitCursor
        Dim dt_tmp As DataTable = CType(ug_bancos.DataSource, DataTable)
        Dim reportesBL As New BL.PlanillaBL.SG_PL_TB_PERSONAL
        Dim str_fecha As String = uce_Mes.Text
        Using crystalBL As New LR.ClsReporte
            crystalBL.Muestra_Reporte(gStr_RutaRep & "\SG_PL_18.rpt", dt_tmp, "", "pEmp;" & gStr_NomEmpresa, _
                                                                              "pMes;" & str_fecha.ToUpper, _
                                                                                "pTipoPer;" & uce_TipoPersonal.Text)
        End Using
        reportesBL = Nothing
        Me.Cursor = Cursors.Default

    End Sub

    Private Sub Cargar_TipoPersonal()
        Dim tipoPersonalBL As New BL.PlanillaBL.SG_PL_TB_TIPO_PERSONAL
        uce_TipoPersonal.DataSource = tipoPersonalBL.getTipos()
        uce_TipoPersonal.ValueMember = "TP_ID"
        uce_TipoPersonal.DisplayMember = "TP_DESCRIPCION"
        tipoPersonalBL = Nothing
    End Sub

    Private Sub uce_Mes_ValueChanged(sender As System.Object, e As System.EventArgs) Handles uce_Mes.ValueChanged
        Call Cargar_Data_para_Bancos()
    End Sub

    Private Sub uce_TipoPersonal_ValueChanged(sender As System.Object, e As System.EventArgs) Handles uce_TipoPersonal.ValueChanged
        Call Cargar_Data_para_Bancos()
    End Sub

    Private Sub Cargar_Data_para_Bancos()

        If uce_Mes.SelectedIndex = -1 Then Exit Sub
        If uce_TipoPersonal.SelectedIndex = -1 Then Exit Sub

        Dim reportesBL As New BL.PlanillaBL.SG_PL_Reportes
        ug_bancos.DataSource = reportesBL.get_Reporte_para_Bancos(une_ayo.Value, uce_Mes.Value, uce_TipoPersonal.Value, gInt_IdEmpresa, uos_Periodo.Value, uos_Neto.Value)
        reportesBL = Nothing

    End Sub

    Private Sub une_ayo_ValueChanged(sender As System.Object, e As System.EventArgs) Handles une_ayo.ValueChanged
        Call Cargar_Data_para_Bancos()
    End Sub

    Private Sub uos_Neto_ValueChanged(sender As System.Object, e As System.EventArgs) Handles uos_Neto.ValueChanged
        Call Cargar_Data_para_Bancos()
    End Sub

    Private Sub uos_Periodo_ValueChanged(sender As System.Object, e As System.EventArgs) Handles uos_Periodo.ValueChanged
        Call Cargar_Data_para_Bancos()
    End Sub
End Class