from GeometryShapes import GeometryShape, plot_starships
from GeometryCollisions import detect_collision

class SmallStarship(GeometryShape):
    default_shape = [
        (0, 2),
        (1, 0),
        (1, -1),
        (-1, -1),
        (-1, 0)
    ]

class BigStarship(GeometryShape):
  default_shape = [
      (1, -2),
      (2, -1),
      (1, 3),
      (-1, 3),
      (-2, -1),
      (-1, -2)
  ]

s1 = SmallStarship(center=(0,0))
vector = (-6, -5)
s2 = BigStarship(center=(7,8))
s2.rotate(vector)

collisions = detect_collision(
    stationary_poly=s1.get_points(),
    moving_poly=s2.get_points(),
    v=vector)

if collisions:
    print("Обнаружено столкновение (точки пересечения):")
    for pt in collisions:
        print(pt)
else:
    print("Столкновений не обнаружено.")

plot_starships(s1, s2, vector)
