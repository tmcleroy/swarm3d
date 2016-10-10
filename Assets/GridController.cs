using UnityEngine;

public class GridController : MonoBehaviour {

	public GameObject tileObj;
	public static int gridWidth = 20;
	public static int gridHeight = 10;	
	public static int cellSize = 10;
	public static Grid grid = new Grid(gridWidth, gridHeight);
	public static Layout layout;

	// Use this for initialization
	void Start () {
		tileObj = (GameObject)Instantiate(Resources.Load("Tile"));
		layout = new Layout(Layout.flat, new Vector2(cellSize, cellSize), new Vector2(0, 0));

		foreach (Hex h in grid.cellMap.Values) {
			Vector2 pos = Layout.hexToPixel(layout, h);
			Instantiate(tileObj, new Vector3(pos.x, 5, pos.y), Quaternion.identity);
		}
	}
	
	// Update is called once per frame
	void Update () {

	}
}
