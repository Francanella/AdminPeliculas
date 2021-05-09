namespace WebApplication2.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class urlImage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Peliculas", "URLImage", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Peliculas", "URLImage");
        }
    }
}
