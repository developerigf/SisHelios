Public Class AdmisionBE

    Public Class SG_AD_TB_TIP_DOC_PER
        Private _TD_ID As Integer
        Private _TD_DESCRIPCION As String
        Private _TD_ABREVIATURA As String
        Private _TD_COD_SUNAT As String
        Private _TD_IDEMPRESA As Integer
        Private _TD_USUARIO As String
        Private _TD_TERMINAL As String
        Private _TD_FECREG As String
        Private _TD_COD_CONTA As String

        Public Sub New(TD_ID_ As Integer, TD_DESCRIPCION_ As String, TD_ABREVIATURA_ As String, TD_COD_SUNAT_ As String, TD_IDEMPRESA_ As Integer, TD_USUARIO_ As String, TD_TERMINAL_ As String, TD_FECREG_ As String, TD_COD_CONTA_ As String)
            _TD_COD_CONTA = TD_COD_CONTA_
            _TD_ID = TD_ID_
            _TD_DESCRIPCION = TD_DESCRIPCION_
            _TD_ABREVIATURA = TD_ABREVIATURA_
            _TD_COD_SUNAT = TD_COD_SUNAT_
            _TD_IDEMPRESA = TD_IDEMPRESA_
            _TD_USUARIO = TD_USUARIO_
            _TD_TERMINAL = TD_TERMINAL_
            _TD_FECREG = TD_FECREG_
        End Sub

        Public Sub New()
            _TD_COD_CONTA = String.Empty
            _TD_ID = 0
            _TD_DESCRIPCION = String.Empty
            _TD_ABREVIATURA = String.Empty
            _TD_COD_SUNAT = String.Empty
            _TD_IDEMPRESA = 0
            _TD_USUARIO = String.Empty
            _TD_TERMINAL = String.Empty
            _TD_FECREG = String.Empty
        End Sub

        Public Property TD_COD_CONTA As String
            Get
                Return _TD_COD_CONTA
            End Get
            Set(value As String)
                _TD_COD_CONTA = value
            End Set
        End Property

        Public Property TD_ID As Integer
            Get
                Return _TD_ID
            End Get
            Set(value As Integer)
                _TD_ID = value
            End Set
        End Property

        Public Property TD_DESCRIPCION As String
            Get
                Return _TD_DESCRIPCION
            End Get
            Set(value As String)
                _TD_DESCRIPCION = value
            End Set
        End Property

        Public Property TD_ABREVIATURA As String
            Get
                Return _TD_ABREVIATURA
            End Get
            Set(value As String)
                _TD_ABREVIATURA = value
            End Set
        End Property

        Public Property TD_COD_SUNAT As String
            Get
                Return _TD_COD_SUNAT
            End Get
            Set(value As String)
                _TD_COD_SUNAT = value
            End Set
        End Property

        Public Property TD_IDEMPRESA As Integer
            Get
                Return _TD_IDEMPRESA
            End Get
            Set(value As Integer)
                _TD_IDEMPRESA = value
            End Set
        End Property

        Public Property TD_USUARIO As String
            Get
                Return _TD_USUARIO
            End Get
            Set(value As String)
                _TD_USUARIO = value
            End Set
        End Property

        Public Property TD_TERMINAL As String
            Get
                Return _TD_TERMINAL
            End Get
            Set(value As String)
                _TD_TERMINAL = value
            End Set
        End Property

        Public Property TD_FECREG As String
            Get
                Return _TD_FECREG
            End Get
            Set(value As String)
                _TD_FECREG = value
            End Set
        End Property

    End Class

    Public Class SG_AD_TB_ESTADO_CIVIL
        Private _EC_ID As Integer
        Private _EC_DESCRIPCION As String
        Private _EC_ABREVIATURA As String
        Private _EC_IDEMPRESA As Integer

        Public Sub New(EC_ID_ As Integer, EC_DESCRIPCION_ As String, EC_ABREVIATURA_ As String, EC_IDEMPRESA_ As Integer)
            _EC_ID = EC_ID_
            _EC_DESCRIPCION = EC_DESCRIPCION_
            _EC_ABREVIATURA = EC_ABREVIATURA_
            _EC_IDEMPRESA = EC_IDEMPRESA_
        End Sub

        Public Sub New()
            _EC_ID = 0
            _EC_DESCRIPCION = String.Empty
            _EC_ABREVIATURA = String.Empty
            _EC_IDEMPRESA = 0
        End Sub

        Public Property EC_IDEMPRESA As Integer
            Get
                Return _EC_IDEMPRESA
            End Get
            Set(value As Integer)
                _EC_IDEMPRESA = value
            End Set
        End Property

        Public Property EC_ABREVIATURA As String
            Get
                Return _EC_ABREVIATURA
            End Get
            Set(value As String)
                _EC_ABREVIATURA = value
            End Set
        End Property

        Public Property EC_DESCRIPCION As String
            Get
                Return _EC_DESCRIPCION
            End Get
            Set(value As String)
                _EC_DESCRIPCION = value
            End Set
        End Property

        Public Property EC_ID As Integer
            Get
                Return _EC_ID
            End Get
            Set(value As Integer)
                _EC_ID = value
            End Set
        End Property


    End Class

    Public Class SG_AD_TB_NACIONALIDAD
        Private _NA_ID As String
        Private _NA_DESCRIPCION As String
        Private _NA_IDEMPRESA As Integer
        Private _NA_USUARIO As String
        Private _NA_TERMINAL As String
        Private _NA_FECREG As String

        Public Sub New(NA_ID_ As String, NA_DESCRIPCION_ As String, NA_IDEMPRESA_ As Integer, NA_USUARIO_ As String, NA_TERMINAL_ As String, NA_FECREG_ As String)
            _NA_ID = NA_ID_
            _NA_DESCRIPCION = NA_DESCRIPCION_
            _NA_IDEMPRESA = NA_IDEMPRESA_
            _NA_USUARIO = NA_USUARIO_
            _NA_TERMINAL = NA_TERMINAL_
            _NA_FECREG = NA_FECREG_
        End Sub

        Public Sub New()
            _NA_ID = 0
            _NA_DESCRIPCION = String.Empty
            _NA_IDEMPRESA = 0
            _NA_USUARIO = String.Empty
            _NA_TERMINAL = String.Empty
            _NA_FECREG = String.Empty
        End Sub

        Public Property NA_ID As String
            Get
                Return _NA_ID
            End Get
            Set(value As String)
                _NA_ID = value
            End Set
        End Property

        Public Property NA_DESCRIPCION As String
            Get
                Return _NA_DESCRIPCION
            End Get
            Set(value As String)
                _NA_DESCRIPCION = value
            End Set
        End Property

        Public Property NA_IDEMPRESA As Integer
            Get
                Return _NA_IDEMPRESA
            End Get
            Set(value As Integer)
                _NA_IDEMPRESA = value
            End Set
        End Property

        Public Property NA_USUARIO As String
            Get
                Return _NA_USUARIO
            End Get
            Set(value As String)
                _NA_USUARIO = value
            End Set
        End Property

        Public Property NA_TERMINAL As String
            Get
                Return _NA_TERMINAL
            End Get
            Set(value As String)
                _NA_TERMINAL = value
            End Set
        End Property

        Public Property NA_FECREG As String
            Get
                Return _NA_FECREG
            End Get
            Set(value As String)
                _NA_FECREG = value
            End Set
        End Property



    End Class

    Public Class SG_AD_TB_HISTO_CLINI
        Private _HC_NUM_HIST As Integer
        Private _HC_IDCLIENTE As Integer
        Private _HC_NOMBRE1 As String
        Private _HC_NOMBRE2 As String
        Private _HC_APE_PAT As String
        Private _HC_APE_MAT As String
        Private _HC_APE_CASADA As String
        Private _HC_TDOC As BE.AdmisionBE.SG_AD_TB_TIP_DOC_PER
        Private _HC_NDOC As String
        Private _HC_FNAC As String
        Private _HC_FING As String
        Private _HC_SEXO As String
        Private _HC_EST_CIVIL As Integer
        Private _HC_DIR As String
        Private _HC_OCUPACION As String
        Private _HC_IDNACIONALIDAD As String
        Private _HC_USUARIO As String
        Private _HC_TERMINAL As String
        Private _HC_FECREG As String

        Public Sub New(HC_NUM_HIST_ As Integer, HC_IDCLIENTE_ As Integer, HC_NOMBRE1_ As String, HC_NOMBRE2_ As String, HC_APE_PAT_ As String, HC_APE_MAT_ As String, HC_APE_CASADA_ As String, HC_TDOC_ As BE.AdmisionBE.SG_AD_TB_TIP_DOC_PER, HC_NDOC_ As String, HC_FNAC_ As String, HC_FING_ As String, HC_SEXO_ As String, HC_EST_CIVIL_ As Integer, HC_DIR_ As String, HC_OCUPACION_ As String, HC_IDNACIONALIDAD_ As String, HC_USUARIO_ As String, HC_TERMINAL_ As String, HC_FECREG_ As String)
            _HC_NUM_HIST = HC_NUM_HIST_
            _HC_IDCLIENTE = HC_IDCLIENTE_
            _HC_NOMBRE1 = HC_NOMBRE1_
            _HC_NOMBRE2 = HC_NOMBRE2_
            _HC_APE_PAT = HC_APE_PAT_
            _HC_APE_MAT = HC_APE_MAT_
            _HC_APE_CASADA = HC_APE_CASADA_
            _HC_TDOC = HC_TDOC_
            _HC_NDOC = HC_NDOC_
            _HC_FNAC = HC_FNAC_
            _HC_FING = HC_FING_
            _HC_SEXO = HC_SEXO_
            _HC_EST_CIVIL = HC_EST_CIVIL_
            _HC_DIR = HC_DIR_
            _HC_OCUPACION = HC_OCUPACION_
            _HC_IDNACIONALIDAD = HC_IDNACIONALIDAD_
            _HC_USUARIO = HC_USUARIO_
            _HC_TERMINAL = HC_TERMINAL_
            _HC_FECREG = HC_FECREG_
        End Sub

        Public Sub New()
            _HC_NUM_HIST = 0
            _HC_IDCLIENTE = 0
            _HC_NOMBRE1 = String.Empty
            _HC_NOMBRE2 = String.Empty
            _HC_APE_PAT = String.Empty
            _HC_APE_MAT = String.Empty
            _HC_APE_CASADA = String.Empty
            _HC_TDOC = Nothing
            _HC_NDOC = String.Empty
            _HC_FNAC = String.Empty
            _HC_FING = String.Empty
            _HC_SEXO = String.Empty
            _HC_EST_CIVIL = 0
            _HC_DIR = String.Empty
            _HC_OCUPACION = String.Empty
            _HC_IDNACIONALIDAD = String.Empty
            _HC_USUARIO = String.Empty
            _HC_TERMINAL = String.Empty
            _HC_FECREG = String.Empty
        End Sub

        Public Property HC_NUM_HIST As Integer
            Get
                Return _HC_NUM_HIST
            End Get
            Set(value As Integer)
                _HC_NUM_HIST = value
            End Set
        End Property

        Public Property HC_IDCLIENTE As Integer
            Get
                Return _HC_IDCLIENTE
            End Get
            Set(value As Integer)
                _HC_IDCLIENTE = value
            End Set
        End Property

        Public Property HC_NOMBRE1 As String
            Get
                Return _HC_NOMBRE1
            End Get
            Set(value As String)
                _HC_NOMBRE1 = value
            End Set
        End Property

        Public Property HC_NOMBRE2 As String
            Get
                Return _HC_NOMBRE2
            End Get
            Set(value As String)
                _HC_NOMBRE2 = value
            End Set
        End Property

        Public Property HC_APE_PAT As String
            Get
                Return _HC_APE_PAT
            End Get
            Set(value As String)
                _HC_APE_PAT = value
            End Set
        End Property


        Public Property HC_APE_MAT As String
            Get
                Return _HC_APE_MAT
            End Get
            Set(value As String)
                _HC_APE_MAT = value
            End Set
        End Property


        Public Property HC_APE_CASADA As String
            Get
                Return _HC_APE_CASADA
            End Get
            Set(value As String)
                _HC_APE_CASADA = value
            End Set
        End Property

        Public Property HC_TDOC As BE.AdmisionBE.SG_AD_TB_TIP_DOC_PER
            Get
                Return _HC_TDOC
            End Get
            Set(value As BE.AdmisionBE.SG_AD_TB_TIP_DOC_PER)
                _HC_TDOC = value
            End Set
        End Property

        Public Property HC_NDOC As String
            Get
                Return _HC_NDOC
            End Get
            Set(value As String)
                _HC_NDOC = value
            End Set
        End Property

        Public Property HC_FNAC As String
            Get
                Return _HC_FNAC
            End Get
            Set(value As String)
                _HC_FNAC = value
            End Set
        End Property

        Public Property HC_FING As String
            Get
                Return _HC_FING
            End Get
            Set(value As String)
                _HC_FING = value
            End Set
        End Property

        Public Property HC_SEXO As String
            Get
                Return _HC_SEXO
            End Get
            Set(value As String)
                _HC_SEXO = value
            End Set
        End Property

        Public Property HC_EST_CIVIL As Integer
            Get
                Return _HC_EST_CIVIL
            End Get
            Set(value As Integer)
                _HC_EST_CIVIL = value
            End Set
        End Property

        Public Property HC_DIR As String
            Get
                Return _HC_DIR
            End Get
            Set(value As String)
                _HC_DIR = value
            End Set
        End Property

        Public Property HC_OCUPACION As String
            Get
                Return _HC_OCUPACION
            End Get
            Set(value As String)
                _HC_OCUPACION = value
            End Set
        End Property

        Public Property HC_IDNACIONALIDAD As String
            Get
                Return _HC_IDNACIONALIDAD
            End Get
            Set(value As String)
                _HC_IDNACIONALIDAD = value
            End Set
        End Property

        Public Property HC_USUARIO As String
            Get
                Return _HC_USUARIO
            End Get
            Set(value As String)
                _HC_USUARIO = value
            End Set
        End Property

        Public Property HC_TERMINAL As String
            Get
                Return _HC_TERMINAL
            End Get
            Set(value As String)
                _HC_TERMINAL = value
            End Set
        End Property

        Public Property HC_FECREG As String
            Get
                Return _HC_FECREG
            End Get
            Set(value As String)
                _HC_FECREG = value
            End Set
        End Property

    End Class

End Class
