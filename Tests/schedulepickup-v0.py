import requests
pickupid=1634
put_schedule_pickup = requests.put(url='../api/pickups/%s' % pickupid, data={'schedule':'2014-10-03T19:30:02+00:00'})
print(put_schedule_pickup.status_code, put_schedule_pickup.text)
