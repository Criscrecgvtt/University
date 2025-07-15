var net = require('net')
var nick = process.argv[2] || 'client'
var client = net.connect({port: 8080}, function() {console.log(nick+' connected to server!')})

process.stdin.resume()
process.stdin.setEncoding('utf8')

process.stdin.on('data' ,(str)=> {client.write(nick+': '+str)})
process.stdin.on('end',()=> {client.write('connection closed')})

client.on('data', function(str) {console.log(str.toString())})
client.on('end', function() {console.log('disconnected from server')})