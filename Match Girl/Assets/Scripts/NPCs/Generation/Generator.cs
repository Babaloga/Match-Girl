using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Anima2D;

public class Generator : MonoBehaviour {

    Archetype archetype;

    public bool male = true;

    public SpriteMeshInstance head;
    public SpriteMeshInstance torso;
    public SpriteMeshInstance hat;
    public SpriteMeshInstance rightThigh;
    public SpriteMeshInstance leftThigh;
    public SpriteMeshInstance rightShin;
    public SpriteMeshInstance rightUpperArm;
    public SpriteMeshInstance leftShin;
    public SpriteMeshInstance leftFoot;
    public SpriteMeshInstance leftForearm;
    public SpriteMeshInstance leftUpperArm;
    public SpriteMeshInstance leftHand;
    public SpriteMeshInstance rightFoot;
    public SpriteMeshInstance rightHand;
    public SpriteMeshInstance rightForearm;

    private void Start()
    {
        if (transform.parent.GetComponent<SpecialInteraction>())
        {
            archetype = transform.parent.GetComponent<SpecialInteraction>().characterArchetype;
        }
        else
        {
            NPCInteractionBasic npc = transform.parent.GetComponent<NPCInteractionBasic>();

            if(male) archetype = TypeDictionary.instance.maleDictionary[npc.npcType];
            else archetype = TypeDictionary.instance.femaleDictionary[npc.npcType];
        }

        GenerateNPC();
    }

    public void GenerateNPC()
    {
        Division d_hat = archetype.hats[Random.Range(0, archetype.hats.Length)];
        hat.spriteMesh = d_hat.parts[0].spriteMesh;

        Division d_head = archetype.heads[Random.Range(0, archetype.heads.Length)];
        head.spriteMesh = d_head.parts[0].spriteMesh;
        rightHand.spriteMesh = d_head.parts[2].spriteMesh;
        leftHand.spriteMesh = d_head.parts[1].spriteMesh;

        Division d_body = archetype.bodies[Random.Range(0, archetype.bodies.Length)];
        torso.spriteMesh = d_body.parts[0].spriteMesh;
        rightUpperArm.spriteMesh = d_body.parts[1].spriteMesh;
        rightForearm.spriteMesh = d_body.parts[2].spriteMesh;
        leftUpperArm.spriteMesh = d_body.parts[3].spriteMesh;
        leftForearm.spriteMesh = d_body.parts[4].spriteMesh;

        Division d_legs = archetype.legs[Random.Range(0, archetype.legs.Length)];
        rightThigh.spriteMesh = d_legs.parts[0].spriteMesh;
        rightShin.spriteMesh = d_legs.parts[1].spriteMesh;
        leftThigh.spriteMesh = d_legs.parts[2].spriteMesh;
        leftShin.spriteMesh = d_legs.parts[3].spriteMesh;

        Division d_feet = archetype.feet[Random.Range(0, archetype.feet.Length)];
        leftFoot.spriteMesh = d_feet.parts[0].spriteMesh;
        rightFoot.spriteMesh = d_feet.parts[1].spriteMesh;
    }
}
