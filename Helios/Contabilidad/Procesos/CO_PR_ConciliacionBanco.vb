﻿Public Class CO_PR_ConciliacionBanco

    Private Sub CO_PR_ConciliacionBanco_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        une_Ayo.Value = gDat_Fecha_Sis.Year
        Call CargarCombo_ConMeses(uce_Mes)
        Call Cargar_Combos_Bancos()
        uce_Mes.Value = gDat_Fecha_Sis.Month
    End Sub

    Private Sub Tool_salir_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Tool_salir.Click
        Me.Close()
    End Sub

    Private Sub Tool_Consultar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Tool_Consultar.Click
        Call Cargar_Movimientos()
    End Sub

    Private Sub Cargar_Combos_Bancos()
        Try

            Dim BancosBL As New BL.ContabilidadBL.SG_CO_TB_BANCO

            uce_Bancos.DataSource = BancosBL.getBancos(New BE.ContabilidadBE.SG_CO_TB_BANCO With {.BA_IDEMPRESA = New BE.ContabilidadBE.SG_CO_TB_EMPRESA With {.EM_ID = gInt_IdEmpresa}})
            uce_Bancos.DisplayMember = "BA_NOMBRE"
            uce_Bancos.ValueMember = "BA_ID"

            BancosBL = Nothing

            uce_Bancos.SelectedIndex = -1
            uce_CtasCorrientes.SelectedIndex = -1
        Catch ex As Exception
            ShowError(ex.Message)
        End Try
    End Sub

    Private Sub Cargar_Movimientos()
        Try
            Dim BancosBL As New BL.ContabilidadBL.SG_CO_TB_BANCO
            Dim dt As DataTable = BancosBL.getMovimientos_de_Banco(une_Ayo.Value, uce_Mes.Value, IIf(uchk_Conciliados.Checked, 1, 0), uce_CtasCorrientes.Value, gInt_IdEmpresa)

            Call LimpiaGrid(ug_MovBancarios, uds_MovBancarios)

            For i As Integer = 0 To dt.Rows.Count - 1
                ug_MovBancarios.DisplayLayout.Bands(0).AddNew()
                ug_MovBancarios.Rows(i).Cells("AD_IDDET").Value = dt.Rows(i)("AD_IDDET")
                ug_MovBancarios.Rows(i).Cells("AC_FEC_VOUCHER").Value = dt.Rows(i)("AC_FEC_VOUCHER")
                ug_MovBancarios.Rows(i).Cells("AD_TDOC").Value = dt.Rows(i)("AD_TDOC")
                ug_MovBancarios.Rows(i).Cells("AD_NDOC").Value = dt.Rows(i)("AD_NDOC")
                ug_MovBancarios.Rows(i).Cells("AD_DEBE").Value = dt.Rows(i)("AD_DEBE")
                ug_MovBancarios.Rows(i).Cells("AD_HABER").Value = dt.Rows(i)("AD_HABER")
                ug_MovBancarios.Rows(i).Cells("AD_GLOSA").Value = dt.Rows(i)("AD_GLOSA")
                ug_MovBancarios.Rows(i).Cells("AD_ES_CONCI").Value = IIf(dt.Rows(i)("AD_ES_CONCI") = 1, True, False)
                ug_MovBancarios.Rows(i).Cells("AD_ANHO_CONI").Value = dt.Rows(i)("AD_ANHO_CONI")
                ug_MovBancarios.Rows(i).Cells("AD_MES_CONCI").Value = dt.Rows(i)("AD_MES_CONCI")
                If dt.Rows(i)("DO_COLOR_DOC").ToString.Equals(String.Empty) Then
                    ug_MovBancarios.Rows(i).Appearance.BackColor = Color.Empty
                Else
                    ug_MovBancarios.Rows(i).Appearance.BackColor = Color.FromName(dt.Rows(i)("DO_COLOR_DOC"))
                End If

            Next

            ug_MovBancarios.UpdateData()

            dt.Dispose()

            'traemos el saldo anterios, y calculamos el saldo final
            Dim SaldoBL As New BL.ContabilidadBL.SG_CO_TB_SALDO_MOV_BANCO
            Dim SaldoBE As New BE.ContabilidadBE.SG_CO_TB_SALDO_MOV_BANCO

            With SaldoBE
                .SMB_ANHO = une_Ayo.Value
                .SMB_MES = CInt(uce_Mes.Value) - 1
                .SMB_CUENTA = uce_CtasCorrientes.Value
                .SMB_IDEMPRESA = New BE.ContabilidadBE.SG_CO_TB_EMPRESA With {.EM_ID = gInt_IdEmpresa}
            End With

            ume_saldoAnterior.Value = SaldoBL.getSaldo_Anterior(SaldoBE)


            ume_sum_debe.Value = 0
            ume_sum_haber.Value = 0
            SaldoBE.SMB_MES = CInt(uce_Mes.Value)

            'sumamos los Movimientos del mes 
            Dim sumas_mes As List(Of Double) = BancosBL.getSuma_Mov_Conciliados_x_Mes(SaldoBE)
            ume_sum_debe.Value = sumas_mes(0)
            ume_sum_haber.Value = sumas_mes(1)





            SaldoBL = Nothing
            sumas_mes = Nothing
            SaldoBE = Nothing
            BancosBL = Nothing


            'Calculamos el saldo final del mes 
            ume_SAldoFinMes.Value = CDbl(ume_saldoAnterior.Value) + (CDbl(ume_sum_debe.Value) - CDbl(ume_sum_haber.Value))
            ume_SAldoFinMes.Appearance.ForeColor = Color.Navy

            If CDbl(ume_SAldoFinMes.Value) < 0 Then
                ume_SAldoFinMes.Appearance.ForeColor = Color.Red
            End If

            ulbl_total_filas.Text = "Total Registros : " & ug_MovBancarios.Rows.Count.ToString
            If ug_MovBancarios.Rows.Count > 0 Then
                ug_MovBancarios.Focus()
                ug_MovBancarios.Rows(0).Activate()
            End If


        Catch ex As Exception
            ShowError(ex.Message)
        End Try
    End Sub

    Private Sub Tool_Guardar_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Tool_Guardar.Click
        Try

            If Not Preguntar("Seguro de Grabar?") Then Exit Sub

            Dim BancosBL As New BL.ContabilidadBL.SG_CO_TB_BANCO
            Dim Lista As New List(Of BE.ContabilidadBE.SG_CO_TB_ASIENTO_DET)
            Dim conciliado As BE.ContabilidadBE.SG_CO_TB_ASIENTO_DET

            ug_MovBancarios.UpdateData()

            For i As Integer = 0 To ug_MovBancarios.Rows.Count - 1
                conciliado = New BE.ContabilidadBE.SG_CO_TB_ASIENTO_DET()
                If ug_MovBancarios.Rows(i).Cells("AD_ES_CONCI").Value Then conciliado.AD_ES_CONCI = 1
                conciliado.AD_IDDET = ug_MovBancarios.Rows(i).Cells("AD_IDDET").Value
                conciliado.AD_ANHO_CONI = une_Ayo.Value
                conciliado.AD_MES_CONCI = uce_Mes.Value
                Lista.Add(conciliado)
            Next

            BancosBL.Update_Conciliacion(Lista)

            Avisar("Listo!")


            conciliado = Nothing
            Lista = Nothing
            BancosBL = Nothing
        Catch ex As Exception
            ShowError(ex.Message)
        End Try
    End Sub

    Private Sub uce_Bancos_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles uce_Bancos.ValueChanged
        Try
            Dim CtasCorrientesBL As New BL.ContabilidadBL.SG_CO_TB_BANCO_CTACTE
            Dim BancoBE As New BE.ContabilidadBE.SG_CO_TB_BANCO_CTACTE With {.BC_IDBANCO = New BE.ContabilidadBE.SG_CO_TB_BANCO With {.BA_ID = uce_Bancos.Value}}

            Dim cuentas As New List(Of BE.ContabilidadBE.SG_CO_TB_BANCO_CTACTE)
            cuentas = CtasCorrientesBL.getCtasCorrientes(BancoBE)

            uce_CtasCorrientes.Items.Clear()

            For Each cuenta As BE.ContabilidadBE.SG_CO_TB_BANCO_CTACTE In cuentas
                uce_CtasCorrientes.Items.Add(cuenta.BC_IDCUENTA.PC_IDCUENTA, String.Format("{0} - {1}", cuenta.BC_NUMERO_CTA, cuenta.BC_DESCRIPCION))
            Next

            'uce_CtasCorrientes.DataSource = CtasCorrientesBL.getCtasCorrientes(BancoBE)
            'uce_CtasCorrientes.DisplayMember = "BC_ID_CTA"
            'uce_CtasCorrientes.ValueMember = "BC_DESCRIPCION"

            cuentas = Nothing
            CtasCorrientesBL = Nothing
            BancoBE = Nothing

        Catch ex As Exception
            ShowError(ex.Message)
        End Try
    End Sub

    Private Sub uce_CtasCorrientes_ValueChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles uce_CtasCorrientes.ValueChanged
        Try

            If une_Ayo.Value.ToString = String.Empty Then Exit Sub
            If uce_Mes.SelectedIndex = -1 Then Exit Sub
            If uce_Bancos.SelectedIndex = -1 Then Exit Sub

            Call Cargar_Movimientos()

        Catch ex As Exception
            ShowError(ex.Message)
        End Try
    End Sub

    Private Sub Tool_cerrarMes_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Tool_cerrarMes.Click
        Try

            If Not Preguntar("Esta seguro de grabar?") Then
                Exit Sub
            End If

            Dim SaldoBL As New BL.ContabilidadBL.SG_CO_TB_SALDO_MOV_BANCO
            Dim SaldoBE As New BE.ContabilidadBE.SG_CO_TB_SALDO_MOV_BANCO
            SaldoBE.SMB_ANHO = une_Ayo.Value
            SaldoBE.SMB_MES = uce_Mes.Value
            SaldoBE.SMB_CUENTA = uce_CtasCorrientes.Value
            SaldoBE.SMB_IDEMPRESA = New BE.ContabilidadBE.SG_CO_TB_EMPRESA With {.EM_ID = gInt_IdEmpresa}
            SaldoBE.SMB_SALDO = ume_SAldoFinMes.Value
            SaldoBL.Insert(SaldoBE)

            Avisar("Listo!")

            SaldoBE = Nothing
            SaldoBL = Nothing

        Catch ex As Exception
            ShowError(ex.Message)
        End Try
    End Sub

    Private Sub uchk_Conciliados_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles uchk_Conciliados.CheckedChanged
        Call Cargar_Movimientos()
    End Sub
End Class