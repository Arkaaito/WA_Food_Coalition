import requests
pickupid=123456
put_cancel_pickup = requests.put(url='/api/pickups/%s' % pickupid, data='status':'canceled')
