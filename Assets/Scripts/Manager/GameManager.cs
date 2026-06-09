using System;
using Input;
using Save;
using UnityEngine;

namespace Manager
{
    public class GameManager : PersistentSingleton<GameManager>
    {
        [SerializeField] private float _dayDurationInSeconds;
        public float TimeOfDay;
        public float DayNumber {get ; private set;}

        public event Action OnNewDay;

        private void Update()
        {
            TimeOfDay += (24f / _dayDurationInSeconds) * Time.deltaTime;
            TimeOfDay %= 24;
            
        }

        public void Sleep()
        {
            TimeOfDay = 6;
            DayNumber += 1;
            OnNewDay?.Invoke();
            
            SaveManager.Instance.SaveGame();
        }

        private void Start()
        {
            SaveManager.Instance.LoadGame();
        }
    }
}