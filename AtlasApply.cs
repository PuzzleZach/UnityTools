using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using System.IO;

public class AtlasApply : MonoBehaviour
{
    public Material newAtlasMaterial;

    [SerializeField]
    public string projectName;

    [SerializeField]
    public string modelFolder;
    
    [SerializeField]
    public string fileExtension = "*.fbx";

    [ContextMenu("ApplyAtlas")]
    void ApplyAtlasToModels()
    {
        string folderPath = Path.Combine(Application.dataPath, modelFolder);
        string[] modelPaths = Directory.GetFiles(folderPath, fileExtension);

        foreach (string modelPath in modelPaths)
        {
            string newPath = modelPath.Split(projectName)[1].Replace("\\", "/").Substring(1);
            GameObject model =  AssetDatabase.LoadAssetAtPath<GameObject>(newPath);
            
            if (model == null) continue;

            Renderer[] renderers = model.GetComponentsInChildren<Renderer>();

            foreach (Renderer renderer in renderers)
            {
                Material[] materials = renderer.sharedMaterials;

                for (int i = 0; i < materials.Length; i++)
                {
                    if (materials[i].name == "Atlas")
                    {
                        materials[i] = newAtlasMaterial;   
                    }
                }
                renderer.materials = materials;
            }
        }
        AssetDatabase.Refresh();
    }
}
