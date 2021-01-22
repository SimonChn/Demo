using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

namespace Ruinarc
{
    [Serializable]
    public class SavablePlayerPrefsValue<T>
    {
        public delegate void OnChangeHandler(object sender);
        public event OnChangeHandler OnChange;

        private readonly string playerPrefsKey;
        private T value;

        #region Constructor

        public SavablePlayerPrefsValue(string playerPrefsKey, T value = default(T))
        {
            if (string.IsNullOrEmpty(playerPrefsKey))
            {
                throw new Exception("Empty key to save value in PlayerPrefs");
            }

            this.playerPrefsKey = playerPrefsKey;

            if (PlayerPrefs.HasKey(playerPrefsKey))
            {
                LoadValue();
            }
            else
            {
                this.value = value;
                SaveValue();
            }
        }
        #endregion

        public T Value
        {
            get
            {
                return value;
            }
            set
            {
                this.value = value;
                SaveValue();

                OnChange?.Invoke(this);
            }
        }

        private void LoadValue()
        {
            var stringToDeserialize = PlayerPrefs.GetString(playerPrefsKey, "");

            var bytes = Convert.FromBase64String(stringToDeserialize);
            var memorystream = new MemoryStream(bytes);
            var bf = new BinaryFormatter();

            value = (T)bf.Deserialize(memorystream);
        }

        private void SaveValue()
        {
            var memorystream = new MemoryStream();
            var bf = new BinaryFormatter();
            bf.Serialize(memorystream, value);

            var stringToSave = Convert.ToBase64String(memorystream.ToArray());

            PlayerPrefs.SetString(playerPrefsKey, stringToSave);
        }
    }
}
