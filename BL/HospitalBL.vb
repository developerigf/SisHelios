Imports System.Data
Imports System.Data.SqlClient
Imports System.Globalization
Imports Microsoft.ApplicationBlocks.Data

Public Class HospitalBL

    Public Class Funciones
        Inherits ClsBD

        Public Function get_Personal(ByVal empresa As Integer) As DataTable
            get_Personal = Nothing
            Dim query As String = "SELECT PE_ID,PE_CODIGO,PE_APE_PAT,PE_APE_MAT,PE_NOM_PRI+' '+PE_NOM_SEG AS NOMBRE,TT_DESCRIPCION "
            query = query & "FROM SG_PL_TB_PERSONAL A "
            query = query & "left JOIN (SELECT * FROM SG_PL_TB_TIPO_PERSO_TARIFA WHERE TT_ID_EMPRESA = " & empresa & ") B "
            query = query & "ON A.PE_ID_TIPO_PERSO_TARIFA = B.TT_ID "
            query = query & "WHERE PE_ID_EMPRESA = " & empresa & " AND PE_ID_TIPO_PER = 2 order by 3"


            Return SqlHelper.ExecuteDataset(Cn, CommandType.Text, query).Tables(0)

        End Function


        Public Function get_Area_Personal(ByVal idpersonal_ As Integer, ByVal empresa_ As Integer) As String
            Dim rpta As String = String.Empty
            Dim myCmd As New SqlCommand
            Dim str_query As String = "select PE_ID_AREA from SG_PL_TB_personal where pe_id = " & idpersonal_.ToString & " and pe_id_empresa = " & empresa_.ToString
            Dim drr As SqlDataReader

            drr = SqlHelper.ExecuteReader(Cn, CommandType.Text, str_query)

            If drr.HasRows Then
                Do While drr.Read()
                    rpta = drr("PE_ID_AREA")
                Loop
            End If
            drr.Close()

            Return rpta
        End Function

        Public Function get_Suma_Total_Horas(ByVal idpersonal_ As Integer, ByVal ayo_ As Integer, ByVal mes_ As Integer, ByVal empresa_ As Integer) As List(Of String)
            get_Suma_Total_Horas = Nothing

            Dim myCmd As New SqlCommand
            Dim drr As SqlDataReader

            Dim fecha_tmp As Date = "01/" & mes_.ToString.PadLeft(2, "0") & "/" & ayo_.ToString
            Dim fecha_ini As Date = "25/" & fecha_tmp.AddMonths(-1).Month.ToString.PadLeft(2, "0") & "/" & fecha_tmp.AddMonths(-1).Year.ToString  'ObtenerPrimerDia(fecha_tmp)
            Dim fecha_fin As Date = "24/" & fecha_tmp.Month.ToString.PadLeft(2, "0") & "/" & fecha_tmp.Year.ToString 'ObtenerUltimoDia(fecha_tmp)

            drr = SqlHelper.ExecuteReader(Cn, "SG_PL_SP_S_MARCAS_X_RANGO", idpersonal_, CDate(fecha_ini), CDate(fecha_fin))


            Dim tiempo_Acumulado As New TimeSpan(0, 0, 0)
            Dim tiempo_extra As New TimeSpan(0, 0, 0)

            If drr.HasRows Then
                Do While drr.Read()

                    Dim marcacion As String = drr("MA_TIEMPO").ToString
                    Dim indice As Integer = marcacion.IndexOf(":")


                    Dim hora As Integer = marcacion.Substring(0, indice)
                    Dim minuto As Integer = marcacion.Remove(0, indice + 1)

                    Dim tiempo_tmp As New TimeSpan(hora, minuto, 0)

                    tiempo_Acumulado = tiempo_Acumulado.Add(tiempo_tmp)

                Loop
            End If
            drr.Close()


            If (tiempo_Acumulado.Days * 24) + tiempo_Acumulado.Hours > 150 Then
                Dim tiempo_fijo As New TimeSpan(150, 0, 0)
                tiempo_extra = tiempo_Acumulado.Subtract(tiempo_fijo)
            End If

            tiempo_Acumulado = tiempo_Acumulado.Subtract(tiempo_extra)

            Dim hora_n As Integer = (tiempo_Acumulado.Days * 24) + tiempo_Acumulado.Hours
            Dim minutos_n As Integer = tiempo_Acumulado.Minutes

            Dim hora_ex As Integer = (tiempo_extra.Days * 24) + tiempo_extra.Hours
            Dim minutos_ex As Integer = tiempo_extra.Minutes


            Dim lista_horas As New List(Of String)

            lista_horas.Add(hora_n & " : " & minutos_n.ToString.PadLeft(2, "0"))
            lista_horas.Add(hora_ex & " : " & minutos_ex.ToString.PadLeft(2, "0"))

            Return lista_horas


        End Function

        Public Function get_Servicios(ByVal empresa_ As Integer) As SqlDataReader
            get_Servicios = Nothing

            Dim sbquery As New System.Text.StringBuilder
            sbquery.Append("SELECT TI_ID, cast(TI_ID as varchar(1) )+' - '+TI_DESCRIPCION AS 'TI_DESCRIPCION' FROM SG_PL_TB_TIPO_TARIFA WHERE TI_ID_EMPRESA = " & empresa_.ToString)

            Return SqlHelper.ExecuteReader(Cn, CommandType.Text, sbquery.ToString)

        End Function

        Public Function get_UltimoDiaMarcacion(ByVal idpersonal_ As Integer, ByVal ayo_ As Integer, ByVal mes_ As Integer) As List(Of String)
            Dim lsita_tmp As New List(Of String)

            Dim query As String = "SELECT TOP 1 MA_FECHA,MA_ITEM FROM SG_PL_TB_MARCA_ASIS WHERE MA_IDPERSONAL = " & idpersonal_ & " AND MONTH(MA_FECHA) = " & mes_.ToString & " AND YEAR(MA_FECHA) = " & ayo_.ToString & " ORDER BY MA_FECHA DESC,MA_ITEM DESC"
            Dim dt_tmp As New DataTable

            dt_tmp = SqlHelper.ExecuteDataset(Cn, CommandType.Text, query).Tables(0)

            If dt_tmp.Rows.Count > 0 Then
                lsita_tmp.Add(dt_tmp.Rows(0)("MA_FECHA"))
                lsita_tmp.Add(dt_tmp.Rows(0)("MA_ITEM").ToString)
            End If

            dt_tmp.Dispose()

            Return lsita_tmp

        End Function

        Public Function get_Lista_marcacion_x_Personal(ByVal idpersonal_ As Integer, ByVal fechaIni_ As DateTime, ByVal fechaFin_ As DateTime) As DataTable
            Return SqlHelper.ExecuteDataset(Cn, "SG_PL_SP_S_MARCA_ASIS_X_IDPER", idpersonal_, CDate(fechaIni_), CDate(fechaFin_)).Tables(0)
        End Function

        Public Sub Insert_Marcacion_Diaria(ByVal entidad As BE.HospitalBE.SG_PL_TB_MARCA_ASIS)
            With entidad
                SqlHelper.ExecuteNonQuery(Cn, "SG_PL_SP_I_MARCA_ASIS", .MA_IDPERSONAL, IIf(.MA_FECHA = String.Empty, DBNull.Value, CDate(.MA_FECHA)), .MA_HORA_ENT, _
                                          .MA_TM_ENT, .MA_HORA_SAL, .MA_TM_SAL, .MA_TIEMPO, .MA_IDTIPO_REG, .MA_OBS, .MA_IDEMPRESA, .MA_IDSERVICIO, _
                                          IIf(.MA_VACA_INI = String.Empty, DBNull.Value, .MA_VACA_INI), IIf(.MA_VACA_FIN = String.Empty, DBNull.Value, .MA_VACA_FIN), _
                                          .MA_ES_REFRI, .MA_ES_FERIADO)
            End With
        End Sub

        Public Sub Update_Marcacion_Diaria(ByVal entidad As BE.HospitalBE.SG_PL_TB_MARCA_ASIS, ByVal entidadMod As BE.HospitalBE.SG_PL_TB_MARCA_ASIS, ByVal fecha_Ini_ As String, ByVal fecha_Fin_ As String)

            With entidad
                SqlHelper.ExecuteNonQuery(Cn, "SG_PL_SP_U_MARCA_ASIS", .MA_IDPERSONAL, IIf(.MA_FECHA = String.Empty, DBNull.Value, CDate(.MA_FECHA)), .MA_HORA_ENT, _
                                          .MA_TM_ENT, .MA_HORA_SAL, .MA_TM_SAL, .MA_TIEMPO, .MA_IDTIPO_REG, .MA_OBS, .MA_IDEMPRESA, .MA_IDSERVICIO, .MA_ITEM, _
                                          IIf(.MA_VACA_INI = String.Empty, DBNull.Value, .MA_VACA_INI), IIf(.MA_VACA_FIN = String.Empty, DBNull.Value, .MA_VACA_FIN), _
                                         .MA_ES_REFRI, .MA_ES_FERIADO)
            End With

            If Not entidadMod Is Nothing Then
                With entidad
                    SqlHelper.ExecuteNonQuery(Cn, "SG_PL_SP_I_MARCA_ASIS_MOD", .MA_IDPERSONAL, CDate(.MA_FECHA), .MA_HORA_ENT, .MA_TM_ENT, .MA_HORA_SAL, .MA_TM_SAL, .MA_TIEMPO, .MA_IDTIPO_REG, _
                                              .MA_OBS, .MA_IDEMPRESA, .MA_IDSERVICIO, fecha_Ini_, fecha_Fin_)
                End With
            End If

        End Sub

        Public Function get_Tipo_Registro() As DataTable
            Dim sbquery As New System.Text.StringBuilder
            sbquery.Append("SELECT TR_ID,TR_DESCRIPCION FROM SG_PL_TB_RH_TIPOREG ")
            Return SqlHelper.ExecuteDataset(Cn, CommandType.Text, sbquery.ToString).Tables(0)
        End Function

        Public Function get_Personal_Asistencial_Horas(ByVal empresa_ As Integer) As DataTable

            Dim sbquery As New System.Text.StringBuilder
            sbquery.Append("SELECT PE_ID,PE_CODIGO,PE_APE_PAT+' '+PE_APE_MAT+' '+PE_NOM_PRI+' '+PE_NOM_SEG AS NOMBRES ")
            sbquery.Append("FROM SG_PL_TB_PERSONAL ")
            sbquery.Append("WHERE PE_ID_AREA IN (9,10) AND PE_ID_EMPRESA = " & empresa_.ToString & " AND PE_ID_EST_TRABAJADOR = 1  ")
            sbquery.Append("ORDER BY PE_APE_PAT ")

            Return SqlHelper.ExecuteDataset(Cn, CommandType.Text, sbquery.ToString).Tables(0)

        End Function

        Public Function get_Lista_Personal_Asis_Arbol(ByVal empresa_ As Integer, ByVal area_ As Integer) As DataTable

            Dim sbquery As New System.Text.StringBuilder
            sbquery.Append("SELECT PE_ID,PE_CODIGO,PE_APE_PAT+' '+PE_APE_MAT+' '+PE_NOM_PRI+' '+PE_NOM_SEG AS 'NOMBRES' ")
            sbquery.Append("FROM SG_PL_TB_PERSONAL WHERE PE_ID_EMPRESA = " & empresa_.ToString & "  ")
            sbquery.Append("AND PE_ID_AREA  = " & area_.ToString & " AND PE_ID_EST_TRABAJADOR = 1 ")
            sbquery.Append("ORDER BY PE_APE_PAT ")

            Return SqlHelper.ExecuteDataset(Cn, CommandType.Text, sbquery.ToString).Tables(0)


        End Function

        Public Sub Delete_Marcacion_Diaria(ByVal entidad As BE.HospitalBE.SG_PL_TB_MARCA_ASIS)

            With entidad
                SqlHelper.ExecuteNonQuery(Cn, "SG_PL_SP_D_MARCA_ASIS", .MA_IDPERSONAL, .MA_FECHA, .MA_IDEMPRESA,.MA_IDSERVICIO)
            End With

        End Sub


        Public Function get_Cabeceras_Para_Visto(ByVal ayo As Integer, ByVal mes As Integer, ByVal empresa As Integer) As DataTable
            Return SqlHelper.ExecuteDataset(Cn, "SG_PL_SP_S_DATOS_PARA_VISTOBUENO", ayo, mes, empresa).Tables(0)
        End Function

        Public Function get_Cabeceras_Para_Visto_RRHH(ByVal ayo As Integer, ByVal mes As Integer, ByVal empresa As Integer) As DataTable
            Return SqlHelper.ExecuteDataset(Cn, "SG_PL_SP_S_LISTA_VISTA_RRHH", ayo, mes, empresa).Tables(0)
        End Function

        Public Function get_Cabeceras_Periodos(ByVal ayo As Integer, ByVal empresa As Integer) As DataTable
            Return SqlHelper.ExecuteDataset(Cn, "SG_PL_SP_S_CAB_REG_PERIODOS", ayo, empresa).Tables(0)
        End Function

        Public Function get_Estado_por_Periodo(ByVal ayo_ As Integer, ByVal mes_ As Integer, ByVal empresa_ As Integer) As DataTable
            Return SqlHelper.ExecuteDataset(Cn, "SG_PL_SP_S_ESTADO_X_PERIODO", ayo_, mes_, empresa_).Tables(0)
        End Function

        Public Function get_Detalle_Mes_Mod(ByVal ayo_ As Integer, ByVal mes_ As Integer, ByVal empresa_ As Integer) As DataTable


            Dim dt_tmp As DataTable = SqlHelper.ExecuteDataset(Cn, "SG_PL_SP_S_DET_MES_CEROS", empresa_).Tables(0)
            Dim str_query As String = ""
            Dim drr As SqlDataReader

            For i As Integer = 0 To dt_tmp.Rows.Count - 1

                Dim fecha_tmp As Date = "01/" & mes_.ToString.PadLeft(2, "0") & "/" & ayo_.ToString
                Dim fecha_ini As Date = "25/" & fecha_tmp.AddMonths(-1).Month.ToString.PadLeft(2, "0") & "/" & fecha_tmp.AddMonths(-1).Year.ToString  'ObtenerPrimerDia(fecha_tmp)
                Dim fecha_fin As Date = "24/" & fecha_tmp.Month.ToString.PadLeft(2, "0") & "/" & fecha_tmp.Year.ToString 'ObtenerUltimoDia(fecha_tmp)

                drr = SqlHelper.ExecuteReader(Cn, "SG_PL_SP_S_MARCAS_X_RANGO", dt_tmp.Rows(i)("PE_ID"), CDate(fecha_ini), CDate(fecha_fin))

                Dim provider As CultureInfo = CultureInfo.InvariantCulture

                Dim tiempo_Hospi As New TimeSpan(0, 0, 0)
                Dim tiempo_Normal As New TimeSpan(0, 0, 0)
                Dim tiempo_Inter As New TimeSpan(0, 0, 0)
                Dim tiempo_Uci As New TimeSpan(0, 0, 0)
                Dim tiempo_Feriado As New TimeSpan(0, 0, 0)
                Dim cont_refrigerios As Integer = 0

                If drr.HasRows Then
                    Do While drr.Read()

                        Dim marcacion As String = drr("MA_TIEMPO").ToString
                        Dim indice As Integer = marcacion.IndexOf(":")

                        Dim hora As Integer = marcacion.Substring(0, indice)
                        Dim minuto As Integer = marcacion.Remove(0, indice + 1)

                        Dim tiempo_tmp As New TimeSpan(hora, minuto, 0)

                        Select Case drr("MA_IDSERVICIO")
                            Case 0 'Hospitalizacion
                                tiempo_Hospi = tiempo_Hospi.Add(tiempo_tmp)
                            Case 1
                                tiempo_Normal = tiempo_Normal.Add(tiempo_tmp)
                            Case 2
                                tiempo_Inter = tiempo_Inter.Add(tiempo_tmp)
                            Case 3
                                tiempo_Uci = tiempo_Uci.Add(tiempo_tmp)
                        End Select


                        If drr("MA_ES_FERIADO") = 1 Then
                            tiempo_Feriado = tiempo_Feriado.Add(tiempo_tmp)
                        End If

                        If drr("MA_ES_REFRI") = 1 Then
                            cont_refrigerios += 1
                        End If

                    Loop
                End If

                drr.Close()


                Dim tiempo_extra As New TimeSpan(0, 0, 0)

                If ((tiempo_Hospi.Days * 24) + tiempo_Hospi.Hours) > 150 Then
                    Dim tiempo_fijo As New TimeSpan(150, 0, 0)
                    tiempo_extra = tiempo_Hospi.Subtract(tiempo_fijo)
                End If

                tiempo_Hospi = tiempo_Hospi.Subtract(tiempo_extra)


                Dim hora_hospi_f As Integer = (tiempo_Hospi.Days * 24) + tiempo_Hospi.Hours
                Dim minutos_hospi_f As Integer = tiempo_Hospi.Minutes

                Dim hora_hospi_e As Integer = (tiempo_extra.Days * 24) + tiempo_extra.Hours
                Dim minutos_hospi_e As Integer = tiempo_extra.Minutes

                Dim hora_n As Integer = (tiempo_Normal.Days * 24) + tiempo_Normal.Hours
                Dim minutos_n As Integer = tiempo_Normal.Minutes

                Dim hora_i As Integer = (tiempo_Inter.Days * 24) + tiempo_Inter.Hours
                Dim minutos_i As Integer = tiempo_Inter.Minutes

                Dim hora_u As Integer = (tiempo_Uci.Days * 24) + tiempo_Uci.Hours
                Dim minutos_u As Integer = tiempo_Uci.Minutes

                Dim hora_feriado As Integer = (tiempo_Feriado.Days * 24) + tiempo_Feriado.Hours
                Dim minutos_feriado As Integer = tiempo_Feriado.Minutes


                Dim tiempo_tot_salaBBs As New TimeSpan
                tiempo_tot_salaBBs = tiempo_tot_salaBBs.Add(tiempo_Normal)
                tiempo_tot_salaBBs = tiempo_tot_salaBBs.Add(tiempo_Inter)
                tiempo_tot_salaBBs = tiempo_tot_salaBBs.Add(tiempo_Uci)

                Dim hora_tot_bbs As Integer = (tiempo_tot_salaBBs.Days * 24) + tiempo_tot_salaBBs.Hours
                Dim minutos_tot_bbs As Integer = tiempo_tot_salaBBs.Minutes


                dt_tmp(i)("CUNA_NORMAL") = hora_n.ToString.PadLeft(2, "0") & ":" & minutos_n.ToString.PadLeft(2, "0")
                dt_tmp(i)("INTERMEDIOS") = hora_i.ToString.PadLeft(2, "0") & ":" & minutos_i.ToString.PadLeft(2, "0")
                dt_tmp(i)("UCI") = hora_u.ToString.PadLeft(2, "0") & ":" & minutos_u.ToString.PadLeft(2, "0")
                dt_tmp(i)("TOTAL") = hora_tot_bbs.ToString.PadLeft(2, "0") & ":" & minutos_tot_bbs
                dt_tmp(i)("FIJAS") = hora_hospi_f.ToString.PadLeft(2, "0") & ":" & minutos_hospi_f.ToString.PadLeft(2, "0")
                dt_tmp(i)("EXTRAS") = hora_hospi_e.ToString.PadLeft(2, "0") & ":" & minutos_hospi_e.ToString.PadLeft(2, "0")
                dt_tmp(i)("EXTRAS_DOBLE") = hora_feriado.ToString.PadLeft(2, "0") & ":" & minutos_feriado.ToString.PadLeft(2, "0")
                dt_tmp(i)("REFRIGERIO") = cont_refrigerios

            Next

            Return dt_tmp



        End Function

        Public Function get_Detalle_Mes(ByVal ayo As Integer, ByVal mes As Integer, ByVal personal As Integer, ByVal empresa As Integer, ByRef nuevo As Boolean) As DataTable
            get_Detalle_Mes = Nothing

            Dim query As String = "SELECT * FROM SG_PL_TB_PERSONAL_HORAS_DET WHERE PHD_ANHO = " & ayo.ToString & " AND PHD_MES = " & mes.ToString & " AND PHD_IDEMPRESA = " & empresa.ToString & " AND PHD_IDPERSONAL = " & personal.ToString
            Dim drr As SqlDataReader = SqlHelper.ExecuteReader(Cn, CommandType.Text, query)

            If drr.HasRows Then nuevo = False Else nuevo = True

            drr.Close()

            Return SqlHelper.ExecuteDataset(Cn, "SG_PL_SP_S_TARIFA_REGISTRO", ayo, mes, personal, empresa).Tables(0)
        End Function

        Public Function get_Estado_Marcaciones(ByVal ayo As Integer, ByVal empresa As Integer) As DataTable
            Return SqlHelper.ExecuteDataset(Cn, "SG_PL_SP_S_ESTADOMARCACIONES", ayo, empresa).Tables(0)
        End Function

        Public Function get_Periodos_Registrados(ByVal ayo As Integer, ByVal empresa As Integer) As DataTable
            Return SqlHelper.ExecuteDataset(Cn, "SG_PL_SP_S_LISTA_CAB_ENVIO", ayo, empresa).Tables(0)
        End Function

        Public Sub Insert_Horas_Personal_ET_Cabecera(ByVal Entidad_C As BE.HospitalBE.SG_PL_TB_PERSONAL_HORAS_CAB)

            Dim drr As SqlDataReader = Nothing
            Dim query As String = "SELECT * FROM SG_PL_TB_PERSONAL_HORAS_CAB WHERE PHC_ANHO = " & Entidad_C.PHC_ANHO.ToString & " AND PHC_MES = " & Entidad_C.PHC_MES.ToString & " AND PHC_IDEMPRESA = " & Entidad_C.PHC_IDEMPRESA.ToString

            drr = SqlHelper.ExecuteReader(Cn, CommandType.Text, query)

            If drr.HasRows Then
                drr.Read()

                If drr("PHC_ESTADO") = 1 Then
                    Throw New Exception("El periodo ya esta procesado, No se puede Continuar")
                End If

                If drr("PHC_OK_SISTEMAS") = 1 Then
                    Throw New Exception("El periodo ya tiene el visto de Sistemas, Solo podra visualizar la informacion")
                End If

                If drr("PHC_OK_CONTABILIDAD") = 1 Then
                    Throw New Exception("El periodo ya tiene el visto de Contabilidad, Solo podra visualizar la informacion")
                End If
                drr.Close()

            Else

                If Not drr.IsClosed Then
                    drr.Close()
                End If

                With Entidad_C
                    SqlHelper.ExecuteNonQuery(Cn, "SG_PL_SP_I_PERSONAL_HORAS_CAB", .PHC_ANHO, .PHC_MES, .PHC_OK_SISTEMAS, .PHC_OK_CONTABILIDAD, .PHC_ESTADO, .PHC_IDEMPRESA, _
                                              .PHC_USUARIO, .PHC_TERMINAL, CDate(.PHC_FECREG))
                End With

            End If


        End Sub

        Public Sub Update_Visto_Bueno_RR_HH(ByVal Entidad_C As BE.HospitalBE.SG_PL_TB_PERSONAL_HORAS_CAB)
            Dim query As String = ""

            With Entidad_C
                query = "UPDATE SG_PL_TB_PERSONAL_HORAS_CAB SET  PHC_OK_CONTABILIDAD = " & .PHC_OK_CONTABILIDAD.ToString & " WHERE PHC_ANHO = " & .PHC_ANHO.ToString & " AND PHC_MES = " & .PHC_MES.ToString & " AND PHC_IDEMPRESA = " & .PHC_IDEMPRESA.ToString
            End With

            SqlHelper.ExecuteNonQuery(Cn, CommandType.Text, query)

        End Sub

        Public Sub Update_Visto_Bueno_Sistemas(ByVal Entidad_C As BE.HospitalBE.SG_PL_TB_PERSONAL_HORAS_CAB)

            Dim query As String = "UPDATE SG_PL_TB_PERSONAL_HORAS_CAB SET  PHC_OK_SISTEMAS = 2 WHERE PHC_ANHO = 2012 AND PHC_MES = 1 AND PHC_IDEMPRESA = 1"
            With Entidad_C
                query = "UPDATE SG_PL_TB_PERSONAL_HORAS_CAB SET  PHC_OK_SISTEMAS = " & .PHC_OK_SISTEMAS.ToString & " WHERE PHC_ANHO = " & .PHC_ANHO.ToString & " AND PHC_MES = " & .PHC_MES.ToString & " AND PHC_IDEMPRESA = " & .PHC_IDEMPRESA.ToString
            End With

            SqlHelper.ExecuteNonQuery(Cn, CommandType.Text, query)

        End Sub

        Public Sub Update_Visto_Sistema_x_Personal(ByVal entidad As BE.HospitalBE.SG_PL_TB_PERSONAL_HORAS_DET)
            With entidad
                SqlHelper.ExecuteNonQuery(Cn, "SG_PL_SP_U_PER_HOR_DET", .PHD_IDPERSONAL, .PHD_ANHO, .PHD_MES, .PHD_SIS_OK, .PHD_IDEMPRESA, .PHD_OBS)
            End With
        End Sub

        Public Sub Update_Estado_deEnvioCoordinadora(ByVal Entidad_C As BE.HospitalBE.SG_PL_TB_PERSONAL_HORAS_CAB)

            Dim query As String = ""
            With Entidad_C
                query = "UPDATE SG_PL_TB_PERSONAL_HORAS_CAB SET  PHC_ESTADO = " & .PHC_ESTADO.ToString & " WHERE PHC_ANHO = " & .PHC_ANHO.ToString & " AND PHC_MES = " & .PHC_MES.ToString & " AND PHC_IDEMPRESA = " & .PHC_IDEMPRESA.ToString
            End With

            SqlHelper.ExecuteNonQuery(Cn, CommandType.Text, query)

        End Sub

        Public Sub Insert_Horas_Personal_ET(ByVal LisDetalles As List(Of BE.HospitalBE.SG_PL_TB_PERSONAL_HORAS_DET), ByVal bol_nuevo As Boolean)
            For Each detalle As BE.HospitalBE.SG_PL_TB_PERSONAL_HORAS_DET In LisDetalles
                With detalle
                    SqlHelper.ExecuteNonQuery(Cn, "SG_PL_SP_I_PERSONAL_HORAS_DET", .PHD_IDPERSONAL, .PHD_IDTIPO_TARIFA, .PHD_VALOR_HORA, .PHD_ANHO, .PHD_MES, .PHD_IDEMPRESA, .PHD_USUARIO, _
                                              .PHD_TERMINAL, CDate(.PHD_FECREG), .PHD_OBS, .PHD_HORA_F, .PHD_HORA_E, .PHD_HORA_E_DOBLE, .PHD_TOT_HOR_SALA_BBS, .PHD_TOT_REFRI)
                End With
            Next
        End Sub
    End Class

End Class
