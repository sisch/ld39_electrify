using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Texture2D Map;
    public Transform Board;
    public Mapping[] ColorMappings;
    Dictionary<Vector2, IActiveElement> _elements;
    float scaleFactor;
    Vector2 scaleOffset;

    // Use this for initialization
    void Start()
    {
        scaleFactor = 1/3f;
        scaleOffset = new Vector2(-30f, -23.8f);
        _elements = new Dictionary<Vector2, IActiveElement>();
        Debug.Log("initialize level");
        CreateLevel();
        Debug.Log("Make Connections");
        ConnectLevel();
    }

    // Update is called once per frame
    void Update()
    {
    }

    void CreateLevel()
    {
        for (var x = 1; x < Map.width - 1; ++x)
        {
            for (var y = 1; y < Map.height - 1; ++y)
            {
                {
                    var pixel = Map.GetPixel(x, y);
                    if (pixel == Color.white)
                    {
                        continue;
                    }
                    foreach (var cMap in ColorMappings)
                    {
                        if (cMap.color != pixel)
                        {
                            continue;
                        }
                        var go = Instantiate(
                            cMap.prefab, scaleFactor * (new Vector2(x, y) + scaleOffset), Quaternion.identity, Board);
                        IActiveElement el = null;
                        switch (cMap.Type)
                        {
                            case ActiveElement.ElementType.And:
                            {
                                el = go.GetComponent<LogicAnd>();
                                break;
                            }
                            case ActiveElement.ElementType.Or:
                            {
                                el = go.GetComponent<LogicOr>();
                                break;
                            }
                            case ActiveElement.ElementType.Input:
                            {
                                el = go.GetComponent<Input>();
                                break;
                            }
                            case ActiveElement.ElementType.Output:
                            {
                                el = go.GetComponent<Output>();
                                break;
                            }
                            case ActiveElement.ElementType.Wire:
                            {
                                el = go.GetComponent<Wire>();
                                break;
                            }
                        }
                        if (el != null)
                        {
                            _elements.Add(new Vector2(x, y), el);
                        }
                    }
                }
            }
        }
    }

    void ConnectLevel()
    {
        for (var y = 1; y < Map.height - 1; ++y)
        {
            for (var x = 1; x < Map.width - 1; ++x)
            {
                {
                    var pixel = Map.GetPixel(x, y);
                    if (pixel == Color.white)
                    {
                        continue;
                    }
                    foreach (var cMap in ColorMappings)
                    {
                        if (cMap.color != pixel)
                        {
                            continue;
                        }
                        //Debug.Log(cMap.type);
                        switch (cMap.Type)
                        {
                            case ActiveElement.ElementType.Input:
                            {
                                ConnectInput(x, y);
                                break;
                            }
                            case ActiveElement.ElementType.Wire:
                            {
                                ConnectWire(x, y);
                                break;
                            }
                            case ActiveElement.ElementType.And:
                            {
                                ConnectLogic(x, y);
                                break;
                            }
                            case ActiveElement.ElementType.Or:
                            {
                                ConnectLogic(x, y);
                                break;
                            }
                            case ActiveElement.ElementType.Output:
                            {
                                ConnectOutput(x, y);
                                break;
                            }
                        }
                    }
                }
            }
        }
    }

    void ConnectInput(int x, int y)
    {
        Connect(x, y, 1, 0, Color.green);
        Connect(x, y, -1, 0, Color.green);
        Connect(x, y, 0, 1, Color.green);
        Connect(x, y, 0, -1, Color.green);

    }

    void ConnectWire(int x, int y)
    {
        var pixel = Map.GetPixel(x - 1, y);
        Connect(x, y, -1, 0, pixel);
        pixel = Map.GetPixel(x + 1, y);
        Connect(x, y, 1, 0, pixel);
        pixel = Map.GetPixel(x, y - 1);
        Connect(x, y, 0, -1, pixel);
        pixel = Map.GetPixel(x, y + 1);
        Connect(x, y, 0, 1, pixel);
    }

    void Connect(int x, int y, int dx, int dy, Color pixel)
    {
        if (pixel == Color.white)
        {
            return;
        }
        IActiveElement otherBlock = null;
        IActiveElement thisBlock = null;
        bool willConnect = _elements.TryGetValue(new Vector2(x + dx, y + dy), out otherBlock);
        willConnect = willConnect && _elements.TryGetValue(new Vector2(x, y), out thisBlock);
        if (willConnect)
        {
            thisBlock.AddBlock(otherBlock);
        }
    }

    void ConnectLogic(int x, int y)
    {
        var x2 = x;
        var y2 = y;
        var pixel = Map.GetPixel(x + 1, y);
        if (pixel != Color.white)
        {
            x2++;
        }
        pixel = Map.GetPixel(x, y + 1);
        if (pixel != Color.white)
        {
            y2++;
        }
        pixel = Map.GetPixel(x - 1, y);
        if (pixel != Color.white)
        {
            x2--;
        }
        pixel = Map.GetPixel(x, y - 1);
        if (pixel != Color.white)
        {
            y2--;
        }
        IActiveElement otherBlock = null;
        IActiveElement thisBlock = null;
        bool willConnect = _elements.TryGetValue(new Vector2(x2, y2), out otherBlock);
        willConnect = willConnect && _elements.TryGetValue(new Vector2(x, y), out thisBlock);
        if (willConnect)
        {
            thisBlock.AddBlock(otherBlock);
        }

        // Connect inputs
        if (x2 > x)
        {
            if (_elements.TryGetValue(new Vector2(x, y), out otherBlock)
                && _elements.TryGetValue(new Vector2(x - 1, y + 1), out thisBlock))
            {
                thisBlock.AddBlock(otherBlock);
            }
            if (_elements.TryGetValue(new Vector2(x, y), out otherBlock)
                && _elements.TryGetValue(new Vector2(x - 1, y - 1), out thisBlock))
            {
                thisBlock.AddBlock(otherBlock);
            }
        }
        else if (x2 < x)
        {
            if (_elements.TryGetValue(new Vector2(x, y), out otherBlock)
                && _elements.TryGetValue(new Vector2(x + 1, y + 1), out thisBlock))
            {
                thisBlock.AddBlock(otherBlock);
            }
            if (_elements.TryGetValue(new Vector2(x + 1, y - 1), out thisBlock))
            {
                thisBlock.AddBlock(otherBlock);
            }
        }
    }

    void ConnectOutput(int x, int y)
    {
        //Block otherBlock;
        //Block thisBlock;
    }
}