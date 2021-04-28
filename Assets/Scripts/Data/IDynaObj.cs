using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 此接口用于顺序创建游戏对象
/// </summary>
interface IDynaObj
{
    void DynaCreate();

    void DynaDestroy();
}
