import requests
post_add_member = {'Name':'FoodPlace', 'ContactFirstName':'Faux', 'ContactLastName':'Pearson' 'Email':'oonceoonce@wu.bs', 'Address':'1234 ABC Street', 'City':'Nowhere', 'Zip':'98052', 'Phone':'3602061234'}
post_response = requests.post(url='../api/members', data=post_add_member)
