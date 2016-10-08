using UnityEngine;
using System;

public class Layout {
    public Orientation orientation;
    public Vector2 size;
    public Vector2 origin;

    public Layout(Orientation orientation, Vector2 size, Vector2 origin) {
        this.orientation = orientation;
        this.size = size;
        this.origin = origin;
    }

    static public Orientation pointy = new Orientation(Math.Sqrt(3.0), Math.Sqrt(3.0) / 2.0, 0.0, 3.0 / 2.0, Math.Sqrt(3.0) / 3.0, -1.0 / 3.0, 0.0, 2.0 / 3.0, 0.5);
    static public Orientation flat = new Orientation(3.0 / 2.0, 0.0, Math.Sqrt(3.0) / 2.0, Math.Sqrt(3.0), 2.0 / 3.0, 0.0, -1.0 / 3.0, Math.Sqrt(3.0) / 3.0, 0.0);

    static public Vector2 hexToPixel(Layout layout, Hex h)    {
        Orientation M = layout.orientation;
        Vector2 size = layout.size;
        Vector2 origin = layout.origin;
        double x = (M.f0 * h.q + M.f1 * h.r) * size.x;
        double y = (M.f2 * h.q + M.f3 * h.r) * size.y;
        return new Vector2((float)x + origin.x, (float)y + origin.y);
    }


    static public FractionalHex pixelToHex(Layout layout, Vector2 p) {
        Orientation M = layout.orientation;
        Vector2 size = layout.size;
        Vector2 origin = layout.origin;
        Vector2 pt = new Vector2((p.x - origin.x) / size.x, (p.y - origin.y) / size.y);
        double q = M.b0 * pt.x + M.b1 * pt.y;
        double r = M.b2 * pt.x + M.b3 * pt.y;
        return new FractionalHex(q, r, -q - r);
    }


    static public Vector2 hexCornerOffset(Layout layout, int corner) {
        Orientation M = layout.orientation;
        Vector2 size = layout.size;
        double angle = 2.0 * Math.PI * (M.startAngle - corner) / 6;
        return new Vector2((float)(size.x * Math.Cos(angle)), (float)(size.y * Math.Sin(angle)));
    }


    static public Vector2[] polygonCorners(Layout layout, Hex h) {
        Vector2[] corners = new Vector2[6];
        Vector2 center = Layout.hexToPixel(layout, h);
        for (int i = 0; i < 6; i++) {
            Vector2 offset = Layout.hexCornerOffset(layout, i);
            corners[i] = new Vector2(center.x + offset.x, center.y + offset.y);
        }
        return corners;
    }

}

public class Orientation
{
    public double f0, f1, f2, f3;
    public double b0, b1, b2, b3;
    public double startAngle;

    public Orientation(double f0, double f1, double f2, double f3, double b0, double b1, double b2, double b3, double startAngle)    {
        this.f0 = f0;
        this.f1 = f1;
        this.f2 = f2;
        this.f3 = f3;
        this.b0 = b0;
        this.b1 = b1;
        this.b2 = b2;
        this.b3 = b3;
        this.startAngle = startAngle;
    }
}