using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SingletonTest :Singleton<SingletonTest> {

    protected SingletonTest() { } // guarantee this will be always a singleton only - can't use the constructor!
}
