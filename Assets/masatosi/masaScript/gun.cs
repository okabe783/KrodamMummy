using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR;

public class gun : MonoBehaviour
{
    bool _gunType = true;
    float _charge = 0;
    [SerializeField] GameObject _muzzle;
    [SerializeField] GameObject _shot;
    [SerializeField] GameObject _cShot;
    [SerializeField] LayerMask _gameObjectLayer;
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

        if (0 != Input.GetAxis("Mouse ScrollWheel"))
        {
            _gunType = !_gunType;
        }
        _charge += Time.deltaTime;
    }
    void shotDown()
    {
        if(_gunType)
        {
            Vector3 center = new Vector3(Screen.width / 2, Screen.height / 2, 0);
            Ray ray = Camera.main.ScreenPointToRay(center);
            Debug.DrawRay(ray.origin, ray.direction * 100);
            RaycastHit hit;
            Physics.Raycast(ray, out hit, 100, _gameObjectLayer);
            GameObject shot = Instantiate(_shot);
            shot.transform.position = _muzzle.transform.position;
            shot.GetComponent<shotTest>().Date(hit.point, 10f);

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
            Vector3 center = new Vector3(Screen.width / 2, Screen.height / 2, 0);
            Ray ray = Camera.main.ScreenPointToRay(center);
            Debug.DrawRay(ray.origin, ray.direction * 100);
            RaycastHit hit;
            Physics.Raycast(ray, out hit, 100, _gameObjectLayer);
            GameObject cShot = Instantiate(_cShot);
            cShot.transform.position = _muzzle.transform.position;
            cShot.GetComponent<shotTest>().Date(hit.point, _charge*10);
            _charge = 0;
        }
    }
}
