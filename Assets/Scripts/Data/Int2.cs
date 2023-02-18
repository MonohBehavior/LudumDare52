public struct Int2
{
    public int x;
    public int y;

    public Int2(int _x, int _y)
    {
        x = _x;
        y = _y;
    }

    public Int2 Add(Int2 first, Int2 second)
    {
        return new Int2(first.x + second.x, first.y + second.y);
    }

    public Int2 Add(Int2 addingInt2)
    {
        return new Int2(x + addingInt2.x, y + addingInt2.y);
    }

    public bool Equals(Int2 compareInt)
    {
        return x == compareInt.x && y == compareInt.y;
    }
}