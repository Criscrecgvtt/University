const fsp = require('fs').promises

function directorio() {
    return fsp.readdir('.').then(x=>x.filter(file=>file.slice(-3)=='.js'))
}
function lee(f) {
    return fsp.readFile(f).then(x=>{console.log(f+': '+x.length); return x.length})
}

directorio()
    .then(x=>Promise.all(x.map(lee)))
    .then(s=>console.log('TOTAL: '+s.reduce((a,b)=>a+b)))

