def segment_line_intersection(P0, v, A, B, eps=1e-10):
    """
    Проверяет пересечение отрезка P0 -> (P0 + v) с отрезком A->B.
    Если пересечение найдено, возвращает (t, u),
    где t – параметр движения вдоль вектора v (для отрезка движения),
    а u – параметр вдоль ребра A->B.
    Если пересечения нет или линии параллельны, возвращает None.
    """

    s = (B[0] - A[0], B[1] - A[1])
    r = v

    r_cross_s = r[0] * s[1] - r[1] * s[0]
    if abs(r_cross_s) < eps:
        return None

    A_P0 = (A[0] - P0[0], A[1] - P0[1])

    t = (A_P0[0] * s[1] - A_P0[1] * s[0]) / r_cross_s
    u = (A_P0[0] * r[1] - A_P0[1] * r[0]) / r_cross_s

    if 0 <= t <= 1 and 0 <= u <= 1:
        return t, u
    return None


def detect_collision(stationary_poly, moving_poly, v, fast = False):
    """
    Для каждой вершины подвижной фигуры (moving_poly)
    проверяет, пересекается ли её траектория (сдвиг на вектор v)
    с любым ребром неподвижной фигуры (stationary_poly).

    Возвращает список точек столкновения.
    """
    collisions = []
    n_stationary = len(stationary_poly)
    for P in moving_poly:
        for i in range(n_stationary):
            A = stationary_poly[i]
            B = stationary_poly[(i + 1) % n_stationary]
            res = segment_line_intersection(P, v, A, B)
            if res is not None:
                t, u = res
                collision_point = (P[0] + t * v[0], P[1] + t * v[1])
                collisions.append(collision_point)
                if fast:
                    return collisions
    return collisions
