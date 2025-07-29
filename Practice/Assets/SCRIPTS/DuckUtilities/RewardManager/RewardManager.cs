using DuckGame.Ultilities;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum REWARDTYPE { Money, Vehicle, Decal, Token, MoneyAndToken, Diamond, MoneyAndDiamond, DiamondAndToken, Pack}

public enum CardType { VehicleFull, VehicleFragment, Item }


[System.Serializable]
public struct RewardPack
{
    public string giftName;
    public CardType cardType;
}

[System.Serializable]
public class Reward
{
    public REWARDTYPE rewardType;
    public int rewardValue;
    public RewardPack rewardPack;

    public Reward(REWARDTYPE rewardType, int rewardValue, RewardPack rewardPack = new RewardPack())
    {
        this.rewardType = rewardType;
        this.rewardValue = rewardValue;
        this.rewardPack = rewardPack;
    }

    public Reward(REWARDTYPE rewardType, RewardPack rewardPack)
    {
        this.rewardType = rewardType;
        this.rewardPack = rewardPack;
    }
}
public class RewardManager : Singleton<RewardManager>
{
    Queue<Reward> allReward = new Queue<Reward>();

    public Reward currentRewad;

    public void AddReward(Reward reward)
    {
        allReward.Enqueue(reward);
    }

    public void AddReward(Reward[] reward)
    {
        foreach(Reward rw in reward)
        {
            allReward.Enqueue(rw);
        }
    }

    public Reward GetReward()
    {
        if (allReward.Count == 0) return null;
        currentRewad = allReward.Dequeue();
        return currentRewad;
    }

    public void ShowReward()
    {
        if (allReward.Count == 0)
        {
            //if (GameDatas.FirstTimeReward == "true")
            //{
            //GameDatas.FirstTimeReward = "false";
            if (GUIManager.Instance.currentGUI == GUITEMPLATE.Game)
            {
               
            }
            else
            {
                    //GameManager.Instance.UnloadVehicleUnpack();
            }
            //}
            //else
            //{
            //    GameManager.Instance.ToLuckySpin();               
            //}
            return;
        }
        GetReward();

        switch(currentRewad.rewardType)
        {
            case REWARDTYPE.Money:
                //GameManager.Instance.LoadMoneyReward();
                break;

            case REWARDTYPE.Pack:
                //GameManager.Instance.LoadOpeningPack();
                break;
        }

    }
}
