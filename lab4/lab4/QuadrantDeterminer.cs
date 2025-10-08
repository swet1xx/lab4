namespace lab4
{
    public static class QuadrantDeterminer
    {
        public static int GetQuadrant(Point point)
        {
            if (point.X > 0 && point.Y > 0) return 1;
            if (point.X < 0 && point.Y > 0) return 2;
            if (point.X < 0 && point.Y < 0) return 3;
            if (point.X > 0 && point.Y < 0) return 4;
            return 0; // Точка на осях
        }

        public static string GetQuadrantDescription(int quadrant)
        {
            return quadrant switch
            {
                1 => "Перша чверть",
                2 => "Друга чверть",
                3 => "Третя чверть",
                4 => "Четверта чверть",
                0 => "Точка знаходиться на осях координат",
                _ => "Невідома чверть"
            };
        }
    }
}