Imports Microsoft.ApplicationBlocks.Data
Imports System.Data.SqlClient
Imports System.Data

Public Class AdmisionBL

    Public Class SG_AD_TB_SEXO
        Inherits ClsBD


        Public Function getSexos() As DataTable
            Return SqlHelper.ExecuteDataset(Cn, "SG_AD_SP_S_SEXO").Tables(0)
        End Function
    End Class

    Public Class SG_AD_TB_TIP_DOC_PER
        Inherits ClsBD

        Public Function getTiposDocs(empresa_ As Integer) As DataTable
            Return SqlHelper.ExecuteDataset(Cn, "SG_AD_SP_S_TIP_DOC_PER", empresa_).Tables(0)
        End Function

        Public Function getTiposDocs_x_Id(id_ As Integer, empresa_ As Integer) As DataTable
            Return SqlHelper.ExecuteDataset(Cn, "SG_AD_SP_S_TIPDOC_PER_BYID", id_, empresa_).Tables(0)
        End Function

    End Class

    Public Class SG_AD_TB_ESTADO_CIVIL
        Inherits ClsBD

        Public Function getEstados(empresa_ As Integer) As DataTable
            Return SqlHelper.ExecuteDataset(Cn, "SG_AD_SP_S_ESTADO_CIVIL", empresa_).Tables(0)
        End Function

    End Class

    Public Class SG_AD_TB_NACIONALIDAD
        Inherits ClsBD

        Public Function getNacionalidades(empresa_ As Integer) As DataTable
            Return SqlHelper.ExecuteDataset(Cn, "SG_AD_SP_S_NACIONALIDAD", empresa_).Tables(0)
        End Function

    End Class


    Public Class SG_AD_TB_HISTO_CLINI
        Inherits ClsBD

        Public Function getHistorias_Base(empresa_ As Integer) As DataTable
            Return SqlHelper.ExecuteDataset(Cn, "SG_AD_SP_S_HISTCLIN_BASE", empresa_).Tables(0)
        End Function

        Public Sub Insert(Entidad As BE.AdmisionBE.SG_AD_TB_HISTO_CLINI, empresa_ As Integer)

            Dim IdCliente As Integer = 0
            Dim dt_numdoc_tmp As DataTable = Nothing
            Dim nombre As String = Entidad.HC_NOMBRE1.ToString & " " & Entidad.HC_NOMBRE2 & " " & Entidad.HC_APE_PAT & " " & Entidad.HC_APE_MAT & " " & Entidad.HC_APE_CASADA
            dt_numdoc_tmp = SqlHelper.ExecuteDataset(Cn, "SG_AD_SP_S_CLIENTE_X_DOC", Entidad.HC_NDOC, empresa_).Tables(0)

            'verifica si existe el cliente en facturacion
            If dt_numdoc_tmp.Rows.Count > 0 Then

                IdCliente = dt_numdoc_tmp.Rows(0)("CL_ID")
            Else

                'graba el cliente en facturacion
                IdCliente = SqlHelper.ExecuteScalar(Cn, "SG_FA_SP_I_CLIENTE", nombre, Entidad.HC_TDOC.TD_ID, Entidad.HC_NDOC, _
                                          Entidad.HC_DIR, 0, Entidad.HC_USUARIO, _
                                          Entidad.HC_TERMINAL, Entidad.HC_FECREG, empresa_, 1, "", 0)

            End If

            dt_numdoc_tmp.Dispose()

            'si tiene numero de documento lo registramos en contabilidad
            If Entidad.HC_NDOC.ToString.Length > 0 Then

                Dim anexoBL As New BL.ContabilidadBL.SG_CO_TB_ANEXO
                Dim anexoBE As New BE.ContabilidadBE.SG_CO_TB_ANEXO
                Dim cod_doc_conta As Integer = 0
                Dim documentoBL As New BL.AdmisionBL.SG_AD_TB_TIP_DOC_PER
                Dim dt_tmp As DataTable = documentoBL.getTiposDocs_x_Id(Entidad.HC_TDOC.TD_ID, empresa_)
                If dt_tmp.Rows.Count > 0 Then
                    cod_doc_conta = dt_tmp.Rows(0)("TD_COD_CONTA")
                Else
                    cod_doc_conta = 1
                End If
                dt_tmp.Dispose()
                documentoBL = Nothing

                anexoBE.AN_IDANEXO = 0
                anexoBE.AN_DESCRIPCION = nombre
                anexoBE.AN_ES_RELACIONADO = 0
                anexoBE.AN_IDEMPRESA = New BE.ContabilidadBE.SG_CO_TB_EMPRESA With {.EM_ID = empresa_}
                anexoBE.AN_NUM_DOC = Entidad.HC_NDOC
                anexoBE.AN_PC_FECREG = Entidad.HC_FECREG
                anexoBE.AN_PC_TERMINAL = Entidad.HC_TERMINAL
                anexoBE.AN_PC_USUARIO = Entidad.HC_USUARIO
                anexoBE.AN_TIPO_ANEXO = New BE.ContabilidadBE.SG_CO_TB_TIPOANEXO With {.TA_CODIGO = BE.ContabilidadBE.TipoA.Cliente}
                anexoBE.AN_TIPO_DOC = New BE.ContabilidadBE.SG_CO_TB_TIPO_DOC_IDENTIDAD With {.DI_CODIGO = cod_doc_conta}
                anexoBE.AN_TIPO_EMPRESA = New BE.ContabilidadBE.SG_CO_TB_TIPOEMPRESA With {.TE_CODIGO = BE.ContabilidadBE.TipoEmpresa.Natural}

                anexoBL.Insert_x_Admision(anexoBE)

                anexoBE = Nothing
                anexoBL = Nothing

            End If



            
                With Entidad
                    SqlHelper.ExecuteNonQuery(Cn, "SG_AD_SP_I_HISTO_CLINI", IdCliente, .HC_NOMBRE1, .HC_NOMBRE2, .HC_APE_PAT, .HC_APE_MAT, .HC_APE_CASADA, .HC_TDOC.TD_ID, .HC_NDOC, .HC_FNAC, .HC_FING, .HC_SEXO, .HC_EST_CIVIL, .HC_DIR, .HC_OCUPACION, .HC_IDNACIONALIDAD, .HC_USUARIO, .HC_TERMINAL, .HC_FECREG)
                End With


            

        End Sub

        Public Function Existe_HC(unmdoc_ As String, empresa_ As Integer) As Boolean
            'verificar que no se dupliquen las historias por el numero de documento DNI,ruc,ce,etc

            Dim rpta As Boolean = False

            If SqlHelper.ExecuteScalar(Cn, "SG_AD_SP_S_EXIST_HC", unmdoc_, empresa_) > 0 Then
                rpta = Not rpta
            End If

            Return rpta

        End Function

        Public Sub Update(Entidad As BE.AdmisionBE.SG_AD_TB_HISTO_CLINI)
            With Entidad
                SqlHelper.ExecuteNonQuery(Cn, "SG_AD_SP_I_HISTO_CLINI", .HC_NUM_HIST, .HC_IDCLIENTE, .HC_NOMBRE1, .HC_NOMBRE2, .HC_APE_PAT, .HC_APE_MAT, .HC_APE_CASADA, .HC_TDOC, .HC_NDOC, .HC_FNAC, .HC_FING, .HC_SEXO, .HC_EST_CIVIL, .HC_DIR, .HC_OCUPACION, .HC_IDNACIONALIDAD, .HC_USUARIO, .HC_TERMINAL, .HC_FECREG)
            End With
        End Sub

    End Class


End Class
