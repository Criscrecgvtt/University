const net = require('net')
let server = net.createServer(onClientConnected) // Create Server instance 
server.listen(8080, 'localhost', () => {console.log(`server listening on ${server.address()}`)})

function onClientConnected(client) {  
	let remoteAddress = `${client.remoteAddress}:${client.remotePort}`
	console.log(`new client connected: ${remoteAddress}`)
 
	client.on('data', (data) => {
		console.log(`${remoteAddress} Recv: ${data}`)
		client.write(`Echo: ${data}`)
	})
	client.on('close', ()    => {console.log(`connection from ${remoteAddress} closed`)})
	client.on('error', (err) => {console.log(`Connection ${remoteAddress} error: ${err.message}`)})
}