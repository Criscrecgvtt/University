end_state ="123804765"

def getNSwap(state):
    tot = 0
    lstate = list(state)
    for i in range(0,9):
        if end_state[i] != lstate[i]:
            pos = lstate.index(end_state[i])
            lstate[i], lstate[pos] = lstate[pos], lstate[i]
            tot += 1
    return tot

def getNMaxSwap(state):
    tot = 0
    lstate = list(state)
    lend = list(end_state)

    while lstate != lend:
        bpos = lstate.index('0')
        if bpos == 4:
            i = 0
            while lstate[i] == lend[i]: i += 1
            lstate[bpos], lstate[i] = lstate[i], lstate[bpos]
            bpos = i
            tot += 1
        while bpos != 4:
            tpos = lstate.index(lend[bpos])
            lstate[bpos], lstate[tpos] = lstate[tpos], lstate[bpos]
            bpos = tpos
            tot += 1
    return tot
