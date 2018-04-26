using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour {

    public Vector3 cubeScale;
    private GameObject[] wallArray;

    private int[] wallBoolArray = new int[4];

    private float Xmodifier = 0;
    private float Ymodifier = 0;

    private int[] wallHoleArray = new int[12];

    private List<List<List<List<int>>>> WallList;

    private System.Random Generator = new System.Random(42);

    
    // Use this for initialization
    void Start()
    {
        cubeScale = new Vector3(10, 10, 1);
        wallArray = new GameObject[2916];
        wallBoolArray[0] = 0;
        wallBoolArray[1] = 0;
        wallBoolArray[2] = 0;
        wallBoolArray[3] = 0;

        InitWallsVar();

        for (int i = 0; i < 12; i++)
        {
            wallHoleArray[i] = Generator.Next(3, 7);
        }

        initWalls();
    }

    // Update is called once per frame
    void Update()
    {

    }



    public List<List<List<List<int>>>> InitWallsVar()                                                          //On crée une variable stockant tous les murs
    {
        List<List<List<List<int>>>> WallList = new List<List<List<List<int>>>>();
        for (int Z = 0; Z < 9; Z++)
        {
            List<List<List<int>>> array1 = new List<List<List<int>>>();
            for (int i = 0; i < 9; i++)
            {
                List<List<int>> array2 = new List<List<int>>();

                for (int j = 0; j < 9; j++)
                {
                    List<int> array3 = new List<int>();
                    array3.Add(0);
                    array3.Add(0);
                    array2.Add(array3);
                }
                array1.Add(array2);
            }
            WallList.Add(array1);
        }

        return (WallList);
    }

    public void initWalls()
    {

        WallList = InitWallsVar();
        int init = 0;

        for (int Z = 0; Z < 9; Z++)
        {
            if (Z == 1)                  //Permet d'indenter les positions des murs afin de faire l'intégralité du labyrinthe
            {
                Xmodifier = 90;
                Ymodifier = 0;
            }
            if (Z == 2)
            {
                Xmodifier = 180;
                Ymodifier = 0;
            }
            if (Z == 3)
            {
                Xmodifier = 0;
                Ymodifier = 90;
            }
            if (Z == 4)
            {
                Xmodifier = 90;
                Ymodifier = 90;
            }
            if (Z == 5)
            {
                Xmodifier = 180;
                Ymodifier = 90;
            }
            if (Z == 6)
            {
                Xmodifier = 0;
                Ymodifier = 180;
            }
            if (Z == 7)
            {
                Xmodifier = 90;
                Ymodifier = 180;
            }
            if (Z == 8)
            {
                Xmodifier = 180;
                Ymodifier = 180;
            }


            for (int i = 0; i < 9; i++)
            {
                for (int l = 0; l < 9; l++)
                {
                    for (int k = 0; k < 2; k++)
                    {
                        int y = Generator.Next(11);
                        if (y > 6)
                            WallList[Z][i][l][k] = 1;
                        else
                            WallList[Z][i][l][k] = 0;
                    }

                    /*if(WallList[Z][i][l][0] == 1 && WallList[Z][i][l][1] == 1 && WallList[Z][i+1][l][0] == 1 && WallList[Z][i][l+1][1] == 1)
					{
						int y = Generator.Next(2);
                        WallList[Z][i][l][y] = 0;
					}

                    if(WallList[Z][i][l][0] == 1 && WallList[Z][i][l][1] == 1 && WallList[Z][i + 1][l][0] == 1 && WallList[Z][i][l + 1][1] == 1)
					{
						int y = Generator.Next(4);
                        WallList[Z][i][l][y] = 1;
					}*/

                    /*if(i==0)
						wallBoolArray[0] = 1;
					if(l== 0)
						wallBoolArray[1] = 1;
                        */
                    /*
					if(Z == 0 && i == wallHoleArray[0] && l == 8)
						wallBoolArray[1] =0;
					if(Z == 0 && i == 8 && l ==  wallHoleArray[2])
						wallBoolArray[2] =0;

					if(Z == 1 && i == wallHoleArray[0] && l == 0)
						wallBoolArray[3] =0;
					if(Z == 1 && i == 8 && l ==  wallHoleArray[3])
						wallBoolArray[2] =0;
					if(Z == 1 && i == wallHoleArray[1] && l == 8)
						wallBoolArray[1] =0;

					if(Z == 2 && i == wallHoleArray[1] && l == 0)
						wallBoolArray[3] =0;
					if(Z == 2 && i == 8 && l ==  wallHoleArray[4])
						wallBoolArray[2] =0;

					if(Z == 3 && i == 0 && l ==  wallHoleArray[2])
						wallBoolArray[0] =0;
					if(Z == 3 && i == wallHoleArray[5] && l == 8)
						wallBoolArray[1] =0;
					if(Z == 3 && i == 8 && l ==  wallHoleArray[7])
						wallBoolArray[2] =0;

					if(Z == 4 && i == wallHoleArray[5] && l == 0)
						wallBoolArray[3] =0;
					if(Z == 4 && i == 0 && l ==  wallHoleArray[3])
						wallBoolArray[0] =0;
					if(Z == 4 && i == wallHoleArray[6] && l == 8)
						wallBoolArray[1] =0;
					if(Z == 4 && i == 8 && l ==  wallHoleArray[8])
						wallBoolArray[2] =0;

					if(Z == 5 && i == 0 && l ==  wallHoleArray[4])
						wallBoolArray[0] =0;
					if(Z == 5 && i == wallHoleArray[6] && l == 0)
						wallBoolArray[3] =0;
					if(Z == 5 && i == 8 && l == wallHoleArray[9])
						wallBoolArray[2] =0;
						
					if(Z == 6 && i == 0 && l ==  wallHoleArray[7])
						wallBoolArray[0] =0;
					if(Z == 6 && i == wallHoleArray[10] && l == 8)
						wallBoolArray[1] =0;

					if(Z == 7 && i == wallHoleArray[10] && l == 0)
						wallBoolArray[3] =0;
					if(Z == 7 && i == 0 && l ==  wallHoleArray[8])
						wallBoolArray[0] =0;
					if(Z == 7 && i == wallHoleArray[11] && l == 8)
						wallBoolArray[1] =0;

					if(Z == 8 && i == wallHoleArray[11] && l == 0)
						wallBoolArray[3] =0;
					if(Z == 8 && i == 0 && l ==  wallHoleArray[9])
						wallBoolArray[0] =0;*/


                    if (WallList[Z][i][l][0] == 1 && !(i == 0 && (Z == 0 || Z == 1 || Z == 2)))                                                //Nicolas changer le type de mur
                    {
                        wallArray[init] = GameObject.CreatePrimitive(PrimitiveType.Cube);

                        wallArray[init].transform.position = new Vector3((130f - l * 10 - Xmodifier), 5f, (-134.5f + i * 10 + Ymodifier)) / 10;
                        wallArray[init].transform.localScale = cubeScale / 10;
                        wallArray[init].transform.SetParent(gameObject.transform);

                        //Surement à supprimer dans cette partie
                        wallArray[init].AddComponent<Rigidbody>();
                        Rigidbody rig = wallArray[init].GetComponent<Rigidbody>();
                        rig.isKinematic = true;
                        rig.useGravity = false;
                        rig.freezeRotation = true;
                        rig.constraints = RigidbodyConstraints.FreezeAll;
                        
                        init++;
                    }

                    /*if(wallBoolArray[1] == 1)
					{
						wallArray[init] = GameObject.CreatePrimitive(PrimitiveType.Cube);

						wallArray[init].transform.position = new Vector3((130f - l*10 - Xmodifier)-5, 5f, (-134.5f + i*10 + Ymodifier)+5)/10;
						wallArray[init].transform.localScale = cubeScale/10;
						wallArray[init].transform.Rotate(new Vector3(0f,90f,0f));
						wallArray[init].transform.SetParent(gameObject.transform);
						init++;
					}

					if(wallBoolArray[2] == 1)
					{
						wallArray[init] = GameObject.CreatePrimitive(PrimitiveType.Cube);

						wallArray[init].transform.position = new Vector3((130f - l*10 - Xmodifier), 5f, (-134.5f + i*10 + Ymodifier)+10)/10;
						wallArray[init].transform.localScale = cubeScale/10;
						wallArray[init].transform.SetParent(gameObject.transform);
						init++;
					}*/

                    if (WallList[Z][i][l][1] == 1)
                    {
                        wallArray[init] = GameObject.CreatePrimitive(PrimitiveType.Cube);

                        wallArray[init].transform.position = new Vector3((130f - l * 10 - Xmodifier) + 5, 5f, (-134.5f + i * 10 + Ymodifier) + 5) / 10;
                        wallArray[init].transform.localScale = cubeScale / 10;
                        wallArray[init].transform.Rotate(new Vector3(0f, 90f, 0f));
                        wallArray[init].transform.SetParent(gameObject.transform);

                        wallArray[init].AddComponent<Rigidbody>();
                        Rigidbody rig = wallArray[init].GetComponent<Rigidbody>();
                        rig.isKinematic = true;
                        rig.useGravity = false;
                        rig.freezeRotation = true;
                        rig.constraints = RigidbodyConstraints.FreezeAll;

                        init++;
                    }

                }
            }

        }
    }
}
