using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using com.dug.UI.Networks;

public class UserSessionChange : MonoBehaviour {

    [MenuItem("사용자변경/wanjoong-1000")]
    static void Change1()
    {
        UserData.Instance.userIndex = 1000;
        UserData.Instance.nickName = "wanjoong";
    }
    [MenuItem("사용자변경/enyoung-1001")]
    static void Change2()
    {
        UserData.Instance.userIndex = 1001;
        UserData.Instance.nickName = "enyoung";
    }

    [MenuItem("사용자변경/jiyou-1002")]
    static void Change3()
    {
        UserData.Instance.userIndex = 1002;
        UserData.Instance.nickName = "jiyou";
    }


}
