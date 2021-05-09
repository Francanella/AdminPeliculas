namespace WebApplication2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Sinopsis : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Peliculas", "Sinopsis", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Peliculas", "Sinopsis");
        }
    }
}
