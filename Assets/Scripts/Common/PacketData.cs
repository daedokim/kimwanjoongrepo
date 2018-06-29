using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using com.dug.common;

[Serializable]
public class PacketData
{
    public int packetNum;

    public Dictionary<string, System.Object> data;

    public string error;
}
