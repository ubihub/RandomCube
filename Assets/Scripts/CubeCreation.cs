using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

namespace Eterio.Realities.Basic
{

    public class CubeCreation : MonoBehaviour
    {
        [SerializeField] private int min_number_Cube = 2;
        [SerializeField] private int max_number_Cube = 10;
        [SerializeField] private float min_size_Cube = 0.2f;
        [SerializeField] private float max_size_Cube = 1.5f;
        [SerializeField] private string tag_name = "RandomCube";
        [SerializeField] private Vector3 initPosition = new Vector3();
        [SerializeField] private Vector3 offset = new Vector3();
        [SerializeField] private GameObject cubePrefab ;


        List<KeyValuePair<GameObject, float>> cubes = new List<KeyValuePair<GameObject, float>>();


        // Start is called before the first frame update
        void Start()
        {
            TaskOnClick();
        }

        // Update is called once per frame
        void Update()
        {
        }

        private void ClearCubes()
        {
            GameObject[] allCubes = UnityEngine.GameObject.FindGameObjectsWithTag(tag_name);
            if (allCubes == null) return;
            foreach (GameObject cube in allCubes)
            {
                Destroy(cube);
            }
            cubes.Clear();
            System.GC.Collect();
        }

        public void TaskOnClick()
        {
            ClearCubes();
            offset.x = 0.0f;
            int RandomNumber = Random.Range(min_number_Cube, max_number_Cube);
            for (int index = 0; index < RandomNumber; index++)
            {
                float RandomSize = Random.Range(min_size_Cube, max_size_Cube);
                GameObject cube = CreateCube(RandomSize);
                KeyValuePair<GameObject, float> cubePair = new KeyValuePair<GameObject, float>(cube, cube.transform.localScale.x);
                cubes.Add(cubePair);
            }

            cubes.Sort( delegate (KeyValuePair<GameObject, float> pair1,
                            KeyValuePair<GameObject, float> pair2)
                            {
                                return pair1.Value.CompareTo(pair2.Value);
                            }
                        );

            foreach (KeyValuePair<GameObject, float> cubePair in cubes)
            {
                GameObject orderedCube = cubePair.Key;
                orderedCube.transform.position = initPosition + offset;
                offset.x = offset.x + 11.5f / ( cubes.Count - 1 );
            }
        }


        private GameObject CreateCube(float cubeSize)
        {
            //Debug.Log("creating Cube");
            //create Cube
            GameObject cube;
            //cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube = Instantiate(cubePrefab, initPosition, Quaternion.identity);
            cube.tag = tag_name;
            cube.transform.localScale = new Vector3(cubeSize, cubeSize, cubeSize);
            return cube;
        }
    }
}
