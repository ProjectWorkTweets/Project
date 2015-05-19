using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using System.Configuration;
using System.Data.SqlClient;

namespace Bogles.Charts.Data
{
    public class DataAccess
    {


        string connectionString;

        public DataAccess()
        {
            this.connectionString = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
        }

        Dictionary<DataAccess, DataAccess> data = new Dictionary<DataAccess, DataAccess>();


        public Int64 GetProducts()
        {
            //string cs = "Server=192.168.0.9;Port=5432;Database=ecomm;User Id=ecommerce;Password=password";
            //int count = 0;

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                string query = @"
SELECT COUNT (*)
FROM prodotti
WHERE prezzo > :min AND prezzo < :max";
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    command.CommandType = CommandType.Text;
                    command.Parameters.Add(new NpgsqlParameter("min", 10));
                    command.Parameters.Add(new NpgsqlParameter("max", 13));
                    return (Int64)command.ExecuteScalar();
                    //while (reader.Read())
                    //count++;
                    

                }
            }
            
            
        }

        

        //=========================================================================================
        //======================== FUNZIONE PER RITORNARE TUTTI I PRODOTTI =======================
        //=========================================================================================

//        public List<Supplier> GetSuppliers()
//        {
//            using (SqlConnection connection = new SqlConnection(connectionString))
//            {
//                connection.Open();
//                string query = @"
//                                SELECT [SupplierID]
//                                 ,[CompanyName]
//                                 ,[ContactName]
//                                 ,[ContactTitle]
//                                 ,[Address]
//                                 ,[City]
//                                 ,[Region]
//                                 ,[PostalCode]
//                                 ,[Country]
//                                 ,[Phone]
//                                 ,[Fax]
//                                 ,[HomePage]
//                                FROM [dbo].[Suppliers]";

//                using (SqlCommand command = connection.CreateCommand())
//                {
//                    command.CommandText = query;
//                    command.CommandType = CommandType.Text;

//                    SqlDataReader reader = command.ExecuteReader();
//                    List<Supplier> Suppliers = new List<Supplier>();
//                    while (reader.Read())
//                    {
//                        Supplier p = new Supplier();
//                        p.SupplierID = (int)reader["SupplierID"];
//                        p.CompanyName = reader["CompanyName"] as string;
//                        p.ContactName = reader["ContactName"] == DBNull.Value ? null : reader["ContactName"] as string;
//                        p.ContactTitle = reader["ContactTitle"] == DBNull.Value ? null : reader["ContactTitle"] as string;
//                        p.Address = reader["Address"] == DBNull.Value ? null : reader["Address"] as string;
//                        p.City = reader["City"] == DBNull.Value ? null : reader["City"] as string;
//                        p.Region = reader["Region"] == DBNull.Value ? null : reader["Region"] as string;
//                        p.PostalCode = reader["PostalCode"] == DBNull.Value ? null : reader["PostalCode"] as string;
//                        p.Country = reader["Country"] == DBNull.Value ? null : reader["Country"] as string;
//                        p.Phone = reader["Phone"] == DBNull.Value ? null : reader["Phone"] as string;
//                        p.Fax = reader["Fax"] == DBNull.Value ? null : reader["Fax"] as string;
//                        p.HomePage = reader["HomePage"] == DBNull.Value ? null : reader["HomePage"] as string;

//                        Suppliers.Add(p);

//                    }
//                    return Suppliers;
//                }

//            }

//        }
        //.........................................................................................
        //.........................................................................................




        /// <summary>
        /// INSERIMENTO Supplier
        /// </summary>
        /// <param name="Supplier"></param>
//        public void InsertSupplier(Supplier Supplier)
//        {
//            using (SqlConnection connection = new SqlConnection(connectionString))
//            {
//                connection.Open();
//                string query = @"
//                                INSERT INTO [dbo].[Suppliers]
//                                ([CompanyName]
//                                 ,[ContactName]
//                                 ,[ContactTitle]
//                                 ,[Address]
//                                 ,[City]
//                                 ,[Region]
//                                 ,[PostalCode]
//                                 ,[Country]
//                                 ,[Phone]
//                                 ,[Fax]
//                                 ,[HomePage])
//                               VALUES
//                               (@CompanyName
//                               ,@ContactName
//                               ,@ContactTitle
//                               ,@Address
//                               ,@City
//                               ,@Region
//                               ,@PostalCode
//                               ,@Country
//                               ,@Phone
//                               ,@Fax
//                               ,@HomePage) ";

//                using (SqlCommand command = connection.CreateCommand())
//                {
//                    command.CommandText = query;
//                    command.CommandType = CommandType.Text;
//                    command.Parameters.Add(new SqlParameter("@CompanyName", Supplier.CompanyName));
//                    command.Parameters.Add(new SqlParameter("@ContactName", Supplier.ContactName));
//                    command.Parameters.Add(new SqlParameter("@ContactTitle", Supplier.ContactTitle));
//                    command.Parameters.Add(new SqlParameter("@Address", Supplier.Address));
//                    command.Parameters.Add(new SqlParameter("@City", Supplier.City));
//                    command.Parameters.Add(new SqlParameter("@Region", Supplier.Region));
//                    command.Parameters.Add(new SqlParameter("@PostalCode", Supplier.PostalCode));
//                    command.Parameters.Add(new SqlParameter("@Country", Supplier.Country));
//                    command.Parameters.Add(new SqlParameter("@Phone", Supplier.Phone));
//                    command.Parameters.Add(new SqlParameter("@Fax", Supplier.Fax));
//                    command.Parameters.Add(new SqlParameter("@HomePage", Supplier.HomePage));

//                    command.ExecuteNonQuery();


//                }
//            }

//        }



        /// <summary>
        /// Ritorna il numero di prodotti esisteni
        /// </summary>
        /// <returns></returns>
        public int GetSuppliersCount()
        {
            using (var connection = new SqlConnection(this.connectionString))
            {
                connection.Open();
                string query = @"SELECT COUNT (*) FROM Suppliers";
                using (var command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    command.CommandType = CommandType.Text;
                    return (int)command.ExecuteScalar();
                }

            }

        }


    }
}
