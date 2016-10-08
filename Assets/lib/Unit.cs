
public class Unit {
	public Hex position;
    public Behavior behavior;

    public static Hex northEast = Hex.directions[0];
    public static Hex southEast = Hex.directions[1];
    public static Hex south = Hex.directions[2];
    public static Hex southWest = Hex.directions[3];
    public static Hex northWest = Hex.directions[4];
    public static Hex north = Hex.directions[5];

    public Unit (Hex position, Behavior behavior) {
        this.position = position;
        this.behavior = behavior;
    }

    public void behave () {
        this.behavior.behave(this);
    }

    public void move (Hex direction) {
        this.position = Hex.add(this.position, direction);
    }

    public Hex[] getNeighbors () {
        return Hex.neighbors(this.position);
    }
}
