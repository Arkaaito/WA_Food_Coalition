import requests
import json
#post_pickup = {'Notes':'in my basement, door code is 666','Items':'human livers','Donor':{'OptIn':True,'FirstName':'Dirty','DeviceId':'B06D4C35-3E2A-4CAE-A393-D2E7C713B51B','Phone':'4256218392','Email':'clevar@bored.com','LastName':'Dan'},'Zip':'98101','City':'MochasareYummy','Address':'1634 Junkyard Lane', 'Latitude':1343242353542, 'Longitude':579832470987}
post_pickup = {"Notes":"Notes","Items":"Items","Donor":{"OptIn":1,"FirstName":"FirstName","DeviceId":"B06D4C35-3E2A-4CAE-A393-D2E7C713B51B","Phone":"4256218392","Email":"email@email.com","LastName":"LastName"},"Zip":"98101","City":"City","Address":"Address", "Latitude":47, "Longitude":-121}
post_response = requests.post(url='http://70.42.168.133/api/pickups', data=json.dumps(post_pickup), headers={"Content-Type":"application/json"})
print(post_response.status_code, post_response.text)
