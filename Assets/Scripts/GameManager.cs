using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { private set; get; }




    // Start is called before the first frame update
    void Awake()
    {
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);
            Instance = this;
            inventory = new List<InventoryItem>();
        }
        else
        {
            Destroy(gameObject);
        }

    }
    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    public void LoadScene(REGISTEREDSCENES scene)
    {

        SceneManager.LoadScene((int)scene);
    }


    public void LoadSceneDelayed(REGISTEREDSCENES scene, float delay)
    {
        StartCoroutine(LoadSceneDelayedCoroutine(delay, scene));
    }

    private IEnumerator LoadSceneDelayedCoroutine(float delay, REGISTEREDSCENES r)
    {
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene((int)r);
    }

    public enum REGISTEREDSCENES
    {
        MainMenu = 0,
        StartStory = 1,
        Jungle = 2,
        Left = 3,
        Right = 4,
        Cliff = 5,
        Fork = 6

    }


    List<InventoryItem> inventory;

    public enum InventoryItem
    {
        invalid = -1,
        stick,
        chicken
    }

    public bool HasInventoryItem(InventoryItem ii)
    {
        return inventory.Contains(ii);

    }

    public bool HasInventoryItem(string s)
    {
        InventoryItem ii = (InventoryItem)Enum.Parse(typeof(InventoryItem), s);
        return inventory.Contains(ii);
    }


    public void AddInventoryItem(InventoryItem ii)
    {
        inventory.Add(ii);
    }

    public void RemoveInventoryItem(InventoryItem ii)
    {
        _ = inventory.Remove(ii);
    }

}
