using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class S_ScreenEdges : MonoBehaviour
{
    [SerializeField]
    private List<Collider> _plateformesMouvantesList;

    private Collider _storeCollider;

    [SerializeField]
    private GameObject _player;

    [SerializeField]
    private RawImage _screenEdges;

    [SerializeField]
    private List<Texture> _screenEdgesImages;

    private void Start()
    {
        StartCoroutine(CheckPlayerDistanceToMovable());
        StartCoroutine(AnimateScreenEdges());
    }

    IEnumerator AnimateScreenEdges()
    {
        while (true)
        {
            for(int i = 0; i < _screenEdgesImages.Count; i++)
            {
                _screenEdges.texture = _screenEdgesImages[i];
                yield return new WaitForSeconds(0.15f);
                if (i == 2)
                {
                    i = 0;
                }
            }
            yield return new WaitForSeconds(0.15f);
        }
    }

    IEnumerator CheckPlayerDistanceToMovable()
    {
        while (true)
        {
            _storeCollider = _plateformesMouvantesList[0];
            for (int i = 1; i < _plateformesMouvantesList.Count; i++)
            {
                if (Vector3.Distance(_player.transform.position, _plateformesMouvantesList[i].transform.position) < Vector3.Distance(_player.transform.position, _storeCollider.transform.position))
                {
                    _storeCollider = _plateformesMouvantesList[i];
                }

            }

            _screenEdges.color = new Color(_screenEdges.color.r, _screenEdges.color.b, _screenEdges.color.g, (4 / (1.5f * Vector3.Distance(_player.transform.position, _storeCollider.transform.position)) * 5));
            if (Vector3.Distance(_player.transform.position, _storeCollider.transform.position) > 50.0f)
            {
                _screenEdges.color = new Color(_screenEdges.color.r, _screenEdges.color.b, _screenEdges.color.g, 0);
            }
            else if (Vector3.Distance(_player.transform.position, _storeCollider.transform.position) > 40.0f && Vector3.Distance(_player.transform.position, _storeCollider.transform.position) < 50.0f)
            {
                _screenEdges.color = Color.Lerp(_screenEdges.color, new Color(_screenEdges.color.r, _screenEdges.color.b, _screenEdges.color.g, (4 / (1.5f * Vector3.Distance(_player.transform.position, _storeCollider.transform.position)) * 5)), Time.deltaTime);
            }
            else
            {
                _screenEdges.color = new Color(_screenEdges.color.r, _screenEdges.color.b, _screenEdges.color.g, (4 / (1.5f * Vector3.Distance(_player.transform.position, _storeCollider.transform.position)) * 5));
            }
            /*if (_screenEdges.color.a < 0.05f)
            {
                _screenEdges.color = new Color(_screenEdges.color.r, _screenEdges.color.b, _screenEdges.color.g, 0.0f);
            }
            else if (_screenEdges.color.a >= 0.3f)
            {
                _screenEdges.color = new Color(_screenEdges.color.r, _screenEdges.color.b, _screenEdges.color.g, 0.5f);
            }*/
            yield return new WaitForSeconds(0.01f);
        }
    }
}
