import RPi.GPIO as GPIO
from flask import Flask,render_template,request,redirect
from flask import *

GPIO.setmode(GPIO.BCM)
GPIO.setwarnings(False)
#define sensors GPIOs
button = 20
senPIR = 16
#define actuators GPIOs

ledYlw = 20
ledBlu = 26
#initialize GPIO status variables
buttonSts = 0
senPIRSts = 0

ledYlwSts = 0
ledBluSts = 0
# Define button and PIR sensor pins as an input
GPIO.setup(button, GPIO.IN)   
GPIO.setup(senPIR, GPIO.IN)
# Define led pins as output
  
GPIO.setup(ledYlw, GPIO.OUT) 
GPIO.setup(ledBlu, GPIO.OUT) 
# turn leds OFF 

GPIO.output(ledYlw, GPIO.LOW)
GPIO.output(ledBlu, GPIO.LOW)

app = Flask(__name__)
@app.route('/')
def index():
# Read GPIO Status
	buttonSts = GPIO.input(button)
	senPIRSts = GPIO.input(senPIR)
	ledYlwSts = GPIO.input(ledYlw)
	ledBluSts = GPIO.input(ledBlu)
	templateData = {'button'  : buttonSts,'senPIR'  : senPIRSts,'ledYlw'  : ledYlwSts,'ledBlu'  : ledBluSts,}
	return render_template('index.html', **templateData)
@app.route('/<deviceName>/<action>')
def action(deviceName, action):
	if deviceName == 'ledYlw':
		actuator = ledYlw
	if deviceName == 'ledBlu':
		actuator = ledBlu
	if action == "on":
		GPIO.output(actuator, GPIO.HIGH)
	if action == "off":
		GPIO.output(actuator, GPIO.LOW)
	
	ledYlwSts = GPIO.input(ledYlw)
	ledBluSts = GPIO.input(ledBlu)

	templateData = {'ledYlw' : ledYlwSts,'ledBlu': ledBluSts}
	return render_template('index.html', **templateData)
if __name__ == "__main__":
    app.run(host='192.168.1.4', debug=True, port=8000)
