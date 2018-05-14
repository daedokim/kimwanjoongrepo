using UnityEngine;
using UnityEngine.UI;
using com.dug.UI.presenter;

namespace com.dug.UI.view
{
    public class StatusView : MonoBehaviour, IView
    {
        private StatusPresenter presenter;

        private void Awake()
        {
            presenter = new StatusPresenter(this);
        }

        public void SetStatusText(string text)
        {
        }
    }
}
 
