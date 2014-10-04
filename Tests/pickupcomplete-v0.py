import requests
pickupid=123456
put_complete_pickup = requests.put(url='/api/pickups/%s' % pickupid, data='status':'completed')
