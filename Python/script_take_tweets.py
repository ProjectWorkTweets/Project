#libraries
import json

#external function
import function


states_id = {
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
	"United Kingdom" : "6416b8512febefc9",
}


tweets = function.connectingTwitter()
variabletest = ''
count = 0
for indicelist in tweets:
    tweet = indicelist
    count += 1
    for hashtag in tweet['entities']['hashtags']:
            variabletest += str(count) + hashtag['text'] + '  ' 

#save in a file

with open('python_tweets_json.txt', 'w') as f:
    json.dump(x, f)

with open('python_filter_tweets.txt', 'w') as f:
    json.dump(x[1], f)

with open('python_variabletest.txt', 'w') as f:
    json.dump(variabletest, f)