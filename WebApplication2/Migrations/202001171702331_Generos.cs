namespace WebApplication2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Generos : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Generos",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Nombre = c.String(nullable: false),
                        FechaCreacion = c.DateTime(nullable: false),
                        FechaEdicion = c.DateTime(nullable: false),
                        Eliminado = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Peliculas", "GeneroId", c => c.Guid());
            CreateIndex("dbo.Peliculas", "GeneroId");
            AddForeignKey("dbo.Peliculas", "GeneroId", "dbo.Generos", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Peliculas", "GeneroId", "dbo.Generos");
            DropIndex("dbo.Peliculas", new[] { "GeneroId" });
            DropColumn("dbo.Peliculas", "GeneroId");
            DropTable("dbo.Generos");
        }
    }
}
