using UnityEngine;
using System.Collections;

public class GameObjectGrid : Grid {
	
	public float cellWidth;
	public float cellHeight;
	public Transform parent;
	
	public GameObjectGrid(int rows, int cols, float cellWidth, float cellHeight, Transform parent) : base(rows, cols){
		this.cellWidth = cellWidth;
		this.cellHeight = cellHeight;
		this.parent = parent;
	}
	
	new public void Set(int row, int col, Gridable obj){
		base.Set(row, col, obj);
	}
	
	new public Gridable Get(int row, int col){
		return base.Get(row, col);
	}
	
	public void Draw(){
		for(var row = 0; row < this.rows; row++){
			for(var col = 0; col < this.cols; col++){
				if(base.isPopulated(row, col)){
					Gridable gridable = this.Get(row, col);
					GameObject prefab = gridable.GetPrefab();
					Vector3 position = this.ComputePosition(row, col);
					Quaternion rotation = Quaternion.Euler(0, 0, 0);
					GameObject go = MonoBehaviour.Instantiate(prefab, position, rotation) as GameObject;
					go.transform.parent = this.parent;
				}
			}
		}
	}
	
	private Vector3 ComputePosition(int row, int col){
		Vector3 centerDelta = this.ComputeLocalCenterParentCenterDelta();
		
		float x = row * cellHeight + cellHeight / 2 + centerDelta.x;
		float y = col * cellWidth + cellWidth / 2 + centerDelta.y;
		float z = this.parent.position.z;
		
		return new Vector3(x, y, z);
	}
	
	private float ComputeWidth(){
		return this.cols * this.cellWidth;
	}
	
	private float ComputeHeight(){
		return this.rows * this.cellHeight;
	}
	
	private Vector3 ComputeLocalCenterParentCenterDelta(){
		float deltaX = -this.ComputeWidth() / 2;
		float deltaY = -this.ComputeHeight() / 2;
		float deltaZ = this.parent.position.z; //TODO: Support the 3rd dimension. #notonlytwo
		
		return new Vector3(deltaX, deltaY, deltaZ);
	}
}