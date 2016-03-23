using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using MongoDB.Driver.Wrappers;
using MongoDB.Bson;
namespace SqlLite
{
    class Program
    {

      

        static void Main(string[] args)
        {


            ExperimentMongoDb();
            Console.Read();
        }

        static void ExperimentSqlLite()
        {
            SQLiteConnection conn = new SQLiteConnection("data source=Robert");
            conn.Open();


            using (SQLiteCommand cmd = conn.CreateCommand())
            {

                for (int i = 0; i < 100; i++)
                {

                    cmd.CommandText = string.Format("insert into Person values('{0}')", "Robert" + i.ToString());
                    cmd.ExecuteNonQuery();

                }
            }
        }

        static void ExperimentMongoDb()
        {
            var mongo = MongoDB.Driver.MongoServer.Create();
            mongo.Connect();
           var db=mongo.GetDatabase("Movie");
           var movies = db.GetCollection("Movies");
           var movie = new BsonDocument();
           movie["title"] = "Star Wars";
           movie["ReleaseTime"] = DateTime.Now;
           movies.Insert(movie);
        }
    }
}
