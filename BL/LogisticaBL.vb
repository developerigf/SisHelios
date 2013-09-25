
Imports Microsoft.ApplicationBlocks.Data
Imports System.Data.SqlClient
Imports System.Data

Public Class LogisticaBL

    Public Class SG_LO_TB_UNI_MED
        Inherits ClsBD

        Public Function getMedidas(empresa_ As Integer)
            Return SqlHelper.ExecuteDataset(Cn, "SG_LO_SP_S_UNI_MED", empresa_).Tables(0)
        End Function

    End Class

    Public Class SG_LO_TB_MARCA
        Inherits ClsBD

        Public Function getMarcas(empresa_ As Integer) As DataTable
            Return SqlHelper.ExecuteDataset(Cn, "SG_LO_SP_S_MARCA", empresa_).Tables(0)
        End Function
    End Class

    Public Class SG_LO_TB_FABRICANTE
        Inherits ClsBD

        Public Function getFabricantes(empresa_ As Integer) As DataTable
            Return SqlHelper.ExecuteDataset(Cn, "SG_LO_SP_S_FABRICANTE", empresa_).Tables(0)
        End Function

    End Class

    Public Class SG_LO_TB_PAIS
        Inherits ClsBD

        Public Function getPaises(empresa_ As Integer) As DataTable
            Return SqlHelper.ExecuteDataset(Cn, "SG_LO_SP_S_PAIS", empresa_).Tables(0)
        End Function

    End Class

    Public Class SG_LO_TB_UBICACION_ART
        Inherits ClsBD

        Public Function getUbicaciones(empresa_ As Integer)
            Return SqlHelper.ExecuteDataset(Cn, "SG_LO_SP_S_UBICACION_ART", empresa_).Tables(0)
        End Function

    End Class

    Public Class SG_LO_TB_GRUPO_ARTICULO
        Inherits ClsBD

        Public Function getGrupos(empresa_ As Integer) As DataTable
            Return SqlHelper.ExecuteDataset(Cn, "SG_LO_SP_S_GRUPO_ARTICULO", empresa_).Tables(0)
        End Function

    End Class
End Class
