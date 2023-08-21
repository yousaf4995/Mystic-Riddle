using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnumUtility 
{
    public enum CardState
    {
        None,
        Correct,
        InCorrect
    }

    public enum CardSpawnType
    {
        RowsWise,
        SpriteBase,
        CardsSizeBase
    }
}
