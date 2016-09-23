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
    Vector3 playerLastPosition;
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
                        rigid.useGravity = true;
                        //add player real world velocity to the rigidbody
                        rigid.velocity += (Head.position + bodyCenter - playerLastPosition) / Time.deltaTime;
                    }
                    else
                    {
                        playerLastPosition = Head.position + bodyCenter;
                        //Player is currently falling in the real world
                        rigid.useGravity = false;//Turns off virtual gravity because real world acceleration is moving the player
                        //rigid.AddForce(Vector3.up * grav, ForceMode.Acceleration);
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
                    if (Physics.Raycast(new Vector3(Head.position.x, Head.position.y - bodyHeight, Head.position.z), Vector3.down, out hit, 0.1f))
                    {
                        gravState = GravStateEnum.OnGround;
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

    public Gain FilterGains(Gain gain)
    {
        switch (gravState)
        {
            //case GravStateEnum.FallingReal:
            //    break;
            case GravStateEnum.FallingVirtual:
                return new Gain(-1, -1, -1, -1);
                break;
            default:
                return gain;
                break;
        }
    }
}
