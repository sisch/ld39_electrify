using System;
using UnityEngine;

[Serializable]
public class ActiveElement
{
    public enum ElementType
    {
        Input,
        Wire,
        And,
        Or,
        Output,
    }
}