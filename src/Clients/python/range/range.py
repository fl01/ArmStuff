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
	requests.post(cfg.Endpoint, json.dumps(data), headers = {'Content-type':'application/json', 'Code':cfg.Code})

class RangeChecker:
  def IsEmpty(self, cfg):
	GPIO.output(cfg.TriggerPin, True)
	time.sleep(0.00001)
	GPIO.output(cfg.TriggerPin, False)

	while GPIO.input(cfg.EchoPin)==0:
		pulse_start = time.time()

	while GPIO.input(cfg.EchoPin)==1:
		pulse_end = time.time()

	pulse_duration = pulse_end - pulse_start
	print ('Pulse duration - ' + str(pulse_duration))
	distance = round(pulse_duration * 17150, 2)
	
	# some time range lags. most likely it is caused by chinese sensor, but who knows
	if distance > 1000:
		return False    

	print ('Distance - ' + str(distance))
	diff = abs(cfg.SpareWcRange - distance);
	print ('Difference cm - ' + str(diff))
	diffPercent = diff / cfg.SpareWcRange * 100;
	print ('Difference % - ' + str(diffPercent))
	isEmpty = diffPercent < cfg.MaxRangeDifference;
	print ('Is Empty = ' + str(isEmpty) + "Max Diff Percent - " + str(cfg.MaxRangeDifference))
	return isEmpty

  def Setup(self, cfg):
	GPIO.setmode(GPIO.BCM)
	GPIO.setup(cfg.TriggerPin, GPIO.OUT)
	GPIO.setup(cfg.EchoPin, GPIO.IN)
	GPIO.output(cfg.TriggerPin, False)
	time.sleep(2)
	
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

def signal_handler(signal, frame):	
	print('You pressed Ctrl+C!')
	GPIO.cleanup()
	sys.exit(0)

signal.signal(signal.SIGINT, signal_handler)

cfg = Config()
cfg.UpdateIfNeeded()
sensor = RangeChecker()
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
    
