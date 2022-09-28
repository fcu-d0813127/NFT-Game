using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalUseLibrary : MonoBehaviour
{
    // The following code is in https://stackoverflow.com/questions/44456133/find-inactive-gameobject-by-name-tag-or-layer
    internal static GameObject FindInActiveObjectByName(string name)
    {
        Transform[] objs = Resources.FindObjectsOfTypeAll<Transform>() as Transform[];
        for (int i = 0; i < objs.Length; i++)
        {
            if (objs[i].hideFlags == HideFlags.None)
            {
                if (objs[i].name == name)
                {
                    return objs[i].gameObject;
                }
            }
        }
        return null;
    }
}
