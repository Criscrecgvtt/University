const fs = require('fs')
const [f1,f2] = process.argv.slice(2)

let count=2
function sort_lines(mystring) { 
    return mystring.toString().split("\n").sort().join("\n")
}

fs.readFile(f1, 'utf-8', (err,data)=>{
    fs.writeFile(f1+"2", sort_lines(data), 'utf-8', (err)=>{
        console.log('fin '+f1)
        if (--count==0) console.log('fin de todos')
    })
})
fs.readFile(f2, 'utf-8', (err,data)=>{
    fs.writeFile(f2+"2", sort_lines(data), 'utf-8', (err)=>{
        console.log('fin '+f2)
        if (--count==0) console.log('fin de todos')
    })
})
