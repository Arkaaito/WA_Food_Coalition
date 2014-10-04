import requests
post_pickup = {'FirstName':'Dirty', 'LastName':'Dan', 'Address':'1634 Junkyard Lane', 'City':'MochasareYummy', 'Zip':'98101', 'Phone':'4256218392', 'Email':'clevar@bored.com', 'Items':'human livers', 'Notes':'in my basement, door code is 666', 'Schedule':'00:00-03:00'}
post_response = requests.post(url='../api/pickups', data=post_pickup)
