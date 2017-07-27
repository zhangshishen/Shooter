using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowBomb : MonoBehaviour {
    int state = 0;  //0 none ;1 banned; 2 OK ; 
    //float camRayLength;
    int floorMask;
    public static bool IsPick = true;
    public Transform player;
    public float radius;
    public GameObject Range;
    public GameObject bomb;
    private Transform pos;
    MeshRenderer mesh;
    // Use this for initialization
    Color bannedColor;
    Color OKColor;
	void Start () {
        mesh = Range.GetComponent<MeshRenderer>();
        pos = Range.GetComponent<Transform>();
        floorMask = LayerMask.GetMask("Floor");
        bannedColor = new Color(1, 0, 0, 0.1f);
        OKColor = new Color(0, 1, 0,0.1f);
	}
    void updateColor(Vector3 v){
        Vector3 v1 = v;
        v1.y = 0.1f;
        pos.transform.position =  v1;
        if(state == 2){
            mesh.material.color = OKColor;
        }else{
            mesh.material.color = bannedColor;
        }
    }
    Vector3 MousePoint(){
		Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

		// Create a RaycastHit variable to store information about what was hit by the ray.
		RaycastHit floorHit;

		// Perform the raycast and if it hits something on the floor layer...
		if (Physics.Raycast(camRay, out floorHit,1000, floorMask))
		{
            return floorHit.point;
			
		}
        return new Vector3(10000, 10000, 10000);
    }
	// Update is called once per frame
	void Update () {
        if (state == 0)
        {
            if (Input.GetKeyDown(KeyCode.F))
            {
                IsPick = false;
                state = 1;
            }

        }else {
			if (Input.GetMouseButtonDown(1))
			{
                IsPick = true;
                mesh.material.color = new Color(0, 0, 0, 0);
                state = 0;
                return;
			}
			Vector3 v = MousePoint();
			if (Vector3.Distance(v, player.position) > radius)
			{
				state = 1;
			}
			else state = 2;
            updateColor(v);
            if (Input.GetMouseButtonDown(0)){
                if (state != 2) return;
                IsPick = false;
                Bomb.dest = v;
                mesh.material.color = new Color(0, 0, 0, 0);
                Instantiate(bomb,player.position,player.rotation);
            }
        }

	}
}
