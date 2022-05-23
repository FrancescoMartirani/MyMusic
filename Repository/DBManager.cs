using MyMusic.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace MyMusic.Repository

{
    public class DBManager
    {

        private static string ConnectionString = @"Server = ACADEMYNETPD01\SQLEXPRESS; Database = MyMusic; Trusted_Connection = True; ";

        public List<Brano> getBrani()
        {

            List<Brano> listaBrani = new List<Brano>();;

            string sql = @"SELECT [Album].[Titolo] AS Titolo_Album, [Bands].[Nome] AS Nome_Band, [Brani].* 
                            FROM [Album], [Bands], [Brani]
                            JOIN [Bands] as Bnd ON Bnd.[ID]  = [Brani].[ID]
                            JOIN [Album] AS A ON [Brani].[Id_Album] = A.[ID]";

            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            using var command = new SqlCommand(sql, connection);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var brano = new Brano
                {

                    ID = Convert.ToInt32(reader["ID"]),
                    Titolo = reader["Titolo"].ToString(),
                    Anno_Uscita = Convert.ToInt32(reader["ID"]),
                    Durata = reader["Durata"].ToString(),
                    Genere = reader["Genere"].ToString(),
                    Nome_Album = reader["Titolo_Album"].ToString(),
                    Nome_Band = reader["Nome_Band"].ToString()

                };

                listaBrani.Add(brano);

            }

            return listaBrani;

        }

    }
}
