using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Inventory_So : ScriptableObject
{
    public List<float>Allitem;
    public List<float> selecteditem;

    public void Selectitem(float newd)
    {
        selecteditem.Add(newd);
    }

     public void Dropitem(float newd)
    {
        selecteditem.Remove(newd);
    }
}