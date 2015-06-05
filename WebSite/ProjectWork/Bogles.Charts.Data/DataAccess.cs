using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Npgsql;
using System.Configuration;
using System.Data.SqlClient;
using System.Web;
using Newtonsoft.Json;





namespace Bogles.Charts.Data
{
    public class DataAccess
    {


        string connectionString;



        public DataAccess()
        {
            this.connectionString = ConfigurationManager.ConnectionStrings["cs"].ConnectionString;
        }



        public string GetLanguages()
        {

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                List<Language> languages = new List<Language>();

                string query = @"
                                SELECT name, color
                                FROM languages";

                using (NpgsqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    command.CommandType = CommandType.Text;


                    NpgsqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        Language l = new Language();
                        l.title = reader["name"] as string;
                        l.color = reader["color"] as string;
                        l.idlanguage = 1;
                        languages.Add(l);
                    }


                    return JsonConvert.SerializeObject(languages);

                }
            }
        }

        public string GetMapData()
        {

            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                List<MapData> data = new List<MapData>();

                string query = @"
                                WITH summary AS 
                                    ( SELECT  countries.idcountry AS cid,countries.name as title,languages.name as language, n_tweet, color, ROW_NUMBER() OVER(PARTITION BY idcountry 
                                    ORDER BY country_language.n_tweet DESC) as rk
                                    FROM countries
                                    INNER JOIN country_language ON country_fk = idcountry
                                    INNER JOIN languages ON language_fk = idlanguage
                                    WHERE country_language.month = :mon)
                                SELECT s.*
                                FROM summary s
                                WHERE s.rk = 1";

                using (NpgsqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    command.CommandType = CommandType.Text;
                    command.Parameters.Add(new NpgsqlParameter(":mon", (DateTime.Today.Month -1).ToString()));

                    NpgsqlDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        MapData l = new MapData();
                        l.id = reader["cid"] as string;
                        l.title = reader["title"] as string;
                        l.customData = reader["language"] as string + " (" + reader["n_tweet"] as string + " tweets)";
                        l.color = reader["color"] as string;

                        data.Add(l);
                    }


                    return JsonConvert.SerializeObject(data);

                }
            }
        }

        public string GetChartData(string id)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();

                string jdata = "["; //apre stringa JSON

                List<CountryLanguage> data = new List<CountryLanguage>();

                string query = @"SELECT  languages.name as language, n_tweet, color, month
                                    FROM countries
                                    INNER JOIN country_language ON country_fk = idcountry
                                    INNER JOIN languages ON language_fk = idlanguage
                                 WHERE countries.idcountry = :id";

                using (NpgsqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    command.CommandType = CommandType.Text;
                    command.Parameters.Add(new NpgsqlParameter(":id", id));

                    NpgsqlDataReader reader = command.ExecuteReader();

                    
                    while (reader.Read())
                    {
                        CountryLanguage l = new CountryLanguage();
                        l.language = reader["language"] as string;
                        l.n_tweet = (int)reader["n_tweet"];
                        l.color = reader["color"] as string;
                        l.month = (int)(reader["month"]);

                        data.Add(l);
                    }

                    
                        int maxMonth = data.Max(r => r.month);
                        int minMonth = data.Min(r => r.month);


                        for (int k = minMonth; k <= maxMonth; k++)
                        {
                            string month = k >= 10 ? Convert.ToString(k) : "0" + Convert.ToString(k);
                            //apre la lista con il mese+anno in questione
                            jdata += "{ \"date\":" + "\"" + month + "-" + DateTime.Today.Year.ToString() + "\",";

                            //aggiunge i record di ciascun linguaggio per il mese in questione
                            foreach (CountryLanguage d in data)
                            {
                                if (d.month == k)
                                    jdata += " \"" + d.language + "\":" + d.n_tweet + ",";

                            }


                            jdata = jdata.TrimEnd(jdata[jdata.Length - 1]); //tolgo ultima virgola
                            //chiude la lista
                            jdata += "},";
                        }

                        jdata = jdata.TrimEnd(jdata[jdata.Length - 1]); //tolgo ultima virgola

                        jdata += "]"; //chiude stringa JSON
                        return jdata;
                    

                }

            }

        }

        public string GetCountryName(string id)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();



                string query = @"SELECT  name
                                    FROM countries
                                 WHERE idcountry = :id";

                using (NpgsqlCommand command = connection.CreateCommand())
                {
                    command.CommandText = query;
                    command.CommandType = CommandType.Text;
                    command.Parameters.Add(new NpgsqlParameter(":id", id));

                    NpgsqlDataReader reader = command.ExecuteReader();


                    if (reader.Read())
                    return reader["name"] as string;
                    
                    return null;


                }

            }
        }

    }
}
