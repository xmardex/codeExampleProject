using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameManagment{
    public class GameManager : Singleton<GameManager>
    {
        [SerializeField]
        private GameSettingsSO gameSettingsSO;
        public GameSettingsSO GameSettings => gameSettingsSO;

        [Header("Set here default levelSO")]
        [SerializeField]
        private LevelSO selectedLevel;
        public LevelSO SelectedLevel{get => selectedLevel; set => selectedLevel = value;}
        private GameMode mode;
        public GameMode GameMode => mode;
        public Action<GameMode> onGameModeChange;
        public static bool isGamePause = false;
        private void Awake() 
        {
            base.SetAsCrossScene();
            Preloader.Instance.onLevelLoadStart += DeinitPrevLevel;
            Preloader.Instance.onLevelLoadFinish += InitLevel;
        }
        public void ChangeGameMode(GameMode mode)
        {
            this.mode = mode;
            onGameModeChange?.Invoke(mode);
        }
        public void StartGameLevel()
        {
            PauseGame(false);
            
            Preloader.Instance.LoadLevel(selectedLevel.SceneName);
            //subscribe before load new level
            
        }
        void DeinitPrevLevel()
        {
            // Before load next level;
            
            HistoryManager.Instance.ClearActionsList();

            //Maybe destroy garbage on level - remove all unnecessary references or something else here.
        }
        void InitLevel(string levelName)
        {
            //Some different specials for level - SFX, VFX etc.
            if(levelName != "MainMenu")
                LevelManager.Instance?.LevelInitialize();
        }
        public void PauseGame(bool isPause)
        {
            Time.timeScale = isPause == true ? 0 : 1;
            isGamePause = isPause;
        }
    }
}