using UnityEngine;
using System.Collections;

public class GridPosition {
	public int x;
	public int y;
	public int z;
	
	public GridPosition(int x, int y, int z){
		this.x = x;
		this.y = y;
		this.z = z;
	}
	
	public GridPosition(int x, int y){
		this.x = x;
		this.y = y;
	}
}