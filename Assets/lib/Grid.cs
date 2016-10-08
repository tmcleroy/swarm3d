using System.Collections.Generic;

public class Grid {

	public Dictionary<int, Hex> cells = new Dictionary<int, Hex>();

    public Grid(int width, int height) {
        for (int q = 0; q < width; q++) {
            int qOffset = q / 2;
            for (int s = -qOffset; s < height - qOffset; s++) {
                Hex h = new Hex(q, -q-s, s);
                this.cells[h.getHash()] = h;
            }
        }
    }

    public Hex getCell(Hex h) {
        return this.cells[h.getHash()];
    }
}
