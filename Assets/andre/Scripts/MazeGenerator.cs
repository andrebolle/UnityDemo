using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

// TODO: Place player at start position

public class MazeGenerator
{
    public int MazeSize { get; private set; }
    public int StartX { get; private set; }
    public int StartY { get; private set; }
    public int FinishX { get; private set; }
    public int FinishY { get; private set; }
    public Cell[,] Maze { get; private set; }

    public MazeGenerator(int mazeSize, int startX, int startY, int finishX, int finishY)
    {
        MazeSize = mazeSize;
        StartX = startX;
        StartY = startY;
        FinishX = finishX;
        FinishY = finishY;

        Maze = new Cell[MazeSize, MazeSize];
        for (int y = 0; y < MazeSize; y++)
        {
            for (int x = 0; x < MazeSize; x++)
            {
                Maze[x, y] = new Cell(x, y);
            }
        }

        GenerateMaze();
        AStar(StartX, StartY, FinishX, FinishY);
    }

    // Thanks to https://copilot.microsoft.com/
    void GenerateMaze()
    {
        // Start with the initial cell
        Cell currentCell = Maze[StartX, StartY];
        // Mark it as visited
        currentCell.Visited = true;

        // Create a stack to store cells to be explored
        Stack<Cell> stack = new Stack<Cell>();
        // Push the initial cell to the stack
        stack.Push(currentCell);

        // While there are still cells in the stack
        while (stack.Count > 0)
        {
            // Pop a cell from the stack and make it the current cell
            currentCell = stack.Pop();

            // Get a list of unvisited neighbours of the current cell
            List<Cell> unvisitedNeighbours = GetUnvisitedNeighbours(currentCell);

            // If the current cell has any unvisited neighbours
            if (unvisitedNeighbours.Count > 0)
            {
                // Push the current cell back to the stack
                stack.Push(currentCell);

                // Choose one of the unvisited neighbours randomly
                Cell chosenCell = unvisitedNeighbours[Random.Range(0, unvisitedNeighbours.Count)];

                // Remove the wall between the current cell and the chosen cell
                RemoveWall(currentCell, chosenCell);

                // Mark the chosen cell as visited
                chosenCell.Visited = true;
                // Push the chosen cell to the stack
                stack.Push(chosenCell);
            }
        }
    }

    // Thanks to https://copilot.microsoft.com/
    List<Cell> GetUnvisitedNeighbours(Cell cell)
    {
        List<Cell> neighbours = new List<Cell>();

        // Add the cell to the north, if it exists and hasn't been visited
        if (cell.Y + 1 < MazeSize && !Maze[cell.X, cell.Y + 1].Visited)
        {
            neighbours.Add(Maze[cell.X, cell.Y + 1]);
        }

        // Add the cell to the east, if it exists and hasn't been visited
        if (cell.X + 1 < MazeSize && !Maze[cell.X + 1, cell.Y].Visited)
        {
            neighbours.Add(Maze[cell.X + 1, cell.Y]);
        }

        // Add the cell to the south, if it exists and hasn't been visited
        if (cell.Y - 1 >= 0 && !Maze[cell.X, cell.Y - 1].Visited)
        {
            neighbours.Add(Maze[cell.X, cell.Y - 1]);
        }

        // Add the cell to the west, if it exists and hasn't been visited
        if (cell.X - 1 >= 0 && !Maze[cell.X - 1, cell.Y].Visited)
        {
            neighbours.Add(Maze[cell.X - 1, cell.Y]);
        }

        return neighbours;
    }

    // Thanks to https://copilot.microsoft.com/
    void RemoveWall(Cell currentCell, Cell chosenCell)
    {
        // If the chosen cell is to the north of the current cell, remove the north wall of the current cell and the south wall of the chosen cell
        if (currentCell.Y < chosenCell.Y)
        {
            currentCell.NorthWall = false;
            chosenCell.SouthWall = false;
        }
        // If the chosen cell is to the east of the current cell, remove the east wall of the current cell and the west wall of the chosen cell
        else if (currentCell.X < chosenCell.X)
        {
            currentCell.EastWall = false;
            chosenCell.WestWall = false;
        }
        // If the chosen cell is to the south of the current cell, remove the south wall of the current cell and the north wall of the chosen cell
        else if (currentCell.Y > chosenCell.Y)
        {
            currentCell.SouthWall = false;
            chosenCell.NorthWall = false;
        }
        // If the chosen cell is to the west of the current cell, remove the west wall of the current cell and the east wall of the chosen cell
        else if (currentCell.X > chosenCell.X)
        {
            currentCell.WestWall = false;
            chosenCell.EastWall = false;
        }
    }

