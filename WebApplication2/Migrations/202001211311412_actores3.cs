namespace WebApplication2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class actores3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Actors", "Pelicula_Id", "dbo.Peliculas");
            DropIndex("dbo.Actors", new[] { "Pelicula_Id" });
            CreateTable(
                "dbo.ActorPeliculas",
                c => new
                    {
                        Actor_Id = c.Guid(nullable: false),
                        Pelicula_Id = c.Guid(nullable: false),
                    })
                .PrimaryKey(t => new { t.Actor_Id, t.Pelicula_Id })
                .ForeignKey("dbo.Actors", t => t.Actor_Id, cascadeDelete: true)
                .ForeignKey("dbo.Peliculas", t => t.Pelicula_Id, cascadeDelete: true)
                .Index(t => t.Actor_Id)
                .Index(t => t.Pelicula_Id);
            
            DropColumn("dbo.Actors", "Pelicula_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Actors", "Pelicula_Id", c => c.Guid());
            DropForeignKey("dbo.ActorPeliculas", "Pelicula_Id", "dbo.Peliculas");
            DropForeignKey("dbo.ActorPeliculas", "Actor_Id", "dbo.Actors");
            DropIndex("dbo.ActorPeliculas", new[] { "Pelicula_Id" });
            DropIndex("dbo.ActorPeliculas", new[] { "Actor_Id" });
            DropTable("dbo.ActorPeliculas");
            CreateIndex("dbo.Actors", "Pelicula_Id");
            AddForeignKey("dbo.Actors", "Pelicula_Id", "dbo.Peliculas", "Id");
        }
    }
}
