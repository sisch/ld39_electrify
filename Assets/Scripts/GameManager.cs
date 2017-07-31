using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager Instance;
    public float tickInterval = 0.3f;
    public GameObject WinningScreen;
    List<IActiveElement> AllBlocks
    {
        get { return _allBlocks ?? (_allBlocks = new List<IActiveElement>()); }
    }
    List<IActiveElement> _allBlocks;
    void Awake()
    {
        if (!Instance)
        {
            Instance = this;
        } else { Destroy(this);}
    }

    void Start()
    {
        Initialize();
    }

    public void RegisterBlock(IActiveElement block)
    {
        AllBlocks.Add(block);
    }

    void MakeTick()
    {
        foreach (IActiveElement block in  AllBlocks)
        {
            block.MakeTick();
        }
    }

    public void EndGame()
    {
        CancelInvoke();
        WinningScreen.SetActive(true);
    }

    public void Initialize()
    {
        WinningScreen.SetActive(false);
        InvokeRepeating("MakeTick", 1f, tickInterval);
    }
}
