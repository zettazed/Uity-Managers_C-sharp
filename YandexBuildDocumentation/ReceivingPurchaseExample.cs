using UnityEngine;
using UnityEngine.Events;

namespace YG.Example
{
    [HelpURL("https://www.notion.so/PluginYG-d457b23eee604b7aa6076116aab647ed#10e7dfffefdc42ec93b39be0c78e77cb")]
    public class ReceivingPurchaseExample : MonoBehaviour
    {
        public static ReceivingPurchaseExample Instance;

        [SerializeField] UnityEvent successPurchased;
        [SerializeField] UnityEvent failedPurchased;

        private void OnEnable()
        {
            YandexGame.PurchaseSuccessEvent += SuccessPurchased;
            YandexGame.PurchaseFailedEvent += FailedPurchased;
        }

        private void OnDisable()
        {
            YandexGame.PurchaseSuccessEvent -= SuccessPurchased;
            YandexGame.PurchaseFailedEvent -= FailedPurchased;
        }

        private void Awake()
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            //PlayerPrefs.DeleteAll();
            //YandexGame.ResetSaveProgress();
        }

        //private void Start()
        //{
            //SoundManager.SetVolumeMusic(0f);
            //MonoSingleton<PlayerDataManager>.Instance.IsOnSoundBGM = false;
            //MonoSingleton<PlayerDataManager>.Instance.SaveOptionSound();
            //YandexGame.ConsumePurchases();
        //}

        void SuccessPurchased(string id)
        {
            successPurchased?.Invoke();

            Debug.Log("GetBuyMy = " + id);
            switch (id)
            {
                case "gems1":
                    
                    break;
            }

            YandexGame.SaveProgress();
            // Ваш код для обработки покупки. Например:
            //if (id == "50")
            //    YandexGame.savesData.money += 50;
            //else if (id == "250")
            //    YandexGame.savesData.money += 250;
            //else if (id == "1500")
            //    YandexGame.savesData.money += 1500;
            //YandexGame.SaveProgress();
        }

        void FailedPurchased(string id)
        {
            failedPurchased?.Invoke();
        }
    }
}
