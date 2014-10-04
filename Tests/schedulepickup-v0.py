import requests
pickupid=123456
put_schedule_pickup = requests.put(url='/api/pickups/%s' % pickupid, data='schedule':'2014-10-03T19:30:02+00:00')
