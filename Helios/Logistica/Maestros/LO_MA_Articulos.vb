Public Class LO_MA_Articulos

    Private Sub LO_MA_Articulos_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

    End Sub

    Private Sub Cargar_UMs()
        Dim uniMedBL As New BL.LogisticaBL.SG_LO_TB_UNI_MED

        uce_um_compra.DataSource = uniMedBL.getMedidas(gInt_IdEmpresa)
        uce_um_compra.DisplayMember = "UM_DESCRIPCION"
        uce_um_compra.ValueMember = "UM_ID"

        uce_um_venta.DataSource = uniMedBL.getMedidas(gInt_IdEmpresa)
        uce_um_venta.DisplayMember = "UM_DESCRIPCION"
        uce_um_venta.ValueMember = "UM_ID"

        uce_um_distri.DataSource = uniMedBL.getMedidas(gInt_IdEmpresa)
        uce_um_distri.DisplayMember = "UM_DESCRIPCION"
        uce_um_distri.ValueMember = "UM_ID"

        uce_um_peso.DataSource = uniMedBL.getMedidas(gInt_IdEmpresa)
        uce_um_peso.DisplayMember = "UM_DESCRIPCION"
        uce_um_peso.ValueMember = "UM_ID"

        uniMedBL = Nothing

    End Sub

    Private Sub Cargar_Marca()
        Dim marcaBL As New BL.LogisticaBL.SG_LO_TB_MARCA
        uce_marca.DataSource = marcaBL.getMarcas(gInt_IdEmpresa)
        uce_marca.DisplayMember = "MA_DESCRIPCION"
        uce_marca.ValueMember = "MA_ID"
        marcaBL = Nothing
    End Sub

    Private Sub Cargar_Fabricante()
        Dim fabriBL As New BL.LogisticaBL.SG_LO_TB_FABRICANTE
        uce_fabricante.DataSource = fabriBL.getFabricantes(gInt_IdEmpresa)
        uce_fabricante.DisplayMember = "FA_DESCRIPCION"
        uce_fabricante.ValueMember = "FA_ID"
        fabriBL = Nothing
    End Sub

    Private Sub Cargar_Paises()
        Dim paisBL As New BL.LogisticaBL.SG_LO_TB_PAIS
        uce_pais.DataSource = paisBL.getPaises(gInt_IdEmpresa)
        uce_pais.ValueMember = "PA_ID"
        uce_pais.DisplayMember = "PA_DESCRIPCION"
        paisBL = Nothing

    End Sub


    Private Sub Cargar_Ubicaciones()
        Dim ubiBL As New BL.LogisticaBL.SG_LO_TB_UBICACION_ART
        uce_Ubicaciones.DataSource = ubiBL.getUbicaciones(gInt_IdEmpresa)
        uce_Ubicaciones.DisplayMember = "UA_DESCRIPCION"
        uce_Ubicaciones.ValueMember = "UA_ID"
        ubiBL = Nothing
    End Sub

    Private Sub Cargar_Grupos()
        Dim grupoBL As New BL.LogisticaBL.SG_LO_TB_GRUPO_ARTICULO

        uce_GrupoArt.DataSource = grupoBL.getGrupos(gInt_IdEmpresa)
        uce_GrupoArt.DisplayMember = "GA_DESCRIPCION"
        uce_GrupoArt.ValueMember = "GA_ID"

        grupoBL = Nothing
    End Sub
End Class