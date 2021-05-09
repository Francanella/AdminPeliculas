namespace WebApplication2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Genero2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Generos", "GeneroSeleccionado", c => c.Guid());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Generos", "GeneroSeleccionado");
        }
    }
}
