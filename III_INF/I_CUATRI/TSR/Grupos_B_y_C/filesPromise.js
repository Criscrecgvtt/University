const fs = require('fs').promises
let ficheros = process.argv.slice(2) // pasamos lista de ficheros como argumento en linea comandos

async function tallaFichero(f) {return fs.readFile(f).then(data => data.length)}

function resumen(talla) {
	console.log(talla)
	let total = 0
	for (i in ficheros) {
		console.log(`Fichero: ${ficheros[i]} talla: ${talla[i]}`)
		total += talla[i]
	}
	console.log(`Talla total: ${total}`)
}

async function leeTodos(ficheros) {
	return Promise.all(ficheros.map(tallaFichero))
}


async function main() {
	resumen(await leeTodos(ficheros))
}
main()


//resumen(await leeTodos(ficheros))
