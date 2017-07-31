using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wire : MonoBehaviour, IActiveElement
{

    public const string Type = "wire";
    public List<IActiveElement> neighbouringElements;

    [SerializeField] bool isActive;
    public bool IsActiveInNextTick;

    public Sprite[] Sprites;
    public SpriteRenderer SpriteRenderer;

    IActiveElement activationSource;
    // Use this for initialization
    void Awake()
    {
        neighbouringElements = new List<IActiveElement>();
        isActive = false;
        IsActiveInNextTick = false;
    }

    void Start()
    {
        GameManager.Instance.RegisterBlock(this);    
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Activate(IActiveElement element)
    {
        IsActiveInNextTick = true;
        activationSource = element;
    }

    public void MakeTick()
    {
        SpriteRenderer.sprite = isActive ? Sprites[1] : Sprites[0];
        if (!IsActive())
        {
            return;
        }
        foreach (var element in neighbouringElements)
        {
            if (element == activationSource)
            {
                continue;
            }
            element.Activate(this);
        }
    }

    public void AddBlock(IActiveElement element)
    {
        neighbouringElements.Add(element);
    }

    public bool IsActive()
    {
        bool tmp = isActive;
        isActive = IsActiveInNextTick;
        IsActiveInNextTick = false;
        return tmp;
    }
}
