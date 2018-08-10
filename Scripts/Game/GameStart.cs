using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStart : MonoBehaviour {

    private void Awake()
    {
        ScenesLoadManager.Instance.GetScene<MainScene>().OnSceneEnter();
    }
}
