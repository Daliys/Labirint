using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public GameObject Character;

    public GameObject CellPrefab;
    public GameObject WallPrefab;
    public GameObject GoalPrefab;

    public int mapSizeX;
    public int mapSizeY;
    private float objectsScale;
    private Vector2 positionOffset;

    enum TypeOfCell
    {
        WallCell, EmptyCell, MovebleCell, GoalCell
    }
    enum Direction
    {
        Left, Right, Down, Up
    }

    TypeOfCell[,] map;

    void Start()
    {
        // подгоняем маштаб клеток под разные значения размеров лабиринта
        if(mapSizeY * 16 < mapSizeX * 9) objectsScale = 1 / ((float)(mapSizeX * 1f) / 16f);
        else objectsScale = 1 / ((float)(mapSizeY * 1f )/ 9f);
        positionOffset.x = mapSizeX * objectsScale / -2f + objectsScale/2f;
        positionOffset.y = mapSizeY * objectsScale / -2f + objectsScale/2f;
        Character.transform.localScale = new Vector2(objectsScale - objectsScale * 0.05f, objectsScale - objectsScale * 0.05f);

        Game.currestScore = 0;
        map = new TypeOfCell[mapSizeX, mapSizeY];
        GenerateMap();
    }


    void GenerateMap()
    {
    
        // генерация карты с пустыми очиночными ячейками и стенами
        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                if ((i % 2 == 0) || (j % 2 == 0)) map[i, j] = TypeOfCell.WallCell;
                else if ((i % 2 != 0) && (j % 2 != 0)) map[i, j] = TypeOfCell.EmptyCell;
                else map[i, j] = TypeOfCell.WallCell;
            }
        }


        bool isFindingFinish = false;

        int randomY = (int)(Random.Range(1, map.GetLength(1) - 2) );
        randomY = randomY % 2 == 0 ? randomY - 1 : randomY;
        randomY = randomY < 1 ? 1 : randomY;

        int randomX = (int)(Random.Range(1, map.GetLength(0) - 2) * 0.1f );
        randomX = randomX % 2 == 0 ? randomX - 1 : randomX;
        randomX = randomX < 1 ? 1 : randomX;

        Position movementPosition = new Position(randomX, randomY);
        map[movementPosition.x, movementPosition.y] = TypeOfCell.MovebleCell;
        Character.transform.position = new Vector2(positionOffset.x + movementPosition.x * (objectsScale), positionOffset.y + movementPosition.y * (objectsScale));
        
        while (!isFindingFinish)
        {
            List<Direction> posibleMovebleDirection = GetPosibleMovebleDirection(map, movementPosition, TypeOfCell.EmptyCell);

            if (posibleMovebleDirection != null)
            {
                Direction randomDirection = posibleMovebleDirection[Random.Range(0, posibleMovebleDirection.Count)];
                movementPosition = FillMapByDirection(map, movementPosition, randomDirection);
            }
            else
            {
                List<Position> emptyCells = GetEmptyAvailibleCell(map);
                if (emptyCells == null) {
                    isFindingFinish = true;
                    break;
                }

                bool isSelectedNewCell = false;
                Position randomEmptyCell = null;
                List<Direction> movableDirection = new List<Direction>();

                while (!isSelectedNewCell)
                {
                    randomEmptyCell = emptyCells[Random.Range(0, emptyCells.Count)];

                    movableDirection = GetPosibleMovebleDirection(map, randomEmptyCell, TypeOfCell.MovebleCell);
                    if (movableDirection == null)
                    {
                        emptyCells.Remove(randomEmptyCell);
                        continue;
                    }
                    else isSelectedNewCell = true;
                }
                movementPosition = randomEmptyCell;


                Direction randomDirection = movableDirection[Random.Range(0, movableDirection.Count)];
                FillMapByDirection(map, new Position(randomEmptyCell.x, randomEmptyCell.y), randomDirection);
            }
        }

        Position goalPosition = GetRandomFindingPosition(map);
        map[goalPosition.x, goalPosition.y] = TypeOfCell.GoalCell;

        GenerateObjectOnScene(map);

    }

    private Position GetRandomFindingPosition(TypeOfCell[,] map)
    {
        Position finishPosition = null;

        bool isFindingRandomPosition = false;
        while (!isFindingRandomPosition)
        {
            int pos = Random.Range(1, map.GetLength(1) - 2);
            if (map[map.GetLength(0) - 2, pos] != TypeOfCell.MovebleCell) continue;
            else
            {
                finishPosition = new Position(map.GetLength(0) - 2 , pos);
                isFindingRandomPosition = true;
            }
        }

        return finishPosition;
    }


    // создание игровых объектов на сцене по карте генерации
    private void GenerateObjectOnScene(TypeOfCell[,] map)
    {
        for (int i = 0; i < map.GetLength(0); i++)
        {
            for (int j = 0; j < map.GetLength(1); j++)
            {
                GameObject gameCell = null;
                if (map[i, j] == TypeOfCell.MovebleCell) gameCell = Instantiate(CellPrefab);
                else if (map[i, j] == TypeOfCell.WallCell) gameCell = Instantiate(WallPrefab);
                else if (map[i, j] == TypeOfCell.GoalCell) gameCell = Instantiate(GoalPrefab);
                
                gameCell.transform.localScale = new Vector2(objectsScale, objectsScale);
                gameCell.transform.position = new Vector2(positionOffset.x + i * (objectsScale ) , positionOffset.y + j * (objectsScale));
                gameCell.transform.parent = transform;
            }
        }
    }

    // заполняет клетки карты значениями клеток лабиринта
    private Position FillMapByDirection(TypeOfCell[,] map, Position movementPosition, Direction direction)
    {
        map[movementPosition.x, movementPosition.y] = TypeOfCell.MovebleCell;
        switch (direction)
        {
            case Direction.Down:
                map[movementPosition.x + 1, movementPosition.y] = TypeOfCell.MovebleCell;
                map[movementPosition.x + 2, movementPosition.y] = TypeOfCell.MovebleCell;
                movementPosition.x += 2;
                break;

            case Direction.Left:
                map[movementPosition.x, movementPosition.y - 1] = TypeOfCell.MovebleCell;
                map[movementPosition.x, movementPosition.y - 2] = TypeOfCell.MovebleCell;
                movementPosition.y -= 2;
                break;

            case Direction.Right:
                map[movementPosition.x, movementPosition.y + 1] = TypeOfCell.MovebleCell;
                map[movementPosition.x, movementPosition.y + 2] = TypeOfCell.MovebleCell;
                movementPosition.y += 2;
                break;

            case Direction.Up:
                map[movementPosition.x - 1, movementPosition.y] = TypeOfCell.MovebleCell;
                map[movementPosition.x - 2, movementPosition.y] = TypeOfCell.MovebleCell;
                movementPosition.x -= 2;
                break;
        }
        return movementPosition;
    }
    
    // возращаеь (true\false) в зависимости можно ли двигаться в указаном направлении
    private bool IsPosibleMoveToDirection(TypeOfCell[,] map, Position positionFrom ,Direction direction, TypeOfCell selectedTypeOfCell)
    {
        switch (direction)
        {
            case Direction.Down:
                if (positionFrom.x + 2 >= (map.GetLength(0) - 1)) return false;
                if (map[positionFrom.x + 2, positionFrom.y] != selectedTypeOfCell) return false;
                break;

            case Direction.Left:
                if (positionFrom.y - 2 < (0 + 1)) return false;
                if (map[positionFrom.x, positionFrom.y - 2] != selectedTypeOfCell) return false;
                break;

            case Direction.Right:
                if (positionFrom.y + 2 >= (map.GetLength(1) - 1)) return false;
                if (map[positionFrom.x, positionFrom.y + 2] != selectedTypeOfCell) return false;
                break;

            case Direction.Up:
                if (positionFrom.x - 2 < (0 + 1)) return false;
                if (map[positionFrom.x - 2, positionFrom.y] != selectedTypeOfCell) return false;
                break;
        }
        return true;
    }

    // возращает список направлений куда с текущей клетки можно передвигаться
    private List<Direction> GetPosibleMovebleDirection(TypeOfCell[,] map, Position positionFrom, TypeOfCell  selectedTypeOfCell)
    {
        List<Direction> movebleDirections = new List<Direction>();
        if (IsPosibleMoveToDirection(map, positionFrom, Direction.Down, selectedTypeOfCell)) movebleDirections.Add(Direction.Down);
        if (IsPosibleMoveToDirection(map, positionFrom, Direction.Left, selectedTypeOfCell)) movebleDirections.Add(Direction.Left);
        if (IsPosibleMoveToDirection(map, positionFrom, Direction.Right, selectedTypeOfCell)) movebleDirections.Add(Direction.Right);
        if (IsPosibleMoveToDirection(map, positionFrom, Direction.Up, selectedTypeOfCell)) movebleDirections.Add(Direction.Up);

        if (movebleDirections.Count == 0) return null;
        return movebleDirections;
    }

    // возращает массив позиций которые еще не участвовали в генерации
    private List<Position> GetEmptyAvailibleCell(TypeOfCell[,] map)
    {
        List<Position> avalibleCells = new List<Position>();
        for(int i = 1; i < map.GetLength(0) - 1; i++)
        {
            for(int j = 1; j < map.GetLength(1) - 1; j++)
            {
                if (map[i, j] == TypeOfCell.EmptyCell) avalibleCells.Add(new Position(i, j));
            }
        }
        if (avalibleCells.Count == 0) return null;
        return avalibleCells;
    }

    public void SetMapSize(int x, int y)
    {
        mapSizeX = x;
        mapSizeY = y;
    }

    public void GenerateRandomGoal()
    {
        bool isGenerated = false;
        while (!isGenerated)
        {
            int randX = Random.Range(1, mapSizeX - 2);
            int randY = Random.Range(1, mapSizeY - 2);
            if (map[randX, randY] != TypeOfCell.MovebleCell) continue;

            map[randX, randY] = TypeOfCell.GoalCell;

            GameObject gameCell = Instantiate(GoalPrefab);
            gameCell.transform.localScale = new Vector2(objectsScale, objectsScale);
            gameCell.transform.position = new Vector2(positionOffset.x + randX * (objectsScale), positionOffset.y + randY * (objectsScale));
            gameCell.transform.parent = transform;
            isGenerated = true;
        }
    }

}
