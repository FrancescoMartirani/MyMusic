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

            string sql = @"SELECT * FROM [Album]";

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
                    Anno_Uscita = Convert.ToInt32(reader["ID"]),

                };

                listaAlbum.Add(album);

            }

            return listaAlbum;

        }

        public bool aggiungiBrano(Brano brano){

            string sql = @"INSERT INTO [Bands]
                        ([Nome])
                        OUTPUT INSERTED.ID
                        VALUES (@Nome)";

            string sql2 = @"INSERT INTO [Album]
                        ([Id_Band],
                        [Titolo])
                        OUTPUT INSERTED.ID
                        VALUES (@Id_Band, 
                                @Titolo)";

            string sql3 = @"INSERT INTO [Brani]
                        ([Titolo]
                            ,[Anno_Uscita] 
                            ,[Durata]
                            ,[Genere]
                            ,[Id_Band]
                            ,[Id_Album])
                        VALUES (@Titolo,
                                @Anno_Uscita,
                                @Durata,
                                @Genere,
                                @Id_Band,
                                @Id_Album)";

            using var connection = new SqlConnection(ConnectionString);
            connection.Open();

            using var command = new SqlCommand(sql, connection);
            command.Parameters.AddWithValue("@Nome", brano.Nome_Band);
            var id_band = Convert.ToInt32(command.ExecuteScalar());


            using var command2 = new SqlCommand(sql2, connection);
            command2.Parameters.AddWithValue("@Titolo", brano.Nome_Album);
            command2.Parameters.AddWithValue("@Id_Band", id_band);
            var id_album = Convert.ToInt32(command2.ExecuteScalar());

            using var command3 = new SqlCommand(sql3, connection);
            command3.Parameters.AddWithValue("@Titolo", brano.Titolo);
            command3.Parameters.AddWithValue("@Anno_Uscita", brano.Anno_Uscita);
            command3.Parameters.AddWithValue("@Durata", brano.Durata);
            command3.Parameters.AddWithValue("@Genere", brano.Genere);
            command3.Parameters.AddWithValue("@Id_Band", id_band);
            command3.Parameters.AddWithValue("@Id_Album", id_album);

            return command3.ExecuteNonQuery() > 1;

        }

        public bool eliminaBrano(Album album)
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
