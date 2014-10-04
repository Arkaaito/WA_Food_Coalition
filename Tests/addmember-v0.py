import requests
post_add_member = {'memberName':'FoodPlace', 'contactName':'Faux Pearson', 'email':'oonceoonce@wu.bs', 'address':'1234 ABC Street', 'city':'Nowhere', 'zip':'98052', 'phone':'3602061234'}
post_response = requests.post(url='/api/members', data=post_add_member)
