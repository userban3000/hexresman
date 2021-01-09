using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class HexGrid : MonoBehaviour {

    [Header("Grid Size")]
    public int width = 6;
    public int height = 6;

    [Header("Prefabs")]
    public HexCell cellPrefab;
    public Text cellLabelPrefab;

    Canvas gridCanvas;

    HexCell[] cells;
    HexMesh hexMesh;

    private void Awake() {
        gridCanvas = GetComponentInChildren<Canvas>();
        hexMesh = GetComponentInChildren<HexMesh>();

        cells = new HexCell[height * width];

        for ( int z = 0, i = 0; z < height; z++ ) {
            for ( int x = 0; x < width; x++ ) {
                CreateCell(x, z, i++);
            }
        }
    }

    private void Start() {
        hexMesh.Triangulate(cells);
    }

    void CreateCell (int x, int z, int i) {
        Vector3 pos;
        pos.x = (x + z * 0.5f - z/2 ) * (HexMetrics.innerRadius * 2f);
        pos.y = 0f;
        pos.z = z * (HexMetrics.outerRadius * 1.5f);

        HexCell cell = cells[i] = Instantiate<HexCell>(cellPrefab);
        cell.transform.SetParent(transform, false);
        cell.transform.localPosition = pos;
        cell.coordinates = HexCoordinates.FromOffsetCoordinates(x, z);

        Text label = Instantiate<Text>(cellLabelPrefab);
        label.rectTransform.SetParent(gridCanvas.transform, false);
        label.rectTransform.anchoredPosition = new Vector2 (pos.x, pos.z);
        label.text = cell.coordinates.ToStringOnSeparateLines();
    }

    //to be moved somewhere else, sometime else.

    private void Update() {
        if ( Input.GetMouseButton(0) ) {
            HandleInput();
        }
    }

    private void HandleInput() {
        Ray inputRay = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if ( Physics.Raycast(inputRay, out hit) ) {
            TouchCell (hit.point);
        }
    }

    private void TouchCell(Vector3 pos) {
        pos = transform.InverseTransformPoint(pos);
        HexCoordinates coordinates = HexCoordinates.FromOffsetCoordinates
        Debug.Log("touched at " + pos);
    }

}
