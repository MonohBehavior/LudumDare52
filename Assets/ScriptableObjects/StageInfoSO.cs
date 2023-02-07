using UnityEngine;

[CreateAssetMenu(fileName = "StageInfo", menuName = "ScriptableObjects/StageInfoSO", order = 1)]
public class StageInfoSO : ScriptableObject
{
    public StageIndex StageIdx;
    public Int2 MapSize;
    public int SubscriberGoal;
    public float TimeLimit;
}

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

    public Int2 Add(Int2 addingInt)
    {
        return new Int2(x + addingInt.x, y + addingInt.y);
    }

    public bool Equals(Int2 compareInt)
    {
        return x == compareInt.x && y == compareInt.y;
    }
}

public enum StageIndex
{
    Stage001 = 0,
    Stage002 = 1,
    Stage003 = 2,
}