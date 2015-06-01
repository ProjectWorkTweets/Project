import java.sql.CallableStatement;
import java.sql.PreparedStatement;
import java.sql.Statement;
import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.ResultSet;
import java.sql.SQLException;
import java.util.Map;
import java.util.Vector;

public class GetLanguages {
	public static void main(String args[]) {
		try {
			// Carichiamo un driver di tipo 1 (bridge jdbc-odbc)
			String driver = "org.postgresql.Driver";
			Class.forName(driver);
			// Creiamo la stringa di connessione
			String url = "jdbc:postgresql://52.16.255.142:5432/projectworktweets";
			// Otteniamo una connessione con username e password
			Connection con = DriverManager.getConnection(url, "enrico",
					"DioCano");
			Statement cmd = con.createStatement();
			// VIUSALIZZA
			String qry1 = "SELECT idlanguage, name, color FROM languages ORDER BY languages";
			String qry2 = "SELECT idtweet, country,tweet_text,creationdate FROM tweets";

			String qry3 = "SELECT tweets.country,languages.name AS language, to_char(tweets.creationdate,'Mon') as month, count(*) AS n_tweets "
					+ "FROM tweets "
					+ "INNER JOIN language_tweet ON tweets.idtweet=language_tweet.tweet_fk "
					+ "INNER JOIN languages ON language_tweet.language_fk=languages.idlanguage "
					+ "WHERE tweets.country = 'GB' AND extract(month from tweets.creationdate) = 5 "
					+ "GROUP BY tweets.country, language, month";

			// Vector<String> languages = new Vector<String>();
			Vector<String> language_tweets = new Vector<String>();
			// Vector<String> tweets = new Vector<String>();
			// Vector<String> countries = new Vector<String>();
			StringBuilder columns = new StringBuilder();
			StringBuilder vals = new StringBuilder();

			Get_Tweets g1 = new Get_Tweets();
			// g1.setCountry(language_tweets.get("country"));
			// INSERISCI
			
			 //for (int i = 1; i <= 2; i++) { 
			//country_fk,language_fk,month,n_tweet
			for(String tmp : language_tweets) {
			String qry4 ="INSERT INTO country_language (month,n_tweets,country_fk,language_fk) values (?,?,?,?)";
			int ins = cmd.executeUpdate(qry4);
			
			 
			 }
			 
			/*for(String tmp : language_tweets)
			{
			PreparedStatement ps = con.prepareStatement("INSERT into country_language (month,country_fk,language_fk) values (?,?)");
			ps.setString(1,tmp);
			ps.setString(2,tmp);
			ps.setString(3,tmp);
			ps.execute();
			}*/

			ResultSet res1 = cmd.executeQuery(qry3);
			// ResultSet res2 = cmd.executeQuery(qry2);
			System.out.println("Sei connesso");

			// Stampiamo i risultati riga per riga
			while (res1.next()) { //
				// System.out.println(res1.getString("id"));
				language_tweets.add(res1.getString("month"));
				language_tweets.add(res1.getString("n_tweets"));
				language_tweets.add(res1.getString("country"));
				language_tweets.add(res1.getString("language"));
				;
				// language_tweets.add(res1.getString("language_fk"));

				/*
				 * tweets.add(res2.getString("idtweet"));
				 * tweets.add(res2.getString("country"));
				 * tweets.add(res2.getString("tweet_text"));
				 * tweets.add(res2.getString("creationdate"));
				 */

			}

			// System.out.println(language_tweets.indexOf(language_tweets));
			System.out.println(language_tweets);// visualizza contenuto della
			// lista
			// System.out.println(tweets);
			// System.out.println(res1);

			res1.close();
			// res2.close();
			cmd.close();
			con.close();
		} catch (SQLException e) {
			e.printStackTrace();
		} catch (ClassNotFoundException e) {
			e.printStackTrace();
		}
	}
}
