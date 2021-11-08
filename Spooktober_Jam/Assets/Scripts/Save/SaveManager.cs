using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Spooktober
{
    public class SaveManager : MonoBehaviour
    {
        [SerializeField] private string m_saveFileName;

        [SerializeField] private Texture2D[] m_entityImages;
        [SerializeField] private string[] m_entityImageFileNames;

        public void SaveGame()
        {
            var destination = Application.persistentDataPath + m_saveFileName;
            var file = File.Exists(destination) ? File.OpenWrite(destination) : File.Create(destination);

            var hasSeenMonsters = new []
            {
                GameManager.monster0seen,
                GameManager.monster1seen,
                GameManager.monster2seen,
                GameManager.monster2secondDialogueSeen
            };
            
            var saveData = new SaveData
            (
                1,
                GameManager.totalScore,
                GameManager.highScore,
                hasSeenMonsters,
                GameManager.lostToEntity
            );

            var binaryFormatter = new BinaryFormatter();
            binaryFormatter.Serialize(file, saveData);
            file.Close();
        }

        public void LoadGame()
        {
            var destination = Application.persistentDataPath + m_saveFileName;
            if (!File.Exists(destination)) { return; }
            var file = File.OpenRead(destination);

            var binaryFormatter = new BinaryFormatter();
            var saveData = (SaveData) binaryFormatter.Deserialize(file);
            file.Close();

            GameManager.totalScore = saveData.TotalScore;
            GameManager.highScore = saveData.HighestScore;

            GameManager.monster0seen = saveData.HasSeenMonsters[0];
            GameManager.monster1seen = saveData.HasSeenMonsters[1];
            GameManager.monster2seen = saveData.HasSeenMonsters[2];
            if(saveData.Version > 0) { GameManager.monster2secondDialogueSeen = saveData.HasSeenMonsters[3]; }

            GameManager.lostToEntity = saveData.LostToEntity;
        }

        public void SaveEntityImage(int _id)
        {
            var desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            var destination = desktopPath + "\\" + m_entityImageFileNames[_id] + ".png";

            if (File.Exists(destination)) { return; }
            
            var textureBytes = m_entityImages[_id].EncodeToPNG();
            File.WriteAllBytes(destination, textureBytes);
        }
    }
}
