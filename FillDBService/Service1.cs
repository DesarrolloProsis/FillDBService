using FillDBService.Methods;
using FillDBService.Models.DBContext;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace FillDBService
{
    public partial class Service1 : ServiceBase
    {
        OracleConnection OracleConn = new OracleConnection();
        AppDBContext db = new AppDBContext();
        public string Notification;
        ValidacionesService validaciones = new ValidacionesService();
        FillArchivo1A Archivo1A = new FillArchivo1A();

        public Service1()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            ConnectOracleDB();
            if (!Validations())
            {
                //Telegram
            }
            else
            {

            }
        }

        protected override void OnStop()
        {
        }

        public void Prueba()
        {
            try
            {
                Notification = "\n que onda \n hey";

            }
            catch (Exception)
            {
                throw;
            }

            //ConnectOracleDB();
        }

        public void ConnectOracleDB()
        {
            try
            {
                string Ip = GetIp();
                string Plaza = Ip.Split('.')[2];
                
                OracleConn.ConnectionString = "User Id=GEADBA;Password=fgeuorjvne;  Data Source=(DESCRIPTION=(ADDRESS_LIST=(ADDRESS=(PROTOCOL=TCP)(HOST=10.3." + Plaza + ".221)(PORT=1521)))(CONNECT_DATA=(SERVER=DEDICATED)(SERVICE_NAME=GEAPROD)))";
                OracleConn.Open();
            }
            catch (Exception)
            {
                if (OracleConn.State == ConnectionState.Open)
                {
                    OracleConn.Close();
                }
                throw;
            }
        }

        public bool Validations()
        {
            try
            {
                if (!validaciones.ValidarCarrilesCerrados(db, OracleConn))
                {
                    Notification = validaciones.Message;
                    return false;
                }
                else if (!validaciones.ValidarBolsas(db, OracleConn))
                {
                    Notification = validaciones.Message;
                    return false;
                }
                else if (!validaciones.ValidarComentarios(db, OracleConn))
                {
                    Notification = validaciones.Message;
                    return false;
                }
                else
                    return true;

            }
            catch (Exception)
            {

                throw;
            }
        }

        public void FillDB()
        {
            string Turno = validaciones.TurnoP;
            string Fecha = validaciones.FechaInicio.Substring(0 , 10); //Excluímos la fecha
            string NoPlaza = db.Tramos.FirstOrDefault().NumeroPlaza;
            string Delegacion = db.Tramos.FirstOrDefault().Delegacion;

            Archivo1A.BitacoraOperacion(Turno, Fecha, NoPlaza, Delegacion, OracleConn, db);
        }

        public string GetIp()
        {
            var ip = Dns.GetHostEntry(Dns.GetHostName());
            return ip.AddressList.LastOrDefault().ToString();
        }

        public void GetPlazaNumber(string Segmento)
        {
            //switch (Segmento)
            //{
            //    case "20": //Tepoztzotlán
            //        NoPlaza = "004";
            //        break;
            //    case "21": //Jorobas
            //        NoPlaza = "069";
            //        break;
            //    case "22": //Polotitlan
            //        NoPlaza = "070";
            //        break;
            //    case "23": //Palmillas
            //        NoPlaza = "005";
            //        break;
            //    case "24": //Chichimequillas
            //        NoPlaza = "027";
            //        break;
            //    case "25": //Queretaro
            //        NoPlaza = "006";
            //        break;
            //    case "27": //Libramiento
            //        NoPlaza = "061";
            //        break;
            //    case "28": //Villagrán
            //        NoPlaza = "083";
            //        break;
            //    case "29": //Cerro Gordo
            //        NoPlaza = "086";
            //        break;
            //    case "30": //Salamanca
            //        NoPlaza = "041";
            //        break; 
            //    default:
            //        break;
            //}
        }
    }
}
