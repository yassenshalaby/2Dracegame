using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    public GameObject spikePrefab;
    public GameObject shieldPrefab;
    public float spawnInterval = 1.5f;
    public float minInterval = 0.3f;
    public float difficultyRate = 0.08f;

    private float[] _laneX = { -2.5f, 0f, 2.5f };
    private float _spawnY = 10f;
    private float _timer;
    private float _gameTime;

    void Update()
    {
        _gameTime += Time.deltaTime;
        _timer -= Time.deltaTime;

        if (_timer <= 0f)
        {
            SpawnObjects();
            spawnInterval -= difficultyRate;
            if (spawnInterval < minInterval)
                spawnInterval = minInterval;
            _timer = spawnInterval;
        }
    }

    void SpawnObjects()
    {
        int lane = Random.Range(0, _laneX.Length);
        Vector3 spawnPos = new Vector3(_laneX[lane], _spawnY, 0f);
        GameObject spike = Instantiate(spikePrefab, spawnPos, Quaternion.identity);
        spike.GetComponent<Obstacle>().speed = 5f + (_gameTime * 0.05f);

        if (Random.Range(0, 10) < 3)
        {
            int shieldLane = (lane + 1) % _laneX.Length;
            Vector3 shieldPos = new Vector3(_laneX[shieldLane], _spawnY, 0f);
            Instantiate(shieldPrefab, shieldPos, Quaternion.identity);
        }
    }
}