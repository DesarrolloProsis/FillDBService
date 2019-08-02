using FillDBService.Models;
using FillDBService.Models.DBContext;
using FillDBService.Services;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FillDBService.Methods
{
    class FillArchivo1A
    {
        string Message = string.Empty;
        string StrQuerys = string.Empty;
        string H_inicio_turno = string.Empty;
        string H_fin_turno = string.Empty;

        List<Bitacora> AllLines = new List<Bitacora>();
        Bitacora NewLine = new Bitacora();
        MetodosGlbRepository MtGlb = new MetodosGlbRepository();

        string CajeroOracle = string.Empty;
        string CajeroSQL = string.Empty;
        string EncargadoOracle = string.Empty;
        string EncargadoSQL = string.Empty;
        string AdministradorOracle = string.Empty;
        string AdministradorSQL = string.Empty;
        string BolsaOracle = string.Empty;
        /// <summary>
        /// Método para llenar la BD "Archivos Planos Beta"
        /// </summary>
        /// <param name="Turno"></param>
        /// <param name="Fecha"></param>
        /// <param name="IdPlaza"></param>
        /// <param name="Delegacion"></param>
        /// <param name="OracleConn"></param>
        public void ClearVariables()
        {
            CajeroOracle = string.Empty;
            CajeroSQL = string.Empty;
            EncargadoOracle = string.Empty;
            EncargadoSQL = string.Empty;
            AdministradorOracle = string.Empty;
            AdministradorSQL = string.Empty;
            BolsaOracle = string.Empty;
            StrQuerys = string.Empty;
        }
        public void BitacoraOperacion(string Turno, string Fecha, string IdPlaza, string Delegacion, OracleConnection OracleConn, AppDBContext db)
        {

            switch (Turno)
            {
                case "1":
                    H_inicio_turno = Fecha + " 22:00:00";
                    H_fin_turno = Fecha + " 05:59:59";
                    break;
                case "2":
                    H_inicio_turno = Fecha + " 06:00:00";
                    H_fin_turno = Fecha + " 13:59:59";
                    break;
                case "3":
                    H_inicio_turno = Fecha + " 14:00:00";
                    H_fin_turno = Fecha + "21:59:59";
                    break;
                default:
                    break;
            }
            DateTime _H_inicio_turno = DateTime.ParseExact(H_inicio_turno, "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            DateTime _H_fin_turno = DateTime.ParseExact(H_fin_turno, "MM/dd/yyyy HH:mm:ss", CultureInfo.InvariantCulture);

            //Carriles Abiertos
            StrQuerys = "SELECT	FIN_POSTE.Id_Gare, " +
                        "TYPE_VOIE.libelle_court_voie_L2, " +
                        "Voie, " +
                        "'zzz', " +
                        "TO_CHAR(Numero_Poste,'FM09'), " +
                        "TO_CHAR(Date_Fin_Poste,'MM/DD/YY'), " +
                        "TO_CHAR(Date_Fin_Poste,'HH24:MI'), " +
                        "Matricule, " +
                        "Sac, " +
                        "FIN_POSTE.Id_Voie, " +
                        "DATE_DEBUT_POSTE,Date_Fin_Poste, " +
                        "TO_CHAR(Date_Debut_Poste,'YYYYMMDDHH24MISS'), " +
                        "TO_CHAR(Date_Fin_Poste,'YYYYMMDDHH24MISS') " +
                        ",TYPE_VOIE.libelle_court_voie " +
                        ",FIN_POSTE_CPT22, " +
                        "ROUND((DATE_FIN_POSTE - DATE_DEBUT_POSTE) * (60 * 24), 2) AS time_in_minutes " +
                        "FROM 	TYPE_VOIE, " +
                        "FIN_POSTE, " +
                        "SITE_GARE " +
                        "WHERE	FIN_POSTE.Id_Voie	=	TYPE_VOIE.Id_Voie " +
                        "AND FIN_POSTE.id_reseau	= 	SITE_GARE.id_Reseau " +
                        "AND	FIN_POSTE.id_Gare	=	SITE_GARE.id_Gare " +
                        "AND	SITE_GARE.id_reseau		= 	'01' " +
                        "AND	SITE_GARE.id_Site		=	'" + IdPlaza.Substring(1, 2) + "' " +
                        "AND (Id_Mode_Voie IN (1,7,9)) " +
                        "AND ((DATE_DEBUT_POSTE >= TO_DATE('" + _H_inicio_turno.ToString("yyyyMMddHHmmss") + "','YYYYMMDDHH24MISS')) " +
                        "AND (DATE_DEBUT_POSTE <= TO_DATE('" + _H_fin_turno.ToString("yyyyMMddHHmmss") + "','YYYYMMDDHH24MISS'))) " +
                        "AND (FIN_POSTE.Id_Voie = '1' " +
                        "OR FIN_POSTE.Id_Voie = '2' " +
                        "OR FIN_POSTE.Id_Voie = '3' " +
                        "OR FIN_POSTE.Id_Voie = '4' " +
                        "OR FIN_POSTE.Id_Voie = 'X' " +
                        ") " +
                        "ORDER BY Id_Gare, " +
                        "Id_Voie, " +
                        "Voie, " +
                        "Date_Debut_Poste," +
                        "Date_Fin_Poste, " +
                        "Numero_Poste, " +
                        "Matricule " +
                        ",Sac";
            if (MtGlb.QueryDataSet(StrQuerys, "FIN_POSTE", OracleConn))
            {

                foreach (DataRow item in MtGlb.Ds.Tables["FIN_POSTE"].Rows)
                {
                    ClearVariables();

                    NewLine = new Bitacora();

                    NewLine.Fecha = Convert.ToDateTime(Fecha);

                    string NumGea = Convert.ToString(item["Voie"]).Substring(1, 2);

                    var Carril = db.Carriles.Where(x => x.Carril.Substring(1) == NumGea).FirstOrDefault();

                    if (Carril != null)
                    {
                        NewLine.NumeroCarril = Carril.NumeroCarril;
                        NewLine.IdGare = Carril.IdGare;
                    }
                    else
                    {
                        NewLine.NumeroCarril = string.Empty;
                        NewLine.IdGare = string.Empty;
                        Message = "\n No se encontró el Carril: " + Convert.ToString(item["Voie"]).Substring(1, 2);
                    }

                    NewLine.IdTurno = Turno;

                    NewLine.HoraInicial = Convert.ToDateTime(item["DATE_DEBUT_POSTE"]).ToString("HHmmss");

                    NewLine.HoraFinal = Convert.ToDateTime(item["Date_Fin_Poste"]).ToString("HHmmss");

                    //Identificador de operación    Caracter X(2)   Valores posibles:  Tabla 17 - Códigos de Operación por Carril.
                    StrQuerys = "SELECT	LANE_ASSIGN.Id_plaza,LANE_ASSIGN.Id_lane,TO_CHAR(LANE_ASSIGN.MSG_DHM,'MM/DD/YY HH24:MI:SS'),LANE_ASSIGN.SHIFT_NUMBER,LANE_ASSIGN.OPERATION_ID, " +
                                "LANE_ASSIGN.DELEGATION, TO_CHAR(LANE_ASSIGN.ASSIGN_DHM,'MM/DD/YY'),LTRIM(TO_CHAR(LANE_ASSIGN.JOB_NUMBER,'09')),	LANE_ASSIGN.STAFF_NUMBER,LANE_ASSIGN.IN_CHARGE_SHIFT_NUMBER " +
                                "FROM 	LANE_ASSIGN, SITE_GARE " +
                                "WHERE	LANE_ASSIGN.id_NETWORK = SITE_GARE.id_Reseau " +
                                "AND LANE_ASSIGN.id_plaza = SITE_GARE.id_Gare " +
                                "AND SITE_GARE.id_reseau = '01' " +
                                "AND	SITE_GARE.id_Site ='" + IdPlaza.Substring(1, 2) + "' " +
                                "AND LANE_ASSIGN.Id_lane = '" + item["Voie"].ToString().Trim() + "' " +
                                "AND ((MSG_DHM >= TO_DATE('" + Convert.ToDateTime(item["DATE_DEBUT_POSTE"]).ToString("yyyyMMddHHmmss") + "','YYYYMMDDHH24MISS')) " +
                                "AND (MSG_DHM <= TO_DATE('" + Convert.ToDateTime(item["DATE_DEBUT_POSTE"]).ToString("yyyyMMddHHmmss") + "','YYYYMMDDHH24MISS'))) " +
                                "ORDER BY LANE_ASSIGN.Id_PLAZA, LANE_ASSIGN.Id_LANE, LANE_ASSIGN.MSG_DHM";

                    if (MtGlb.QueryDataSet2(StrQuerys, "Asig_Carril", OracleConn))
                    {
                        NewLine.IdIdentificadorOperacion = MtGlb.oDataRow2["OPERATION_ID"].ToString();

                        //Verificar Cajero
                        CajeroOracle = MtGlb.oDataRow2["STAFF_NUMBER"].ToString();
                        var ConsultaCajero = db.Cajeros.Where(x => x.NumeroGea == CajeroOracle).FirstOrDefault();

                        if (ConsultaCajero != null)
                            CajeroSQL = ConsultaCajero.NumeroCapufeCajero;

                        //En caso de que no se haya encontrado coincidencia para Cajero SQL
                        if (CajeroSQL == string.Empty)
                        {
                            var ConsultaCajero2 = db.Cajeros.Where(x => x.NumeroGea == item["Matricule"].ToString()).FirstOrDefault();

                            if (ConsultaCajero2 != null)
                                CajeroSQL = ConsultaCajero2.NumeroCapufeCajero;
                            else
                                Message = "\n No se encontró cajero en la operación: " + NewLine.IdIdentificadorOperacion;
                        }

                        NewLine.NumeroCapufeCajero = CajeroSQL;

                        //Verificar Encargado de Turno
                        EncargadoOracle = MtGlb.oDataRow2["IN_CHARGE_SHIFT_NUMBER"].ToString();
                        var ConsultaEncargado = db.Encargados.Where(x => x.NumeroGea == EncargadoOracle).FirstOrDefault();

                        if (ConsultaEncargado != null)
                            EncargadoSQL = ConsultaEncargado.NumeroCapufeEncargado;
                        else
                            Message = "\n No se encontró el encargado en la operación: " + NewLine.IdIdentificadorOperacion;

                        NewLine.NumeroCapufeEncargado = EncargadoSQL;

                        //Verificar Administrador de Plaza
                        var ConsultaAdministrador = db.Administradores.FirstOrDefault();

                        if (ConsultaAdministrador != null)
                            AdministradorSQL = ConsultaAdministrador.NumeroCapufeAdministrador;

                        StrQuerys = "Select MAT_ADMIN From PTM_LASS ";
                        MtGlb.QueryDataSet2(StrQuerys, "PRUEBA", OracleConn);

                        foreach (DataRow indi in MtGlb.Ds2.Tables["PRUEBA"].Rows)
                        {
                            AdministradorOracle = indi[0].ToString();
                            break;
                        }
                        if (AdministradorSQL != AdministradorOracle)
                        {
                            Message += "\n No coíncidió el Administrador de la Oracle con la SQL en el evento: "
                                        + NewLine.IdIdentificadorOperacion + ". Se optó por la de Oracle";

                            NewLine.NumeroCapufeAdministrador = AdministradorOracle;
                        }
                        else
                            NewLine.NumeroCapufeAdministrador = AdministradorSQL;

                        //Verificación de No. de control de preliquidación
                        BolsaOracle = MtGlb.IIf(DBNull.Value.Equals((item["Sac"])), "", item["Sac"].ToString());
                        BolsaOracle = BolsaOracle.Replace("A", "");
                        BolsaOracle = BolsaOracle.Replace("B", "");

                        NewLine.Bolsa = BolsaOracle;

                        AllLines.Add(NewLine);
                    }
                }
            }

            //Carriles Cerrados
            StrQuerys = "SELECT ID_NETWORK, ID_PLAZA,ID_LANE, LANE, BEGIN_DHM, END_DHM, BAG_NUMBER, REPORT_FLAG, GENERATION_DHM " +
                        "FROM CLOSED_LANE_REPORT, SITE_GARE " +
                        "where " +
                        "CLOSED_LANE_REPORT.ID_PLAZA	=	SITE_GARE.id_Gare " +
                        "AND	SITE_GARE.id_Site		=	'" + IdPlaza.Substring(1, 2) + "' " +
                        "AND ((BEGIN_DHM >= TO_DATE('" + _H_inicio_turno.ToString("yyyyMMddHHmmss") + "','YYYYMMDDHH24MISS')) " +
                        "AND (BEGIN_DHM <= TO_DATE('" + _H_fin_turno.ToString("yyyyMMddHHmmss") + "','YYYYMMDDHH24MISS'))) " +
                        "order by BEGIN_DHM";

            StrQuerys = ""; 
            if (MtGlb.QueryDataSet(StrQuerys, "CLOSED_LANE_REPORT", OracleConn))
            {
                foreach (DataRow item in MtGlb.Ds.Tables["CLOSED_LANE_REPORT"].Rows)
                {
                    ClearVariables();

                    NewLine = new Bitacora();

                    NewLine.Fecha = Convert.ToDateTime(Fecha);

                    string NumGea = Convert.ToString(item["LANE"]).Substring(1, 2);

                    var Carril = db.Carriles.Where(x => x.Carril.Substring(1) == NumGea).FirstOrDefault();

                    if (Carril != null)
                    {
                        NewLine.NumeroCarril = Carril.NumeroCarril;
                        NewLine.IdGare = Carril.IdGare;
                    }
                    else
                    {
                        NewLine.NumeroCarril = string.Empty;
                        NewLine.IdGare = string.Empty;
                        Message = "\n No se encontró el Carril: " + Convert.ToString(item["LANE"]).Substring(1, 2);
                    }

                    NewLine.IdTurno = Turno;

                    NewLine.HoraInicial = Convert.ToDateTime(item["BEGIN_DHM"]).ToString("HHmmss");

                    if (Convert.ToDateTime(item["END_DHM"]) > _H_fin_turno)
                        NewLine.HoraFinal = Convert.ToDateTime(_H_fin_turno).ToString("HHmmss") + ",";
                    else
                        NewLine.HoraFinal = Convert.ToDateTime(item["END_DHM"]).ToString("HHmmss") + ",";


                    //CHECAR ENCARGADO Y IDENT OPERACION
                    //Identificador de operación	Caracter 	X(2)	Valores posibles:  Tabla 17 - Códigos de Operación por Carril.
                    StrQuerys = "SELECT	LANE_ASSIGN.Id_plaza,LANE_ASSIGN.Id_lane,TO_CHAR(LANE_ASSIGN.MSG_DHM,'MM/DD/YY HH24:MI:SS'),LANE_ASSIGN.SHIFT_NUMBER,LANE_ASSIGN.OPERATION_ID, " +
                                "LANE_ASSIGN.DELEGATION, TO_CHAR(LANE_ASSIGN.ASSIGN_DHM,'MM/DD/YY'),LTRIM(TO_CHAR(LANE_ASSIGN.JOB_NUMBER,'09')),	LANE_ASSIGN.STAFF_NUMBER,LANE_ASSIGN.IN_CHARGE_SHIFT_NUMBER " +
                                "FROM 	LANE_ASSIGN, SITE_GARE " +
                                "WHERE	LANE_ASSIGN.id_NETWORK = SITE_GARE.id_Reseau " +
                                "AND LANE_ASSIGN.id_plaza = SITE_GARE.id_Gare " +
                                "AND SITE_GARE.id_reseau = '01' " +
                                "AND	SITE_GARE.id_Site ='" + IdPlaza.Substring(1, 2) + "' " +
                                "AND LANE_ASSIGN.Id_lane = '" + item["Voie"].ToString().Trim() + "' " +
                                "AND ((MSG_DHM >= TO_DATE('" + Convert.ToDateTime(item["DATE_DEBUT_POSTE"]).ToString("yyyyMMddHHmmss") + "','YYYYMMDDHH24MISS')) AND (MSG_DHM <= TO_DATE('" + Convert.ToDateTime(item["DATE_DEBUT_POSTE"]).ToString("yyyyMMddHHmmss") + "','YYYYMMDDHH24MISS'))) " +
                                "ORDER BY LANE_ASSIGN.Id_PLAZA, LANE_ASSIGN.Id_LANE, LANE_ASSIGN.MSG_DHM";

                    if (MtGlb.QueryDataSet2(StrQuerys, "Asig_Carril", OracleConn))
                    {
                        NewLine.IdIdentificadorOperacion = MtGlb.oDataRow2["OPERATION_ID"].ToString();

                        //Verificar Encargado de Turno
                        EncargadoOracle = MtGlb.oDataRow2["IN_CHARGE_SHIFT_NUMBER"].ToString();
                        EncargadoSQL = db.Encargados.Where(x => x.NumeroGea == EncargadoOracle).FirstOrDefault().NumeroCapufeEncargado;
                    }
                    else
                    {
                        NewLine.IdIdentificadorOperacion = "X" + item["LANE"].ToString().Substring(0, 1) + ",";
                        Message = "\n No se encontró Encargado: " + Convert.ToString(item["LANE"]).Substring(1, 2);
                    }

                    NewLine.NumeroCapufeCajero = EncargadoSQL;
                    NewLine.NumeroCapufeEncargado = EncargadoSQL;

                    //Verificar Administrador de Plaza
                    var ConsultaAdministrador = db.Administradores.FirstOrDefault();

                    if (ConsultaAdministrador != null)
                        AdministradorSQL = ConsultaAdministrador.NumeroCapufeAdministrador;

                    StrQuerys = "Select MAT_ADMIN From PTM_LASS ";
                    MtGlb.QueryDataSet2(StrQuerys, "PRUEBA", OracleConn);

                    foreach (DataRow indi in MtGlb.Ds2.Tables["PRUEBA"].Rows)
                    {
                        AdministradorOracle = indi[0].ToString();
                        break;
                    }

                    if (AdministradorSQL != AdministradorOracle)
                    {
                        Message += "\n No coíncidió el Administrador de la Oracle con el de la SQL en el evento: "
                                    + NewLine.IdIdentificadorOperacion + ". Se optó por la de Oracle";
                        NewLine.NumeroCapufeAdministrador = AdministradorOracle;
                    }
                    else
                        NewLine.NumeroCapufeAdministrador = AdministradorSQL;

                    NewLine.Bolsa = null;
                }
            }

            //Carriles Cerrados Dos
            StrQuerys = "SELECT VOIE, NUM_SEQUENCE FROM SEQ_VOIE_TOD ";

            if (IdPlaza == "106")
                StrQuerys += "where VOIE <> 'B04' and VOIE <> 'A03' ";

            if (MtGlb.QueryDataSet1(StrQuerys, "SEQ_VOIE_TOD", OracleConn))
            {
                foreach (DataRow item1 in MtGlb.Ds1.Tables["SEQ_VOIE_TOD"].Rows)
                {
                    StrQuerys = "SELECT	* FROM 	FIN_POSTE " +
                                "WHERE	VOIE = '" + item1["VOIE"] + "' " +
                                "AND ((DATE_DEBUT_POSTE >= TO_DATE('" + _H_inicio_turno.ToString("yyyyMMddHHmmss") + "','YYYYMMDDHH24MISS')) " +
                                "AND (DATE_DEBUT_POSTE <= TO_DATE('" + _H_fin_turno.ToString("yyyyMMddHHmmss") + "','YYYYMMDDHH24MISS'))) ";

                    if (MtGlb.QueryDataSet(StrQuerys, "FIN_POSTE", OracleConn) == false)
                    {
                        StrQuerys = "SELECT * " +
                                    "FROM CLOSED_LANE_REPORT, SITE_GARE " +
                                    "where " +
                                    "CLOSED_LANE_REPORT.ID_PLAZA	=	SITE_GARE.id_Gare " +
                                    "AND	SITE_GARE.id_Site		=	'" + IdPlaza.Substring(1, 2) + "' " +
                                    "AND	LANE		=	'" + item1["VOIE"] + "' " +
                                    "AND ((BEGIN_DHM >= TO_DATE('" + _H_inicio_turno.ToString("yyyyMMddHHmmss") + "','YYYYMMDDHH24MISS')) " +
                                    "AND (BEGIN_DHM <= TO_DATE('" + _H_fin_turno.ToString("yyyyMMddHHmmss") + "','YYYYMMDDHH24MISS'))) " +
                                    "order by BEGIN_DHM";

                        if (!MtGlb.QueryDataSet(StrQuerys, "CLOSED_LANE_REPORT", OracleConn))
                        {
                            ClearVariables();

                            NewLine = new Bitacora();

                            NewLine.Fecha = Convert.ToDateTime(Fecha);

                            string NumGea = Convert.ToString(item1["VOIE"]).Substring(1, 2);

                            var Carril = db.Carriles.Where(x => x.NumeroCarril == NumGea).FirstOrDefault();

                            if (Carril != null)
                            {
                                NewLine.NumeroCarril = Carril.NumeroCarril;
                                NewLine.IdGare = Carril.IdGare;
                            }
                            else
                            {
                                NewLine.NumeroCarril = string.Empty;
                                NewLine.IdGare = string.Empty;
                                Message = "\n No se encontró el Carril: " + Convert.ToString(item1["VOIE"]).Substring(1, 2);
                            }

                            NewLine.IdTurno = Turno;

                            NewLine.HoraInicial = _H_inicio_turno.ToString("HHmmss");

                            NewLine.HoraFinal = _H_fin_turno.AddSeconds(1).ToString("HHmmss");

                            NewLine.IdIdentificadorOperacion = "X" + item1["VOIE"].ToString().Substring(0, 1);

                            var ConsultaAdministrador = db.Administradores.FirstOrDefault();

                            if (ConsultaAdministrador != null)
                            {
                                StrQuerys = "Select MAT_ADMIN From PTM_LASS ";
                                MtGlb.QueryDataSet2(StrQuerys, "PRUEBA", OracleConn);

                                foreach (DataRow indi in MtGlb.Ds2.Tables["PRUEBA"].Rows)
                                {
                                    AdministradorOracle = indi[0].ToString();
                                    break;
                                }

                                if (ConsultaAdministrador.NumeroCapufeAdministrador != AdministradorOracle)
                                {
                                    Message += "\n No coíncidió el Administrador de la Oracle con el de la SQL en el evento: "
                                                + NewLine.IdIdentificadorOperacion + ". Se optó por la de Oracle";
                                    AdministradorSQL = AdministradorOracle;
                                }
                                else
                                    AdministradorSQL = ConsultaAdministrador.NumeroCapufeAdministrador;
                            }
                            else
                            {
                                Message = "\n No se encontró al Encargado de Plaza en la operación: " + NewLine.IdIdentificadorOperacion;
                            }
                            NewLine.NumeroCapufeCajero = AdministradorSQL;
                            NewLine.NumeroCapufeEncargado = AdministradorSQL;
                            NewLine.NumeroCapufeAdministrador = AdministradorSQL;

                            NewLine.Bolsa = string.Empty;
                        }
                    }
                }
            }

            if (Message == string.Empty)
                Message = "\n Archivo 1A: OK";
            else
                Message = "\n Archivo 1A" + Message;
        }
    }
}
