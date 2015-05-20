import psycopg2

conn = psycopg2.connect("dbname=projectworktweets user=enrico host=52.16.255.142 port=5432 password=DioCano")
print("Connected.")
conn.close()

