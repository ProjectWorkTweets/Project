import psycopg2

def connection():
	try:
		conn = psycopg2.connect("dbname='ecomm' user='ecommerce' host='localhost' password='password'")
		print("Connected.")
		conn.close()
	except:
		print("Can't connect to the DB.")