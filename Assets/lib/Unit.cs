public class Unit {
    public enum Status { alive, dead };
	public Hex position;
    public Behavior[] behaviors;
    public int health;
    public int damage;
    public Status status;
    public Unit[] neighboringUnits;

    public Unit (Hex position, Behavior[] behaviors, int health = 100, int damage = 20) {
        this.position = position;
        this.behaviors = behaviors;
        this.health = health;
        this.damage = damage;
    }

    public void Update () {
        this.setState();
        if (status == Status.alive) {
            this.live();
        } else {
            this.die();
        }
    }

    public void setState () {
        // set neighboringUnits
        status = health > 0 ? Status.alive : Status.dead;
    }

    public void live () {
        foreach (Behavior b in behaviors) {
            b.behave(this);
        }
    }

    public void die () {

    }

    public void move (Hex direction) {
        position = Hex.add(position, direction);
    }

    public void engageNeighboringUnit () {
        // pick a neighboring unit and engage it
    }

    public Hex[] getNeighbors () {
        return Hex.neighbors(this.position);
    }


    public static Hex northEast = Hex.directions[0];
    public static Hex southEast = Hex.directions[1];
    public static Hex south = Hex.directions[2];
    public static Hex southWest = Hex.directions[3];
    public static Hex northWest = Hex.directions[4];
    public static Hex north = Hex.directions[5];
}
