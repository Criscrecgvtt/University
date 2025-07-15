const fs = require('fs')
const fich = process.argv[2]

function sort_lines(mystring) { 
    return mystring.toString().split("\n").sort().join("\n")
}

fs.readFile(fich, 'utf-8', (err,data)=>{
    fs.writeFileSync(fich+"2", sort_lines(data), 'utf-8')
})
