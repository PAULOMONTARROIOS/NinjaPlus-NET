using Npgsql;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace NinjaPlus.Lib
{
    public static class Database
    {
        private static NpgsqlConnection Connection()
        {
            var connectionString = "Host=pgdb;Username=postgres;Password=qaninja;Database=ninjaplus";
            var connection = new NpgsqlConnection(connectionString);
            connection.Open();

            return connection;
        }

        public static void RemoveByTitle(string title)
        {
            var query = $"DELETE FROM movies WHERE title = '{title}';";

            var command = new NpgsqlCommand(query, Connection());
            command.ExecuteReader();

            Connection().Close();
        }

        public static void InsertMovies()
        {
            var dataSql = Environment.CurrentDirectory + "\\Data\\data.sql";
            var query = File.ReadAllText(dataSql);

            var commnad = new NpgsqlCommand(query, Connection());
            commnad.ExecuteReader();

            Connection().Close();
        }

        public static void DeleteMovies()
        {
            var query = "DELETE FROM movies where title not in ('Resident Evil');";

            var commnad = new NpgsqlCommand(query, Connection());
            commnad.ExecuteReader();

            Connection().Close();
        }

    }
}
