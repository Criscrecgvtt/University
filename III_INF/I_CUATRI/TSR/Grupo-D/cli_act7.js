let req = require('zeromq').socket ('req');

let fnArg = parseInt (process.argv[4]) || 0  
let fn = process.argv[3] || 'undef'   
let port = parseInt (process.argv[2]) || 8888

let url = "tcp://127.0.0.1:" + port;

console.log ("connecting to " + url + " fn= " + fn + ", arg= " + fnArg)
req.connect (url);

let msg = JSON.stringify ( {fn: fn, arg: fnArg} );
console.log (msg);
req.send (msg);

req.on ("message", data =>{
    console.log (data.toString())
} )
