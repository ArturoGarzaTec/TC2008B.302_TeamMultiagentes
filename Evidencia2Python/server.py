from flask import Flask, request, jsonify
from intersection import *

# Get all the matrixes in a list
total = []
for row in all_streets.itertuples():
    total.append(row[1])

def positionsToJson(index):
    json_dict = []
    matrix = total[index]
    for p_col in range(len(matrix)):
        for p_row in range(len(matrix)):
            value = matrix[p_col][p_row]
            if value[0] != 0:
                pos = {
                    "type": int(value[0]),
                    "id": int(value[1]),
                    "x": float(p_col),
                    "y": 0.0,
                    "z": float(p_row)
                }
                json_dict.append(pos)
    return jsonify({'positions': json_dict})

index_count = 0

app = Flask("Test")

@app.route('/', methods=['GET'])
def agents_position():
    global index_count
    if index_count < len(total):
        positions = positionsToJson(index_count)
        index_count += 1
        return positions
    return "Finished Simulation"
    
@app.route('/init', methods=['GET'])
def agents_init():
    return jsonify({"num_agents": NUM_CARS, "w": 10, "h": 10})

if __name__=='__main__':
    app.run(host="localhost", port=8585, debug=True)