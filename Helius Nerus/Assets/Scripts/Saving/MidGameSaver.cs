using System.Collections.Generic;
using System.Text;
using UnityEngine;

public class MidGameSaver
{
    public static MidGameSaver Instance
    {
        get;
        private set;
    } = null;

    public static bool MidGameSaveExists
    {
        get => PlayerPrefs.HasKey("MidGameSave");
    }

    private class SavedData
    {
        // Common
        public int _levelNumber;
        public int _currentScore;
        // Ship
        public int _shipNumber;
        public string _playerParametrs;
        public string _playerArtifacts;

        // Weapons
        public List<string> _weaponParams = new List<string>();
        public List<string> _weaponMods = new List<string>();

        public SavedData()
        {
        }

        public SavedData(int a)
        {
            // common
            _levelNumber = LevelsChanger.CurrentLevel;
            _currentScore = ScoreCounter.Instance.Score;

            // player
            _shipNumber = Player.ShipNumber;
            _playerParametrs = Player.PlayerParameters.SerializeToString();

            StringBuilder sb = new StringBuilder();
            foreach (PlayerArtifact artifact in Player.PlayerArtifacts)
            {
                sb.Append(artifact.MyEnumName);
                sb.Append(',');
            }
            sb.Remove(sb.Length - 1, 1);
            _playerArtifacts = sb.ToString();
            sb.Clear();

            //weapons
            for (int i = 0; i < Player.PlayerWeapons.Length; ++i)
            {
                // params
                _weaponParams.Add(Player.PlayerWeapons[i].WeaponParameters.SerializeToString());
                // modifiers
                foreach (PlayerWeaponModifier modifier in Player.PlayerWeapons[i].WeaponModifiers)
                {
                    sb.Append(modifier.MyEnumName);
                    sb.Append(',');
                }
                sb.Remove(sb.Length - 1, 1);
                _weaponMods.Add(sb.ToString());
                sb.Clear();
            }
        }
    }

    public MidGameSaver()
    {
        Player.PlayerDie += Player_PlayerDie;
        LevelsChanger.LevelSpawn += LevelsChanger_LevelSpawn;
        LevelBoss.FinalBossDied += LevelBoss_FinalBossDied;
    }

    private void Unsubscribe()
    {
        Player.PlayerDie -= Player_PlayerDie;
        LevelsChanger.LevelSpawn -= LevelsChanger_LevelSpawn;
        LevelBoss.FinalBossDied -= LevelBoss_FinalBossDied;
    }

    public static void Initialize()
    {
        if (Instance == null)
            Instance = new MidGameSaver();
    }

    public static void Cleanup()
    {
        if (Instance != null)
            Instance.Unsubscribe();
    }

    private void LevelBoss_FinalBossDied(int a)
    {
        // We ended the game, so we erase the save
        DeleteSave();
#if UNITY_EDITOR
        Debug.Log("Save was erased - end of game");
#endif
    }

    private void LevelsChanger_LevelSpawn()
    {
        // New level started, we save the game
        SaveGame();
#if UNITY_EDITOR
        Debug.Log("Game was saved!");
#endif
    }

    private void Player_PlayerDie()
    {
        //Player died, so we erase the save
        DeleteSave();
#if UNITY_EDITOR
        Debug.Log("Save was erased - player died");
#endif
    }

    public void SaveGame()
    {
        SavedData data = new SavedData(1);
        // ДЖсон ютилити туда сюда плейерпрефс блаблабла
        string dataAsJson = JsonUtility.ToJson(data);

        PlayerPrefs.SetString("MidGameSave", dataAsJson);

        PlayerPrefs.Save();
    }

    public void LoadGame()
    {
        if (PlayerPrefs.HasKey("MidGameSave") == false)
        {
#if UNITY_EDITOR
            Debug.Log("MidGameSave playerprefs not exist");
#endif
            return;
        }
        SavedData data = JsonUtility.FromJson<SavedData>(PlayerPrefs.GetString("MidGameSave"));

        // score
        ScoreCounter.Instance.Score = data._currentScore;

        PlayersCreator.Instance.LoadPlayerFromSave(data._shipNumber);

        // ship and weapons
        Player.Instance.LoadFromSavedData(PlayerParameters.DeserializeFromString(data._playerParametrs), data._playerArtifacts.Split(','), data._weaponParams, data._weaponMods);

        // level
        LevelsChanger.CurrentLevel = data._levelNumber - 1;
    }

    public void DeleteSave()
    {
        PlayerPrefs.DeleteKey("MidGameSave");
    }
}