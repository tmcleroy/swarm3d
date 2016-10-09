
public class UnitMoveDown : UnitController {

	// Use this for initialization
	void Start () {
		base.Start();
		unit = new Unit(new Hex(6, -4, -2), new Behavior[]{Behavior.moveDown});
	}
}
