using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    [SerializeField] private List<Achievement> _achievements = new List<Achievement>();
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private AchievementPanel _achievementPanel;

    private void Start()
    {
        foreach(var achievement in _achievements)
        {
            achievement.Load();
            if (!achievement.Achieved)
            {
                achievement.OnAchieved += HandleAchive;
                _gameManager.OnScore += achievement.CheckAchievement;
            }
        }
    }

    [ContextMenu("Clear Trophies")]
    public void ClearTrophies()
    {
        foreach(var achievement in _achievements)
        {
            GameJolt.API.Trophies.Remove(achievement.GameJoltTrophyID);
        }
    }

    public void HandleAchive(Achievement achievement)
    {
        achievement.OnAchieved -= HandleAchive;
        _achievementPanel.UpdatePanel(achievement.Thumb, achievement.Msg);
        _achievementPanel.ShowAchievement();

        if (GameJolt.API.GameJoltAPI.Instance.HasSignedInUser)
            GameJolt.API.Trophies.Unlock(achievement.GameJoltTrophyID);
    }

}
