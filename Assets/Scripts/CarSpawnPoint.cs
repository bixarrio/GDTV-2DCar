using UnityEngine;

public class CarSpawnPoint : MonoBehaviour
{
    [SerializeField] int _carDirection = 1;

    public void SpawnCar(Transform parent, OtherCarController otherCar)
    {
        var car = Instantiate(otherCar, transform.position, Quaternion.identity);
        car.transform.SetParent(parent, true);
        car.SetDirection(_carDirection);
    }
}
