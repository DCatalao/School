using System;
using System.ComponentModel.DataAnnotations;

namespace School.Web.Data.Entities
{
    public class Course
    {
        // Quando se diz que uma propriedade tem o nome Id e possui um valor Int, o Entity Framework já sabe que esta propriedade será chave primária
        // e não é necessário informar através dos data annotations
        public int Id { get; set; }


        [Display(Name = "Nome")]
        [MaxLength(50, ErrorMessage ="O campo '{0}' permite no máximo {1} caracteres")]
        [Required]
        public string CourseName { get; set; }


        [Display(Name = "Descrição")]
        public string Description { get; set; }


        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)] // Data format string 0:C2 quer dizer que é um valor numérico em formato currency e possui 2 casas decimais
        public decimal Price { get; set; }                                          // Apply Format In Edit Mode = false significa que na base de dados os dados inseridos não serão gravados
                                                                                    // como els estão apresentados e sim como estão por default.

        [Display(Name = "Logo do Curso")]
        public string ImageLogoURL { get; set; }


        [Display(Name = "Data de início")]
        public DateTime? StartDate { get; set; }


        [Display(Name = "Data de término")]
        public DateTime? EndDate { get; set; }

    }
}
