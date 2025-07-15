import main

# Estado de ejemplo
estado = '123804765'
#estado = '023845716'
#estado = '781406235'

#estado = '283164705'

# Invocar el método y mostrar el resultado

result = main.descolocadas(estado)
print(f"El resultado de descolocadas es: {result}")

result = main.secuencias(estado)
print(f"El resultado de secuencias es: {result}")

result = main.filcol(estado)
print(f"El resultado de filas_columnas es: {result}")

