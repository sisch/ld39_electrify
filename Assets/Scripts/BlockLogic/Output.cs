using UnityEngine;

public class Output : MonoBehaviour, IActiveElement
{
    public const string Type = "output";
    public bool IsActiveInNextTick;
    [SerializeField] bool isActive;
    public Sprite[] Sprites;
    public SpriteRenderer SpriteRenderer;

    public void Activate(IActiveElement element)
    {
        IsActiveInNextTick = true;
    }

    public void MakeTick()
    {
        SpriteRenderer.sprite = isActive ? Sprites[1] : Sprites[0];
        isActive = IsActiveInNextTick;
        IsActiveInNextTick = false;
    }

    public void AddBlock(IActiveElement element)
    {
    }

    public bool IsActive()
    {
        return isActive;
    }

    void Start()
    {
        GameManager.Instance.RegisterBlock(this);
    }

    void Update()
    {
        if (isActive)
        {
            ScoreManager.Instance.Score();
        }
    }
}