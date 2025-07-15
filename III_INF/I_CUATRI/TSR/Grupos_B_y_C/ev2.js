const ev = require("events")
const t0=Date.now()
const emitter = new ev.EventEmitter()
const e1 = 'uno', e2 = 'dos', e3 = 'tres'

const [a1,a2] = (() => {
	let n1=0, n2=0
	return [() => console.log(`${Date.now()-t0}) Evento e1:${++n1}`),
	        () => console.log(`${Date.now()-t0})`, ++n2>=n1? `Evento e2:${n2}`: "hay mas eventos de tipo uno")]
})()

emitter.on(e1, a1)
emitter.on(e2, a2)
emitter.on(e3, () => console.log(`${Date.now()-t0}) Evento 3`))
emitter.on(e3, sep(2000, ()=> emitter.emit(e2)))

setInterval( ()=> emitter.emit(e1),  3000)
setInterval( ()=> emitter.emit(e3), 10000)

function sep(ms,f) {
	let t = setInterval(f, ms)
	return () => {if (ms<18000) {clearInterval(t); t = setInterval(f, ms*=3)}}
}
