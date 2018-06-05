using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChipsManager : Singleton<ChipsManager>
{
    [SerializeField]
    public Sprite[] Chips;

    [SerializeField]
    public Sprite[] ChipsBottom;

    private Dictionary<string, List<GameObject>> chipsDic = new Dictionary<string, List<GameObject>>();

    
    public GameObject GetChip(long amount, bool isBottom = false)
    {
        GameObject chip = GetCachedChip(amount, isBottom);

        if (chip == null)
            chip = CreateChip(amount, isBottom);

        chip.SetActive(true);

        return chip;
    }

    private GameObject CreateChip(long amount, bool isBottom)
    {
        GameObject newObj = new GameObject();
        
        
        Image image = newObj.AddComponent<Image>();
        int imageIndex = GetImageIndex(amount);


        if(isBottom)
        {
            image.sprite = ChipsBottom[imageIndex];
        }
        else
        {
            image.sprite = Chips[imageIndex];
        }

        AddCache(amount, isBottom, newObj);

        newObj.name = "Chip";
        return newObj;
    }

    private void AddCache(long amount, bool isBottom, GameObject newObj)
    {
        string key = GetImageIndex(amount) + "_" + (isBottom ? "Bottom" : "Normal");
        List<GameObject> imageList = null;
        if (chipsDic.ContainsKey(key) == false)
        {
            imageList = new List<GameObject>();
        }
        else
        {
            chipsDic.TryGetValue(key, out imageList);
        }

        imageList.Add(newObj);
        chipsDic[key] = imageList;
    }

    public void ReStore(Transform tf)
    {
        tf.name = "Chip";
        tf.gameObject.SetActive(false);
        
        tf.SetParent(this.transform);
    }

    private int GetImageIndex(long amount)
    {
        int index = 0;

        switch(amount)
        {
            case 10:
                index = 0;
                break;
            case 50:
                index = 1;
                break;
            case 100:
                index = 2;
                break;
            case 500:
                index = 3;
                break;
            case 1000:
                index = 4;
                break;
            case 5000:
                index = 5;
                break;
            case 10000:
                index = 6;
                break;
            case 50000:
                index = 7;
                break;
            case 100000:
                index = 8;
                break;
            case 500000:
                index = 9;
                break;
            case 1000000:
                index = 10;
                break;
        }

        if(amount > 1000000)
        {
            index = 10;
        }
        return index;
    }

    private GameObject GetCachedChip(long amount, bool isBottom)
    {
        GameObject chip = null;
        string key = GetImageIndex(amount) + "_" + (isBottom ? "Bottom" : "Normal");
        if (chipsDic.ContainsKey(key))
        {
            List<GameObject> imageList = null;
            chipsDic.TryGetValue(key, out imageList);

            if(imageList != null)
            {
                for(int i = 0;  i < imageList.Count; i++)
                {
                    if(imageList[i].transform.parent == this.transform)
                    {
                        chip = imageList[i];
                    }
                }
            }   
        }

        return chip;
    }

    
}
