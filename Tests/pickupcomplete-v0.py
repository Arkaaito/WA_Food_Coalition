import requests
pickupid=1634
put_complete_pickup = requests.put(url='../api/pickups/%s' % pickupid, data='status':'completed')
print(put_complete_pickup.status_code, put_complete_pickup.text)
