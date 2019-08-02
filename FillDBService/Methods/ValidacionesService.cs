using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FillDBService.Models;
using FillDBService.Models.DBContext;
using Oracle.ManagedDataAccess.Client;

namespace FillDBService.Methods
{
    class ValidacionesService
    {
        public string TurnoP = string.Empty;
        public string FechaInicio = string.Empty;
        string FechaFinal = string.Empty;
        bool BanValidaciones;
        public string Message;

        /// <summary>
        /// Valida carriles cerrados
        /// </summary>
        /// <param name="SQLConn"></param>
        /// <param name="OracleConn"></param>
        /// <returns></returns>
        public bool ValidarCarrilesCerrados(AppDBContext db ,OracleConnection OracleConn)
        {
            BanValidaciones = false;
            Carril Carril = new Carril();
            OracleCommand Cmd = new OracleCommand();
            List<Carril> Carriles = new List<Carril>();

            CheckTurno(db);

            string Query = @"SELECT	LANE_ASSIGN.Id_plaza,
 		                    LANE_ASSIGN.Id_lane,
		                    TO_CHAR(LANE_ASSIGN.MSG_DHM,'MM/DD/YY HH24:MI:SS') AS FECHA_INICIO,
 		                    LANE_ASSIGN.SHIFT_NUMBER,
 		                    LANE_ASSIGN.OPERATION_ID,
		                    TO_CHAR(LANE_ASSIGN.ASSIGN_DHM,'MM/DD/YY') AS FECHA,
		                    LTRIM(TO_CHAR(LANE_ASSIGN.JOB_NUMBER,'09')) AS EMPLEADO,
		                    LANE_ASSIGN.STAFF_NUMBER,
		                    LANE_ASSIGN.IN_CHARGE_SHIFT_NUMBER
                            FROM 	LANE_ASSIGN
                            WHERE	 SHIFT_NUMBER = " + TurnoP + "" +
                            "AND LANE_ASSIGN.OPERATION_ID = 'NA'" +
                            "AND((MSG_DHM >= TO_DATE('" + FechaInicio + "', 'MM-DD-YYYY HH24:MI:SS')) AND(MSG_DHM <= TO_DATE('" + FechaFinal + "', 'MM-DD-YYYY HH24:MI:SS')))" +
                            "ORDER BY LANE_ASSIGN.Id_PLAZA," +
                            "LANE_ASSIGN.Id_LANE," +
                            "LANE_ASSIGN.MSG_DHM ";

            Cmd.CommandText = Query;
            Cmd.Connection = OracleConn;
            OracleDataReader DataReader = Cmd.ExecuteReader();
            while (DataReader.Read())
            {
                Carril = new Carril();
                Carril.LANE = DataReader["ID_LANE"].ToString();
                Carril.FECHA = DataReader["FECHA_INICIO"].ToString();
                Carril.MATRICULE = DataReader["STAFF_NUMBER"].ToString();
                Carriles.Add(Carril);
            }

            // Se verifican que los carriles se encuentren cerrados en la tabla FIN_POSTE
            foreach (Carril item in Carriles)
            {
                string QueryFin_Poste = @"SELECT COUNT(*) FROM FIN_POSTE 
                                        WHERE DATE_DEBUT_POSTE = TO_DATE('" + item.FECHA + "', 'MM/DD/YY HH24:MI:SS') " +
                                        "AND VOIE = '" + item.LANE + "' AND MATRICULE = '" + item.MATRICULE + "'";
                Cmd.CommandText = QueryFin_Poste;
                Cmd.Connection = OracleConn;
                if (Convert.ToInt32(Cmd.ExecuteScalar()) < 1)
                {
                    Message += item.LANE + ", ";
                    BanValidaciones = true;
                }
            }

            return BanValidaciones;
        }

