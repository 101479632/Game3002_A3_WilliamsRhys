using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInventory : MonoBehaviour
{
    private HashSet<KeyType> keys = new HashSet<KeyType>();

    public void AddKey(KeyType key)
    {
        keys.Add(key);
        Debug.Log("Key added to inventory: " + key);
    }

    public bool HasKey(KeyType key)
    {
        return keys.Contains(key);
    }
}
