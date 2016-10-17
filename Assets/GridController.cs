using UnityEngine;

public class GridController : MonoBehaviour {

	public GameObject tileObj;
	public static int gridWidth = 60;
	public static int gridHeight = 30;	
	public static int cellSize = 10;
	public static Grid grid;
	public static Layout layout;

	void Start () {
		grid = new Grid(gridWidth, gridHeight);
		layout = new Layout(Layout.flat, new Vector2(cellSize, cellSize), new Vector2(0, 0));
		tileObj = (GameObject)Instantiate(Resources.Load("Tile"));

		renderTiles();
		renderUnits();
	}
	
	void Update () {

	}

	void renderTiles () {
		foreach (Hex h in grid.cellMap.Values) {
			Vector2 pos = Layout.hexToPixel(layout, h);
			Instantiate(tileObj, new Vector3(pos.x, 5, pos.y), Quaternion.identity);
		}
	}
	void renderUnits () {
		for (int i = 0; i < 40; i++) {
			Instantiate(Resources.Load("randomunit"));
		}
	}
}
