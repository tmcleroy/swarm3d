using UnityEngine;

public class UnitController : MonoBehaviour {

	public Simulation sim;
	public Unit unit;
	private int tick;

	public void Start () {
		sim = GridController.sim;
		tick = 0;
	}
	
	void Update () {
		if (sim.tick != tick) {
			setState();
			positionUnit();
		}
		tick = sim.tick;
	}

	void setState () {
		if (unit.status == Unit.Status.dead) {
			Destroy(this.gameObject);
		}
	}

	void positionUnit () {
		Vector2 origin = Layout.hexToPixel(GridController.layout, unit.Position);
		this.gameObject.transform.position = new Vector3(origin.x, 5, origin.y);
	}
}
