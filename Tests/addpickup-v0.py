import requests
post_pickup = {'donorName':'DirtyDan', 'address':'1634 Junkyard Lane', 'city':'MochasareYummy', 'zip':'98101', 'items':'human livers', 'notes':'in my basement, door code is 666', '00:00-03:00'}
post_response = requests.post(url='/api/pickups', data=post_pickup)
