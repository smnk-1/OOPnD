from GeometryShapes import GeometryShape
from CollisionAlgorithm import CollisionAlgorithm

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

def main():
    moving_ship = SmallStarship(center=(0, 0))
    stationary_ship = BigStarship(center=(0, 0))

    collision = CollisionAlgorithm()
    collision.Execute(
        stationary_shape=stationary_ship,
        moving_shape=moving_ship,
        stationary_name='BigShip',
        moving_name='SmallShip',
        max_vel=10
    )


if __name__ == "__main__":
    main()
