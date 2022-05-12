using UnityEngine;
[System.Serializable]

public class SaveData
{
    // SAVE DATA
    GameObject Gave;

    public SaveData(PlayerController playerStats)
    {
        Gave.GetComponent<PlayerController>().currentKeys = playerStats.currentKeys;
        Gave.GetComponent<PlayerController>().GameManager.GaveLives = playerStats.GameManager.GaveLives;
    }
}
