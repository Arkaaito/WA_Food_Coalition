import requests
memberId=1634
member_delete = requests.delete(url='../api/members/%s' % memberId)
