using MyMusic.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace MyMusic.Repository

{
    public class DBManager_Bands
    {

        private static string ConnectionString = @"Server = ACADEMYNETPD01\SQLEXPRESS; Database = MyMusic; Trusted_Connection = True; ";

        public List<Band> getBands()
        {

            List<Band> listaBands = new List<Band>();;

            string sql = @"SELECT * FROM [Bands]";

            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            using var command = new SqlCommand(sql, connection);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var band = new Band
                {

                    ID = Convert.ToInt32(reader["ID"]),
                    Nome = reader["Nome"].ToString(),
                    Immagine = reader["Immagine"].ToString(),

                };

                listaBands.Add(band);

            }

            return listaBands;

        }

        public bool aggiungiBand(Band band){

            string sql = @"INSERT INTO [Bands]
                        ([Nome],
                         [Immagine])
                        VALUES (@Nome, 
                                @Immagine)";

            using var connection = new SqlConnection(ConnectionString);
            connection.Open();

            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@Nome", band.Nome);
            command.Parameters.AddWithValue("@Immagine", band.Immagine);

            return command.ExecuteNonQuery() > 1;

        }

        public bool eliminaBand(Band band)
        {

            string sql = @"DELETE FROM [Bands]
                            WHERE [ID] = @ID";

            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            using var command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@ID", band.ID);


            return command.ExecuteNonQuery() > 1;

        }

    }
}
