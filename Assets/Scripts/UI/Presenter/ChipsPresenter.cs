using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using com.dug.UI.view;
using com.dug.UI.model;
using com.dug.UI.manager;
using com.dug.UI.events;
using System.Text;
using System;
using UniRx;

namespace com.dug.UI.presenter
{
    public class ChipsPresenter : IPresenter
    {
        private ChipsView view;
        private GameManager manager;

        private StringBuilder chipsText = new StringBuilder();
        private GamePlayerModel model = new GamePlayerModel();
        private long currentUserIndex = 0;

        public ChipsPresenter(ChipsView view) 
        {
            this.view = view;
            manager = GameManager.Instance;

            GameEvent.Instance.AddGamePlayerActionEvent(new UnityAction<GamePlayerModel>(OnGamePlayerActionUpdate));
        }

        private void OnGamePlayerActionUpdate(GamePlayerModel model)
        {
            this.model.Update(model);

            AppendPlayerChipsText();

            currentUserIndex = model.userIndex; 
        }

        private void AppendPlayerChipsText()
        {
            GamePlayerModel player = manager.GetGamePlayerByUserIndex(currentUserIndex);

            if(this.model != null)
            {
                chipsText.Append(this.model.nickName + "의 " + this.model.lastBetType + "베팅금액은 : " + this.model.lastBet + "토탈뱃은 :" + manager.Room.totalBet);
                chipsText.Append("\r\n");
                view.SetChipsText(chipsText.ToString());
            }
            
        }
    }
}
