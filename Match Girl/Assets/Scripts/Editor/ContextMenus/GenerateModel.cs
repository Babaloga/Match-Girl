using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using Anima2D;


public class SpriteMeshMenu {

    [MenuItem("CONTEXT/SpriteMesh/Generate Model")]
    static void GenerateModel(MenuCommand command)
    {
        SpriteMesh targetMesh = (SpriteMesh)command.context;

        string targetPath = AssetDatabase.GetAssetPath(command.context);
        string folderPath = targetPath.Replace("/" + targetMesh.name + ".asset", "");

        string[] pathBroken = folderPath.Split(new char[] { '/' });
        string modelName = pathBroken[pathBroken.Length-1];

        Debug.Log(folderPath);

        Model newModel = ScriptableObject.CreateInstance<Model>();

        newModel.name = modelName;

        foreach(string s in AssetDatabase.FindAssets("t:SpriteMesh", new string[] { folderPath }))
        {
            Debug.Log(AssetDatabase.GUIDToAssetPath(s));
            SpriteMesh mesh = (SpriteMesh)AssetDatabase.LoadAssetAtPath(AssetDatabase.GUIDToAssetPath(s), typeof(SpriteMesh));

            string idKey = mesh.name[0].ToString() + mesh.name[1].ToString();

            if(idKey == "HE")
            {
                newModel.head.spriteMesh = mesh;
            }
            else if(idKey == "TO")
            {
                newModel.torso.spriteMesh = mesh;
            }
            else if (idKey == "HA")
            {
                newModel.hat.spriteMesh = mesh;
            }
            else if (idKey == "RT")
            {
                newModel.rightThigh.spriteMesh = mesh;
            }
            else if (idKey == "LT")
            {
                newModel.leftThigh.spriteMesh = mesh;
            }
            else if (idKey == "RS")
            {
                newModel.rightShin.spriteMesh = mesh;
            }
            else if (idKey == "LS")
            {
                newModel.leftShin.spriteMesh = mesh;
            }
            else if (idKey == "RF")
            {
                newModel.rightFoot.spriteMesh = mesh;
            }
            else if (idKey == "LF")
            {
                newModel.leftFoot.spriteMesh = mesh;
            }
            else if (idKey == "RA")
            {
                newModel.rightForearm.spriteMesh = mesh;
            }
            else if (idKey == "LA")
            {
                newModel.leftForearm.spriteMesh = mesh;
            }
            else if (idKey == "RU")
            {
                newModel.rightUpperArm.spriteMesh = mesh;
            }
            else if (idKey == "LU")
            {
                newModel.leftUpperArm.spriteMesh = mesh;
            }
            else if (idKey == "RH")
            {
                newModel.rightHand.spriteMesh = mesh;
            }
            else if (idKey == "LH")
            {
                newModel.leftHand.spriteMesh = mesh;
            }
            else
            {
                Debug.LogWarning("Invalid Part Code: " + idKey, mesh);
            }
        }

        AssetDatabase.CreateAsset(newModel, "Assets/Resources/NPCModels/" + modelName + ".asset");
    }
}
