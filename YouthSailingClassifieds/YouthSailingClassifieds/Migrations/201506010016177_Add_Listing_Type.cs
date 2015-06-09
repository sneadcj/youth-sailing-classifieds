namespace YouthSailingClassifieds.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Add_Listing_Type : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ListingTypes",
                c => new
                    {
                        ListingTypeId = c.Long(nullable: false, identity: true),
                        Description = c.String(maxLength: 500),
                    })
                .PrimaryKey(t => t.ListingTypeId);
            
            AddColumn("dbo.Listings", "ListingTypeId", c => c.Long());
            CreateIndex("dbo.Listings", "ListingTypeId");
            AddForeignKey("dbo.Listings", "ListingTypeId", "dbo.ListingTypes", "ListingTypeId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Listings", "ListingTypeId", "dbo.ListingTypes");
            DropIndex("dbo.Listings", new[] { "ListingTypeId" });
            DropColumn("dbo.Listings", "ListingTypeId");
            DropTable("dbo.ListingTypes");
        }
    }
}
