using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu]
public class Achievement : ScriptableObject
{
    public Sprite Thumb;
    public string Msg;
    public string PrefKey;
    public bool Achieved;
    public int MinScore;

    public int GameJoltTrophyID;

    public UnityAction<Achievement> OnAchieved;

    public void Load()
    {
        if (PlayerPrefs.HasKey(PrefKey))
        {
            if (PlayerPrefs.GetInt(PrefKey) == 1) Achieved = true;
            else Achieved = false;
        }
        else
        {
            PlayerPrefs.SetInt(PrefKey, 0);
            Achieved = false;
        }
    }

    public void CheckAchievement(int score)
    {
        if (score >= MinScore)
        {
            Achive();
        }
    }

    private void Achive()
    {
        Achieved = true;
        PlayerPrefs.SetInt(PrefKey, 1);
        OnAchieved?.Invoke(this);
    }
}
