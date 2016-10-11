
public class Unit1 : UnitController {

	// Use this for initialization
	new void Start () {
		base.Start();
		Behavior[] behaviors = new Behavior[]{
			Behavior.randomWander,
			Behavior.engageNeighbors
		};
		unit = new Unit(new Hex(6, -4, -2), GridController.grid, behaviors);
	}
}
