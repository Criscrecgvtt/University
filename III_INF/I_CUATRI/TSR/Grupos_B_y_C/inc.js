// importar biblioteca events
const ev = require('events')

// declarar un (o mas) emisor eventos
const emitter = new ev.EventEmitter()

// declaramos eventos (opcional)
const e1="print", e2="read"

// constantes o variables adicionales
const books = ["Walk Me Home", "When I Found You", "Jane's Melody", "Pulse"]

// asociar oyentes (0 o mas acciones en respuesta a) cada evento
emitter.on(e1, createListener(e1))
emitter.on(e1, () => console.log("Something has been printed!!"))
emitter.on(e2, createListener(e2))

// generar eventos
setInterval(()=>emitter.emit(e1), 2000)
setInterval(emitE2(), 3000)

// --- funciones auxiliares
function createListener(eventName) {
    let num = 0
    return (arg) => {
        let book = arg? `, now with book title "${arg}"`: ""
        console.log(`Event ${eventName} ${book} has happened ${++num} times.`)
    }
}
function emitE2() {
    let counter = 0
    return () => emitter.emit(e2, books[counter++ % books.length])
}
