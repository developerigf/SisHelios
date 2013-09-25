Public Class HospitalBE

    Public Class SG_PL_TB_MARCA_ASIS
        Private _MA_IDPERSONAL As Integer
        Private _MA_FECHA As String
        Private _MA_HORA_ENT As String
        Private _MA_TM_ENT As String
        Private _MA_HORA_SAL As String
        Private _MA_TM_SAL As String
        Private _MA_TIEMPO As String
        Private _MA_IDTIPO_REG As Integer
        Private _MA_OBS As String
        Private _MA_IDEMPRESA As Integer
        Private _MA_IDSERVICIO As Integer
        Private _MA_ITEM As Integer
        Private _MA_VACA_INI As String
        Private _MA_VACA_FIN As String
        Private _MA_ES_REFRI As Integer
        Private _MA_ES_FERIADO As Integer

        Public Sub New()
            _MA_VACA_INI = String.Empty
            _MA_VACA_FIN = String.Empty
            _MA_ITEM = 0
            _MA_IDPERSONAL = 0
            _MA_FECHA = String.Empty
            _MA_HORA_ENT = String.Empty
            _MA_TM_ENT = String.Empty
            _MA_HORA_SAL = String.Empty
            _MA_TM_SAL = String.Empty
            _MA_TIEMPO = String.Empty
            _MA_IDTIPO_REG = 0
            _MA_OBS = String.Empty
            _MA_IDEMPRESA = 0
            _MA_IDSERVICIO = 0
            _MA_ES_REFRI = 0
            _MA_ES_FERIADO = 0
        End Sub

        Public Sub New(ByVal MA_IDPERSONAL_ As Integer, ByVal MA_FECHA_ As String, ByVal MA_HORA_ENT_ As String, ByVal MA_TM_ENT_ As String, ByVal MA_HORA_SAL_ As String, ByVal MA_TM_SAL_ As String, ByVal MA_TIEMPO_ As String, ByVal MA_IDTIPO_REG_ As Integer, ByVal MA_OBS_ As String, ByVal MA_IDEMPRESA_ As Integer, ByVal MA_IDSERVICIO_ As Integer, ByVal MA_ITEM_ As Integer, ByVal MA_VACA_INI_ As String, ByVal MA_VACA_FIN_ As String, ByVal MA_ES_REFRI_ As Integer, ByVal MA_ES_FERIADO_ As Integer)
            _MA_ES_REFRI = MA_ES_REFRI_
            _MA_ES_FERIADO = MA_ES_FERIADO_
            _MA_VACA_INI = MA_VACA_INI_
            _MA_VACA_FIN = MA_VACA_FIN_
            _MA_ITEM = MA_ITEM_
            _MA_IDSERVICIO = MA_IDSERVICIO_
            _MA_IDPERSONAL = MA_IDPERSONAL_
            _MA_FECHA = MA_FECHA_
            _MA_HORA_ENT = MA_HORA_ENT_
            _MA_TM_ENT = MA_TM_ENT_
            _MA_HORA_SAL = MA_HORA_SAL_
            _MA_TM_SAL = MA_TM_SAL_
            _MA_TIEMPO = MA_TIEMPO_
            _MA_IDTIPO_REG = MA_IDTIPO_REG_
            _MA_OBS = MA_OBS_
            _MA_IDEMPRESA = MA_IDEMPRESA_
        End Sub

        Public Property MA_ES_REFRI() As Integer
            Get
                Return _MA_ES_REFRI
            End Get
            Set(ByVal value As Integer)
                _MA_ES_REFRI = value
            End Set
        End Property

        Public Property MA_ES_FERIADO() As Integer
            Get
                Return _MA_ES_FERIADO
            End Get
            Set(ByVal value As Integer)
                _MA_ES_FERIADO = value
            End Set
        End Property

        Public Property MA_VACA_INI() As String
            Get
                Return _MA_VACA_INI
            End Get
            Set(ByVal value As String)
                _MA_VACA_INI = value
            End Set
        End Property

        Public Property MA_VACA_FIN() As String
            Get
                Return _MA_VACA_FIN
            End Get
            Set(ByVal value As String)
                _MA_VACA_FIN = value
            End Set
        End Property

        Public Property MA_ITEM() As Integer
            Get
                Return _MA_ITEM
            End Get
            Set(ByVal value As Integer)
                _MA_ITEM = value
            End Set
        End Property

        Public Property MA_IDSERVICIO() As Integer
            Get
                Return _MA_IDSERVICIO
            End Get
            Set(ByVal value As Integer)
                _MA_IDSERVICIO = value
            End Set
        End Property

        Public Property MA_IDPERSONAL() As Integer
            Get
                Return _MA_IDPERSONAL
            End Get
            Set(ByVal value As Integer)
                _MA_IDPERSONAL = value
            End Set
        End Property

        Public Property MA_FECHA() As String
            Get
                Return _MA_FECHA
            End Get
            Set(ByVal value As String)
                _MA_FECHA = value
            End Set
        End Property

        Public Property MA_HORA_ENT() As String
            Get
                Return _MA_HORA_ENT
            End Get
            Set(ByVal value As String)
                _MA_HORA_ENT = value
            End Set
        End Property

        Public Property MA_TM_ENT() As String
            Get
                Return _MA_TM_ENT
            End Get
            Set(ByVal value As String)
                _MA_TM_ENT = value
            End Set
        End Property

        Public Property MA_HORA_SAL() As String
            Get
                Return _MA_HORA_SAL
            End Get
            Set(ByVal value As String)
                _MA_HORA_SAL = value
            End Set
        End Property

        Public Property MA_TM_SAL() As String
            Get
                Return _MA_TM_SAL
            End Get
            Set(ByVal value As String)
                _MA_TM_SAL = value
            End Set
        End Property

        Public Property MA_TIEMPO() As String
            Get
                Return _MA_TIEMPO
            End Get
            Set(ByVal value As String)
                _MA_TIEMPO = value
            End Set
        End Property

        Public Property MA_IDTIPO_REG() As Integer
            Get
                Return _MA_IDTIPO_REG
            End Get
            Set(ByVal value As Integer)
                _MA_IDTIPO_REG = value
            End Set
        End Property

        Public Property MA_OBS() As String
            Get
                Return _MA_OBS
            End Get
            Set(ByVal value As String)
                _MA_OBS = value
            End Set
        End Property

        Public Property MA_IDEMPRESA() As Integer
            Get
                Return _MA_IDEMPRESA
            End Get
            Set(ByVal value As Integer)
                _MA_IDEMPRESA = value
            End Set
        End Property

    End Class

    Public Class SG_PL_TB_PERSONAL_HORAS_DET

        Private _PHD_IDPERSONAL As Integer
        Private _PHD_IDTIPO_TARIFA As Integer
        Private _PHD_VALOR_HORA As Double
        Private _PHD_ANHO As Integer
        Private _PHD_MES As Integer
        Private _PHD_IDEMPRESA As Integer
        Private _PHD_USUARIO As String
        Private _PHD_TERMINAL As String
        Private _PHD_FECREG As String
        Private _PHD_OBS As String
        Private _PHD_SIS_OK As Integer
        Private _PHD_HORA_F As Double
        Private _PHD_HORA_E As Double
        Private _PHD_HORA_E_DOBLE As Double
        Private _PHD_TOT_HOR_SALA_BBS As Double
        Private _PHD_TOT_REFRI As Integer

        Public Sub New(ByVal PHD_IDPERSONAL_ As Integer, ByVal PHD_IDTIPO_TARIFA_ As Integer, ByVal PHD_VALOR_HORA_ As Double, ByVal PHD_ANHO_ As Integer, ByVal PHD_MES_ As Integer, ByVal PHD_IDEMPRESA_ As Integer, ByVal PHD_USUARIO_ As String, ByVal PHD_TERMINAL_ As String, ByVal PHD_FECREG_ As String, ByVal PHD_OBS_ As String, ByVal PHD_SIS_OK_ As Integer, ByVal PHD_HORA_F_ As Double, ByVal PHD_HORA_E_ As Double, ByVal PHD_HORA_E_DOBLE_ As Double, ByVal PHD_TOT_HOR_SALA_BBS_ As Double, ByVal PHD_TOT_REFRI_ As Integer)
            _PHD_TOT_REFRI = PHD_TOT_REFRI_
            _PHD_TOT_HOR_SALA_BBS = PHD_TOT_HOR_SALA_BBS_
            _PHD_HORA_E_DOBLE = PHD_HORA_E_DOBLE_
            _PHD_HORA_E = PHD_HORA_E_
            _PHD_HORA_F = PHD_HORA_F_
            _PHD_SIS_OK = PHD_SIS_OK_
            _PHD_OBS = PHD_OBS_
            _PHD_IDPERSONAL = PHD_IDPERSONAL_
            _PHD_IDTIPO_TARIFA = PHD_IDTIPO_TARIFA_
            _PHD_VALOR_HORA = PHD_VALOR_HORA_
            _PHD_ANHO = PHD_ANHO_
            _PHD_MES = PHD_MES_
            _PHD_IDEMPRESA = PHD_IDEMPRESA_
            _PHD_USUARIO = PHD_USUARIO_
            _PHD_TERMINAL = PHD_TERMINAL_
            _PHD_FECREG = PHD_FECREG_
        End Sub

        Public Sub New()
            _PHD_TOT_REFRI = 0
            _PHD_TOT_HOR_SALA_BBS = 0
            _PHD_HORA_E_DOBLE = 0
            _PHD_HORA_E = 0
            _PHD_HORA_F = 0
            _PHD_SIS_OK = 0
            _PHD_OBS = String.Empty
            _PHD_IDPERSONAL = 0
            _PHD_IDTIPO_TARIFA = 0
            _PHD_VALOR_HORA = 0
            _PHD_ANHO = 0
            _PHD_MES = 0
            _PHD_IDEMPRESA = 0
            _PHD_USUARIO = String.Empty
            _PHD_TERMINAL = String.Empty
            _PHD_FECREG = String.Empty
        End Sub

        Public Property PHD_TOT_REFRI() As Integer
            Get
                Return _PHD_TOT_REFRI
            End Get
            Set(ByVal value As Integer)
                _PHD_TOT_REFRI = value
            End Set
        End Property

        Public Property PHD_TOT_HOR_SALA_BBS() As Double
            Get
                Return _PHD_TOT_HOR_SALA_BBS
            End Get
            Set(ByVal value As Double)
                _PHD_TOT_HOR_SALA_BBS = value
            End Set
        End Property

        Public Property PHD_HORA_E_DOBLE() As Double
            Get
                Return _PHD_HORA_E_DOBLE
            End Get
            Set(ByVal value As Double)
                _PHD_HORA_E_DOBLE = value
            End Set
        End Property

        Public Property PHD_HORA_E() As Double
            Get
                Return _PHD_HORA_E
            End Get
            Set(ByVal value As Double)
                _PHD_HORA_E = value
            End Set
        End Property

        Public Property PHD_HORA_F() As Double
            Get
                Return _PHD_HORA_F
            End Get
            Set(ByVal value As Double)
                _PHD_HORA_F = value
            End Set
        End Property

        Public Property PHD_SIS_OK() As Integer
            Get
                Return _PHD_SIS_OK
            End Get
            Set(ByVal value As Integer)
                _PHD_SIS_OK = value
            End Set
        End Property

        Public Property PHD_OBS() As String
            Get
                Return _PHD_OBS
            End Get
            Set(ByVal value As String)
                _PHD_OBS = value
            End Set
        End Property

        Public Property PHD_IDPERSONAL() As Integer
            Get
                Return _PHD_IDPERSONAL
            End Get
            Set(ByVal value As Integer)
                _PHD_IDPERSONAL = value
            End Set
        End Property

        Public Property PHD_IDTIPO_TARIFA() As Integer
            Get
                Return _PHD_IDTIPO_TARIFA
            End Get
            Set(ByVal value As Integer)
                _PHD_IDTIPO_TARIFA = value
            End Set
        End Property

        Public Property PHD_VALOR_HORA() As Double
            Get
                Return _PHD_VALOR_HORA
            End Get
            Set(ByVal value As Double)
                _PHD_VALOR_HORA = value
            End Set
        End Property

        Public Property PHD_ANHO() As Integer
            Get
                Return _PHD_ANHO
            End Get
            Set(ByVal value As Integer)
                _PHD_ANHO = value
            End Set
        End Property

        Public Property PHD_MES() As Integer
            Get
                Return _PHD_MES
            End Get
            Set(ByVal value As Integer)
                _PHD_MES = value
            End Set
        End Property

        Public Property PHD_IDEMPRESA() As Integer
            Get
                Return _PHD_IDEMPRESA
            End Get
            Set(ByVal value As Integer)
                _PHD_IDEMPRESA = value
            End Set
        End Property

        Public Property PHD_USUARIO() As String
            Get
                Return _PHD_USUARIO
            End Get
            Set(ByVal value As String)
                _PHD_USUARIO = value
            End Set
        End Property

        Public Property PHD_TERMINAL() As String
            Get
                Return _PHD_TERMINAL
            End Get
            Set(ByVal value As String)
                _PHD_TERMINAL = value
            End Set
        End Property

        Public Property PHD_FECREG() As String
            Get
                Return _PHD_FECREG
            End Get
            Set(ByVal value As String)
                _PHD_FECREG = value
            End Set
        End Property


    End Class

    Public Class SG_PL_TB_PERSONAL_HORAS_CAB
        Private _PHC_ANHO As Integer
        Private _PHC_MES As Integer
        Private _PHC_OK_SISTEMAS As Integer
        Private _PHC_OK_CONTABILIDAD As Integer
        Private _PHC_ESTADO As Integer
        Private _PHC_IDEMPRESA As Integer
        Private _PHC_USUARIO As String
        Private _PHC_TERMINAL As String
        Private _PHC_FECREG As String

        Public Sub New(ByVal PHC_ANHO_ As Integer, ByVal PHC_MES_ As Integer, ByVal PHC_OK_SISTEMAS_ As Integer, ByVal PHC_OK_CONTABILIDAD_ As Integer, ByVal PHC_ESTADO_ As Integer, ByVal PHC_IDEMPRESA_ As Integer, ByVal PHC_USUARIO_ As String, ByVal PHC_TERMINAL_ As String, ByVal PHC_FECREG_ As String)
            _PHC_ANHO = PHC_ANHO_
            _PHC_MES = PHC_MES_
            _PHC_OK_SISTEMAS = PHC_OK_SISTEMAS_
            _PHC_OK_CONTABILIDAD = PHC_OK_CONTABILIDAD_
            _PHC_ESTADO = PHC_ESTADO_
            _PHC_IDEMPRESA = PHC_IDEMPRESA_
            _PHC_USUARIO = PHC_USUARIO_
            _PHC_TERMINAL = PHC_TERMINAL_
            _PHC_FECREG = PHC_FECREG_
        End Sub

        Public Sub New()
            _PHC_ANHO = 0
            _PHC_MES = 0
            _PHC_OK_SISTEMAS = 0
            _PHC_OK_CONTABILIDAD = 0
            _PHC_ESTADO = 0
            _PHC_IDEMPRESA = 0
            _PHC_USUARIO = String.Empty
            _PHC_TERMINAL = String.Empty
            _PHC_FECREG = String.Empty
        End Sub

        Public Property PHC_ANHO() As Integer
            Get
                Return _PHC_ANHO
            End Get
            Set(ByVal value As Integer)
                _PHC_ANHO = value
            End Set
        End Property

        Public Property PHC_MES() As Integer
            Get
                Return _PHC_MES
            End Get
            Set(ByVal value As Integer)
                _PHC_MES = value
            End Set
        End Property

        Public Property PHC_OK_SISTEMAS() As Integer
            Get
                Return _PHC_OK_SISTEMAS
            End Get
            Set(ByVal value As Integer)
                _PHC_OK_SISTEMAS = value
            End Set
        End Property

        Public Property PHC_OK_CONTABILIDAD() As Integer
            Get
                Return _PHC_OK_CONTABILIDAD
            End Get
            Set(ByVal value As Integer)
                _PHC_OK_CONTABILIDAD = value
            End Set
        End Property

        Public Property PHC_ESTADO() As Integer
            Get
                Return _PHC_ESTADO
            End Get
            Set(ByVal value As Integer)
                _PHC_ESTADO = value
            End Set
        End Property

        Public Property PHC_IDEMPRESA() As Integer
            Get
                Return _PHC_IDEMPRESA
            End Get
            Set(ByVal value As Integer)
                _PHC_IDEMPRESA = value
            End Set
        End Property

        Public Property PHC_USUARIO() As String
            Get
                Return _PHC_USUARIO
            End Get
            Set(ByVal value As String)
                _PHC_USUARIO = value
            End Set
        End Property

        Public Property PHC_TERMINAL() As String
            Get
                Return _PHC_TERMINAL
            End Get
            Set(ByVal value As String)
                _PHC_TERMINAL = value
            End Set
        End Property

        Public Property PHC_FECREG() As String
            Get
                Return _PHC_FECREG
            End Get
            Set(ByVal value As String)
                _PHC_FECREG = value
            End Set
        End Property

    End Class
End Class
