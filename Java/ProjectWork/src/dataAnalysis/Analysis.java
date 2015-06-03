package dataAnalysis;

import java.io.FileInputStream;
import java.sql.*;
import java.util.*;
import java.util.Date;

public class Analysis {

	public static void main(String args[]) {

		try {

			// Prendo il mese (month) dalla data corrente
			Date date = new Date();
			Calendar cal = Calendar.getInstance();
			cal.setTime(date);
			int month = cal.get(Calendar.MONTH) + 1;

			// Carichiamo un driver di tipo 1 (bridge jdbc-odbc)
			String driver = "org.postgresql.Driver";
			Class.forName(driver);
			// Creiamo la stringa di connessione
			Properties properties = new Properties();
			properties.load(new FileInputStream("connessione.properties"));
			Connection conn = DriverManager.getConnection(
					properties.getProperty("url"),
					properties.getProperty("username"),
					properties.getProperty("password"));
			Statement st = conn.createStatement();

			String qry1 = "SELECT idcountry FROM countries";

			ResultSet res1 = st.executeQuery(qry1);
			List<String> countriesId = new ArrayList<String>();

			while (res1.next()) {
				countriesId.add(res1.getString("idcountry"));
			}

			for (String id : countriesId) {

				String qry2 = "SELECT languages.idlanguage AS language, count(*) AS n_tweets "
						+ "FROM tweets "
						+ "INNER JOIN language_tweet ON tweets.idtweet=language_tweet.tweet_fk "
						+ "INNER JOIN languages ON language_tweet.language_fk=languages.idlanguage "
						+ "WHERE tweets.country = ? AND extract(month from tweets.creationdate) = ? "
						+ "GROUP BY language";

				PreparedStatement pt = conn.prepareStatement(qry2);
				pt.setString(1, id);
				pt.setInt(2, month);

				ResultSet res2 = pt.executeQuery();

				while (res2.next()) {

					String qry3 = "SELECT count(*) AS num FROM country_language WHERE month = ? AND country_fk = ? AND language_fk = ?";
					PreparedStatement pt2 = conn.prepareStatement(qry3);
					pt2.setInt(1, month);
					pt2.setString(2, id);
					pt2.setInt(3, res2.getInt("language"));

					ResultSet res3 = pt2.executeQuery();

					res3.next();

					if (res3.getInt("num") == 0) {
						String qry4 = "INSERT INTO country_language (n_tweet, country_fk, language_fk, month) VALUES(?, ?, ?, ?)";
						PreparedStatement pt3 = conn.prepareStatement(qry4);
						pt3.setInt(1, res2.getInt("n_tweets"));
						pt3.setString(2, id);
						pt3.setInt(3, res2.getInt("language"));
						pt3.setInt(4, month);
						pt3.executeUpdate();

					} else {
						String qry5 = "UPDATE country_language SET n_tweet = ? WHERE month = ? AND country_fk = ? AND language_fk = ?";
						PreparedStatement pt4 = conn.prepareStatement(qry5);
						pt4.setInt(1, res2.getInt("n_tweets"));
						pt4.setInt(2, month);
						pt4.setString(3, id);
						pt4.setInt(4, res2.getInt("language"));
						pt4.executeUpdate();

					}

				}
			}

		} catch (Exception e) {
			e.printStackTrace();

		}

	}
}
