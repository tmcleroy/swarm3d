﻿using System;
using System.Collections.Generic;

public class Behavior {

	private Conditions conditions;
    private Actions actions;

    public Behavior (Conditions conditions, Actions actions) {
        this.conditions = conditions;
        this.actions = actions;
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
    private static List<Func<Unit, bool>> upActions = new List<Func<Unit, bool>> {
        (Unit u) => {
        	u.move(Unit.north);
            return true;
        }
    };
    private static List<Func<Unit, bool>> downActions = new List<Func<Unit, bool>> {
        (Unit u) => {
            u.move(Unit.south);
            return true;
        }
    };


    public static Behavior moveUp = new Behavior(
    	new Conditions(truePredicates, Conditions.Evaluation.ALL),
        new Actions(upActions)
    );

    public static Behavior moveDown = new Behavior(
        new Conditions(truePredicates, Conditions.Evaluation.ALL),
        new Actions(downActions)
    );
}


public class Conditions {
    public enum Evaluation { ALL, ANY };

    public List<Func<Unit, bool>> items;
    public Evaluation evalutation;

    public Conditions (List<Func<Unit, bool>> items, Evaluation evaluation) {
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