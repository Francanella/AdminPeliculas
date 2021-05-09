namespace WebApplication2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Peliculas", "FechaEstreno", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Peliculas", "FechaEstreno", c => c.DateTime(nullable: false));
        }
    }
}
