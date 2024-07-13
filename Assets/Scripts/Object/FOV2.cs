using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.UIElements;

public class FOV2 : MonoBehaviour
{
    public float radius = 5f;
    [Range(0, 360)] public float angle = 348f;
    public LayerMask targetLayer;
    public LayerMask obstructionLayer;

    public GameObject playerRef;
    public bool CanSeePlayer { get; private set; }

    public float speed;
    public float distance = 25f;
    public bool movingRight = true;

    //respawn effect
    private Material material;
    private bool isplayOne = false;
    private bool isplayTwo = false;
    private float fadeSpeed = 5f;
    private bool isTeleport = false;
    private float temp = 0f;

    private void Start()
    {
        StartCoroutine(FOVCheck());
    }

    private IEnumerator FOVCheck()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);
        while (true)
        {
            yield return wait;
            Fov();
        }
    }

    private void Update()
    {
        var renderer = playerRef.GetComponent<Renderer>();
        material = renderer.material;
        transform.Translate(Vector2.right * speed * Time.deltaTime);
        temp -= speed * Time.deltaTime;
        if (temp <= -distance || temp >= distance)
        {
            if (movingRight == true)
            {
                transform.eulerAngles = new Vector3(0, -180, 0);
                movingRight = false;
                temp = 0f;
            }
            else
            {
                transform.eulerAngles = new Vector3(0, 0, 0);
                movingRight = true;
                temp = 0f;
            }
        }
        if (CanSeePlayer)
        {
            playerRef.GetComponent<Animator>().enabled = false;
            playerRef.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            PlayerMovement.runSpeed = 0f;
            isplayOne = true;
            CanSeePlayer = false;
        }
        if (isplayOne)
        {
            material.SetFloat("_Num", Mathf.Lerp(material.GetFloat("_Num"), 0f, fadeSpeed * Time.deltaTime));
            if (material.GetFloat("_Num") < 0.01)
            {
                isplayTwo = true;
                isplayOne = false;
                isTeleport = true;
            }
        }
        if (isplayTwo)
        {
            if (isTeleport)
            {
                PlayerMovement.runSpeed = 7.5f;
                playerRef.transform.position = PlayerDieLevel1.respawnPoint;
                isTeleport = false;
                playerRef.GetComponent<Animator>().enabled = true;
            }
            material.SetFloat("_Num", Mathf.Lerp(material.GetFloat("_Num"), 4.5f, fadeSpeed * Time.deltaTime));
            if (material.GetFloat("_Num") > 4.49)
            {
                isplayTwo = false;
            }
        }
    }

    private void Fov()
    {
        Collider2D[] rangeCheck = Physics2D.OverlapCircleAll(transform.position, radius, targetLayer);

        if (rangeCheck.Length > 0)
        {
            Transform target = rangeCheck[0].transform;
            Vector2 directionToTarget = (target.position - transform.position).normalized;

            if (Vector2.Angle(transform.right, directionToTarget) < angle / 2)
            {
                float distanceToTarget = Vector2.Distance(transform.position, target.position);

                if (!Physics2D.Raycast(transform.position, directionToTarget, distanceToTarget, obstructionLayer))
                {
                    CanSeePlayer = true;
                }
                else
                {
                    CanSeePlayer = false;
                }
            }
            else
            {
                CanSeePlayer = false;
            }
        }
    }

    private Vector2 DirectionFromAngle(float eulerY, float angleInDegrees)
    {
        angleInDegrees -= eulerY;
        return new Vector2(-Mathf.Cos(angleInDegrees * Mathf.Deg2Rad), -Mathf.Sin(angleInDegrees * Mathf.Deg2Rad));
    }
}
