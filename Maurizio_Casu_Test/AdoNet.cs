using Maurizio_Casu_Test.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace Maurizio_Casu_Test
{
    public static class AdoNet
    {
        static string connectionStringSQL = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=GestioneSpese;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        public static void InsertSpesa()
        {
            using SqlConnection connessione = new SqlConnection(connectionStringSQL);
            try
            {
                connessione.Open();

                if (connessione.State == System.Data.ConnectionState.Open)
                {
                    Console.WriteLine("Connessi al DB");
                }
                else
                {
                    Console.WriteLine("Non connessi al DB");
                }
                
                Console.WriteLine("Inserire i dati spesa desiderati:");
                Console.WriteLine("Data:");
                DateTime dataSpesa = DateTime.Parse(Console.ReadLine());
                Console.WriteLine("Descrizione:");
                string descrizione = Console.ReadLine();
                Console.WriteLine("Utente:");
                string utente = Console.ReadLine();
                Console.WriteLine("Importo:");
                decimal importo = decimal.Parse(Console.ReadLine());
                Spesa s = new Spesa
                {
                    Data = dataSpesa,
                    Utente = utente,
                    Descrizione = descrizione,
                    Importo = importo
                };

                string insertSQL = $"insert into Spese(DataSpesa, Descrizione, Utente, Importo) values (@Data, @Descrizione, @Utente, @Importo)";

                SqlCommand insertCommand = connessione.CreateCommand();
                insertCommand.CommandText = insertSQL;
                insertCommand.Parameters.AddWithValue("@Data", s.Data);
                insertCommand.Parameters.AddWithValue("@Descrizione", s.Descrizione);
                insertCommand.Parameters.AddWithValue("@Utente", s.Utente);
                insertCommand.Parameters.AddWithValue("@Importo", s.Importo);
              
                int righeInserite = insertCommand.ExecuteNonQuery();
                if (righeInserite >= 1)
                {
                    Console.WriteLine($"{righeInserite} riga/righe inserite correttamente");
                }
                else
                {
                    Console.WriteLine("OOOOOPS...qualcosa non torna!");
                }

            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Errore SQL: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore generico: {ex.Message}");
            }
            finally
            {
                connessione.Close();
                Console.WriteLine("Connessione chiusa");
            }
        }

        public static void ApproveSpesa()
        {
            using SqlConnection connessione = new SqlConnection(connectionStringSQL);
            try
            {
                connessione.Open();

               
                string selectSQL = $"selectSQL * from Spese";
               

                SqlCommand selectCommand = connessione.CreateCommand();
                selectCommand.CommandType = System.Data.CommandType.Text;
                selectCommand.CommandText = "SELECT * FROM Spese";

                SqlDataReader reader = selectCommand.ExecuteReader();

                while(reader.Read())
                {
                    string formattedDate = ((DateTime)reader["DataSpesa"]).ToString("dd-MM-yyyy");
                    Console.WriteLine(reader["Id"].ToString() + "\t" + formattedDate + "\t" + reader["CategoriaId"].ToString() + "\t" + reader["Descrizione"] + "\t" + reader["Utente"] + "\t" + reader["Importo"].ToString() + "\t" + reader["Approvato"].ToString() + "\t");
                }
                Console.WriteLine("Inserire il codice id della spesa da approvare.");
                int idApprove = int.Parse(Console.ReadLine());

                string insertSQL = $"UPDATE Spese SET APPROVATO=1 WHERE ID={idApprove}";

                selectCommand.ExecuteReader();
                 while (reader.Read())
                {
                    string formattedDate = ((DateTime)reader["DataSpesa"]).ToString("dd-MM-yyyy");
                    Console.WriteLine(reader["Id"].ToString() + "\t" + formattedDate + "\t" + reader["CategoriaId"].ToString() + "\t" + reader["Descrizione"] + "\t" + reader["Utente"] + "\t" + reader["Importo"].ToString() + "\t" + reader["Approvato"].ToString() + "\t");
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Errore SQL: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore generico: {ex.Message}");
            }
            finally
            {
                connessione.Close();
                Console.WriteLine("Connessione chiusa");
            }


        }

        public static void DeleteSpesa()
        {
            DataSet dataset = new DataSet();
            SqlConnection connessione;
            using (connessione = new SqlConnection(connectionStringSQL));
            {

                try
                {
                    connessione.Open();


                    SqlDataAdapter Adapter = new SqlDataAdapter();
                   
                    Adapter.SelectCommand = new SqlCommand("Select * from Spesa", connessione);
                    Adapter.MissingSchemaAction = MissingSchemaAction.AddWithKey;

                    var spese = Adapter.Fill(dataset, "Spesa");

                    
                    foreach (DataRow reader in dataset.Tables["Spesa"].Rows)
                    {
                        string formattedDate = ((DateTime)reader["DataSpesa"]).ToString("dd-MM-yyyy");
                        Console.WriteLine(reader["Id"].ToString() + "\t" + formattedDate + "\t" + reader["CategoriaId"].ToString() + reader["Descrizione"] + reader["Utente"] + reader["Importo"].ToString() + reader["Approvato"].ToString() + "\t"); ;
                    }

                }
                catch (SqlException ex)
                {
                    Console.WriteLine($"Errore SQL: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Errore generico: {ex.Message}");
                }
                finally
                {
                    connessione.Close();
                    Console.WriteLine("Connessione chiusa");
                }
            }
            using (connessione = new SqlConnection(connectionStringSQL)) ;
            {
                Console.WriteLine(connectionStringSQL);
                connessione.Open();


                SqlCommand selectCommand;

                /* using (selectCommand = connessione.CreateCommand());
                 {

                     selectCommand.CommandType = System.Data.CommandType.Text;
                     selectCommand.CommandText = "SELECT * FROM Spese";

                     SqlDataReader reader = selectCommand.ExecuteReader();

                     while (reader.Read())
                     {
                         string formattedDate = ((DateTime)reader["DataSpesa"]).ToString("dd-MM-yyyy");
                         Console.WriteLine(reader["Id"].ToString() + "\t" + formattedDate + "\t" + reader["CategoriaId"].ToString() + reader["Descrizione"] + reader["Utente"] + reader["Importo"].ToString() + reader["Approvato"].ToString() + "\t");
                     }
                     reader.Close();
                 }*/
                try
                {

                    Console.WriteLine("Inserire il codice id della spesa da cancellare.");
                    int idApprove = int.Parse(Console.ReadLine());

                    using (selectCommand = connessione.CreateCommand());
                    {

                        string insertSQL = $"DELETE from Spese WHERE ID={idApprove}";
                        selectCommand.CommandText = insertSQL;
                        SqlDataReader reader = selectCommand.ExecuteReader();

                        while (reader.Read())
                        {
                            string formattedDate = ((DateTime)reader["DataSpesa"]).ToString("dd-MM-yyyy");
                            Console.WriteLine(reader["Id"].ToString() + "\t" + formattedDate + "\t" + reader["CategoriaId"].ToString() + "\t" + reader["Descrizione"] + "\t" + reader["Utente"] + "\t" + reader["Importo"].ToString() + "\t" + reader["Approvato"].ToString() + "\t");
                        }
                        reader.Close();
                    }
                }
                
                catch (SqlException ex)
                {
                    Console.WriteLine($"Errore SQL: {ex.Message}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Errore generico: {ex.Message}");
                }
                finally
                {
                    connessione.Close();
                    Console.WriteLine("Connessione chiusa");
                }
            }
        }
        public static void ShowApprovedSpesa()
        { 
            using SqlConnection connessione = new SqlConnection(connectionStringSQL);
            try
            {
                connessione.Open();


                string selectSQL = $"selectSQL * from Spese";
                SqlCommand selectCommand;

                using (selectCommand = connessione.CreateCommand());
                {

                    selectCommand.CommandType = System.Data.CommandType.Text;
                    selectCommand.CommandText = "SELECT * FROM Spese WHERE APPROVATO=1";

                    SqlDataReader reader = selectCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        string formattedDate = ((DateTime)reader["DataSpesa"]).ToString("dd-MM-yyyy");
                        Console.WriteLine(reader["Id"].ToString() + "\t" + formattedDate + "\t" + reader["CategoriaId"].ToString() + reader["Descrizione"] + reader["Utente"] + reader["Importo"].ToString() + reader["Approvato"].ToString() + "\t");
                    }
                    reader.Close();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Errore SQL: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore generico: {ex.Message}");
            }
            finally
            {
                connessione.Close();
                Console.WriteLine("Connessione chiusa");
            }


        }
        public static void ShowUserSpesa()
        {
            using SqlConnection connessione = new SqlConnection(connectionStringSQL);
            try
            {
                connessione.Open();
                
                SqlCommand selectCommand;
                Console.WriteLine("Inserire l'utente del quale si vogliono ricercare le spese:");
                string utente = Console.ReadLine();

                using (selectCommand = connessione.CreateCommand()) ;
                {

                    selectCommand.CommandType = System.Data.CommandType.Text;
                    selectCommand.CommandText = $"SELECT * FROM Spese WHERE Utente='{utente}'";

                    SqlDataReader reader = selectCommand.ExecuteReader();

                    while (reader.Read())
                    {
                        string formattedDate = ((DateTime)reader["DataSpesa"]).ToString("dd-MM-yyyy");
                        Console.WriteLine(reader["Id"].ToString() + "\t" + formattedDate + "\t" + reader["CategoriaId"].ToString() + reader["Descrizione"] + reader["Utente"] + reader["Importo"].ToString() + reader["Approvato"].ToString() + "\t");
                    }
                    reader.Close();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Errore SQL: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore generico: {ex.Message}");
            }
            finally
            {
                connessione.Close();
                Console.WriteLine("Connessione chiusa");
            }
        }
        public static void ShowCategorySpesa()
        {
            using SqlConnection connessione = new SqlConnection(connectionStringSQL);
            try
            {
                connessione.Open();

                SqlCommand selectCommand;

                using (selectCommand = connessione.CreateCommand()) ;
                {

                    selectCommand.CommandType = System.Data.CommandType.Text;
                    selectCommand.CommandText = "select count(Categoria) as Quantita from Categorie c join Spese s on c.Id = s.CategoriaId group by Categoria ";

                    SqlDataReader reader = selectCommand.ExecuteReader();

                    while (reader.Read())
                    {
                       
                        Console.WriteLine(reader["Quantita"].ToString() + "\t");
                    }
                    reader.Close();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine($"Errore SQL: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Errore generico: {ex.Message}");
            }
            finally
            {
                connessione.Close();
                Console.WriteLine("Connessione chiusa");
            }


        }

    }
}
