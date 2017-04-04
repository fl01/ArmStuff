import time
import datetime
import os.path
import json
from json import JSONEncoder
import requests
import signal
import sys
import RPi.GPIO as GPIO

ConfigFilePath = os.path.join(os.path.dirname(__file__), 'config.cfg')

class DefaultEncoder(JSONEncoder):
    def default(self, o):
        return o.__dict__    
		
class HttpClient:
  def Post(self, cfg, data):
	print (json.dumps(data))
	try:
		requests.post(cfg.Endpoint, json.dumps(data), headers = {'Content-type':'application/json', 'Code':cfg.Code})
	except:
		print ("Http Error")

class PirSensor:
  def IsEmpty(self, cfg):
	isEmpty = True
	while GPIO.input(cfg.PirPin):
		isEmpty = False
		time.sleep(0.5)

	return isEmpty;	

  def Setup(self, cfg):
	GPIO.setmode(GPIO.BCM)
	GPIO.setup(cfg.PirPin, GPIO.IN)
	
class Config:
  Endpoint = ""
  DelayBetweenChecks = 0
  LastUpdateTime = 0
  PirPin = 0
  SensorType = 0
  Code = ""
  DeviceId = ""
  
  def UpdateIfNeeded(self):
    if time.time() - self.LastUpdateTime > 60 and os.path.isfile(ConfigFilePath):
		fileLines = open(ConfigFilePath, "r").readlines()
		keyValues = dict(map(lambda line: tuple(line.split('##')[0].strip().split('=')), fileLines))
		self.Endpoint = keyValues['Endpoint']
		self.DelayBetweenChecks = int(keyValues['DelayBetweenChecks'])
		self.PirPin = int(keyValues['PirPin'])
		self.SensorType = int(keyValues['SensorType'])
		self.Code = keyValues['Code']
		self.DeviceId = keyValues['DeviceId']
		self.LastUpdateTime = time.time()

def signal_handler(signal, frame):	
	print('You pressed Ctrl+C!')
	GPIO.cleanup()
	sys.exit(0)

signal.signal(signal.SIGINT, signal_handler)

cfg = Config()
cfg.UpdateIfNeeded()
sensor = PirSensor()
sensor.Setup(cfg)

print ('---Settings--')
print (json.dumps(DefaultEncoder().encode(cfg)))
print ('-------------')

while True:
	if not sensor.IsEmpty(cfg):
		movementData = {}
		movementData['DeviceId'] = cfg.DeviceId
		movementData['Value'] = 1
		movementData['Sensor'] = cfg.SensorType
		movementData['EntryDate'] = datetime.datetime.fromtimestamp(time.time()).strftime('%Y-%m-%d %H:%M:%S')
		HttpClient().Post(cfg, movementData)
		
	cfg.UpdateIfNeeded()		
	time.sleep(cfg.DelayBetweenChecks)
    
