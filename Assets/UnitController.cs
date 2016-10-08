using UnityEngine;
using System.Collections;

public class UnitController : MonoBehaviour {

	public GameObject unitObj;
	private Unit unit;
	private int frame;

	// Use this for initialization
	void Start () {
		unitObj = (GameObject)Instantiate(Resources.Load("Unit"));
		unit = new Unit(new Hex(6, -4, -2), Behavior.moveDown);
	}
	
	// Update is called once per frame
	void Update () {
		if (frame++ % 60 == 0) {
			unit.behave();
			Vector2 origin = Layout.hexToPixel(GridController.layout, unit.position);
			unitObj.transform.position = new Vector3(origin.x, 5, origin.y);
		}
		
	}
}
