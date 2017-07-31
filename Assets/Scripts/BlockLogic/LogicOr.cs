using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class LogicOr : MonoBehaviour, IActiveElement
{
    public const string Type = "or";
    public List<IActiveElement> neighbouringElements;

    public Sprite[] Sprites;
    public SpriteRenderer SpriteRenderer;

    int activationEnergy;
    [SerializeField] bool isActive;

    public void Activate(IActiveElement element)
    {
        activationEnergy++;
    }

    public void MakeTick()
    {
        SpriteRenderer.sprite = isActive ? Sprites[1] : Sprites[0];
        if (IsActive())
        {
            foreach (var element in neighbouringElements)
            {
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
        isActive = false || activationEnergy > 0;
        activationEnergy = 0;
        return tmp;
    }

    // Use this for initialization
    void Awake()
    {
        neighbouringElements = new List<IActiveElement>();
        isActive = false;
        activationEnergy = 0;
    }

    void Start()
    {
        GameManager.Instance.RegisterBlock(this);
    }

    // Update is called once per frame
    void Update()
    {
    }
}