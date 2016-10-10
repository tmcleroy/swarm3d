
public class UnitMoveUp : UnitController {

	// Use this for initialization
	new void Start () {
		base.Start();
		unit = new Unit(new Hex(6, -12, 6), GridController.grid, new Behavior[]{Behavior.moveUp});
	}
}
