using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour
{
    public CameraPos cameraPos;

    public static CameraFollow Instance;


    private void Awake()
    {
        Instance = this;
    }

    private void LateUpdate()
    {
        FollowPlayer();
    }

    void FollowPlayer()
    {
        if (Vector3.Distance(transform.position, cameraPos.transform.position) > 0.1f)
        {
            transform.position = Vector3.Lerp(transform.position, cameraPos.transform.position, cameraPos.followSpeed);
            transform.rotation = cameraPos.transform.rotation;
        }
    }
}
