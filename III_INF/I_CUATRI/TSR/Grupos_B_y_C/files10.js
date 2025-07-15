const fs = require('fs')
const fich = process.argv[2]

function sort_lines(mystring) { 
    return mystring.toString().split("\n").sort().join("\n")
}

const r=fs.readFileSync(fich, 'utf-8')
fs.writeFileSync(fich+"2", sort_lines(r), 'utf-8')
