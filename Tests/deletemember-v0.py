import requests
memberId=54321
member_delete = requests.delete(url='../api/members/%s' % memberId)
