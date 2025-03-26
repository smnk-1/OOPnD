import math
import matplotlib.pyplot as plt

class GeometryShape:
    default_shape = None

    def __init__(self, center=(0, 0)):
        if self.default_shape is None or len(self.default_shape) == 0:
            raise ValueError("В подклассе должен быть определён непустой default_shape")
        self.center = center
        self.shape = self._translate_shape(self.default_shape, self.center)

    def _translate_shape(self, shape, center):
        """
        Смещает точки фигуры так, чтобы они располагались относительно центра.
        """
        cx, cy = center
        return [(x + cx, y + cy) for x, y in shape]

    def set_center(self, new_center):
        """
        Обновляет центр фигуры и смещает текущие точки.
        """
        dx = new_center[0] - self.center[0]
        dy = new_center[1] - self.center[1]
        self.center = new_center
        self.shape = [(x + dx, y + dy) for x, y in self.shape]

    def get_points(self):
        """
        Возвращает текущие координаты точек фигуры.
        """
        return self.shape

    def rotate(self, vector):
        """
        Поворачивает фигуру вокруг её центра.
        Вычисляет угол поворота theta относительно базового вектора (0, 1)
        по нормализованному входному вектору.
        Для каждой точки (x, y) выполняется:
            x' = cx + (x - cx) * cos(theta) - (y - cy) * sin(theta)
            y' = cy + (x - cx) * sin(theta) + (y - cy) * cos(theta)
        где (cx, cy) — координаты центра фигуры.
        """
        vx, vy = vector
        norm = math.sqrt(vx**2 + vy**2)
        if norm == 0:
            raise ValueError("Вектор для поворота не должен быть нулевым")

        ux, uy = vx / norm, vy / norm
        theta = math.atan2(vy, vx) - math.pi/2
        cx, cy = self.center

        rotated_points = []
        for x, y in self.shape:
            x_rel = x - cx
            y_rel = y - cy
            x_rot = x_rel * math.cos(theta) - y_rel * math.sin(theta)
            y_rot = x_rel * math.sin(theta) + y_rel * math.cos(theta)
            rotated_points.append((x_rot + cx, y_rot + cy))
        self.shape = rotated_points

def plot_starships(starship1, starship2, vector):
    """
    Отображает на графике два объекта типа GeometryShape и стрелку,
    исходящую из центра второго объекта.
    """
    def close_polygon(poly):
        return poly + [poly[0]]

    center2 = starship2.center
    poly1 = starship1.get_points()
    poly2 = starship2.get_points()
    p2cx, p2cy = starship2.center
    p2cx += vector[0]
    p2cy += vector[1]
    starship2.set_center((p2cx, p2cy))
    poly2_moved = starship2.get_points()

    poly1_closed = close_polygon(poly1)
    poly2_closed = close_polygon(poly2)
    poly2_m_closed = close_polygon(poly2_moved)

    x1, y1 = zip(*poly1_closed)
    x2, y2 = zip(*poly2_closed)
    x2_2, y2_2 = zip(*poly2_m_closed)

    fig, ax = plt.subplots(figsize=(8, 8))

    ax.plot(x1, y1, marker='o', linestyle='-', color='blue', label='Starship 1')
    ax.plot(x2, y2, marker='o', linestyle='-', color='red', label='Starship 2')
    ax.plot(x2_2, y2_2, marker='o', linestyle='--', color='grey', label='Starship 2 moved')

    ax.arrow(center2[0], center2[1], vector[0], vector[1],
             head_width=0.3, head_length=0.3, fc='green', ec='green', linewidth=2)


    ax.grid(True)
    ax.axis('equal')
    ax.set_xlabel("X")
    ax.set_ylabel("Y")
    ax.legend()
    plt.show()
