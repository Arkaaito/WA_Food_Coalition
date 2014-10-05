import requests
donorId=1634
requests.get(url='../api/pastdonations/%s' % donorId)
