using System;
public class UnitRandom : UnitController {

	// Use this for initialization
	new void Start () {
		base.Start();
		Behavior[] behaviors = new Behavior[]{
			Behavior.randomWander(),
			Behavior.engageNeighbors
		};
		unit = new Unit(sim, sim.grid.getRandomCell(), behaviors);
	}
}
