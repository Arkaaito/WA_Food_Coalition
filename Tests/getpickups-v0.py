import requests
lat=47.597685
longi=-122.327957
distance=32186.9
requests.get(url='http://70.42.168.133/api/pickups/range/%s/%s/%s' % lat,longi,distance)
