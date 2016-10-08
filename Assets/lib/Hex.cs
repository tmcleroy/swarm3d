using System;

public class Hex {

	public int q;
	public int r;
	public int s;

	public Hex(int q, int r, int s) {
        this.q = q;
        this.r = r;
        this.s = s;
	}

	public override String ToString () {
		return String.Format("({0}, {1}, {1})", this.q, this.r, this.s);
	}

    public int getHash () {
        return String.Format("{0},{1},{1}", this.q, this.r, this.s).GetHashCode();
    }

	static public Hex add(Hex a, Hex b) {
        return new Hex(a.q + b.q, a.r + b.r, a.s + b.s);
    }


    static public Hex subtract(Hex a, Hex b) {
        return new Hex(a.q - b.q, a.r - b.r, a.s - b.s);
    }

    static public int length(Hex hex) {
        return (int)((Math.Abs(hex.q) + Math.Abs(hex.r) + Math.Abs(hex.s)) / 2);
    }


    static public int distance(Hex a, Hex b) {
        return Hex.length(Hex.subtract(a, b));
    }


    static public Hex scale(Hex a, int k) {
        return new Hex(a.q * k, a.r * k, a.s * k);
    }


    static public Hex neighbor(Hex hex, int direction) {
        return Hex.add(hex, Hex.directions[direction]);
    }

    static public Hex[] neighbors(Hex hex) {
		int numDirections = Hex.directions.Length;
        Hex[] neighbors = new Hex[numDirections];
        for (int i = 0; i < numDirections; i++) {
			neighbors[i] = Hex.add(hex, Hex.directions[i]);
        }
        return neighbors;
    }

    static public Hex diagonalNeighbor(Hex hex, int direction) {
        return Hex.add(hex, Hex.diagonals[direction]);
    }

    static public Hex[] directions = new Hex[] {
        new Hex(1, 0, -1),
        new Hex(1, -1, 0),
        new Hex(0, -1, 1),
        new Hex(-1, 0, 1),
        new Hex(-1, 1, 0),
        new Hex(0, 1, -1)
    };

    static public Hex[] diagonals = new Hex[] {
        new Hex(2, -1, -1),
        new Hex(1, -2, 1),
        new Hex(-1, -1, 2),
        new Hex(-2, 1, 1),
        new Hex(-1, 2, -1),
        new Hex(1, 1, -2),
    };
}





public class FractionalHex {
    public double q;
    public double r;
    public double s;

    public FractionalHex(double q, double r, double s) {
        this.q = q;
        this.r = r;
        this.s = s;
    }

    static public Hex hexRound(FractionalHex h) {
        int q = (int)(Math.Round(h.q));
        int r = (int)(Math.Round(h.r));
        int s = (int)(Math.Round(h.s));
        double q_diff = Math.Abs(q - h.q);
        double r_diff = Math.Abs(r - h.r);
        double s_diff = Math.Abs(s - h.s);
        if (q_diff > r_diff && q_diff > s_diff) {
            q = -r - s;
        } else if (r_diff > s_diff) {
            r = -q - s;
        } else {
            s = -q - r;
        }
        return new Hex(q, r, s);
    }

    static public FractionalHex hexLerp(FractionalHex a, FractionalHex b, double t) {
        return new FractionalHex(a.q * (1 - t) + b.q * t, a.r * (1 - t) + b.r * t, a.s * (1 - t) + b.s * t);
    }

    static public Hex[] hexLinedraw(Hex a, Hex b) {
        int N = Hex.distance(a, b);
        FractionalHex a_nudge = new FractionalHex(a.q + 0.000001, a.r + 0.000001, a.s - 0.000002);
        FractionalHex b_nudge = new FractionalHex(b.q + 0.000001, b.r + 0.000001, b.s - 0.000002);
        Hex[] results = new Hex[N];
        double step = 1.0 / Math.Max(N, 1);
        for (int i = 0; i <= N; i++) {
            results[i] = FractionalHex.hexRound(FractionalHex.hexLerp(a_nudge, b_nudge, step * i));
        }
        return results;
    }

}