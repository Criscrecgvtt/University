import main

# Estat d'eixemple
#estat = '123804765'
#estat = '023845716'
#estat = '781406235'

estat = '283164705'

# Invocar heurística i mostrar el resultat

result = main.descolocadas(estat)
print(f"El resultat de descol.locades és: {result}")

result = main.secuencias(estat)
print(f"El resultat de seqüències és: {result}")

result = main.filcol(estat)
print(f"El resultat de files_col.lumnes és: {result}")

