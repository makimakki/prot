using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchDirector : MonoBehaviour
{
    [SerializeField] private Canvas canvaMap;
    [SerializeField] private Canvas canvaItem;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Terrain terrain;
    [SerializeField] private GameObject goHero;
    public Animator PlayerAnimator;
    [SerializeField] private PlayerController playerCtl;
    [SerializeField] public GameObject prefabTakarabako; // 生成するプレハブ
    private float moveDuration = 0.4f; // 宝箱が飛び上がるのにかかる時間
    private float additionalHeight = 0.5f; // さらに高く飛び上がるための高さ

    public void ClickBag()
    {
        canvaItem.gameObject.SetActive(true);
        canvaMap.gameObject.SetActive(false);
    }

    public void CloseBag()
    {
        canvaItem.gameObject.SetActive(false);
        canvaMap.gameObject.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        //TerrainData terrainData = gameObject.GetComponent<Terrain>().terrainData; // テレインデータを取得
        // テクスチャをリセット
        //var alphamaps = terrainData.GetAlphamaps(0, 0, terrainData.alphamapResolution, terrainData.alphamapResolution);
        //for (int x = 0; x < alphamaps.GetLength(0); x++)
        //{
        //    for (int y = 0; y < alphamaps.GetLength(1); y++)
        //    {
        //        alphamaps[x, y, 0] = 1f;
        //        alphamaps[x, y, 1] = 0f;
        //    }
        //}
        //terrainData.SetAlphamaps(0, 0, alphamaps);
        // 高さを中間にリセット
        //var heights = terrainData.GetHeights(0, 0, terrainData.heightmapResolution, terrainData.heightmapResolution);
        //for (int x = 0; x < heights.GetLength(0); x++)
        //{
        //    for (int y = 0; y < heights.GetLength(1); y++)
        //    {
        //        heights[x, y] = 100f;
        //    }
        //}
        //terrainData.SetHeights(0, 0, heights);
    }

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            //var ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            //RaycastHit hit;
            //if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            //{
            //    TerrainData terrainData = hit.collider.gameObject.GetComponent<Terrain>().terrainData; // テレインデータを取得

            //    int hx = Mathf.FloorToInt(hit.textureCoord.x * terrainData.heightmapResolution); // ハイトマップのX座標
            //    int hy = Mathf.FloorToInt(hit.textureCoord.y * terrainData.heightmapResolution); // ハイトマップのY座標
            //    Debug.Log(hx + ":" + hy);
            //    float[,] heights = terrainData.GetHeights(hx, hy, 1, 1); // クリック箇所のヘイトマップを取得[1x1]
            //    //heights[0, 0] = 0.1625f; // 高さを0にする 0.15はマイナス 0.2は山、0.18も山、0.16はマイナス、0.17は山、
            //    heights[0, 0] = 0.163f;
            //    terrainData.SetHeightsDelayLOD(hx, hy, heights); // ハイトマップに反映
            //}


        }
    }

    public void ClickShovel()
    {
        //    TerrainData terrainData = terrain.terrainData;
        //    Vector3 terrainPosition = terrain.transform.position;

        //    // キャラクターのワールド座標をテレインのローカル座標に変換
        //    float relativeX = goHero.transform.position.x - terrainPosition.x;
        //    float relativeZ = goHero.transform.position.z - terrainPosition.z;

        //    // ローカル座標をハイトマップ座標に変換
        //    int mapX = Mathf.FloorToInt((relativeX / terrainData.size.x) * terrainData.heightmapResolution);
        //    int mapZ = Mathf.FloorToInt((relativeZ / terrainData.size.z) * terrainData.heightmapResolution);

        //    // 半径0.1mをハイトマップの座標に変換
        //    int radius = Mathf.CeilToInt((0.5f / terrainData.size.x) * terrainData.heightmapResolution);


        //    // ハイトマップの高さを取得
        //    int startX = Mathf.Clamp(mapX - radius, 0, terrainData.heightmapResolution - 1);
        //    int startZ = Mathf.Clamp(mapZ - radius, 0, terrainData.heightmapResolution - 1);
        //    int width = Mathf.Clamp(mapX + radius, 0, terrainData.heightmapResolution) - startX;
        //    int height = Mathf.Clamp(mapZ + radius, 0, terrainData.heightmapResolution) - startZ;
        //    float[,] heights = terrainData.GetHeights(startX, startZ, width, height);

        //    //float[,] heights = terrainData.GetHeights(mapX, mapZ, 1, 1);
        //    //float height = heights[0, 0];
        //    // heightsの全要素に0.1fを設定
        //    for (int x = 0; x < heights.GetLength(0); x++)
        //    {
        //        for (int y = 0; y < heights.GetLength(1); y++)
        //        {
        //            // 中心からの距離を計算
        //            int dx = x - radius;
        //            int dy = y - radius;
        //            float distance = Mathf.Sqrt(dx * dx + dy * dy);

        //            // 距離が半径以内の場合にのみ高さを設定
        //            if (distance <= radius)
        //            {
        //                heights[x, y] = 0.163f;
        //            }
        //        }
        //    }

        //    //heights[0, 0] = 0.163f;


        //    terrainData.SetHeightsDelayLOD(mapX, mapZ, heights); // ハイトマップに反映

        //FlattenAll();
        playerCtl.StartJump();


    }

    public void AnaHoru(Transform targetTransForm)
    {

        TerrainData terrainData = terrain.terrainData;
        Vector3 terrainPosition = terrain.transform.position;

        // キャラクターのワールド座標をテレインのローカル座標に変換
        float relativeX = targetTransForm.position.x - terrainPosition.x;
        float relativeZ = targetTransForm.position.z - terrainPosition.z;

        // ローカル座標をハイトマップ座標に変換
        int mapX = Mathf.FloorToInt((relativeX / terrainData.size.x) * terrainData.heightmapResolution);
        int mapZ = Mathf.FloorToInt((relativeZ / terrainData.size.z) * terrainData.heightmapResolution);

        // 半径0.1mをハイトマップの座標に変換
        int radius = Mathf.CeilToInt((0.8f / terrainData.size.x) * terrainData.heightmapResolution);

        // ハイトマップの高さを取得
        int startX = Mathf.Clamp(mapX - radius, 0, terrainData.heightmapResolution - 1);
        int startZ = Mathf.Clamp(mapZ - radius, 0, terrainData.heightmapResolution - 1);
        int width = Mathf.Clamp(mapX + radius, 0, terrainData.heightmapResolution) - startX;
        int height = Mathf.Clamp(mapZ + radius, 0, terrainData.heightmapResolution) - startZ;
        float[,] heights = terrainData.GetHeights(startX, startZ, width, height);

        // heightsの全要素に0.1fを設定 (円形のマスクを適用)
        for (int x = 0; x < heights.GetLength(0); x++)
        {
            for (int y = 0; y < heights.GetLength(1); y++)
            {
                // 中心からの距離を計算 (テレインの座標系で)
                float distance = Mathf.Sqrt(Mathf.Pow((x + startX) - mapX, 2) + Mathf.Pow((y + startZ) - mapZ, 2));

                // 距離が半径以内の場合にのみ高さを設定
                if (distance <= radius)
                {
                    // 今の地面の高さ - 掘りたい高さ(m)　/ 全体の高さ
                    heights[x, y] = (float)(heights[x, y]*250f - 0.6) / 250f;//0.1638f;
                }
            }
        }

        // 更新されたハイトマップデータをテレインに反映
        terrainData.SetHeights(startX, startZ, heights);

        Vector3 spawnPosition = targetTransForm.position;
        spawnPosition.y -= 4f;
        GameObject takarabako = Instantiate(prefabTakarabako, spawnPosition, Quaternion.identity);

        Vector3 targetPosition = targetTransForm.position;
        targetPosition.y += additionalHeight; // 目標位置を高く設定

        StartCoroutine(MoveUp(takarabako.transform, targetPosition, moveDuration));

        //PlayerAnimator.SetBool("isJump", false);

    }


    IEnumerator MoveUp(Transform objTransform, Vector3 targetPosition, float duration)
    {
        float elapsedTime = 0.0f;
        Vector3 startingPos = objTransform.position;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            t = EaseOutExpo(t);
            objTransform.position = Vector3.Lerp(startingPos, targetPosition, t);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        objTransform.position = targetPosition;
    }

    // EaseOutExpo 関数
    float EaseOutExpo(float t)
    {
        return t == 1 ? 1 : 1 - Mathf.Pow(2, -30 * t);
    }

    // EaseOutQuad 関数
    float EaseOutQuad(float t)
    {
        return t * (2 - t);
    }

    public void FlattenAll()
    {
        // 現在のテレインを取得
        Terrain terrain = Terrain.activeTerrain;
        if (terrain == null)
        {
            Debug.LogError("No active terrain found");
            return;
        }

        TerrainData terrainData = terrain.terrainData;

        // テレインの解像度を取得
        int heightmapResolution = terrainData.heightmapResolution;

        // 新しい高さの2次元配列を作成し、すべての要素をフラットに設定
        float[,] heights = new float[heightmapResolution, heightmapResolution];

        // すべての高さを0に設定 (0でフラットにする場合)
        for (int x = 0; x < heightmapResolution; x++)
        {
            for (int y = 0; y < heightmapResolution; y++)
            {
                heights[x, y] = 100/250f; // フラットにする高さの値を設定
            }
        }

        // 更新された高さデータをテレインに適用
        terrainData.SetHeights(0, 0, heights);
    }
}
//int ax = Mathf.FloorToInt(hit.textureCoord.x * terrainData.alphamapResolution); // アルファマップのX座標
//int ay = Mathf.FloorToInt(hit.textureCoord.y * terrainData.alphamapResolution); // アルファマップのY座標
//var alphamaps = terrainData.GetAlphamaps(ax, ay, 1, 1); // クリック箇所のアルファマップを取得[1x1]
//alphamaps[0, 0, 0] = 0; // 1個目のテクスチャを無効
//alphamaps[0, 0, 1] = 1; // 2個目のテクスチャを有効
//terrainData.SetAlphamaps(ax, ay, alphamaps); // アルファマップに反映


