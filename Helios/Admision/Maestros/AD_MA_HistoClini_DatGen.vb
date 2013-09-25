Public Class AD_MA_HistoClini_DatGen

    Dim Bol_Nuevo As Boolean = False

    Private Sub AD_MA_HistoClini_DatGen_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        Call Formatear_Grilla_Selector(ug_Lista_Hist_Clin)
        Call Inicializar_Estado_Botones_Tool(ToolS_Mantenimiento)
        Call MostrarTabs(0, utc_historia, 0)
        Call Cargar_Combos()
        Call CargarDatos()

    End Sub

    Private Sub Tool_Nuevo_Click(sender As System.Object, e As System.EventArgs) Handles Tool_Nuevo.Click

        Call Cambiar_Estado_Botones_Tool(ToolS_Mantenimiento)
        Call MostrarTabs(1, utc_historia, 1)
        Call Limpiar_Controls_InGroupox(ugb_datos)
        Call Limpiar_Controls_InGroupox(ugb_codigos)

        Bol_Nuevo = True

        If uce_est_civil.Items.Count > 0 Then uce_est_civil.SelectedIndex = 0
        If uce_Nacionalidad.Items.Count > 0 Then uce_Nacionalidad.SelectedIndex = 0
        If uce_tip_doc.Items.Count > 0 Then uce_tip_doc.SelectedIndex = 0

        txt_ape_pat.Focus()

    End Sub

    Private Sub CargarDatos()

        Dim historiaBL As New BL.AdmisionBL.SG_AD_TB_HISTO_CLINI
        ug_Lista_Hist_Clin.DataSource = historiaBL.getHistorias_Base(gInt_IdEmpresa)
        historiaBL = Nothing

    End Sub

    Private Sub Cargar_Combos()

        Dim nacionalidadBL As New BL.AdmisionBL.SG_AD_TB_NACIONALIDAD
        uce_Nacionalidad.DataSource = nacionalidadBL.getNacionalidades(gInt_IdEmpresa)
        uce_Nacionalidad.DisplayMember = "NA_DESCRIPCION"
        uce_Nacionalidad.ValueMember = "NA_ID"
        nacionalidadBL = Nothing

        Dim documentosBL As New BL.AdmisionBL.SG_AD_TB_TIP_DOC_PER
        uce_tip_doc.DataSource = documentosBL.getTiposDocs(gInt_IdEmpresa)
        uce_tip_doc.DisplayMember = "TD_ABREVIATURA"
        uce_tip_doc.ValueMember = "TD_ID"
        documentosBL = Nothing

        Dim sexoBL As New BL.AdmisionBL.SG_AD_TB_SEXO
        uos_sexo.DataSource = sexoBL.getSexos()
        uos_sexo.ValueMember = "SE_ID"
        uos_sexo.DisplayMember = "SE_DESCRIPCION"
        sexoBL = Nothing

        Dim estcivilBL As New BL.AdmisionBL.SG_AD_TB_ESTADO_CIVIL
        uce_est_civil.DataSource = estcivilBL.getEstados(gInt_IdEmpresa)
        uce_est_civil.DisplayMember = "EC_DESCRIPCION"
        uce_est_civil.ValueMember = "EC_ID"
        estcivilBL = Nothing

        'uce_est_civil.Items.Clear()
        'uce_est_civil.Items.Add(1, "SOLTERO")
        'uce_est_civil.Items.Add(2, "CASADO")

    End Sub


    Private Sub Tool_Grabar_Click(sender As System.Object, e As System.EventArgs) Handles Tool_Grabar.Click


        Dim historiaBE As New BE.AdmisionBE.SG_AD_TB_HISTO_CLINI
        Dim historiaBL As New BL.AdmisionBL.SG_AD_TB_HISTO_CLINI

        If historiaBL.Existe_HC(txt_num_doc.Text.Trim, gInt_IdEmpresa) Then
            Avisar("Ya existe la Hist. Clin. con el documento : " & txt_num_doc.Text.Trim)
            Exit Sub
        End If


        With historiaBE
            .HC_NUM_HIST = 0
            .HC_IDCLIENTE = 0
            .HC_NOMBRE1 = txt_nom1.Text.Trim
            .HC_NOMBRE2 = txt_nom2.Text.Trim
            .HC_APE_PAT = txt_ape_pat.Text.Trim
            .HC_APE_MAT = txt_ape_mat.Text.Trim
            .HC_APE_CASADA = txt_ape_cas.Text.Trim
            .HC_TDOC = New BE.AdmisionBE.SG_AD_TB_TIP_DOC_PER With {.TD_ID = uce_tip_doc.Value}
            .HC_NDOC = txt_num_doc.Text.Trim
            .HC_FNAC = udte_fec_nac.Value
            .HC_FING = udte_fec_reg.Value
            .HC_SEXO = uos_sexo.Value
            .HC_EST_CIVIL = uce_est_civil.Value
            .HC_DIR = txt_dir.Text.Trim
            .HC_OCUPACION = txt_ocupacion.Text.Trim
            .HC_IDNACIONALIDAD = uce_Nacionalidad.Value
            .HC_USUARIO = String.Format("{0}-{1}", Environment.UserName, gStr_Usuario_Sis)
            .HC_TERMINAL = Environment.MachineName
            .HC_FECREG = Now.Date
        End With


        historiaBL.Insert(historiaBE, gInt_IdEmpresa)

        historiaBE = Nothing
        historiaBL = Nothing


        Call Avisar("Listo!")
        Call CargarDatos()
        Call Tool_Cancelar_Click(sender, e)


    End Sub

    Private Sub Tool_Editar_Click(sender As System.Object, e As System.EventArgs) Handles Tool_Editar.Click

    End Sub

    Private Sub Tool_Cancelar_Click(sender As System.Object, e As System.EventArgs) Handles Tool_Cancelar.Click
        Call Cambiar_Estado_Botones_Tool(ToolS_Mantenimiento)
        Call MostrarTabs(0, utc_historia, 0)
    End Sub

    Private Sub Tool_Eliminar_Click(sender As System.Object, e As System.EventArgs) Handles Tool_Eliminar.Click

    End Sub

    Private Sub Tool_Salir_Click(sender As System.Object, e As System.EventArgs) Handles Tool_Salir.Click
        Me.Close()
    End Sub

    Private Sub txt_ape_pat_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles txt_ape_pat.KeyDown, udte_fec_reg.KeyDown, udte_fec_nac.KeyDown, uce_tip_doc.KeyDown, uce_est_civil.KeyDown, txt_ocupacion.KeyDown, txt_num_doc.KeyDown, txt_nom2.KeyDown, txt_nom1.KeyDown, txt_dir.KeyDown, txt_ape_mat.KeyDown, txt_ape_cas.KeyDown
        If e.KeyCode = Keys.Enter Then SendKeys.Send(vbTab)
    End Sub

    Private Sub uos_sexo_KeyDown(sender As System.Object, e As System.Windows.Forms.KeyEventArgs) Handles uos_sexo.KeyDown
        If e.KeyCode = Keys.Enter Then
            uce_est_civil.Focus()
        End If
    End Sub
End Class