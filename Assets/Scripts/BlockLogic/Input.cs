using System.Collections.Generic;
using UnityEngine;

public class Input : MonoBehaviour, IActiveElement
{
    public const string Type = "input";
    public List<IActiveElement> neighbouringElements;
    public bool IsActiveInNextTick;

    public Sprite[] Sprites;
    public SpriteRenderer SpriteRenderer;

    public void SendSignal()
    {
        foreach (var element in neighbouringElements)
        {
            element.Activate(this);
        }
        ScoreManager.Instance.DrainPower();
    }

    IActiveElement activationSource;

    [SerializeField] bool isActive;

    public void Activate(IActiveElement element)
    {
        IsActiveInNextTick = true;
        activationSource = element;
    }

    public void MakeTick()
    {
        SpriteRenderer.sprite = isActive ? Sprites[1] : Sprites[0];
        if (IsActive())
        {
            foreach (var element in neighbouringElements)
            {
                if (element == activationSource)
                {
                    continue;
                }
                element.Activate(this);
            }
        }
    }

    public void AddBlock(IActiveElement element)
    {
            neighbouringElements.Add(element);
    }

    public bool IsActive()
    {
        var tmp = isActive;
        isActive = IsActiveInNextTick;
        IsActiveInNextTick = false;
        return tmp;
    }

    void Start()
    {
        GameManager.Instance.RegisterBlock(this);
    }

    // Use this for initialization
    void Awake()
    {
        neighbouringElements = new List<IActiveElement>();
        isActive = false;
        IsActiveInNextTick = false;
    }

    // Update is called once per frame
    void Update()
    {
    }
}