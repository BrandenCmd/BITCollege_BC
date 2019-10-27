namespace BITCollege_BC.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddStoredProcedures : DbMigration
    {
        public override void Up()
        {
            //Call script to create the stored procedure
            this.Sql(Properties.Resource1.create_next_number);
        }
        
        public override void Down()
        {
            //Call script to drop the stored procedure
            this.Sql(Properties.Resource1.drop_next_number);
        }
    }
}
