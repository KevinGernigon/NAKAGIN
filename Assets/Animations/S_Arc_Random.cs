using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S_Arc_Random : MonoBehaviour
{
   //[SerializeField] private List<Animator> _arc_AC;

    [SerializeField] private Animator _arc_ACPoint1;
    [SerializeField] private Animator _arc_ACPoint2;


    private float _randDelay;
    private int _randChoice;
    private int max;
    private bool _animcanPlay;

    // Start is called before the first frame update
    private void Start()
    {
        //max = _arc_AC.Count;

        StartCoroutine(delayAnimsv2());
        _animcanPlay = false;
    }

    private void Update()
    {
        if (_animcanPlay)
        {
            StartCoroutine(delayAnimsv2());
            _animcanPlay = false;
        }

    }

    IEnumerator delayAnimsv2()
    {
            _randDelay = Random.Range(0, 5f);

            yield return new WaitForSeconds(_randDelay);

            _arc_ACPoint1.Rebind();
            _arc_ACPoint1.Play("A_Test_Arc_electrique_mouvement");
            _arc_ACPoint2.Rebind();
            _arc_ACPoint2.Play("A_Test_Arc_electrique_mouvement");

            yield return new WaitForSeconds(120f);

            _animcanPlay = true;

    }



   /* IEnumerator delayAnims()
    {
        for(int i = 0; i < max; i++)
        {
            _randChoice = Random.Range(0, _arc_AC.Count);
            _randDelay = Random.Range(0, 0.0025f);   
            _arc_AC[_randChoice].Play("A_Test_Arc_electrique_mouvement");
            _arc_AC.RemoveAt(_randChoice);
            yield return new WaitForSeconds(_randDelay);
        }
    }
   */



}
