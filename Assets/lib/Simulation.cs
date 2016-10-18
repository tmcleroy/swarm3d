using System.Collections.Generic;
using System.Timers;

public class Simulation {

	public Grid grid;
	public List<Unit> units;
	public int tick;
	private Timer timer;
	private static int interval = 200;

	public Simulation (Grid grid) {
		this.grid = grid;
		this.tick = 0;
		this.units = new List<Unit>();

		this.timer = new Timer();
		timer.Elapsed += new ElapsedEventHandler(onTick);
		timer.Interval = interval;
		timer.Enabled = true;
	}

	private void onTick(object source, ElapsedEventArgs e) {
     	tick++;
		units.ForEach(u => u.update());
 	}

	public void addUnit (Unit unit) {
		units.Add(unit);
	}

	public void removeUnit (Unit unit) {
		units.Remove(unit);
	}

}
