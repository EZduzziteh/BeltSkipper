using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{

    public List<GameObject> ItemsToDrop = new List<GameObject>();
    public int MaxDrops;
    public int MinDrops;
    public void DropResource()
    {
        //randomly figure out how many items to drop
        int randomnum = Random.Range(MinDrops, MaxDrops);

        for (int i = 0; i <= MaxDrops; i++)
        {
            //Randomly select from drop list which items to drop and then instantiate them
            GameObject temp=GameObject.Instantiate(ItemsToDrop[Random.Range(0, ItemsToDrop.Count)]);
            temp.transform.position = transform.position;
        }
    }
}
