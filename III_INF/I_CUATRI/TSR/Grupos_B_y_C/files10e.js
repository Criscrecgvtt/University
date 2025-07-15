const fs = require('fs')
const files = process.argv.slice(2)

let count = files.length

function sort_lines(mystring) { 
    return mystring.toString().split("\n").sort().join("\n")
}

files.forEach( file=>{
    fs.readFile(file, 'utf-8', (err,data)=>{
        fs.writeFile(file+"2", sort_lines(data), 'utf-8', (err)=>{
            console.log('fin '+file)
            if (--count==0) console.log('fin de todos')
        })
    })
})
