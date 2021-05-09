namespace WebApplication2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Genero3 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Generos", "GeneroId", c => c.Guid());
            DropColumn("dbo.Generos", "GeneroSeleccionado");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Generos", "GeneroSeleccionado", c => c.Guid());
            DropColumn("dbo.Generos", "GeneroId");
        }
    }
}
