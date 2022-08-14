using UnityEngine;


namespace Scripts
{
    public class CoinsCounter : MonoBehaviour
    {
        public int coins = 1;

        [SerializeField] private int Sale;

        public void Counter()
        {
            
            Load();
            Save();
        }

        private void Load()
        {
            if (PlayerPrefs.HasKey("CoinsCounter"))
            {
                this.coins = PlayerPrefs.GetInt("CoinsCounter") + Sale;
                Debug.Log("Coins:" + "\t" + coins);
            }

        }
        public void Save()
        {
            
            PlayerPrefs.SetInt("CoinsCounter", this.coins);
            PlayerPrefs.Save();
        }
    }
}