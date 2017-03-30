import time
import datetime
import os.path
import json
from json import JSONEncoder
import requests

ConfigFilePath = os.path.join(os.path.dirname(__file__), 'config.cfg')

class DefaultEncoder(JSONEncoder):
    def default(self, o):
        return o.__dict__    
		
class HttpClient:
  def Post(self, cfg, data):
	print (json.dumps(data))
	requests.post(cfg.Endpoint, json.dumps(data), headers = {'Content-type':'application/json', 'Code':cfg.Code})

class RangeChecker:
  def IsEmpty(self, config):
	# TODO
	return False
	
class Config:
  Endpoint = ""
  SpareWcRange = 0
  DelayBetweenChecks = 0
  LastUpdateTime = 0
  MaxRangeDifference = 0
  TriggerPin = 0
  EchoPin = 0
  SensorType = 0
  Code = ""
  DeviceId = ""
  
  def UpdateIfNeeded(self):
    if time.time() - self.LastUpdateTime > 60 and os.path.isfile(ConfigFilePath):
		fileLines = open(ConfigFilePath, "r").readlines()
		keyValues = dict(map(lambda line: tuple(line.split('##')[0].strip().split('=')), fileLines))
		self.Endpoint = keyValues['Endpoint']
		self.SpareWcRange = int(keyValues['SpareWcRange'])
		self.DelayBetweenChecks = float(keyValues['DelayBetweenChecks'])
		self.MaxRangeDifference = int(keyValues['MaxRangeDifference'])
		self.DeviceId = keyValues['DeviceId']
		self.TriggerPin = int(keyValues['TriggerPin'])
		self.EchoPin = int(keyValues['EchoPin'])
		self.SensorType = int(keyValues['SensorType'])
		self.Code = keyValues['Code']
		self.LastUpdateTime = time.time()
		
sensor = RangeChecker()
cfg = Config()

cfg.UpdateIfNeeded()
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
    