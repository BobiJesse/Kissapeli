using Unity.Mathematics;

[System.Serializable]
public struct DropTiles
{
    public int2 coordinates;

    public int fromY;

    public DropTiles(int x, int y, int distance)
    {
        coordinates.x = x;
        coordinates.y = y;
        fromY = y + distance;
        
    }
}