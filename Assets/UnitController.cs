using UnityEngine;

public class UnitController : MonoBehaviour {

	public GameObject unitObj;
	public Unit unit;
	// private GameObject[] neighborObjs = new GameObject[6];
	private int frame;

	// Use this for initialization
	public void Start () {
		unitObj = (GameObject)Instantiate(Resources.Load("Unit"));
		// for (int i = 0; i < neighborObjs.Length; i++) {
		// 	neighborObjs[i] = (GameObject)Instantiate(Resources.Load("NeighborMarker"));
		// }
	}
	
	// Update is called once per frame
	void Update () {
		if (frame++ % 60 == 0) {
			unit.Update();
			setState();
			positionUnit();
			// positionNeighbors();
		}
	}

	void setState () {
		if (unit.status == Unit.Status.dead) {
			Destroy(this.gameObject);
			Destroy(unitObj);
		}
	}

	void positionUnit () {
		Vector2 origin = Layout.hexToPixel(GridController.layout, unit.Position);
		unitObj.transform.position = new Vector3(origin.x, 5, origin.y);
	}

	// void positionNeighbors () {
	// 	Hex[] neighborHexes = unit.getNeighboringCells();
	// 	for (int i = 0; i < neighborObjs.Length; i++) {
	// 		Vector2 neighborOrigin = Layout.hexToPixel(GridController.layout, neighborHexes[i]);
	// 		neighborObjs[i].transform.position = new Vector3(neighborOrigin.x, 5, neighborOrigin.y);
	// 	}
	// }
}
