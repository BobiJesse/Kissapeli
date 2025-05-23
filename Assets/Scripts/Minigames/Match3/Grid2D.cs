using Unity.Mathematics;
using UnityEngine;

public struct Grid2D<T>
{
    public T[] grid;
    private int2 size;

    public int2 Size => size;
    public int Width => size.x;
    public int Height => size.y;
    public bool IsInvalid => grid == null || grid.Length == 0;

    public Grid2D(int2 size)
    {
        this.size = size;
        grid = new T[size.x * size.y];
    }

    public T this[int2 index]
    {
        get => grid[index.x + index.y * size.x];
        set => grid[index.x + index.y * size.x] = value;
    }

    public T this[int x, int y]
    {
        get => grid[x + y * size.x];
        set => grid[x + y * size.x] = value;
    }
    public bool AreValidCoordinates(int2 c) =>
        0 <= c.x && c.x < size.x && 0 <= c.y && c.y < size.y;

    public void Swap(int2 a, int2 b) => (this[a], this[b]) = (this[b], this[a]);
}
