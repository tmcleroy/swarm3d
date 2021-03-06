﻿using System;
using System.Collections.Generic;
using System.Linq;

public class Grid {

	public Dictionary<int, Hex> cellMap = new Dictionary<int, Hex>();
    public Dictionary<int, Unit> unitMap = new Dictionary<int, Unit>();
    private Random random;

    public Grid(int width, int height) {
        random = new Random();
        for (int q = 0; q < width; q++) {
            int qOffset = q / 2;
            for (int s = -qOffset; s < height - qOffset; s++) {
                Hex h = new Hex(q, -q-s, s);
                cellMap[h.getHash()] = h;
            }
        }
    }

    public Hex getCell(Hex h) {
        return cellMap[h.getHash()];
    }

    public Unit getUnit(Hex h) {
        return unitMap[h.getHash()];
    }

    public Hex getRandomCell() {
        return cellMap.ElementAt(random.Next(0, cellMap.Count)).Value;
    }

    public void moveUnit(Unit u, Hex oldPos, Hex newPos) {
        unitMap[newPos.getHash()] = u;
        try {
            unitMap.Remove(oldPos.getHash());
        } catch (NullReferenceException) { };
    }

    public List<Unit> getUnits(Hex[] hexes) {
        List<Unit> units = new List<Unit>();
        foreach (Hex h in hexes) {
            try {
                Unit u = unitMap[h.getHash()];
                units.Add(u);
            } catch (KeyNotFoundException) { }
        }
        return units;
    }
}
