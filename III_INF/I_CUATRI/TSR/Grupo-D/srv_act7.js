let myMath = require ('./myMath')
let rep = require('zeromq').socket ('rep');

let port = parseInt ( process.argv[2])|| 8888 
let url = "tcp://127.0.0.1:" + port; 

rep.bind ( url, err =>{
    if (err) throw err;
    console.log ("listening to " + url)
} )

rep.on ("message", data =>{
    let msgStr = data.toString();
    let msg = JSON.parse (msgStr);

    console.log ("Received: " + msgStr);
    
    let result = myMath.calculate (msg.fn, msg.arg);
    console.log ("Received: " + msgStr + ", result= " + result);

    rep.send ( JSON.stringify ( { res: result } ))

} )