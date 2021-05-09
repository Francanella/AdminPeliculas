namespace WebApplication2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class test : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ActorPeliculas", newName: "PeliculaActors");
            DropPrimaryKey("dbo.PeliculaActors");
            AddPrimaryKey("dbo.PeliculaActors", new[] { "Pelicula_Id", "Actor_Id" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.PeliculaActors");
            AddPrimaryKey("dbo.PeliculaActors", new[] { "Actor_Id", "Pelicula_Id" });
            RenameTable(name: "dbo.PeliculaActors", newName: "ActorPeliculas");
        }
    }
}
