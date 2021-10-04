using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour {

    private Camera currentCamera;


    public Bead SelectBead() {


        if (!Input.GetMouseButtonDown(0)) return null;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit)) {
            Debug.Log(hit.transform.name);
            return hit.transform.GetComponent<Bead>();
        }

        return null;

    }



    void Awake() {
        currentCamera = Camera.main;
    }


}
