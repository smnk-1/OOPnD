import csv
from GeometryCollisions import detect_collision

class CollisionAlgorithm:    
    def run_(self, stationary_shape, moving_shape):
        coordinates = [x for x in range(-2*self.max_vel, 2*self.max_vel + 1)]
        vectors = [x for x in range(-1* self.max_vel, self.max_vel + 1)]
        coordinates.remove(0)
        vectors.remove(0)
        count = 0
        collisions_data = []
        for dx in coordinates:
            for dy in coordinates:
                for dvx in vectors:
                    for dvy in vectors:
                        count += 1
                        moving_shape.set_center((dx, dy))
                        moving_shape.rotate((dvx, dvy))

                        collisions = detect_collision(
                            stationary_poly=stationary_shape.get_points(),
                            moving_poly=moving_shape.get_points(),
                            v=(dvx, dvy),
                            fast=True)
                        if collisions:
                            collisions_data.append([dx, dy, dvx, dvy])
        return collisions_data
    
    def Execute(self, stationary_shape, moving_shape, stationary_name, moving_name, max_vel):
        self.max_vel = max_vel
        data = self.run_(stationary_shape, moving_shape)
        with open(f'collision_data/collisions_stat_{stationary_name}_move_{moving_name}_maxvel_{max_vel}.csv', mode='w', newline='') as csv_file:
            writer = csv.writer(csv_file)
            for subarray in data:
                writer.writerow(subarray)
