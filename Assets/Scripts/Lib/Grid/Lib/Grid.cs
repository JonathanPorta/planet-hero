using UnityEngine;
using System.Collections;

public class Grid {
	public int rows;
	public int cols;
	
	private Gridable[,] grid;
	
	public Grid(int rows, int cols){
		this.rows = rows;
		this.cols = cols;
		
		this.grid = new Gridable[rows, cols];
	}
	
	public void Set(int row, int col, Gridable obj){
		this.grid[row, col] = obj;
	}
	
	public Gridable Get(int row, int col){
		return this.grid[row, col];
	}
	
	public bool isPopulated(int row, int col){
		return this.Get(row, col) != null;
	}
}
