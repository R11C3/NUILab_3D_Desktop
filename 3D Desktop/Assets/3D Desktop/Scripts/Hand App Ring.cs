using UnityEngine;

public class HandAppRing : MonoBehaviour
{
    [SerializeField] GameObject[] apps;

    [SerializeField] Transform anchor;

    [SerializeField] float appDistance = 0.25f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = anchor.position;

        float diff = 0;

        for (int i = 0; i < apps.Length; i++)
        {
            Vector3 pos = new Vector3
            (
                anchor.localPosition.x + diff,
                0.5f,
                0f
            );

            apps[i].transform.position = pos;

            diff += appDistance;
        }
    }
}
