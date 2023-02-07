public class Grid
{
    public int XPixels;
    public int YPixels;

    public readonly int PixelSize = 30;

    public Grid(int xPixels = 35, int yPixels = 14)
    {
        XPixels = xPixels;
        YPixels = yPixels;
    }

    public bool CheckOutsideOfGrid(Int2 currentPosition)
    {
        if (currentPosition.x < 0 || currentPosition.x >= XPixels
            || currentPosition.y < 0 || currentPosition.y >= YPixels)
        {
            return true;
        }

        return false;
    }
}