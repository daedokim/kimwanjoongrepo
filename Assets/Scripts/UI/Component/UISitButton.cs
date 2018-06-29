using com.dug.UI.Managers;
using com.dug.UI.DTO;
using com.dug.UI.Networks;
using UniRx;
using UnityEngine;
using UnityEngine.UI;


namespace com.dug.UI.component
{
    public class UISitButton : MonoBehaviour
    {
        public int chairIndex = -1;
        private bool enable = true;

        public bool Enable { set { this.enable = value; } }

        void Awake()
        {
            this.gameObject.GetComponent<Button>().OnClickAsObservable()
                .Subscribe(_ =>
                {
                    if (GameManager.Instance.GetGamePlayerByUserIndex(UserData.Instance.userIndex) == null)
                    {
                        Room room = GameManager.Instance.Room;
                        PopupManager.Instance.OpenPopup(PopupManager.PopupTypes.SELECT_BUYIN_POPUP, room.buyInMin, room.buyInMax, chairIndex);
                    }

                });
        }

        private void Start()
        {
            this.ObserveEveryValueChanged(x => x.enable).Subscribe(x =>
            {
                this.gameObject.SetActive(enable);
            });
        }
    }

}
