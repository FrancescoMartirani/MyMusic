using MyMusic.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace MyMusic.Repository

{
    public class DBManager_Brani
    {

        private static string ConnectionString = @"Server = ACADEMYNETPD01\SQLEXPRESS; Database = MyMusic; Trusted_Connection = True; ";

        public List<Brano> getBrani()
        {

            List<Brano> listaBrani = new List<Brano>();;

            string sql = @"SELECT [Album].[Titolo] AS Titolo_Album, [Bands].[Nome] AS Nome_Band, [Brani].* 
                            FROM [Album], [Bands], [Brani]
                            WHERE [Bands].[ID] = [Brani].[Id_Band] AND [Album].[ID] = [Brani].[Id_Album]";

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
                    Anno_Uscita = Convert.ToInt32(reader["Anno_Uscita"]),
                    Durata = reader["Durata"].ToString(),
                    Genere = reader["Genere"].ToString(),
                    Nome_Album = reader["Titolo_Album"].ToString(),
                    Nome_Band = reader["Nome_Band"].ToString()

                };

                listaBrani.Add(brano);

            }

            return listaBrani;

        }

        public bool aggiungiBrano(Brano brano){

            string sql_control = @"SELECT [Bands].[ID] FROM [Bands]
                        WHERE [Bands].[Nome] = @Nome";

            using var connection = new SqlConnection(ConnectionString);
            connection.Open();

            using var command_control = new SqlCommand(sql_control, connection);
            command_control.Parameters.AddWithValue("@Nome", brano.Nome_Band);
            var id_band = Convert.ToInt32(command_control.ExecuteScalar());

            if(id_band > 1)
            {

                string sql_control2 = @"SELECT [Album].[ID] FROM [Album]
                                 WHERE [Album].[Titolo] = @Titolo";
                string sql_control3 = @"SELECT [Album].[Id_Band] FROM [Album]
                                 WHERE [Album].[Titolo] = @Titolo";

                using var command_control2 = new SqlCommand(sql_control2, connection);
                command_control2.Parameters.AddWithValue("@Titolo", brano.Nome_Album);
                using var command_control3 = new SqlCommand(sql_control3, connection);
                command_control3.Parameters.AddWithValue("@Titolo", brano.Nome_Album);

                var id_album = Convert.ToInt32(command_control2.ExecuteScalar());
                var id_band_album = Convert.ToInt32(command_control3.ExecuteScalar());

                if(id_album > 1 && id_band_album == id_band)
                {

                    string sql_insert = @"INSERT INTO [Brani]
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

                    using var command_insert = new SqlCommand(sql_insert, connection);
                    command_insert.Parameters.AddWithValue("@Titolo", brano.Titolo);
                    command_insert.Parameters.AddWithValue("@Anno_Uscita", brano.Anno_Uscita);
                    command_insert.Parameters.AddWithValue("@Durata", brano.Durata);
                    command_insert.Parameters.AddWithValue("@Genere", brano.Genere);
                    command_insert.Parameters.AddWithValue("@Id_Band", id_band);
                    command_insert.Parameters.AddWithValue("@Id_Album", id_album);

                    return command_insert.ExecuteNonQuery() > 1;

                }

                else
                {

                    return false;

                }


            }

            else
            {

                return false;

            }

        }

        public bool eliminaBrano(Brano brano)
        {

            string sql = @"DELETE FROM [Brani]
                            WHERE [ID] = @ID";

            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            using var command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@ID", brano.ID);


            return command.ExecuteNonQuery() > 1;

        }

    }
}
