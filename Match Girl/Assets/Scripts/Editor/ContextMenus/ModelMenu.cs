using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ModelMenu{

    [MenuItem("CONTEXT/Model/Generate Divisions")]
    static void GenerateDivisions(MenuCommand command)
    {
        Model model = (Model)command.context;

        if(!AssetDatabase.IsValidFolder("Assets/Resources/NPCDivisions/" + model.name + "_divisions"))
            AssetDatabase.CreateFolder("Assets/Resources/NPCDivisions", model.name + "_divisions");

        Division hat = new Division(model.modelType, DivisionType.hat, new Part[] {model.hat});
        AssetDatabase.CreateAsset(hat, "Assets/Resources/NPCDivisions/"+ model.name + "_divisions/hat.asset");

        Division headAndHands = new Division(model.modelType, DivisionType.headAndHands, new Part[] { model.head, model.leftHand, model.rightHand });
        AssetDatabase.CreateAsset(headAndHands, "Assets/Resources/NPCDivisions/" + model.name + "_divisions/headAndHands.asset");

        Division body = new Division(model.modelType, DivisionType.body, new Part[] { model.torso, model.rightUpperArm, model.rightForearm, model.leftUpperArm, model.leftForearm});
        AssetDatabase.CreateAsset(body, "Assets/Resources/NPCDivisions/" + model.name + "_divisions/body.asset");

        Division legs = new Division(model.modelType, DivisionType.legs, new Part[] { model.rightThigh, model.rightShin, model.leftThigh, model.leftShin });
        AssetDatabase.CreateAsset(legs, "Assets/Resources/NPCDivisions/" + model.name + "_divisions/legs.asset");

        Division feet = new Division(model.modelType, DivisionType.feet, new Part[] { model.leftFoot, model.rightFoot });
        AssetDatabase.CreateAsset(feet, "Assets/Resources/NPCDivisions/" + model.name + "_divisions/feet.asset");
    }

}
