using System;
using System.Collections.Generic;

public class Behavior {

	private Conditions conditions;
    private Actions actions;
    private static Random random;
    private static int moveCount;
    private static Hex wanderDirection;
    private static Hex[] directions = new Hex[6]{Unit.north, Unit.northEast, Unit.northWest, Unit.south, Unit.southEast, Unit.southWest};

    public Behavior (Conditions conditions, Actions actions) {
        this.conditions = conditions;
        this.actions = actions;
        random = new Random();
        moveCount = 0;
        wanderDirection = directions[random.Next(0, directions.Length)];
    }

    public bool behave (Unit u) {
        bool result = this.conditions.result(u);
        if (result) {
            this.actions.perform(u);
        }
        return result;
    }


    private static List<Func<Unit, bool>> truePredicates = new List<Func<Unit, bool>> {
        (Unit u) => true
    };
    private static List<Func<Unit, bool>> neighboringUnitPredicates = new List<Func<Unit, bool>> {
        (Unit u) => u.neighboringUnits.Count > 0
    };
    private static List<Func<Unit, bool>> stillActions = new List<Func<Unit, bool>> { (Unit u) => true };
    private static List<Func<Unit, bool>> upActions = new List<Func<Unit, bool>> {
        (Unit u) => {
        	u.move(Unit.north);
            return true;
        }
    };
    private static List<Func<Unit, bool>> wanderActions = new List<Func<Unit, bool>> {
        (Unit u) => {
            wanderDirection = (moveCount++ % 4 == 0) ? directions[random.Next(0, directions.Length)] : wanderDirection;
        	u.move(wanderDirection);
            return true;
        }
    };
    private static List<Func<Unit, bool>> downActions = new List<Func<Unit, bool>> {
        (Unit u) => {
            u.move(Unit.south);
            return true;
        }
    };
    private static List<Func<Unit, bool>> engageNeighboringUnitActions = new List<Func<Unit, bool>> {
        (Unit u) => {
            u.engageNeighboringUnit();
            return true;
        }
    };

    public static Behavior standStill = new Behavior(
    	new Conditions(truePredicates),
        new Actions(stillActions)
    );
    public static Behavior moveUp = new Behavior(
    	new Conditions(truePredicates),
        new Actions(upActions)
    );
    public static Behavior moveDown = new Behavior(
        new Conditions(truePredicates),
        new Actions(downActions)
    );
    public static Behavior randomWander = new Behavior(
        new Conditions(truePredicates),
        new Actions(wanderActions)
    );
    public static Behavior engageNeighbors = new Behavior(
        new Conditions(neighboringUnitPredicates),
        new Actions(engageNeighboringUnitActions)
    );
}


public class Conditions {
    public enum Evaluation { ALL, ANY };

    public List<Func<Unit, bool>> items;
    public Evaluation evalutation;

    public Conditions (List<Func<Unit, bool>> items, Evaluation evaluation = Evaluation.ALL) {
        this.items = items;
        this.evalutation = evaluation;
    }

    public bool result (Unit u) {
        bool reduced = true;
        foreach (Func<Unit, bool> func in this.items) {
            bool pass = func(u);
            if (pass && this.evalutation == Evaluation.ANY) {
                reduced = true;
                break;
            } else if (this.evalutation == Evaluation.ALL) {
                if (pass) {
                    reduced = reduced && pass;
                } else {
                    reduced = false;
                    break;
                }
            }
        }
        return reduced;
    }
}

public class Actions {
    public List<Func<Unit, bool>> items;

    public Actions (List<Func<Unit, bool>> items) {
        this.items = items;
    }

    public void perform (Unit u) {
		foreach (Func<Unit, bool> func in this.items) {
			func(u);
		}
    }
}