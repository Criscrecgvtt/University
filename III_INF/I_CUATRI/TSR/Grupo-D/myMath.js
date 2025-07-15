function fibo(n) {
    return (n < 2) ? 1 : fibo(n - 2) + fibo(n - 1)
}

function fact(n) {
    return (n < 2) ? 1 : n * fact(n - 1)
}

function prim(n) {
    for (var i = 2; i <= Math.sqrt(n); i++)
        if (n % i === 0) return false
    return true
}

function calculate(f, n) {
    var res = 0
    switch (f) {
        case 'fibo': res = fibo(n); break
        case 'fact': res = fact(n); break
        case 'prim': res = prim(n); break
        case 'sen': res = Math.sin(n); break
        case 'cos': res = Math.cos(n); break
        case 'tan': res = Math.tan(n); break
        case 'log': res = Math.log(n); break
        case 'exp': res = Math.exp(n); break
        case 'raiz': res = Math.sqrt(n); break
        default: res = NaN
    }
    return res
}

ids = ['fibo', 'fact', 'prim', 'sen', 'cos', 'tan',
    'log', 'exp', 'raiz', 'strange'];

exports.calculate = calculate
exports.funcs = ids
