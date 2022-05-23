using MyMusic.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace MyMusic.Repository

{
    public class DBManager_Album
    {

        private static string ConnectionString = @"Server = ACADEMYNETPD01\SQLEXPRESS; Database = MyMusic; Trusted_Connection = True; ";

        public List<Album> getAlbum()
        {

            List<Album> listaAlbum = new List<Album>();;

            string sql = @"SELECT [Bands].[Nome] AS Nome_Band, [Album].* 
                        FROM [Bands], [Album]
                        WHERE [Bands].[ID] = [Album].[Id_Band]";

            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            using var command = new SqlCommand(sql, connection);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var album = new Album
                {

                    ID = Convert.ToInt32(reader["ID"]),
                    Titolo = reader["Titolo"].ToString(),
                    Anno_Uscita = Convert.ToInt32(reader["Anno_Uscita"]),
                    Nome_Band = reader["Nome_Band"].ToString()

                };

                listaAlbum.Add(album);

            }

            return listaAlbum;

        }

        public bool aggiungiAlbum(Album album)
        {

            string sql_control = @"SELECT [Bands].[ID] FROM [Bands]
                                   WHERE [Bands].[Nome] = @Nome";

            using var connection = new SqlConnection(ConnectionString);
            connection.Open();

            using var command_control = new SqlCommand(sql_control, connection);
            command_control.Parameters.AddWithValue("@Nome", album.Nome_Band);

            var id_band = Convert.ToInt32(command_control.ExecuteScalar());

            if (id_band > 0)
            {

                string sql_insert = @"INSERT INTO [Album]
                        ([Titolo],
                        [Anno_Uscita],
                        [Id_Band])
                        VALUES (@Titolo,
                        @Anno_Uscita,
                        @Id_Band)";

                using var command_insert = new SqlCommand(sql_insert, connection);

                command_insert.Parameters.AddWithValue("@Titolo", album.Titolo);
                command_insert.Parameters.AddWithValue("@Anno_Uscita", album.Anno_Uscita);
                command_insert.Parameters.AddWithValue("@Id_Band", id_band);

                return command_insert.ExecuteNonQuery() > 1;

            }

            else
            {

                return false;

            }


        }

        public bool eliminaAlbum(Album album)
        {

            string sql = @"DELETE FROM [Album]
                            WHERE [ID] = @ID";

            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            using var command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@ID", album.ID);


            return command.ExecuteNonQuery() > 1;

        }

    }
}
