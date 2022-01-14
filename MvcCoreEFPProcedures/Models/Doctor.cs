using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace MvcCoreEFPProcedures.Models
{
    [Table("DOCTOR")]
    public class Doctor
    {
        [Key]
        [Column("DOCTOR_NO")]
        public int NDoctor { get; set; }
        [Column("HOSPITAL_COD")]
        public int HospitalCod { get; set; }
        [Column("APELLIDO")]
        public string Apellido { get; set; }
        [Column("ESPECIALIDAD")]
        public string Especialidad { get; set; }
        [Column("SALARIO")]
        public int Salario { get; set; }


    }
}
