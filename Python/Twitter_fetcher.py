

# standard library
import base64
import os

# 3rd party
import requests
import io, json
import psycopg2
import datetime
from logentries import LogentriesHandler
import logging


log = logging.getLogger('logentries')
log.setLevel(logging.INFO)
# Note if you have set up the logentries handler in Django, the following line is not necessary
log.addHandler(LogentriesHandler('ae523509-0ec4-47e2-920f-f24c12d15527'))


''' Dichiarazione di 3 vocabolari per gli stati e i linguaggi di programmazione '''

#Vocabulary "state name: twitter state id" (sono gli id associati allo stato su twitter (usufruibili con le API)
states_twitter_id = {
"Austria" : "30410557050f13a5",
"Belgium" : "78bfaf3f12c05982",
"Bulgaria" : "1ef1183ed7056dc1",
"Croatia" : "8d65596349ee2e01",
"Cyprus" : "f0af1239cbebb474",
"Czech Republic" : "6b5d375c346e3be9",
"Denmark" : "c29833e68a86e703",
"Estonia" : "e222580e9a5Bb499",
"Finland" : "e7c97cdfef3a741a",
"France" : "991b4344edc2d528",
"Germany" : "fdcd221ac44fa326",
"Great Britain" : "6416b8512febefc9",
"Greece" : "2ee7eeaa84dbe65a",
"Hungary" : "81b8dcbe189773f2",
"Ireland" : "78e9729ff12a648e",
"Italy" : "c799e2d3a79f810e",
"Latvia" : "d0e642e8a900f679",
"Lithuania" : "d5cde4dddd7e6f94",
"Luxembourg" : "7fb8d824354f11ea",
"Malta" : "1d834adff5d584df",
"The Netherlands" : "879d7cfc66c9c290",
"Poland" : "d9874951d5976bdf",
"Portugal" : "c9f6408fbe911554",
"Romania" : "f7531639e8db5e12",
"Slovakia" : "34ed2e67dd5a22bb",
"Slovenia" : "58f54743b1a62911",
"Spain" : "ecdce75d48b13b64",
"Sweeden" : "82b141af443cb1b8",
}

#Vocabulary "state name: DB table state id" eg.("Italy" : "IT")

states_db_id = {}

#Populates the dictionary with DB values
conn = psycopg2.connect("dbname='projectworktweets' user='enrico' host='52.16.255.142' password='DioCano'")
cur = conn.cursor()
cur.execute("SELECT * FROM countries;")
l = cur.fetchone()
while l != None:
    states_db_id[l[1]]=l[0]  #l[1] = name in countries table (Italy), l[0] = countryid in countries table (IT)
    l = cur.fetchone()
cur.close()

languages_db_id = {}

#Populates the dictionary with DB values
cur = conn.cursor()
cur.execute("SELECT idlanguage, name FROM languages;")
l = cur.fetchone()
while l != None:
    languages_db_id[l[1]]=l[0]  #l[1] = name in languages table (Java), l[0] = languageid in countries table (2)
    l = cur.fetchone()
cur.close()




def get_token():
    credentials = '{}:{}'.format('yAVb4Qk23P6xwjrk95lm0PA5g', 'dlmF4ROc9OB9wM7j8zbowGVYsoGUOKPwn1s7dBrzsFj6yA66wV')
    credentials_enc = base64.b64encode(credentials.encode())
    resp = requests.post(
        'https://api.twitter.com/oauth2/token',
        headers={'Authorization': 'Basic {}'.format(credentials_enc.decode())},
        data={'grant_type': 'client_credentials'}
    )
    resp.raise_for_status()
    resp_data = resp.json()
    return resp_data['access_token']


def get_tweets(token):

    '''
    Returns:
      a list of tweets (dictionaries) as returned by twitter API
    '''
    url = 'https://api.twitter.com/1.1/search/tweets.json'
    cur = conn.cursor()
    cur2 = conn.cursor()
    cur3 = conn.cursor()
    cur4 = conn.cursor()

    '''Create Folder "File"'''

    '''mypath = './File'
    if not os.path.exists(mypath):
        os.makedirs(mypath)'''
    for state in states_twitter_id:
        for language in languages_db_id:

            if language == "C++":
                resp = requests.get(
                    url,
                    params={'q': 'place:' + states_twitter_id[state] + ' cplusplus', 'count':3,'result_type':'recent'},
                    headers={'Authorization': 'Bearer {}'.format(token)})

            elif language == "C#":
                resp = requests.get(
                    url,
                    params={'q': 'place:' + states_twitter_id[state] + ' csharp', 'count':3,'result_type':'recent'},
                    headers={'Authorization': 'Bearer {}'.format(token)})

            elif language == "C":
                resp = requests.get(
                    url,
                    params={'q': 'place:' + states_twitter_id[state] + ' #c', 'count':3,'result_type':'recent'},
                    headers={'Authorization': 'Bearer {}'.format(token)})

            elif language == "PHP":
                resp = requests.get(
                    url,
                    params={'q': 'place:' + states_twitter_id[state] + ' #php', 'count':3,'result_type':'recent'},
                    headers={'Authorization': 'Bearer {}'.format(token)})

            else:
                resp = requests.get(
                    url,
                    params={'q': 'place:' + states_twitter_id[state] + ' ' + language, 'count':3,'result_type':'recent'},
                    headers={'Authorization': 'Bearer {}'.format(token)})

            resp.raise_for_status()
            data = resp.json()
            for tweet in data['statuses']:

                #Controllo che il tweet non sia gia stato salvato
                cur2.execute("SELECT count(idtweet) FROM tweets WHERE idtweet = %s", (str(tweet['id']),))

                if cur2.fetchone()[0] == 0:
                    #Creazione di un tipo "datetime" in python dalla data che ci arriva da twitter
                    ts = datetime.datetime.strptime(tweet['created_at'],'%a %b %d %H:%M:%S +0000 %Y').strftime('%Y-%m-%d')

                    cur.execute("INSERT INTO tweets (idtweet, country, tweet_text, creationdate) VALUES (%s, %s, %s, %s)", 
                        (tweet['id'], states_db_id[state], tweet['text'], ts))

                    cur.execute("INSERT INTO language_tweet(language_fk, tweet_fk) VALUES (%s, %s)", 
                        (languages_db_id[language], tweet['id']))
                    print("nuovo tweet "+state+language)
                else:
                    cur3.execute("SELECT country FROM tweets WHERE idtweet = %s", (str(tweet['id']),))
                    if cur3.fetchone()[0] == states_db_id[state]:
                        #Controllo che quel dato tweet e quel dato linguaggio non siano gia presenti nel DB
                        cur4.execute("SELECT count(*) FROM language_tweet WHERE tweet_fk = %s AND language_fk = %s", 
                            (str(tweet['id']), languages_db_id[language]))
                        if cur4.fetchone()[0] == 0:
                            cur.execute("INSERT INTO language_tweet(language_fk, tweet_fk) VALUES (%s, %s)", 
                                (languages_db_id[language], tweet['id']))
                            print("nuovo linguaggio"+state+language)
                    print("tweet gia presente")

            '''with open('./File/python_tweets_Italy_'+language+'.txt', 'w') as f:
                if len(data['statuses']) > 0:
                    for tweet in data['statuses']:
                        json.dump(tweet['text']+" | "+states_db_id["Italy"]+" | "+tweet['created_at']+" ///// ", f)'''
    conn.commit()
    cur.close()
    cur2.close()
    cur3.close()
    cur4.close()
    conn.close()
