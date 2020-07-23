using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace School.Web.Data.Entities
{
    public class Discipline : IEntity
    {
        public int Id { get; set; }



        [Display(Name = "Disciplina")]
        [MaxLength(50, ErrorMessage = "O campo '{0}' permite no máximo {1} caracteres")]
        [Required]
        public string DisciplineName { get; set; }



        [Display(Name = "Descrição")]
        public string Description { get; set; }



        [Display(Name = "Carga Horária")]
        public int Workload { get; set; }



        [Display(Name = "Máximo de alunos")] //Talvez mudar esta propriedade para a classe Course????
        public int StudentsMaxQuantity { get; set; }
    }
}
