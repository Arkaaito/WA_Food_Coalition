import requests
post_pickup = {'Notes':'in my basement, door code is 666','Items':'human livers','Donor':{'OptIn':True,'FirstName':'Dirty','DeviceId':'666','Phone':'4256218392','Email':'clevar@bored.com','LastName':'Dan'},'Zip':'98101','City':'MochasareYummy','Address':'1634 Junkyard Lane'}
post_response = requests.post(url='../api/donors', data=post_pickup)
