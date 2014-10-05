import requests
pickupid=1634
put_cancel_pickup = requests.put(url='../api/pickups/%s' % pickupid, data='status':'canceled')
print(put_cancel_pickup.status_code, put_cancel_pickup.text)
