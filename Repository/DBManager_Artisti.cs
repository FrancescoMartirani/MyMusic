using MyMusic.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace MyMusic.Repository

{
    public class DBManager_Artisti
    {

        private static string ConnectionString = @"Server = ACADEMYNETPD01\SQLEXPRESS; Database = MyMusic; Trusted_Connection = True; ";

        public List<Artista> getArtisti()
        {

            List<Artista> listaArtisti = new List<Artista>();;

            string sql = @"SELECT [Bands].[Nome] AS Nome_Band, [Artisti].[ID] AS Id_Artista, [Artisti].[Nome] AS Nome_Artista,
                           [Artisti].[Cognome], [Artisti].[Nome_Arte]
                           FROM [Bands], [Artisti]
                            WHERE [Bands].[ID] = [Artisti].[Id_Band]";

            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            using var command = new SqlCommand(sql, connection);
            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                var artista = new Artista
                {
                    ID = Convert.ToInt32(reader["Id_Artista"]),
                    Nome = reader["Nome_Artista"].ToString(),
                    Cognome = reader["Cognome"].ToString(),
                    Nome_Arte = reader["Nome_Arte"].ToString(),
                    Nome_Band = reader["Nome_Band"].ToString(),

                };

                listaArtisti.Add(artista);

            }

            return listaArtisti;

        }

        public bool aggiungiArtista(Artista artista){

            string sql_control = @"SELECT [Bands].[ID] FROM [Bands]
                                   WHERE [Bands].[Nome] = @Nome";

            using var connection = new SqlConnection(ConnectionString);
            connection.Open();

            using var command_control = new SqlCommand(sql_control, connection);
            command_control.Parameters.AddWithValue("@Nome", artista.Nome_Band);

            var id_band = Convert.ToInt32(command_control.ExecuteScalar());

            if (id_band != null)
            {

                string sql_insert = @"INSERT INTO [Artisti]
                        ([Nome],
                        [Cognome],
                        [Nome_Arte],
                        [Id_Band])
                        VALUES (@Nome,
                        @Cognome,
                        @Nome_Arte,
                        @Id_Band)";

                using var command_insert = new SqlCommand(sql_insert, connection);
       
                command_insert.Parameters.AddWithValue("@Nome", artista.Nome);
                command_insert.Parameters.AddWithValue("@Cognome", artista.Cognome);
                command_insert.Parameters.AddWithValue("@Id_Band", id_band);
                command_insert.Parameters.AddWithValue("@Nome_Arte", artista.Nome_Arte);

                return command_insert.ExecuteNonQuery() > 1;

            }

            else
            {

                return false;

            }
            

        }

        public bool eliminaArtista(Artista artista)
        {

            string sql = @"DELETE FROM [Artisti]
                            WHERE [ID] = @ID;
                            DELETE FROM [Brani]
                            WHERE [Brani].[ID] = @ID";

            using var connection = new SqlConnection(ConnectionString);
            connection.Open();
            using var command = new SqlCommand(sql, connection);

            command.Parameters.AddWithValue("@ID", artista.ID);


            return command.ExecuteNonQuery() > 1;

        }

    }
}
