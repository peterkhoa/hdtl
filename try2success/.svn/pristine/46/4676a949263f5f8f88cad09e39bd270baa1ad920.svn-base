using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace hoachdinhtuonglai
{
    public class TestingCollection : List<Testing>
    {
        public TestingCollection() { }

        public TestingCollection(IEnumerable<Testing> list):base (list){}
    }

    public class Testing
    {
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; }
        }
        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; }
        }

       
    }

    public static class TestingPP
    {
        private static readonly string SelectAllRow = @"Select * from testing";

        private static readonly string SelectByKeyUserName = @"Select * from Testing where name like %@name%";

        private static readonly string SelectByKeyID = @"Select * from Testing where id =@0";

        private static readonly string InsertData = @"Insert into Testing(Name) values(@name)";
        private static readonly string UpdateData = @"Update Testing set name=@name where id=@id";
        static Database petapoco = ConnectionPP.getConnection();

        public static TestingCollection SelectAll()
        {
            //var testing = null;
            //var petapoco = new Database(SqlHelper.ConnectionString, "System.Data.SqlClient");
            ////petapoco.Connection.Open();
            //petapoco.Connection.Open();
            petapoco.EnableAutoSelect = false;
            //petapoco.EnableNamedParams = false;
            petapoco.ForceDateTimesToUtc = false;

            //Create
            int x = petapoco.Execute(SelectAllRow);

            //Read
            TestingCollection Testing = new TestingCollection(petapoco.Fetch<Testing>(SelectAllRow));
            //petapoco.Connection.Close();
            return Testing;

            
        }

        public static Testing SelectById(int id)
        {
            //var petapoco = new Database(SqlHelper.ConnectionString, "System.Data.SqlClient");
            //petapoco.Connection.Open();
            //petapoco.EnableNamedParams = false;
            petapoco.EnableAutoSelect = false;
            petapoco.ForceDateTimesToUtc = false;


            //petapoco.Execute(SelectByKeyID, id,);

            Testing testing = petapoco.FirstOrDefault<Testing>(SelectByKeyID,id);
            return testing;
        }

        public static long Insert(Testing testing)
        {
            //var petapoco = ConnectionPP.getConnection();
            petapoco.EnableAutoSelect = false;
            petapoco.ForceDateTimesToUtc = false;

            var x = petapoco.Insert("Testing","ID",true,testing);
            petapoco.CloseSharedConnection();
            return long.Parse(x.ToString());
        }
    }
}
