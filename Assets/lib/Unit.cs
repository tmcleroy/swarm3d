using System;
using System.Collections.Generic;
using UnityEngine;

public class Unit {
    public enum Status { alive, dead };
    private Hex position;
    public Grid grid;
    public Behavior[] behaviors;
    public int health;
    public int damage;
    public Status status = Status.alive;
    public List<Unit> neighboringUnits = new List<Unit>();

    public Unit (Hex position, Grid grid, Behavior[] behaviors, int health = 100, int damage = 20) {
        this.grid = grid;
        this.Position = position;
        this.behaviors = behaviors;
        this.health = health;
        this.damage = damage;
    }

    public String toString () {
        return this.status + ": " + this.health;
    }

    public Hex Position {
        get {
            return position;
        }
        set {
            grid.moveUnit(this, position, value);
            position = value;
        }
    }

    public void Update () {
        if (status == Status.alive) {
            live();
            setState();
        } else {
            die();
        }
    }

    private void setState () {        
        neighboringUnits = grid.getUnits(getNeighboringCells());
        status = health > 0 ? Status.alive : Status.dead;
    }

    public void live () {
        foreach (Behavior b in behaviors) {
            b.behave(this);
        }
    }

    public void die () {
        this.Position = Hex.graveYard;
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
            grid.getCell(h);
        } catch (KeyNotFoundException) {
            return false;
        }

        try { // another unit occupies cell?
            grid.getUnit(h);
            return false;
        } catch (KeyNotFoundException) {
            return true;
        }
    }



    public static Hex northEast = Hex.directions[0];
    public static Hex southEast = Hex.directions[1];
    public static Hex south = Hex.directions[2];
    public static Hex southWest = Hex.directions[3];
    public static Hex northWest = Hex.directions[4];
    public static Hex north = Hex.directions[5];
}
