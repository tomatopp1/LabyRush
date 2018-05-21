using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NewWallController : MonoBehaviour
{
    public Vector3 cubeScale;
    private GameObject[] wallArray;

    private List<int[]> WallList;

    private System.Random Generator;// = new System.Random(42);

    //Ajout Nicolas
    public int nbItem; //nombre d'item à faire pop au début
    public GameObject[] itemArray; //List des items a faire pop
    public GameObject spawnZone; //sol
    private System.Random RandItem;
    private int seed;

    //Ajout NicoT
    public Material materiauMur;

    //Quand la seed change elle est synchronisé à l'ensemble des clients
    void OnPhotonCustomRoomPropertiesChanged(Hashtable propertiesThatChanged)
    {
        seed = (int)propertiesThatChanged["Seed"];
    }
    // Use this for initialization
    void Start()
    {
        //Le Master de la room sa seed
        if (PhotonNetwork.isNonMasterClientInRoom != true)
        {
            //Ajout par NICOLAS FINOUX
            ExitGames.Client.Photon.Hashtable ht = new ExitGames.Client.Photon.Hashtable() { { "Seed", (UnityEngine.Random.Range(0, 2000)) } };

            PhotonNetwork.room.SetCustomProperties(ht);
        }
        seed = (int)PhotonNetwork.room.CustomProperties["Seed"]; //recupère la seed sur le réseau
        Generator = new System.Random(seed); //Set la seed au random
        RandItem = new System.Random();

        wallArray = new GameObject[2916];

        Debug.Log("test newWallScript");
        WallList = initWalls(0,0,27,27);
        CreateWalls(WallList);

        if (PhotonNetwork.isNonMasterClientInRoom != true)
            InitAllItem();

    }

    // Update is called once per frame
    void Update()
    {

    }


    //List<List<int>>
    private List<int[]> initWalls(int Xa, int Ya, int Xb, int Yb)
    {
        List<int[]> CurrentWallList = new List<int[]>();

        if ((Xb-Xa <= 1) ||(Yb-Ya <= 1))
        {
            CurrentWallList.Add(new int[4] {0,0,0,0});
            return (CurrentWallList);
        }
        else
        { 
            int Xd = Generator.Next(Xa + 1, Xb);
            int Yd = Generator.Next(Ya + 1, Yb);
            int Rand = Generator.Next(0, 2);
            if (Rand == 0)
            {
                int R1 = Generator.Next(Xa+1, Xd);
                int R2 = Generator.Next(Xd+1, Xb);
                int R3 = Generator.Next(Ya+1, Yb);

                int[] HorizWall1 = new int[4] { Xa, Yd, R1 - 1, Yd };
                int[] HorizWall2 = new int[4] { R1, Yd, R2 - 1, Yd };
                int[] HorizWall3 = new int[4] { R2, Yd, Xb, Yd };

                int[] VertiWall1 = new int[4] { Xd, Ya, Xd, R3 - 1 };
                int[] VertiWall2 = new int[4] { Xd, R3, Xd, Yb };

                CurrentWallList.AddRange(initWalls(Xa, Ya, Xd, Yd));
                CurrentWallList.AddRange(initWalls(Xa, Yd, Xd, Yb));
                CurrentWallList.AddRange(initWalls(Xd, Ya, Xb, Yd));
                CurrentWallList.AddRange(initWalls(Xd, Yd, Xb, Yb));
                CurrentWallList.Add(HorizWall1);
                CurrentWallList.Add(HorizWall2);
                CurrentWallList.Add(HorizWall3);
                CurrentWallList.Add(VertiWall1);
                CurrentWallList.Add(VertiWall2);
                return (CurrentWallList);
            }

            else
            {
                int R1 = Generator.Next(Xa+1, Xb);
                int R2 = Generator.Next(Ya+1, Yd);
                int R3 = Generator.Next(Yd+1, Yb);

                int[] HorizWall1 = new int[4] { Xa, Yd, R1 - 1, Yd };
                int[] HorizWall2 = new int[4] { R1, Yd, Xb, Yd };

                int[] VertiWall1 = new int[4] { Xd, Ya, Xd, R2 - 1 };
                int[] VertiWall2 = new int[4] { Xd, R2, Xd, R3 - 1 };
                int[] VertiWall3 = new int[4] { Xd, R3, Xd, Yb };

                CurrentWallList.AddRange(initWalls(Xa, Ya, Xd, Yd));
                CurrentWallList.AddRange(initWalls(Xa, Yd, Xd, Yb));
                CurrentWallList.AddRange(initWalls(Xd, Ya, Xb, Yd));
                CurrentWallList.AddRange(initWalls(Xd, Yd, Xb, Yb));
                CurrentWallList.Add(HorizWall1);
                CurrentWallList.Add(HorizWall2);
                CurrentWallList.Add(VertiWall1);
                CurrentWallList.Add(VertiWall2);
                CurrentWallList.Add(VertiWall3);
                return (CurrentWallList);
            }
        }
    }


    public void CreateWalls(List<int[]> WallList)
    {
        int init = 0;
        foreach (int[] list in WallList)
        {
            if (!(list[0] == 0 && list[1] == 0 && list[2] == 0 && list[3] == 0) && !(list[0] == list[2] && list[1] == list[3]))
            {
                Debug.Log(list[0] + " " + list[1] + " " + list[2] + " " + list[3]);

                if(list[0] == list[2])                          //Si il n'y a pas de changements dans les coordonnées selon X
                {
                    int size = Math.Abs(list[3] - list[1]);     //On récupère la différence des coordonnées selon Y
                    cubeScale = new Vector3(1, 10, 10 * size);  //On met à jour la taille du mur à faire
                }
                else                                            //Si il n'y a pas de changements dans les coordonnées selon Y
                {
                    int size = Math.Abs(list[2] - list[0]);     //On récupère la différence des coordonnées selon X
                    cubeScale = new Vector3(10 * size, 10, 1);  //On met à jour la taille du mur à faire
                }


                wallArray[init] = GameObject.CreatePrimitive(PrimitiveType.Cube);

                wallArray[init].transform.position = new Vector3(130f - (list[0] + list[2])* 5, 4f, 134.5f - (list[1] + list[3]) * 5) / 10; //placement du mur dans l'espace
                wallArray[init].transform.localScale = cubeScale / 10;
                wallArray[init].transform.SetParent(gameObject.transform);

                //affectation du materiau
                wallArray[init].gameObject.AddComponent<Renderer>();
                wallArray[init].GetComponent<Renderer>().material = materiauMur;
                wallArray[init].gameObject.AddComponent<ReCalcCubeTexture>();

                //Surement à supprimer dans cette partie
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

    //Partie ajouté par NICOLAS FINOUX
    public void PopItem()
    {
        int index;

        Vector3 minSpawn = spawnZone.GetComponent<MeshCollider>().bounds.min; //renvoi les valeurs min du collider
        Vector3 maxSpawn = spawnZone.GetComponent<MeshCollider>().bounds.max; //renvoi les valeurs max du collider

        index = RandItem.Next(0, itemArray.Length);

        PhotonNetwork.Instantiate(itemArray[index].name, new Vector3(RandItem.Next((int)minSpawn.x, (int)maxSpawn.x), 0, Generator.Next((int)minSpawn.z, (int)maxSpawn.z)), Quaternion.identity, 0);

    }

    public void InitAllItem()
    {
        for (int i = 0; i < nbItem; i++)
        {
            PopItem();
        }
    }
}


