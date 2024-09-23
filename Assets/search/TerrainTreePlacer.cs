using UnityEngine;

public class  TerrainTreePlacer : MonoBehaviour
{
    public Terrain terrain;
    [SerializeField] private GameObject objectToPlace;
    [SerializeField] private GameObject objectToPlace2;

    void Start()
    {
        ClearTrees();
        PlaceRandomTrees();
        PlaceObjects(objectToPlace, 5);
        //PlaceObjects(objectToPlace2, 10);
    }
    
    void ClearTrees()
    {
        terrain.terrainData.treeInstances = new TreeInstance[0];
        terrain.Flush();
    }
    void PlaceRandomTrees()
    {
        PlaceRandomItem(0, 2, 30);
        PlaceRandomItem(1, 20, 50);
        PlaceRandomItem(2, 2, 30);
    }

    private void PlaceRandomItem(int typeIndex, int low, int high)
    {
        int numberOfTrees = Random.Range(low, high);
        for (int i = 0; i < numberOfTrees; i++)
        {
            // ランダムな位置を生成
            float randomX = Random.Range(0f, 1f);
            float randomZ = Random.Range(0f, 1f);
            float height = terrain.SampleHeight(new Vector3(randomX * terrain.terrainData.size.x, 0, randomZ * terrain.terrainData.size.z));

            // TreeInstanceを作成
            TreeInstance treeInstance = new TreeInstance
            {
                position = new Vector3(randomX, height / terrain.terrainData.size.y, randomZ),
                //widthScale = Random.Range(0.4f, 1f),
                //heightScale = Random.Range(0.4f, 1f),
                widthScale = 1.0f,
                heightScale = 1.0f,
                color = Color.white,
                lightmapColor = Color.white,
                prototypeIndex = typeIndex
            };

            // TreeInstanceを追加
            terrain.AddTreeInstance(treeInstance);
        }

        // Terrainのデータを更新
        terrain.Flush();
    }


    void PlaceObjects(GameObject gameObject, int placeCount)
    {
        for (int i = 0; i < placeCount; i++)
        {
            Vector3 randomPosition = GetRandomPositionOnTerrain();
            Instantiate(gameObject, randomPosition, Quaternion.identity);
        }
    }

    Vector3 GetRandomPositionOnTerrain()
    {
        TerrainData terrainData = terrain.terrainData;
        float x = Random.Range(0f, terrainData.size.x);
        float z = Random.Range(0f, terrainData.size.z);
        float y = terrain.SampleHeight(new Vector3(x, 0, z));

        return new Vector3(x, y, z);
    }
}