using UnityEngine;

public class PlayerData
{
    public int Subscribers = 0;
    public int LastStage = 0;
    public bool[] StageUnlocked = new bool[5];
    public int NumberOfDeath = 0;

    public void GetASubscriber()
    {
        Subscribers++;
    }

    public void LoseASubscriber()
    {
        CheckEnoughSubscribers(1);
    }

    public void RandomSubsGainOrLose()
    {
        var random = Random.Range(0, 20);

        if (random < 1)
        {
            GetRandomAmountOfSubscribers();
        }
        else
        {
            LoseRandomAmountOfSubscribers();
        }
    }

    public void GetRandomAmountOfSubscribers()
    {
        var percentage = GetRandomSubscribersPercentage();

        Subscribers += (int)(Subscribers * percentage);
    }

    public void LoseRandomAmountOfSubscribers()
    {
        var percentage = GetRandomSubscribersPercentage();

        Subscribers -= (int)(Subscribers * percentage);
    }

    private float GetRandomSubscribersPercentage()
    {
        var random = Random.Range(0, 50);
        var percentage = 0.0f;

        if (random < 30)
        {
            percentage = 0.03f;
        }
        else if (random < 45)
        {
            percentage = 0.05f;
        }
        else if (random < 49)
        {
            percentage = 0.01f;
        }
        else
        {
            percentage = 0.1f;
        }

        return percentage;
    }

    public void CheckEnoughSubscribers(int lostNumbers)
    {
        Subscribers = Subscribers - lostNumbers > 0 ? Subscribers - lostNumbers : 1;
    }
}

public class Settings
{
    public int Volume;
}