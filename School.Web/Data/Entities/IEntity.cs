namespace School.Web.Data.Entities
{
    //A interface IEntity é uma entidade que possui as caracteristicas que são comuns a todas as entidades, como neste exemplo o Id. Todas as entidades vão então herdar desta IEntity
    //Como as classes passam a ligar seus Id a IEntity, não existirá Ids repetidos nem em classes diferentes.

    public interface IEntity
    {
        int Id { get; set; }

    }
}
