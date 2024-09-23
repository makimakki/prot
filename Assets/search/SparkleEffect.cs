using UnityEngine;

public class SparkleEffect : MonoBehaviour
{
    public ParticleSystem sparkleParticles;

    void Start()
    {
        // パーティクルシステムの設定を動的に調整
        var main = sparkleParticles.main;
        main.startLifetime = 1.5f;
        main.startSpeed = 0.7f;
        main.startSize = 0.08f;

        var emission = sparkleParticles.emission;
        emission.rateOverTime = 25;

        var shape = sparkleParticles.shape;
        shape.shapeType = ParticleSystemShapeType.Sphere;
        shape.radius = 0.3f;

        // エフェクトを開始
        sparkleParticles.Play();
    }

    public void StopSparkle()
    {
        // エフェクトを停止（徐々にフェードアウト）
        var emission = sparkleParticles.emission;
        emission.enabled = false;
    }
}