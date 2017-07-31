using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IActiveElement
{
    void Activate(IActiveElement element);
    void MakeTick();
    void AddBlock(IActiveElement element);
    bool IsActive();
}
