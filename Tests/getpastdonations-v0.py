import requests
donorId=12345
requests.get(url='/api/pastdonations/%s' % donorId)
