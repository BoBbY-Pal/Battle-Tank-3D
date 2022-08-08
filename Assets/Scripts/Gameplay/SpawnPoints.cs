using UnityEngine;

public class SpawnPoints : MonoGenericSingleton<SpawnPoints>
{

    // Enemy tank spawn transforms.
    [SerializeField] private Transform[] quarter1 = new Transform[4];
    [SerializeField] private Transform[] quarter2 = new Transform[4];
    [SerializeField] private Transform[] quarter3 = new Transform[4];
    [SerializeField] private Transform[] quarter4 = new Transform[4];

    // Returns random spawn transform.
    public Transform GetRandomSpawnPoint()
    {
        int quarterNumber = Random.Range(1, 4);
        int transformNumber = Random.Range(0, 3);

        return quarterNumber switch
        {
            1 => quarter1[transformNumber],
            2 => quarter2[transformNumber],
            3 => quarter3[transformNumber],
            4 => quarter4[transformNumber],
            _ => quarter1[transformNumber]
        };
    }
}