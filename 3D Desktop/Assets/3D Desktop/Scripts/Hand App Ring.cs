using UnityEngine;

public class HandAppRing : MonoBehaviour
{
    [SerializeField] GameObject appInventory;

    [SerializeField] Transform anchor;

    [SerializeField] float radius = 0.15f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // for (int i = 0; i < appInventory.transform.childCount; i++)
        // {
        //     Transform app = appInventory.transform.GetChild(i);

        //     Vector3 target = 
        //         transform.position + 
        //         transform.right * (i * appDistance + 0.02f);

        //     app.transform.position = target;
        //     app.transform.rotation = transform.rotation;
        //     app.transform.localScale = Vector3.one * 0.1f;
        // }

        float angleStep = 360f / appInventory.transform.childCount;

        for(int i = 0; i < appInventory.transform.childCount; i++)
        {
            float angle = angleStep * i * Mathf.Deg2Rad;

            Vector3 offset = new Vector3(
                Mathf.Cos(angle) * radius,
                Mathf.Sin(angle) * radius,
                0f
            );

            appInventory.transform.GetChild(i).localPosition = offset;
            appInventory.transform.GetChild(i).localScale = Vector3.one * 0.1f;
            appInventory.transform.GetChild(i).transform.rotation = transform.rotation;
        }
    }
}
