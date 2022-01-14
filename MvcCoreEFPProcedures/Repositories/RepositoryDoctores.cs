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

namespace MvcCoreEFPProcedures.Repositories
{
    #region STORESPROCEDURES

//    CREATE PROCEDURE SP_ESPECIALIDADES
//    AS

//    select DISTINCT(especialidad) from doctor
//GO

//CREATE PROCEDURE SP_ALLDOCTORES
//AS

//    SELECT* FROM DOCTOR
//GO

//alter PROCEDURE SP_INCREMENTARSALARIO(@INCREMENTO INT, @ESPECIALIDAD VARCHAR(50))
//AS
//    UPDATE DOCTOR
//    set SALARIO = (SALARIO+@INCREMENTO)
//	WHERE ESPECIALIDAD = @ESPECIALIDAD
//GO

//alter PROCEDURE SP_DOCTORESESPECIALIDAD(@ESPECIALIDAD VARCHAR(50))
//	AS

//    SELECT* FROM DOCTOR
//   WHERE ESPECIALIDAD=@ESPECIALIDAD
//GO
    #endregion
    public class RepositoryDoctores
    {

        private EnfermosContext context;

        public RepositoryDoctores(EnfermosContext context)
        {
            this.context = context;
        }

        public List<string> GetEspecialidades()
        {
            using (DbCommand com =
              this.context.Database.GetDbConnection().CreateCommand())
            {
                string sql = "SP_ESPECIALIDADES";
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = sql;
                com.Connection.Open();
                DbDataReader reader = com.ExecuteReader();

                List<String> especialidades = new List<String>();
                while (reader.Read())
                {
                    String esp = reader["ESPECIALIDAD"].ToString();

                    especialidades.Add(esp);
                }
                reader.Close();
                com.Connection.Close();
                return especialidades;
            }

        }

        public List<Doctor> GetDoctores()
        {
            using (DbCommand com =
                 this.context.Database.GetDbConnection().CreateCommand())
            {
                string sql = "SP_ALLDOCTORES";
                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = sql;
                com.Connection.Open();
                DbDataReader reader = com.ExecuteReader();

                List<Doctor> doctores = new List<Doctor>();
                while (reader.Read())
                {
                    Doctor doctor = new Doctor();
                    doctor.NDoctor = int.Parse(reader["DOCTOR_NO"].ToString());
                    doctor.HospitalCod = int.Parse(reader["HOSPITAL_COD"].ToString());
                    doctor.Apellido = reader["APELLIDO"].ToString();
                    doctor.Especialidad = reader["ESPECIALIDAD"].ToString();
                    doctor.Salario = int.Parse(reader["SALARIO"].ToString());
                    doctores.Add(doctor);
                }
                reader.Close();
                com.Connection.Close();
                return doctores;
            }
        }

        public void IncrementarSalarioEspecialidad(int incremento, string especialidad)
        {
            string sql = "SP_INCREMENTARSALARIO @INCREMENTO, @ESPECIALIDAD";
            SqlParameter paminscripcion = new SqlParameter("@INCREMENTO", incremento);
            SqlParameter pamespecialidad = new SqlParameter("@ESPECIALIDAD", especialidad);
            List<SqlParameter> Params = new List<SqlParameter>();
            Params.Add(paminscripcion);
            Params.Add(pamespecialidad);
            this.context.Database.ExecuteSqlRaw(sql, Params);
        }

        public List<Doctor> GetDoctoresEspecialidad(string especialidad)
        {         
            using (DbCommand com =
                 this.context.Database.GetDbConnection().CreateCommand())
            {
                string sql = "SP_DOCTORESESPECIALIDAD @ESPECIALIDAD";
                SqlParameter pamespecialidad = new SqlParameter("@ESPECIALIDAD", especialidad);
                com.CommandType = CommandType.StoredProcedure;
                com.Parameters.Add(pamespecialidad);
                com.CommandText = sql;
                com.Connection.Open();
                DbDataReader reader = com.ExecuteReader();

                List<Doctor> doctores = new List<Doctor>();
                while (reader.Read())
                {
                    Doctor doctor = new Doctor();
                    doctor.NDoctor = int.Parse(reader["DOCTOR_NO"].ToString());
                    doctor.HospitalCod = int.Parse(reader["HOSPITAL_COD"].ToString());
                    doctor.Apellido = reader["APELLIDO"].ToString();
                    doctor.Especialidad = reader["ESPECIALIDAD"].ToString();
                    doctor.Salario = int.Parse(reader["SALARIO"].ToString());
                    doctores.Add(doctor);
                }
                com.Parameters.Clear();
                reader.Close();
                com.Connection.Close();
                return doctores;
            }
          
        }
    }
}
