import requests
edit_member = {'Name':'FoodPlaces', 'ContactFirstName':'John','ContactLastName':'Pearson' 'Email':'forgot@oo.ps', 'Address':'4321 ABC Street', 'City':'Nowhere', 'Zip':'98101', 'Phone':'4253602061', 'RangeInMeters':16}
put_edit_member = requests.put(url='../api/members', data=edit_member)
print(put_edit_member.status_code, put_edit_member.text)
