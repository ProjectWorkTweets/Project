# standard library
import base64

# 3rd party
import requests
import io, json


consumer_key = 'yAVb4Qk23P6xwjrk95lm0PA5g'
app_secret = 'dlmF4ROc9OB9wM7j8zbowGVYsoGUOKPwn1s7dBrzsFj6yA66wV'
credentials = '{}:{}'.format(consumer_key, app_secret)
credentials_enc = base64.b64encode(credentials.encode())
resp = requests.post('https://api.twitter.com/oauth2/token',
        headers={'Authorization': 'Basic {}'.format(credentials_enc.decode())},
        data={'grant_type': 'client_credentials'})
resp.raise_for_status()
resp_data = resp.json()

url = 'https://api.twitter.com/1.1/search/tweets.json'
resp = requests.get(
        url,
        params={'q': '#python', 'lang': 'it',},
        headers={'Authorization': 'Bearer {}'.format(resp_data['access_token'])})
resp.raise_for_status()
data = resp.json()

#save in a file

with open('python_tweets_json.txt', 'w') as f:
    json.dump(data['statuses'], f)

