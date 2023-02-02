using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_ProceduralBackground : MonoBehaviour
{
    [SerializeField]
    private GameObject _tile;

    private bool _mapGenerated;

    [SerializeField]
    private List<Transform> _centerPos;

    private Transform targetTransform;
    private Vector3 target;
    private Vector3 target2;

    [System.Serializable]
    public class _Rings
    {
        public List<GameObject> _spawnedTiles = new List<GameObject>();
    }

    public List<_Rings> _spawnedRings;

    private float _randomScale;

    void Start()
    {
        StartCoroutine(GenerateMap(45, 100));
    }

    IEnumerator GenerateMap(int num, float radius)
    {
        while (!_mapGenerated)
        {
            for (int y = 0; y < _centerPos.Count; y++)
            {
                var ring = new _Rings();
                radius -= 5 * y;
                num -= 2 * y;
                for (int i = 0; i < num; i++)
                {
                    _randomScale = Random.Range(10.0f, 22.0f);
                    _tile.transform.localScale = new Vector3(15, _randomScale, 15);

                    var radians = 2 * Mathf.PI / num * i;

                    var vertical = Mathf.Sin(radians);
                    var horizontal = Mathf.Cos(radians);

                    var spawnDir = new Vector3(horizontal, vertical, 0.0f);

                    var spawnPos = _centerPos[y].position + spawnDir * radius;
                    var spawnPos2 = -_centerPos[y].position - spawnDir * radius;
                    //spawnPos.y = _centerPos[y].position.y;

                    
                    //_spawnedTiles[y].Add(ring);

                    //var block = Instantiate(_tile, spawnPos, Quaternion.Euler(90.0f, 180.0f, 90 + i * 10));
                    
                    var block = Instantiate(_tile, spawnPos, Quaternion.LookRotation(_centerPos[y].transform.position));
                    var block2 = Instantiate(_tile, spawnPos2, Quaternion.LookRotation(_centerPos[y].transform.position));

                    //block.transform.LookAt(_centerPos[y].transform);

                    
                    
                    target.y = _centerPos[y].position.y;

                    //targetTransform.position = target;
                    block.transform.LookAt(target);
                    block.transform.Rotate(block.transform.rotation.x +90.0f, block.transform.rotation.y, block.transform.rotation.z);                    
                    block2.transform.LookAt(target);
                    block2.transform.Rotate(block2.transform.rotation.x +90.0f, block2.transform.rotation.y, block2.transform.rotation.z);

                    block.GetComponent<Transform>().SetParent(_centerPos[y]);
                    block2.GetComponent<Transform>().SetParent(_centerPos[y]);

                    ring._spawnedTiles.Add(block);
                    ring._spawnedTiles.Add(block2);

                    if (i >= num - 1)
                    {
                        _mapGenerated = true;
                    }
                    yield return new WaitForSeconds(0.02f);
                }
                //_centerPos[y].transform.Rotate(_centerPos[y].eulerAngles.x, _centerPos[y].eulerAngles.y, _centerPos[y].eulerAngles.z);
                //ring._spawnedTiles[y].transform.eulerAngles.Set(_centerPos[y].eulerAngles.x, _centerPos[y].eulerAngles.y, _centerPos[y].eulerAngles.z);
                _spawnedRings.Add(ring);
            }
                //yield return new WaitForSeconds(0.01f);
            yield return new WaitForEndOfFrame();
        }
    }
}
