import requests
donorId=1634
post_response = requests.get(url='../api/pastdonations/%s' % donorId)
print(post_response.status_code, post_response.text)
