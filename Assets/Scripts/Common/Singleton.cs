using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/**
GameManager나 SoundManager와 같은 유일한 객체는 싱글톤 패턴으로 관리하는게 편리하다.싱글톤 클래스 작성법에는 여러가지가 있으나 여기에서는 MonoBehaviour를 상속받는 
싱글톤 클래스를 작성해 보았다.인스턴스를 생성한 방법을 눈여겨 봐야 하는데, 게임을 처음부터 플레이할 경우는 상관없으나 특정 씬만을 테스트할때 
다른 씬의 싱글톤 클래스를 참조해야 할 경우가 있다. 이 때 null exception이 발생하므로 이에 대한 처리로 Resource폴더에서 프리팹을 불러와 인스턴스를 
생성하도록 하였다.이 클래스를 상속받으면 싱글톤 패턴이 적용된다. 
덧붙이자면 참조가 많은 public 프로퍼티나, 변수, 함수는 static으로 작성하여 '클래스명.객체명' 형식으로 바로 접근하도록 하는 것이 바람직하다.
*/
public abstract class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance = null;
    private static object _syncobj = new object();
    private static bool appIsClosing = false;

    public static T Instance
    {
        get
        {
            if (appIsClosing)
                return null;

            lock (_syncobj)
            {
                if (_instance == null)
                {
                    T[] objs = FindObjectsOfType<T>();

                    if (objs.Length > 0)
                        _instance = objs[0];

                    if (objs.Length > 1)
                        Debug.LogError("There is more than one " + typeof(T).Name + " in the scene.");

                    if (_instance == null)
                    {
                        string goName = typeof(T).ToString();
                        GameObject go = GameObject.Find(goName);
                        if (go == null)
                            go = new GameObject(goName);
                        _instance = go.AddComponent<T>();
                    }
                }
                return _instance;
            }
        }
    }

    /// <summary>
    /// When Unity quits, it destroys objects in a random order.
    /// In principle, a Singleton is only destroyed when application quits.
    /// If any script calls Instance after it have been destroyed,
    ///   it will create a buggy ghost object that will stay on the Editor scene
    ///   even after stopping playing the Application. Really bad!
    /// So, this was made to be sure we're not creating that buggy ghost object.
    /// </summary>
    protected virtual void OnApplicationQuit()
    {
        // release reference on exit
        appIsClosing = true;
    }
}
