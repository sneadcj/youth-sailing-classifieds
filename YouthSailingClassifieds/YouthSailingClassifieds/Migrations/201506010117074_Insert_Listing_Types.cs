namespace YouthSailingClassifieds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Insert_Listing_Types : DbMigration
    {
        public override void Up()
        {
            Sql(@"Insert Into ListingTypes(Description)
values('Optimist')

Insert Into ListingTypes(Description)
values('Laser')

Insert Into ListingTypes(Description)
values('420')");
        }
        
        public override void Down()
        {
            Sql(@"delete from ListingTypes");
        }
    }
}
