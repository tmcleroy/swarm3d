using System;
using System.Collections.Generic;
using UnityEngine;

public class Unit {
    
    public Simulation sim;
    public enum Status { alive, dead };
    private Hex position;
    public Behavior[] behaviors;
    public int health;
    public int damage;
    public Status status = Status.alive;
    public List<Unit> neighboringUnits = new List<Unit>();

    public Unit (Simulation sim, Hex position, Behavior[] behaviors, int health = 60, int damage = 20) {
        this.sim = sim;
        this.Position = position;
        this.behaviors = behaviors;
        this.health = health;
        this.damage = damage;
        this.sim.addUnit(this);
    }

    public String toString () {
        return this.status + ": " + this.health;
    }

    public Hex Position {
        get {
            return position;
        }
        set {
            sim.grid.moveUnit(this, position, value);
            position = value;
        }
    }

    public void update () {
        if (isAlive()) {
            live();
            setState();
        }
    }

    private void setState () {        
        neighboringUnits = sim.grid.getUnits(getNeighboringCells());
        status = health > 0 ? Status.alive : Status.dead;
        if (status == Unit.Status.dead) {
            die();
        }
    }

    public void live () {
        foreach (Behavior b in behaviors) {
            b.behave(this);
        }
    }

    public void die () {
        Debug.Log("X_X");
        Position = Hex.graveYard;
        sim.removeUnit(this);
    }

    public void move (Hex direction) {
        Hex potentialPosition = Hex.add(Position, direction);
        if (canMoveTo(potentialPosition)) {
            Position = Hex.add(Position, direction);
        }
    }

    public void engageNeighboringUnit () {
        Debug.Log("engaging a unit with my health at : " + health);
        neighboringUnits[0].health -= damage;
    }

    public Hex[] getNeighboringCells () {
        return Hex.neighbors(this.Position);
    }

    public bool canMoveTo (Hex h) {
        try { // cell exists?
            sim.grid.getCell(h);
        } catch (KeyNotFoundException) {
            return false;
        }

        try { // another unit occupies cell?
            sim.grid.getUnit(h);
            return false;
        } catch (KeyNotFoundException) {
            return true;
        }
    }

    public bool isAlive () {
        return status == Unit.Status.alive;
    }



    public static Hex northEast = Hex.directions[0];
    public static Hex southEast = Hex.directions[1];
    public static Hex south = Hex.directions[2];
    public static Hex southWest = Hex.directions[3];
    public static Hex northWest = Hex.directions[4];
    public static Hex north = Hex.directions[5];
}
