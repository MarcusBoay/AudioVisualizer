using UnityEngine;


[RequireComponent(typeof(AudioSource))]
public class AudioSourceGetSpectrumDataExample : MonoBehaviour
{
    public float height;
    public float innerRingDist;
    public const int maxValue = 2048;
    private float[] spectrum = new float[maxValue];

    public GameObject cube;
    private GameObject[] cubes = new GameObject[maxValue];
    private GameObject cubesParent;

    public FFTWindow windowType;


    private void Start()
    {
        //make cubesParent and set parent to this game object
        cubesParent = new GameObject("Cubes Parent");
        cubesParent.transform.parent = transform;

        for (int i = 1; i < maxValue - 1; i++)
        {
            //make cubes
            cubes[i] = Instantiate(cube, transform.position, transform.rotation);
            //set parent
            cubes[i].gameObject.transform.parent = cubesParent.transform;
            //set cube rotation
            cubes[i].transform.rotation = Quaternion.Euler(0, 0, (float)(i) / (maxValue - 2) * 360);
        }
    }

    void Update()
    {
        AudioListener.GetSpectrumData(spectrum, 0, windowType);

        for (int i = 1; i < maxValue - 1; i++)
        {
            //change size of cube to match amplitude of frequency
            cubes[i].transform.localScale = new Vector3(
                0.1f + spectrum[i] * height,
                cubes[i].transform.localScale.y,
                1);
            //set position of cubes
            cubes[i].transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z) +
                new Vector3(
                (innerRingDist + spectrum[i] * height / 2) * Mathf.Cos(Mathf.Deg2Rad * ((float)i / (maxValue - 2) * 360)),
                (innerRingDist + spectrum[i] * height / 2) * Mathf.Sin(Mathf.Deg2Rad * ((float)i / (maxValue - 2) * 360)),
                0);
        }
    }
}
