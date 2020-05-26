using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Photon.Pun;
using Random = System.Random;

public class matrixe : MonoBehaviour
{
    // 1 -> Top || 2 -> Bot || 3 -> Left || 4 -> Right || 5 -> Spawn || 6 -> Boss
    // 7-> forgeron || 8-> shop || 9-> instructeur || 10 -> cook || 11-> item || 12->house
    [SerializeField] public (bool, bool, bool, bool, bool, bool, bool, bool, bool, bool, bool,bool)[,] matrix;
     public int size;
    [SerializeField] public string neo;
    //[SerializeField] private GameObject boss;
    private Random r = new Random();
    private int tw;
    private int bw;
    private int lw;
    private int rw;
    private int gr;
    private PhotonView PV;
    private PhotonView myPV;
    [SerializeField] public int biome;

    public Vector2 spawnOffset;

    private GameObject parent;
    
    void Awake()
    {
        //Init
        PV = gameObject.GetComponent<PhotonView>();
        if (size % 2 == 0) size += 1;
        biome = 1;

        //Gen
        Generate(Vector2.zero);
    }

    public void Generate(Vector2 spawnPos)
    {
        matrix = new (bool, bool, bool, bool, bool, bool, bool, bool, bool, bool, bool,bool)[size, size];
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                matrix[i, j] = (true, true, true, true, false, false, false, false, false, false, false, false);
            }
        }
        
        parent = new GameObject("DUNGEON");
        parent.tag = "dungeon";
        
        Debug.Log("Generating Dungeon in : " + spawnPos);
        myPV = GetComponent<PhotonView>();
        if (PhotonNetwork.IsMasterClient  && myPV.IsMine)
        {
            Debug.Log("is generating");

            generatedungeon();
            GenerateHouses();

            /* ------------ How to Generate the dungeon around the spawn room ------------
            * Browse the matrix to get the World position of the spawning room
            * Calculating the X's and Y's regarding the spawn position to make it at (0, 0)
            * Instantiate each room at the new coordinates calculated by the SpawnOffset
              --------------------------------------------------------------------------- */

            //Browsing the matrix to get the Spawn coodrinates (world relative coords)
            Vector2 coords = Vector2.zero;

            for (int x = 0; x < size; x++)
            {
                for (int y = 0; y < size; y++)
                {
                    if (matrix[x, y].Item5)
                    {
                        coords = new Vector2(x * 19, y * 12);
                    }
                }
            }

            int cnt = 0;
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (!(matrix[i, j].Item1 && matrix[i, j].Item2 && matrix[i, j].Item3 && matrix[i, j].Item4))
                    {
                        cnt++;
//                        PV.RPC("Generate", RpcTarget.AllBuffered, i, j, cnt, coords);
                        Generate2(i, j, cnt, coords);
                    }
                }
            }
            
           // PV.RPC("SaveGenBool", RpcTarget.AllBufferedViaServer);
            
            //Browse thru matrix to find the new base
            Debug.Log("Getting the Spawn Offset");
            float __i = spawnPos.x;
            float __j = spawnPos.y;

            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (ishere(i,j))
                    {
                        float __x = i * 19 - coords.x;
                        float __y = j * 12 - coords.y;

                        if (__x < __i)
                            __i = __x;
                        if (__y < __j)
                            __j = __y;
                    }
                }
            }
            
            spawnOffset = new Vector2(__i, __j);
            Debug.Log("The spawn offset is : " + spawnOffset);
            
            PV.RPC("SendToStock", RpcTarget.AllBuffered);
            
            GameObject.Find("Canvas").transform.GetChild(5).GetComponent<miniMap>().rStart();
        }
        else
        {
            Debug.Log("not gonna generate");
            //PV.RPC("SaveGenBool", RpcTarget.AllBufferedViaServer);
        }
    }
    public void generatedungeon()
    {
        int maxroom = (size * size) /3;
        int compteur = 5;
        bool boule = true;
        
//        Debug.Log("GenerateDungeon : Setting things up for start room");
        matrix[size / 2, size / 2].Item1 = false;
        matrix[size / 2, size / 2].Item2 = false;
        matrix[size / 2, size / 2].Item3 = false;
        matrix[size / 2, size / 2].Item4 = false;
        matrix[size / 2, size / 2].Item5 = true;
        matrix[size / 2, size / 2].Item6 = false;


        while (compteur < maxroom && boule)
        {
            (int x, int y)= recdungeon();
            if (x >= 0)
            {
                int a = generateroom(x, y, maxroom - compteur);
                compteur += a;
            }
            else boule = false;
        }

        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                checkdoors(i,j);
            }
        }

        AddTheBossRoom();
        SpecialRooms();
    }
    public (int,int) recdungeon()
    {
//        Debug.Log("Recdungeon : Called");
        List<(int,int)> a = new List<(int, int)>();
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if (IsAccesible(i, j)) 
                    a.Add((i,j));
            }
        }
        int b = 0;
        int c = a.Count-1;
        if (c >= 0)
            b += r.Next(c);
        if (c == -1)
            return (-1, -1);
        (int x, int y) = a[b];
        return (x,y);
    }
    
    //Check if inside of the matrix
    public bool isvalid(int i ,int j)
    {
        return (i >= 0 && j >= 0 && i < size && j < size);
    }
    
    //Check if the case is linked to the dungeon
    public bool ishere(int i, int j)
    {
        if (isvalid(i, j))
        {
            return !(matrix[i, j].Item1 && matrix[i, j].Item2 && matrix[i, j].Item3 && matrix[i, j].Item4);
        }
        return false;
    }
    
    public bool IsAccesible(int i, int j)
    {
        bool b = false;
        if (!ishere(i,j))
        {
            if (isvalid(i,j+1) && !matrix[i,j+1].Item2)
                b = true;
            else if (isvalid(i,j-1) && !matrix[i,j-1].Item1)
                b = true;
            else if (isvalid(i+1,j) && !matrix[i+1,j].Item3)
                b = true;
            else if (isvalid(i-1,j) && !matrix[i-1,j].Item4)
                b = true;
        }

        return b;
    }
    
    
    public int generateroom(int i, int j,int diff)
    {
//        Debug.Log("GenerateRoom : Called");
        int size = matrix.GetLength(0);
        int compteur = 0;
        
        
        checkdoors(i,j);
        int d = possibledirections(size, i, j);


        if (d >= 3 && diff >= 3) 
        { 
            int a = r.Next(4);
            if (a == 3)
                compteur += randomdoor3(i, j);
            if (a == 2) 
                compteur += randomdoor2(i, j);
            if (a == 1 )  
                compteur += randomdoor1(i, j);
        }
        else if (d >= 2 && diff >= 2) 
        { 
            int a = r.Next(3); 
            if (a == 2) 
                compteur += randomdoor2(i, j);
            if (a == 1|| a == 0) 
                compteur += randomdoor1(i, j);
        }
        else if (d >= 1 && diff >= 1)
        {
            int a = r.Next(2);
            if (a == 1)
                compteur += randomdoor1(i, j);
        }

        return compteur;
    }
    
    public int possibledirections(int size, int i, int j)
    {
        int d = 0;
        if (!ishere(i + 1, j) && isvalid( i + 1, j))
            d++;
        if (!ishere( i - 1, j) && isvalid( i - 1, j))
            d++;
        if (!ishere( i, j + 1) && isvalid( i, j + 1))
            d++;
        if (!ishere( i, j - 1) && isvalid( i, j - 1))
            d++;
        return d;
    }
    
    public void checkdoors(int i, int j)
    {
        if (isvalid(i + 1, j) && !matrix[i + 1, j].Item3)
            matrix[i, j].Item4 = false;

        if (isvalid(i - 1, j) && !matrix[i - 1, j].Item4)
            matrix[i, j].Item3 = false;
        
        if (isvalid(i,j+1) && !matrix[i,j+1].Item2)
            matrix[i, j].Item1 = false;
        
        if (isvalid(i,j-1) && !matrix[i,j-1].Item1)
            matrix[i, j].Item2 = false;
        
        if (!isvalid(i+1,j) && !matrix[i,j].Item4)
            matrix[i, j].Item4 = true;
        
        if (!isvalid(i-1,j) && !matrix[i,j].Item3)
            matrix[i, j].Item3 = true;
        
        if (!isvalid(i,j+1) && !matrix[i,j].Item1)
            matrix[i, j].Item1 = true;
        
        if (!isvalid(i,j-1) && !matrix[i,j].Item2)
            matrix[i, j].Item2 = true;
    }


    public int randomdoor3(int i, int j)
    {
        int a = 0;
        a += randomdoor1(i, j);
        a += randomdoor2(i, j);
        return a;
    }
    
    
    //fonction qui creuse 2 portes 
    public int randomdoor2(int i, int j)
    {
        int a = 0;
        a += randomdoor1(i, j);
        a += randomdoor1(i, j);
        return a;
    }
    
    
    //fonction qui creuse 1 porte aléatoirement
    public int randomdoor1(int i, int j)
    {
        int size = matrix.GetLength(0);
        bool added = false;
        
        while (!added && possibledirections(size,i,j)!=0)
        {
            int a = r.Next(4);
                    
            if (a == 0 && isvalid(i, j + 1) && matrix[i, j].Item4)
            {
                added = true;
                matrix[i, j].Item4 = false;
            }
            if (a == 1 && isvalid( i, j - 1) && matrix[i, j].Item3)
            {
                added = true;
                matrix[i, j].Item3 = false;
            }
            if (a == 2 && isvalid(i+1, j ) && matrix[i, j].Item2)
            {
                added = true;
                matrix[i, j].Item2 = false;
            }
            if (a == 3 && isvalid( i-1, j) && matrix[i, j].Item1)
            { 
                added = true;
                matrix[i, j].Item1 = false;
            }
        }

        if (added)
        {
            return 1;
        }
        return 0;
    }

    public void AddTheBossRoom()
    {
        int rand = r.Next(4);
        if (rand == 0)
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (ishere( i, j) && IsBossCandidaite(i,j))
                    {
                        matrix[i, j].Item6 = true;
                        return;
                    }
                        
                }
            }
        }
        if (rand == 1)
        {
            for (int i = 0; i < size; i++)
            {
                for (int j = 0; j < size; j++)
                {
                    if (ishere( j, i) && IsBossCandidaite(j,i))
                    {
                        matrix[j, i].Item6 = true;
                        return;
                    }
                        
                }
            }
        }
        if (rand == 2)
        {
            for (int i = size; i > 0; i--)
            {
                for (int j = size; j > 0; j--)
                {
                    if (ishere( i, j) && IsBossCandidaite(i,j))
                    {
                        matrix[i, j].Item6 = true;
                        return;
                    }
                        
                }
            }
        }
        if (rand == 3)
        {
            for (int i = size; i > 0; i--)
            {
                for (int j = size; j > 0; j--)
                {
                    if (ishere( j, i) && IsBossCandidaite(j,i))
                    {
                        matrix[j, i].Item6 = true;
                        return;
                    }
                        
                }
            }
        }
    }

    public bool IsBossCandidaite( int i, int j)
    {
        bool bool1 = !matrix[i, j].Item1;
        bool bool2 = !matrix[i, j].Item2;
        bool bool3 = !matrix[i, j].Item3;
        bool bool4 = !matrix[i, j].Item4;

        bool bool5 = bool1 && !bool2 && !bool3 && !bool4;
        bool bool6 = !bool1 && bool2 && !bool3 && !bool4;
        bool bool7 = !bool1 && !bool2 && bool3 && !bool4;
        bool bool8 = !bool1 && !bool2 && !bool3 && bool4;

        bool bool9 = isvalid( i, j + 1) && matrix[i, j + 1].Item5;
        bool bool10 = isvalid(i, j - 1) && matrix[i, j - 1].Item5;
        bool bool11 = isvalid(i + 1, j) && matrix[i + 1, j].Item5;
        bool bool12 = isvalid(i - 1, j) && matrix[i - 1, j].Item5;

        bool bool13 = bool9 || bool10 || bool11 || bool12;

        return (bool5 || bool6 || bool7 || bool8) && !bool13;
    }


    public void generateforest(GameObject gobj, int i, int j)
    {
         tw = r.Next(0,2);
         bw = r.Next(0,2);
         lw = r.Next(0,2);
         rw = r.Next(0,2);

        if (tw==0)
        {
            gobj.transform.GetChild(2).GetChild(0).gameObject.SetActive(true);
            
            if (matrix[i,j].Item1)
            {
                gobj.transform.GetChild(3).GetChild(0).gameObject.SetActive(true);
            }
            else
            {
                gobj.transform.GetChild(4).GetChild(0).gameObject.SetActive(true);
            }
        }
        else
        {
            gobj.transform.GetChild(2).GetChild(1).gameObject.SetActive(true);
            
            if (matrix[i,j].Item1)
            {
                gobj.transform.GetChild(3).GetChild(1).gameObject.SetActive(true);

            }
            else
            {
                gobj.transform.GetChild(4).GetChild(0).gameObject.SetActive(true);
            }
        }
        if (bw==0)
        {
            gobj.transform.GetChild(2).GetChild(2).gameObject.SetActive(true);
            
            if (matrix[i,j].Item2)
            {
                gobj.transform.GetChild(3).GetChild(2).gameObject.SetActive(true);

            }
            else
            {
                gobj.transform.GetChild(4).GetChild(1).gameObject.SetActive(true);
            }
        }
        else
        {
            gobj.transform.GetChild(2).GetChild(3).gameObject.SetActive(true);
            
            if (matrix[i,j].Item2)
            {
                gobj.transform.GetChild(3).GetChild(3).gameObject.SetActive(true);

            }
            else
            {
                gobj.transform.GetChild(4).GetChild(2).gameObject.SetActive(true);
            }
        }
        if (lw==0)
        {
            gobj.transform.GetChild(2).GetChild(4).gameObject.SetActive(true);
            
            if (matrix[i,j].Item3)
            {
                gobj.transform.GetChild(3).GetChild(4).gameObject.SetActive(true);

            }
            else
            {
                gobj.transform.GetChild(4).GetChild(3).gameObject.SetActive(true);
            }
        }
        else
        {
            gobj.transform.GetChild(2).GetChild(5).gameObject.SetActive(true);
            
            if (matrix[i,j].Item3)
            {
                gobj.transform.GetChild(3).GetChild(5).gameObject.SetActive(true);

            }
            else
            {
                gobj.transform.GetChild(4).GetChild(4).gameObject.SetActive(true);
            }
        }
        if (rw==0)
        {
            gobj.transform.GetChild(2).GetChild(6).gameObject.SetActive(true);
            
            if (matrix[i,j].Item4)
            {
                gobj.transform.GetChild(3).GetChild(6).gameObject.SetActive(true);

            }
            else
            {
                gobj.transform.GetChild(4).GetChild(5).gameObject.SetActive(true);
            }
        }
        else
        {
            gobj.transform.GetChild(2).GetChild(7).gameObject.SetActive(true);
            
            if (matrix[i,j].Item4)
            {
                gobj.transform.GetChild(3).GetChild(7).gameObject.SetActive(true);

            }
            else
            {
                gobj.transform.GetChild(4).GetChild(6).gameObject.SetActive(true);
            }
        }

        gobj.transform.GetChild(1).gameObject.SetActive(gr==0);
    }

    public void SpecialRooms()
    {
        bool item = true;
        while (item)
        {
            int a = r.Next(size);
            int b = r.Next(size);
            if (IsBossCandidaite(a, b) && !matrix[a,b].Item6)
            {
                matrix[a, b].Item11 = true;
                item = false;
            }
        }
        if (size<=7)
        {
            
            bool market = true;
            while (market)
            {
                int a = r.Next(size);
                int b = r.Next(size);
                if (ishere(a, b) && !matrix[a, b].Item11 && !matrix[a,b].Item5 && !matrix[a,b].Item6) 
                {
                    matrix[a, b].Item7 = true;
                    matrix[a, b].Item8 = true;
                    matrix[a, b].Item9 = true;
                    matrix[a, b].Item10 = true;
                    market = false;
                }
            }
        }
        else
        {
            bool forg = true;
            while (forg)
            {
                int a = r.Next(size);
                int b = r.Next(size);
                if (ishere(a, b) && !matrix[a, b].Item11 && !matrix[a,b].Item5 && !matrix[a,b].Item6) 
                {
                    matrix[a, b].Item7 = true;
                    forg = false;
                }
            }
            bool shop = true;
            while (shop)
            {
                int a = r.Next(size);
                int b = r.Next(size);
                if (ishere(a, b) && !matrix[a, b].Item11 && !matrix[a,b].Item5 && !matrix[a,b].Item6) 
                {
                    matrix[a, b].Item8 = true;
                    shop = false;
                }
            }
            bool ins = true;
            while (ins)
            {
                int a = r.Next(size);
                int b = r.Next(size);
                if (ishere(a, b) && !matrix[a, b].Item11 && !matrix[a,b].Item5 && !matrix[a,b].Item6) 
                {
                    matrix[a, b].Item9 = true;
                    ins = false;
                }
            }
            bool cook = true;
            while (cook)
            {
                int a = r.Next(size);
                int b = r.Next(size);
                if (ishere(a, b) && !matrix[a, b].Item11 && !matrix[a,b].Item5 && !matrix[a,b].Item6) 
                {
                    matrix[a, b].Item10 = true;
                    cook = false;
                }
            }
        }
    }

    private void GeneratesShop(bool cook, bool smith, bool shop, bool sensei,GameObject gobj)
    {
        if (cook)
            gobj.transform.GetChild(5).GetChild(0).gameObject.SetActive(true);
        if (smith)
            gobj.transform.GetChild(5).GetChild(1).gameObject.SetActive(true);
        if (shop)
            gobj.transform.GetChild(5).GetChild(2).gameObject.SetActive(true);
        if (sensei)
            gobj.transform.GetChild(5).GetChild(3).gameObject.SetActive(true);
    }
    
    private void Generate2(int i, int j, int cnt, Vector2 sPos)
    {
        GameObject oo = PhotonNetwork.InstantiateSceneObject(Path.Combine("PhotonPrefabs", neo), new Vector3(i*19-sPos.x, j*12-sPos.y), Quaternion.identity);


//        GameObject pp = Instantiate(Resources.Load("mmRoom"), Vector3.zero, Quaternion.identity) as GameObject;
//
//        pp.transform.parent = GameObject.Find("Canvas").transform.GetChild(5);
//        pp.transform.localPosition = new Vector3(i-(sPos.x/19), j-(sPos.x/12))*15;
//        pp.transform.localScale = Vector3.one;
//        
//        if (matrix[i, j].Item5)
//            GameObject.Find("Canvas").transform.GetChild(6).position = pp.transform.position;


        oo.name = $"Room_{cnt}";
        
        oo.transform.parent = parent.transform;
        if (neo=="Room")
            generateforest(oo, i, j);
        else
            GenerateFarm(oo,i,j);
        
        oo.GetComponent<cleanscript>().top = matrix[i, j].Item1;
        oo.GetComponent<cleanscript>().bot = matrix[i, j].Item2;
        oo.GetComponent<cleanscript>().left = matrix[i, j].Item3;
        oo.GetComponent<cleanscript>().right = matrix[i, j].Item4;
        
        oo.GetComponent<cleanscript>().spawn = matrix[i, j].Item5;
        oo.GetComponent<cleanscript>().boss = matrix[i, j].Item6;
        bool forgeron = oo.GetComponent<cleanscript>().forge = matrix[i, j].Item7;
        bool shop = oo.GetComponent<cleanscript>().shop = matrix[i, j].Item8;
        bool sensei = oo.GetComponent<cleanscript>().instructor = matrix[i, j].Item9;
        bool cook = oo.GetComponent<cleanscript>().cook = matrix[i, j].Item10;
        oo.GetComponent<cleanscript>().item = matrix[i, j].Item11;
        
        GeneratesShop(cook,forgeron,shop,sensei,oo);
    }


    public void DestroyDungeon()
    {
        PhotonNetwork.Destroy(parent);
        foreach (GameObject oo in GameObject.FindGameObjectsWithTag("itemshop"))
        {
            PhotonNetwork.Destroy(oo);
        }
    }

    [PunRPC]
    void SendToStock()
    {
        GameObject.Find("varHolder").GetComponent<variablesStock>().spawnOffset = this.spawnOffset;
    }

    public void GenerateHouses()
    {
        for (int i = 0; i < size; i++)
        {
            for (int j = 0; j < size; j++)
            {
                if (r.Next(0, 4)==1)
                    matrix[i, j].Item12 = true;
            }
        }
    }
    
    public void GenerateFarm(GameObject gobj,int i, int j)
    {
        if (matrix[i, j].Item12)
        {
            gobj.transform.GetChild(1).gameObject.SetActive(true);
            gobj.transform.GetChild(0).gameObject.SetActive(false);
            if (!matrix[i, j].Item1)
            {
                gobj.transform.GetChild(4).GetChild(0).gameObject.SetActive(true);
                gobj.transform.GetChild(6).GetChild(0).gameObject.SetActive(true);
            }
            if (!matrix[i,j].Item2)
            {
                gobj.transform.GetChild(4).GetChild(1).gameObject.SetActive(true);
                gobj.transform.GetChild(6).GetChild(1).gameObject.SetActive(true);
            }
            if (!matrix[i,j].Item3)
            {
                gobj.transform.GetChild(4).GetChild(2).gameObject.SetActive(true);
                gobj.transform.GetChild(6).GetChild(2).gameObject.SetActive(true);
            }
            if (!matrix[i,j].Item4)
            {
                gobj.transform.GetChild(4).GetChild(3).gameObject.SetActive(true);
                gobj.transform.GetChild(6).GetChild(3).gameObject.SetActive(true);
            }
        }
        else
        {
            if (isvalid(i,j+1) && matrix[i,j+1].Item12)
            {
                gobj.transform.GetChild(2).GetChild(4).gameObject.SetActive(true);
                if (!matrix[i,j].Item1)
                    gobj.transform.GetChild(4).GetChild(8).gameObject.SetActive(true);
                else
                    gobj.transform.GetChild(3).GetChild(4).gameObject.SetActive(true);
            }
            else
            {
                gobj.transform.GetChild(2).GetChild(0).gameObject.SetActive(true);
                if (!matrix[i,j].Item1)
                    gobj.transform.GetChild(4).GetChild(4).gameObject.SetActive(true);
                else
                    gobj.transform.GetChild(3).GetChild(0).gameObject.SetActive(true);
            }
            
            if (isvalid(i,j-1) && matrix[i,j-1].Item12)
            {
                gobj.transform.GetChild(2).GetChild(5).gameObject.SetActive(true);
                if (!matrix[i,j].Item2)
                    gobj.transform.GetChild(4).GetChild(9).gameObject.SetActive(true);
                else
                    gobj.transform.GetChild(3).GetChild(5).gameObject.SetActive(true);
            }
            else
            {
                gobj.transform.GetChild(2).GetChild(1).gameObject.SetActive(true);
                if (!matrix[i,j].Item2)
                    gobj.transform.GetChild(4).GetChild(5).gameObject.SetActive(true);
                else
                    gobj.transform.GetChild(3).GetChild(1).gameObject.SetActive(true);
            }
            
            if (isvalid(i-1,j) && matrix[i-1,j].Item12)
            {
                gobj.transform.GetChild(2).GetChild(6).gameObject.SetActive(true);
                if (!matrix[i,j].Item3)
                    gobj.transform.GetChild(4).GetChild(10).gameObject.SetActive(true);
                else
                    gobj.transform.GetChild(3).GetChild(6).gameObject.SetActive(true);
            }
            else
            {
                gobj.transform.GetChild(2).GetChild(2).gameObject.SetActive(true);
                if (!matrix[i,j].Item3)
                    gobj.transform.GetChild(4).GetChild(6).gameObject.SetActive(true);
                else
                    gobj.transform.GetChild(3).GetChild(2).gameObject.SetActive(true);
            }
            
            if (isvalid(i+1,j) && matrix[i+1,j].Item12)
            {
                gobj.transform.GetChild(2).GetChild(7).gameObject.SetActive(true);
                if (!matrix[i,j].Item4)
                    gobj.transform.GetChild(4).GetChild(11).gameObject.SetActive(true);
                else
                    gobj.transform.GetChild(3).GetChild(7).gameObject.SetActive(true);
            }
            else
            {
                gobj.transform.GetChild(2).GetChild(3).gameObject.SetActive(true);
                if (!matrix[i,j].Item4)
                    gobj.transform.GetChild(4).GetChild(7).gameObject.SetActive(true);
                else
                    gobj.transform.GetChild(3).GetChild(3).gameObject.SetActive(true);
            }
        }
    }
}


