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
    public Status status;
    public List<Unit> neighboringUnits;

    public Unit (Hex position, Grid grid, Behavior[] behaviors, int health = 100, int damage = 20) {
        this.grid = grid;
        this.Position = position;
        this.behaviors = behaviors;
        this.health = health;
        this.damage = damage;
    }

    public Hex Position {
        get {
            return position;
        }
        set {
            position = value;
            try {
                grid.unitMap[position.getHash()] = this;
            } catch (NullReferenceException) {}
            
        }
    }

    public void Update () {
        setState();
        if (status == Status.alive) {
            live();
        } else {
            die();
        }
    }

    private void setState () {
        // set neighboringUnits
        
        neighboringUnits = grid.getUnits(getNeighboringCells());
        Debug.Log(neighboringUnits); 
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
        Position = Hex.add(Position, direction);
    }

    public void engageNeighboringUnit () {
        // pick a neighboring unit and engage it
        Console.WriteLine("engaging a unit");
    }

    public Hex[] getNeighboringCells () {
        return Hex.neighbors(this.Position);
    }


    public static Hex northEast = Hex.directions[0];
    public static Hex southEast = Hex.directions[1];
    public static Hex south = Hex.directions[2];
    public static Hex southWest = Hex.directions[3];
    public static Hex northWest = Hex.directions[4];
    public static Hex north = Hex.directions[5];
}