        /// <summary>
        /// Validar bolsas
        /// </summary>
        /// <param name="SQLConn"></param>
        /// <param name="OracleConn"></param>
        /// <returns></returns>
        public bool ValidarBolsas(AppDBContext db, OracleConnection OracleConn)
        {
            BanValidaciones = false;
            OracleCommand Cmd = new OracleCommand();
            string rpt = string.Empty;

            CheckTurno(db);

            // Verifica que todos los carriles cerrados tengan bolsa
            string Query = @"SELECT TO_CHAR(C.DATE_FIN_POSTE,'yyyy-mm-dd') AS FECHA, " +
                            "C.MATRICULE AS cajero, " +
                            "C.VOIE AS Carril, " +
                            "C.NUMERO_POSTE AS Corte, " +
                            "TO_CHAR(C.DATE_DEBUT_POSTE,'HH24:mi:SS') AS Inicio_Turno, " +
                            "TO_CHAR(C.DATE_FIN_POSTE,'HH24:mi:SS') AS Fin_Turno, " +
                            "'Entrega no realizada de bolsa '||C.VOIE||' Inicio '||TO_CHAR(C.DATE_DEBUT_POSTE,'HH24:mi:SS')||',Fin '||TO_CHAR(C.DATE_FIN_POSTE,'HH24:mi:SS')||' '||A.MATRICULE||'/'|| A.NOM AS Aviso " +
                            "FROM FIN_POSTE C " +
                            "LEFT JOIN TABLE_PERSONNEL  A ON C.Matricule = A.Matricule " +
                            "WHERE C.DATE_DEBUT_POSTE " +
                            "BETWEEN to_date('" + FechaInicio + "' ,'mm-dd-yyyy HH24:mi:SS') " +
                            "AND to_date('" + FechaFinal + "' ,'mm-dd-yyyy HH24:mi:SS') " +
                            "AND SAC IS NULL AND FIN_POSTE_CPT22 = " + TurnoP + "AND C.ID_MODE_VOIE in (1,7)";

            Cmd.CommandText = Query;
            Cmd.Connection = OracleConn;
            OracleDataReader DataReader = Cmd.ExecuteReader();
            while (DataReader.Read())
            {
                BanValidaciones = true;
                Message += DataReader["Aviso"].ToString();
            }

            return BanValidaciones;
        }

        /// <summary>
        /// Validar Comentarios
        /// </summary>
        /// <param name="SQLConn"></param>
        /// <param name="OracleConn"></param>
        /// <returns></returns>
        public bool ValidarComentarios(AppDBContext db, OracleConnection OracleConn)
        {
            BanValidaciones = false;
            OracleCommand Cmd = new OracleCommand();

            CheckTurno(db);
            // Valida que se hayan capturado los  comentarios en la entrega de Bolsa
            // SE MODIFICIO DATE_FIN_POSTE POR C.DATE_DEBUT_POSTE [RODRIGO]

            string Query = @"SELECT " +
                            "C.COMMENTAIRE AS COMENTARIOS, " +
                            "C.SAC AS BOLSA, " +
                            "C.OPERATING_SHIFT AS TURNO, " +
                            "C.DATE_REDDITION AS FECHA, " +
                            "C.RED_TXT1, " +
                            "''||C.RED_TXT1||' bolsa '||TO_CHAR(C.SAC)||' '||A.MATRICULE||'/'|| A.NOM ||'                          ' AS Aviso " +
                            "FROM REDDITION  C " +
                            "LEFT JOIN TABLE_PERSONNEL  A ON C.Matricule = A.Matricule " +
                            "WHERE DATE_REDDITION " +
                            "BETWEEN to_date('" + FechaInicio + "' ,'mm-dd-yyyy HH24:mi:SS') " +
                            "AND to_date('" + FechaFinal + "' ,'mm-dd-yyyy HH24:mi:SS') " +
                            " AND COMMENTAIRE IS NULL AND C.OPERATING_SHIFT  = " + TurnoP;

            Cmd.CommandText = Query;
            Cmd.Connection = OracleConn;
            OracleDataAdapter myAdapter = new OracleDataAdapter(Cmd);
            OracleDataReader DataReader = Cmd.ExecuteReader();

            while (DataReader.Read())
            {
                BanValidaciones = true;
                Message += DataReader["Aviso"].ToString();
            }

            return BanValidaciones;
        }

        public void CheckTurno(AppDBContext db)
        {
            //Consulta a SQLDB para saber que turno sigue
            TurnoP = db.HistoricoServicios.LastOrDefault().Idturno;
            DateTime LastDBFill = db.HistoricoServicios.LastOrDefault().Fecha;
            switch (TurnoP)
            {
                //El valor de TurnoP es el turno a realizar, por eso 4 = 2, 5 = 3 y 6 = 1 
                case "4":
                    TurnoP = "2";
                    FechaInicio = LastDBFill.ToString("MM/dd/yyyy") + " 06:00:00";
                    FechaFinal = LastDBFill.ToString("MM/dd/yyyy") + " 13:59:59";
                    break;
                case "5":
                    TurnoP = "3";
                    FechaInicio = LastDBFill.ToString("MM/dd/yyyy") + " 14:00:00";
                    FechaFinal = LastDBFill.ToString("MM/dd/yyyy") + " 21:59:59";
                    break;
                case "6":
                    TurnoP = "1";
                    FechaInicio = LastDBFill.ToString("MM/dd/yyyy") + " 22:00:00";
                    FechaFinal = LastDBFill.AddDays(1).ToString("MM/dd/yyyy") + " 05:59:59";
                    break;
            }

        }

    }
}
