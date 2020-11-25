using System.Collections.Generic;
using UnityEngine;

namespace HNUI
{
    public class AchievementPopUpQueue : MonoBehaviour
    {
        [SerializeField] private AchievementPopUp _achievementPopUp = null;
        private Queue<Achievment> _achievementsQueue = new Queue<Achievment>();

        private void Awake()
        {
            Achievment.AchievementHappen += Achievment_AchievementHappen;
        }

        private void Achievment_AchievementHappen(Achievment obj)
        {
            AddAchievementToQueue(obj);
            ProcessAchievements();
        }

        private void AddAchievementToQueue(Achievment achievment)
        {
            _achievementsQueue.Enqueue(achievment);
        }

        private void ProcessAchievements()
        {
            if (_achievementsQueue.Count != 0 && _achievementPopUp.Finished == true)
            {
                _achievementPopUp.ShowAchievement(_achievementsQueue.Dequeue(), ProcessAchievements);
            }
        }
    }
}


