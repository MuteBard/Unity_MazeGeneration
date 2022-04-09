using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class SaveMesh : MonoBehaviour
{
    public static void Save(Mesh mesh, string name){
        AssetDatabase.CreateAsset(mesh, $"Assets/Caves/Generated/{name}");
        AssetDatabase.SaveAssets();
    }
}
