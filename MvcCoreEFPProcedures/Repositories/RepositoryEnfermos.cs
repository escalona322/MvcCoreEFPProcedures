using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using MvcCoreEFPProcedures.Data;
using MvcCoreEFPProcedures.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Threading.Tasks;

#region PROCEDURES ALMACENADAS
//create procedure SP_ALLENFERMOS
//AS 
//	SELECT * FROM ENFERMO
//GO

//CREATE PROCEDURE SP_FINDENEFEWRMO(@INSCRIPCION INT)
//AS
//	SELECT * FROM ENFERMO
//	WHERE INSCRIPCION = @INSCRIPCION
//GO

//CREATE PROCEDURE SP_DELETEENFEDRMO(@INSCRIPCION INT)
//AS
//    DELETE FROM ENFERMO
//	WHERE INSCRIPCION = @INSCRIPCION
//GO
#endregion
namespace MvcCoreEFPProcedures.Repositories
{
    public class RepositoryEnfermos
    {

        private EnfermosContext context;

        public RepositoryEnfermos(EnfermosContext context)
        {
            this.context = context;
        }

        public List<Enfermo> GetEnfermos()
        {

            using (DbCommand com =
                this.context.Database.GetDbConnection().CreateCommand())
            {
                string sql = "SP_ALLENFERMOS";
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = sql;
                com.Connection.Open();
                DbDataReader reader = com.ExecuteReader();

                List<Enfermo> enfermos = new List<Enfermo>();
                while (reader.Read())
                {
                    Enfermo enfermo = new Enfermo();
                    enfermo.Inscripcion = int.Parse(reader["INSCRIPCION"].ToString());
                    enfermo.Apellido = reader["APELLIDO"].ToString();
                    enfermo.Direccion = reader["DIRECCION"].ToString();
                    enfermo.FechaNac = DateTime.Parse(reader["FECHA_NAC"].ToString());
                    enfermo.Sexo = reader["S"].ToString();
                    enfermo.NSS = reader["NSS"].ToString();

                    enfermos.Add(enfermo);
                }
                reader.Close();
                com.Connection.Close();
                return enfermos;

            }
        }

        public Enfermo FindEnfermo(int inscripcion)
        {
            string sql = "SP_FINDENFERMO @INSCRIPCION";

            SqlParameter paraminscripcion = new SqlParameter("@INSCRIPCION", inscripcion);

            var consulta = this.context.Enfermos.FromSqlRaw(sql, paraminscripcion);

            Enfermo enfermo = consulta.AsEnumerable().FirstOrDefault();
            return enfermo;
        }

        public void DeleteEnfermo(int inscripcion)
        {
            string sql = "SP_DELETEENFERMO @INSCRIPCION";
            SqlParameter paminscripcion = new SqlParameter("@INSCRIPCION", inscripcion);
            this.context.Database.ExecuteSqlRaw(sql, paminscripcion);
        }
    }
}
