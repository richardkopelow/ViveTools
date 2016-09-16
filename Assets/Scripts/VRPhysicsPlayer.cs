using UnityEngine;
using System.Collections;

public class VRPhysicsPlayer : MonoBehaviour
{
    const float grav = 9.81f;
    enum GravStateEnum
    {
        FallingReal,
        FallingVirtual,
        OnGround
    }
    public static float Height;

    public Transform Head;
    CapsuleCollider body;

    Rigidbody rigid;
    GravStateEnum gravState = GravStateEnum.OnGround;

    void Start()
    {
        body = Head.gameObject.AddComponent<CapsuleCollider>();
        rigid = GetComponent<Rigidbody>();
    }
    
    void Update()
    {
        float bodyHeight = Head.localPosition.y;
        bool tooTall = bodyHeight > Height;
        if (tooTall)
        {
            bodyHeight = Height;
        }
        body.height = bodyHeight;
        Vector3 bodyCenter = body.center;
        bodyCenter.y = -bodyHeight / 2;
        body.center = bodyCenter;

        switch (gravState)
        {
            case GravStateEnum.FallingReal:
                {
                    if (!tooTall)
                    {
                        //player is no longer falling
                        gravState = GravStateEnum.FallingVirtual;
                    }
                    else
                    {
                        //Player is currently falling in the real world
                        rigid.AddForce(Vector3.up * grav, ForceMode.Acceleration);//Cancels out virtual acceleration because real world acceleration is moving the player
                        RaycastHit hit;
                        if (Physics.Raycast(new Vector3(Head.position.x, Head.position.y - bodyHeight, Head.position.z), Vector3.down, out hit))
                        {
                            gravState = GravStateEnum.OnGround;
                        }
                    }
                }
                break;
            case GravStateEnum.FallingVirtual:
                {
                    RaycastHit hit;
                    if (Physics.Raycast(new Vector3(Head.position.x, Head.position.y - bodyHeight, Head.position.z), Vector3.down, out hit))
                    {

                    }
                }
                break;
            case GravStateEnum.OnGround:
                {
                    if (tooTall)
                    {
                        gravState = GravStateEnum.FallingReal;
                    }
                }
                break;
            default:
                break;
        }

    }
}
