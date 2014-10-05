import requests
memberId=1634
member_delete = requests.delete(url='../api/members/%s' % memberId)
print(member_delete.status_code, member_delete.text)
