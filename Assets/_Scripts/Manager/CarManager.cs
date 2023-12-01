using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarManager : MonoBehaviour
{
    private List<Car> cars = null;
    private Car carPref = null;
    private List<Node> firstColumn = null;

    public System.Func<int, List<Node>> OnGetColumn = null;
    public System.Func<int, List<Node>> OnGetRow = null;

    public void Initialize(int amount)
    {
        carPref = Resources.Load<Car>(string.Format(GameConstant.ITEM_PREFAB_PATH, "Car"));

        cars = new List<Car>();
        firstColumn = OnGetColumn?.Invoke(0);

        Car tempCar = null;

        for (int i = 0; i < amount; i++)
        {
            tempCar = Instantiate(carPref, transform);

            Vector3 carPos = 
                new Vector3(firstColumn[i].WorldPosition.x - GameConstant.NODE_LENGTH*0.75f,
                firstColumn[i].WorldPosition.y,
                firstColumn[i].WorldPosition.z - GameConstant.NODE_LENGTH*0.25f);

            tempCar.Initialize(carPos, OnGetRow?.Invoke(i));
            tempCar.PoolPosition = transform.position;

            cars.Add(tempCar);
        }
    }
}
