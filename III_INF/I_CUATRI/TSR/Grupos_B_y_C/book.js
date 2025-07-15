// importar biblioteca
const ev = require('events')

// emisor
const emitter = new ev.EventEmitter()

// constants per als esdeveniments
const e1 = "print", e2 = "read"

const books = [ "Walk Me Home", "When I Found You", "Jane's Melody", "Pulse" ]

// asociar listeners (0 o mes)
emitter.on(e1, createListener(e1))
emitter.on(e1, () => console.log("Something has been printed!!"))
emitter.on(e2, createListener(e2))

// emetre esdeveniments.   emisor.emit(esdeveniment,args)
setInterval(()=>emitter.emit(e1), 2000)
setInterval(emitE2(), 3000)

// definicions auxiliars
function createListener(eventName) {
    let num = 0
    return (arg) => {
        let book = arg? `, now with title "${arg}"`: ""
        console.log(`Event ${eventName} ${book} has happened ${++num} times.`)
    }
}
function emitE2() {
    let counter = 0
    return () => emitter.emit(e2, books[counter++ % books.length])
}
