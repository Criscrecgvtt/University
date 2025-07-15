
end_state="123804765"

# heuristic function using manhattan distance

def getManhattanDistance(state):
    tot = 0
    for i in range(1,9):
        goal = end_state.index(str(i))
        goalX = int(goal/ 3)
        goalY = goal % 3
        idx = state.index(str(i))
        itemX = int(idx / 3)
        itemY = idx % 3
        tot += (abs(goalX - itemX) + abs(goalY - itemY))
    return tot

def main():
    state = input("Please provide state: ");
    print(f'Manhattan distance from {state} to {end_state} is: {getManhattanDistance(state)}')

if __name__ == '__main__': main()
