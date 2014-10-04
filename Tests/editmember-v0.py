import requests
edit_member = {'memberName':'FoodPlaces', 'contactName':'John Pearson', 'email':'forgot@oo.ps', 'address':'4321 ABC Street', 'city':'Nowhere', 'zip':'98101', 'phone':'4253602061'}
put_edit_member = requests.put(url='/api/members', data=edit_member)
