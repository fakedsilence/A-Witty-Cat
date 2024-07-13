using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shoot : MonoBehaviour
{
    public Transform shootPosition;
    public GameObject Arrow;
    public float DestroyTime = 1f;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(CreateCO());
    }

    // Update is called once per frame
    void Update()
    {

    }

    private IEnumerator CreateCO()
    {
        WaitForSeconds wait = new WaitForSeconds(2f);
        while (true)
        {
            yield return wait;
            GameObject NewArrow = Instantiate(Arrow, shootPosition.position, shootPosition.rotation);
            yield return new WaitForSeconds(DestroyTime);
            Destroy(NewArrow);
        }
    }
}
