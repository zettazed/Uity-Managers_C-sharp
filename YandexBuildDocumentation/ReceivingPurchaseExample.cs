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
                    MetaResource.gems.count += 80;
                    break;
                case "gems2":
                    MetaResource.gems.count += 500;
                    break;
                case "gems3":
                    MetaResource.gems.count += 1200;
                    break;
                case "gems4":
                    MetaResource.gems.count += 2500;
                    break;
                case "gems5":
                    MetaResource.gems.count += 6500;
                    break;
                case "gems6":
                    MetaResource.gems.count += 14000;
                    break;
            }

            YandexGame.SaveProgress();
            // ��� ��� ��� ��������� �������. ��������:
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