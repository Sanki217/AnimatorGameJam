using UnityEngine;

public class FoliageScaler : MonoBehaviour
{
    public Transform player; // Assign the player transform
    public float minDistance = 3f;
    public float maxDistance = 10f;

    private GameObject[] foliageObjects;

    void Start()
    {
        foliageObjects = GameObject.FindGameObjectsWithTag("Foliage");
    }

    void Update()
    {
        foreach (GameObject obj in foliageObjects)
        {
            if (obj == null) continue;

            float distance = Vector3.Distance(player.position, obj.transform.position);

            float t = Mathf.InverseLerp(maxDistance, minDistance, distance); // 0 when far, 1 when close
            float scale = Mathf.Clamp01(t); // Ensure between 0 and 1

            obj.transform.localScale = Vector3.one * scale;
        }
    }
}
