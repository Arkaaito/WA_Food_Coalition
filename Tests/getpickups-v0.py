import requests
lat=47.597685
long=-122.327957
distance=32186.9
requests.get(url='/api/pickups/range/%s/$s/%s' % lat,long,distance)