    public void AStar(int startX, int startY, int finishX, int finishY)
    {
        // Create a priority queue for the open set
        PriorityQueue<Cell> openSet = new PriorityQueue<Cell>();
        // Create a dictionary for the came from map
        Dictionary<Cell, Cell> cameFrom = new Dictionary<Cell, Cell>();
        // Create a dictionary for the cost so far map
        Dictionary<Cell, float> costSoFar = new Dictionary<Cell, float>();

        // Add the start cell to the open set and the cost so far map
        Cell start = Maze[startX, startY];
        openSet.Enqueue(start, 0);
        costSoFar[start] = 0;

        // While there are still cells in the open set
        while (openSet.Count > 0)
        {
            // Get the cell in the open set with the lowest total cost (cost so far + heuristic)
            Cell current = openSet.Dequeue();

            // If this cell is the finish cell, reconstruct the path
            if (current.X == finishX && current.Y == finishY)
            {
                while (current != null)
                {
                    current.SolutionPath = true;
                    cameFrom.TryGetValue(current, out current);
                }
                break;
            }

            // For each neighbor of the current cell
            foreach (Cell neighbor in GetAccessibleAdjacentCells(current))
            {
                // Calculate the new cost to reach the neighbor
                float newCost = costSoFar[current] + 1; // Assuming all moves have a cost of 1

                // If the neighbor has not been visited or the new cost is less than the cost so far
                if (!costSoFar.ContainsKey(neighbor) || newCost < costSoFar[neighbor])
                {
                    // Update the cost so far map
                    costSoFar[neighbor] = newCost;
                    // Calculate the priority for the neighbor (cost so far + heuristic)
                    float priority = newCost + Heuristic(neighbor, Maze[finishX, finishY]);
                    // Add the neighbor to the open set
                    openSet.Enqueue(neighbor, priority);
                    // Update the came from map
                    cameFrom[neighbor] = current;
                }
            }
        }
    }

    // This method returns a list of the valid neighbors of a cell
    List<Cell> GetAccessibleAdjacentCells(Cell cell)
    {
        List<Cell> neighbors = new List<Cell>();

        // Add the cell to the north, if it exists and doesn't have a wall
        if (cell.Y + 1 < MazeSize && !cell.NorthWall)
        {
            neighbors.Add(Maze[cell.X, cell.Y + 1]);
        }

        // Add the cell to the east, if it exists and doesn't have a wall
        if (cell.X + 1 < MazeSize && !cell.EastWall)
        {
            neighbors.Add(Maze[cell.X + 1, cell.Y]);
        }

        // Add the cell to the south, if it exists and doesn't have a wall
        if (cell.Y - 1 >= 0 && !cell.SouthWall)
        {
            neighbors.Add(Maze[cell.X, cell.Y - 1]);
        }

        // Add the cell to the west, if it exists and doesn't have a wall
        if (cell.X - 1 >= 0 && !cell.WestWall)
        {
            neighbors.Add(Maze[cell.X - 1, cell.Y]);
        }

        return neighbors;
    }

    // This method calculates the heuristic for a cell (Manhattan distance to the finish)
    float Heuristic(Cell cell, Cell finish)
    {
        return Mathf.Abs(cell.X - finish.X) + Mathf.Abs(cell.Y - finish.Y);
    }
}



public class Cell
{
    public int X { get; private set; }
    public int Y { get; private set; }
    public bool NorthWall { get; set; }
    public bool EastWall { get; set; }
    public bool SouthWall { get; set; }
    public bool WestWall { get; set; }
    public bool Visited { get; set; }
    public bool SolutionPath { get; set; }

    public Cell(int x, int y)
    {
        X = x;
        Y = y;
        NorthWall = true;
        EastWall = true;
        SouthWall = true;
        WestWall = true;
        Visited = false;
    }

    Stack<Cell> stack = new Stack<Cell>();

}

public class PriorityQueue<T>
{
    private SortedDictionary<float, Queue<T>> queue = new SortedDictionary<float, Queue<T>>();
    private int count = 0;

    public void Enqueue(T item, float priority)
    {
        if (!queue.ContainsKey(priority))
        {
            queue[priority] = new Queue<T>();
        }
        queue[priority].Enqueue(item);
        count++;
    }

    public T Dequeue()
    {
        var pair = queue.First();
        var item = pair.Value.Dequeue();
        if (pair.Value.Count == 0)
        {
            queue.Remove(pair.Key);
        }
        count--;
        return item;
    }

    public int Count
    {
        get { return count; }
    }
}



/*
Here's the "recursive backtracker" maze pseudo-code

1 Choose the initial cell, mark it as visited and push it to the stack
2 While the stack is not empty
   2.1 Pop a cell from the stack and make it a current cell
   2.2 If the current cell has any neighbours which have not been visited
      2.2.1 Push the current cell to the stack
      2.2.2 Choose one of the unvisited neighbours
      2.2.3 Remove the wall between the current cell and the chosen cell
      2.2.4 Mark the chosen cell as visited and push it to the stack

Key Components:

    Maze: A grid of cells, initially separated by walls.
    Cell: A single unit of the maze grid. Coordinates, walls and visited flags
    Stack: A data structure used for storing cells to be explored.
    Neighbor: An adjacent cell that shares a wall with the current cell.

*/


