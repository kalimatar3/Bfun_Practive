using System.Collections;
using System.Collections.Generic;
using Clouds.Ultilities;
using UnityEngine;
using UnityEngine.PlayerLoop;

public abstract class SceneData : Singleton<SceneData>
{
    public abstract void Init();
    public abstract void Denit();
}
