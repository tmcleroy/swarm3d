
public class UnitMoveUp : UnitController {

	// Use this for initialization
	void Start () {
		base.Start();
		unit = new Unit(new Hex(6, -12, 6), Behavior.moveUp);
	}
}
