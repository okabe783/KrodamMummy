using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gun : MonoBehaviour
{
    bool _gunType = true;
    float _charge = 0;
    [SerializeField] GameObject _muzzle;
    [SerializeField] GameObject _cShot;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            shotDown();
        }
        if (Input.GetButtonUp("Fire1"))
        {
            shotUp();
        }
        _charge += Time.deltaTime;
    }
    void shotDown()
    {
        if(_gunType)
        {
            
        }//ハンドガン
        else if (_gunType) 
        {
            _charge = 0;
        }//チャージショット
    }

    void shotUp()
    {
        if(!_gunType)
        {
            GameObject cShot = Instantiate(_cShot);
            cShot.transform.position = _muzzle.transform.position;
            _charge = 0;
            
        }
    }
}
