import requests
pickupid=1634
put_complete_pickup = requests.put(url='../api/pickups/%s' % pickupid, data='status':'completed')
