using System.Collections;
using System.Collections.Generic;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
#endif

[ExecuteInEditMode()]
public class EditorGameTools : MonoBehaviour
{
#if UNITY_EDITOR
    [MenuItem("GameObject/MovableObstacle")]
    public static void AddMovableObstacle()
    {
        GameObject inst = Instantiate(Resources.Load<GameObject>("Obstacles/MovableObstacle"));
        inst.transform.SetParent(Selection.activeGameObject.transform, false);
    }
#endif
}
