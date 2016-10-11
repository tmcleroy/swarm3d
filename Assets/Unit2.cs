
public class Unit2 : UnitController {

	// Use this for initialization
	new void Start () {
		base.Start();
		Behavior[] behaviors = new Behavior[]{
			Behavior.randomWander,
			Behavior.engageNeighbors
		};
		unit = new Unit(new Hex(6, -12, 6), GridController.grid, behaviors, 200);
	}
}
