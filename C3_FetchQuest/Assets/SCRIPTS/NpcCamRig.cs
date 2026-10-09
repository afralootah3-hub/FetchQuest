using Unity.Cinemachine;
using UnityEngine;
using Yarn.Unity;

public class NpcCamRig : MonoBehaviour
{

    /* to do
     * switch between the different transforms to set what view our camera is           DONE
     * switch from 3rd person cam to cameraRig (using CineMachine priority)             DONE
     * set when the switch will happen, by having YarnSpinner call the correct method   DONE
     * switch BACK from the NPC_cCam to our 3rd person cam                              DONE
    */

    public Transform npc_Loc;
    public Transform pc_Loc;
    public Transform pc_move_loc; 

    public int pNum; //priority number 
    public CinemachineCamera npc_cCam;

    [YarnCommand("cam_move")]
    public void cam_move(string locName)
    {

        npc_cCam.Priority = pNum;
        if(locName == "npc")
        {
            npc_cCam.gameObject.transform.position = npc_Loc.position;
        }
        else if(locName == "pc")
        {
            npc_cCam.gameObject.transform.position = pc_Loc.position; 
        }
        else if (locName == "return")
        {
            npc_cCam.Priority = 0;
        }



    }

    public void movePlayer()
    {
        GameController.instance.movePlayerOnNPC(pc_move_loc);


    }


}
