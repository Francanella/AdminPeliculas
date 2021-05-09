namespace WebApplication2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Actores : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Actors",
                c => new
                    {
                        Id = c.Guid(nullable: false),
                        Nombre = c.String(nullable: false),
                        FechaCreacion = c.DateTime(nullable: false),
                        FechaEdicion = c.DateTime(nullable: false),
                        Eliminado = c.Boolean(nullable: false),
                        Pelicula_Id = c.Guid(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Peliculas", t => t.Pelicula_Id)
                .Index(t => t.Pelicula_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Actors", "Pelicula_Id", "dbo.Peliculas");
            DropIndex("dbo.Actors", new[] { "Pelicula_Id" });
            DropTable("dbo.Actors");
        }
    }
}
