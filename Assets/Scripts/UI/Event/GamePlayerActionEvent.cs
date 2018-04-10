using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using com.dug.UI.model;

public class GamePlayerActionEvent : Singleton<GamePlayerActionEvent>
{
    [System.Serializable]
    public class GetTableCardEvent : UnityEvent<GamePlayerModel> { }


    private GetTableCardEvent getTableCardEvent = new GetTableCardEvent();


    public void AddGetTableCardEvent(UnityAction<GamePlayerModel> call)
    {
        getTableCardEvent.AddListener(call);
    }

    public void RemoveGetTableCardEvent(UnityAction<GamePlayerModel> call)
    {
        getTableCardEvent.RemoveListener(call);
    }

    public void RemoveAllGetTableCardEvent()
    {
        getTableCardEvent.RemoveAllListeners();
    }

    public void InvokeGetTableCardEvent(GamePlayerModel model)
    {
        getTableCardEvent.Invoke(model);
    }


}
